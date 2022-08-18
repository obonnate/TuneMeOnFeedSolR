
using FeedIndex;
using HttpWebAdapters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SolrNet;
using SolrNet.Impl;
using System.Net.Http.Headers;
using System.Text;

//GLOBAL PARAMS : Fill in Environment variable for Solr credentials
//Index collection has to be created manually on solr, and the catalogtrack schema must be
//manually edited so that all fields are *not* multivalued (for strange reason, SolrNet by default forces all fields to be multivalued)
const string INDEXER_LOGIN = @"admin";
string INDEXER_PWD = Environment.GetEnvironmentVariable("SOLR_PASSWORD")??"";
const string INDEX_NAME = @"catalogtrack";
const string INDEXER_ENDPOINT = @"http://localhost:57111/solr";    //Exposed from K8S cluster
const string APP_ID = "554282";                               //As Registered on Deezer Developper site
const string APP_SECRET = "75ef5987261e0197bd26829ad7a5cb7d"; //As Registered on Deezer Developper site
const string DEEZER_API_ENDPOINT = @"http://api.deezer.com";
//****

Host.CreateDefaultBuilder()
    ?.ConfigureServices((_, services) => services.AddSingleton<ISolrConnection>(new SolrConnection($"{INDEXER_ENDPOINT}/{INDEX_NAME}") { HttpWebRequestFactory = new BasicAuthHttpWebRequestFactory(INDEXER_LOGIN, INDEXER_PWD) }))
    ?.ConfigureServices((_, services) => services.AddSolrNet<CatalogTrack>($"{INDEXER_ENDPOINT}/{INDEX_NAME}", options =>
        options.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{INDEXER_LOGIN}:{INDEXER_PWD}")))
    ))
    ?.ConfigureServices(services => services.AddSingleton<Executor>())
    ?.ConfigureServices(services => services.AddSingleton<SolRService>())
    ?.ConfigureServices(services => services.AddHttpClient(nameof(DeezerHelper), c => c.BaseAddress = new Uri(DEEZER_API_ENDPOINT)))
    ?.ConfigureServices(services => services.AddSingleton<DeezerHelper>())
    ?.ConfigureServices(services => services.AddSingleton(x => ActivatorUtilities.CreateInstance<DeezerService>(x, APP_ID, APP_SECRET)))
    ?.Build().Services
    ?.GetService<Executor>()?.Execute();

internal class Executor
{
    private readonly SolRService _solRService;
    private readonly DeezerService _deezerService;

    public Executor(SolRService solRService, DeezerService deezerService)
    {
        _solRService = solRService;
        _deezerService = deezerService;
    }

    public async void Execute()
    {
        if (_deezerService?.Token is null)
        {
            Console.WriteLine("Could not connect, please check AppId and Secret on Developer.deezer.com");
            Console.ReadLine();
            Environment.Exit(0);
        }
        if (_deezerService?.Token != null)
        {
            var tracks = await _deezerService.GetTracksByGenre(464); //464 = Rock&Metal, 132 = pop
            Console.WriteLine($"Found {tracks?.Length ?? 0} tracks for genre {464}");
            _solRService.RegisterDocuments(tracks?.Select(_ => _.ToCatalogTrack()), batchSize: 15);
        }
        Console.WriteLine("Index is rebuilt");
        
        var resultst = _solRService.Query(new FeedIndex.SolR.SearchRequest() { SearchText= "rammstein" });
        Console.WriteLine("Searching Test :" + string.Join(Environment.NewLine, resultst?.Select(_ => _.Name) ?? Enumerable.Empty<string>()));
        Console.ReadLine();
    }
}

