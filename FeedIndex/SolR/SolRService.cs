using FeedIndex.SolR;
using SolrNet;
using SolrNet.Commands.Parameters;


namespace FeedIndex
{
    internal class SolRService
    {
        private readonly ISolrOperations<CatalogTrack> _solrCatalog;
        private readonly ISolrConnection _solrConnection;
        public SolRService(ISolrOperations<CatalogTrack> catalog, ISolrConnection solrConnection)
        {
            _solrConnection = solrConnection;
            Startup.Init<CatalogTrack>(_solrConnection);
            _solrCatalog = catalog;
        }
        public void RegisterDocuments(IEnumerable<CatalogTrack>? tracks, int batchSize = 25)
        {
            if (tracks is null)
                return;
            var count = tracks.Count();
            var done = 0;
            while (done < count)
            {
                var batch = tracks.Skip(done).Take(batchSize).ToList();
                _solrCatalog.AddRangeAsync(batch).Wait();
                done += batchSize;
            }
            _solrCatalog.CommitAsync().Wait(); //non async version does not include Authorization header
        }
        public bool Ping()
        {
            var res = _solrCatalog.PingAsync();
            res.Wait();
            return res.Result.Status == 0;
        }
        public IEnumerable<CatalogTrack> Query(SearchRequest sr)
        {
            QueryOptions searchParameters = new();
            AbstractSolrQuery solrQuery = BuildBasicSearchResultsSolrQuery(sr, ref searchParameters);
            var solrResults = _solrCatalog.QueryAsync(solrQuery, searchParameters);
            solrResults.Wait();
            return solrResults.Result;
        }

        public ICollection<KeyValuePair<string,int>> FacetDistribution(string facetName,int rowLimit =0)
        {
            var r = _solrCatalog.QueryAsync(SolrQuery.All, new QueryOptions
            {
                Rows = rowLimit,
                Facet = new FacetParameters { Queries = new[] { new SolrFacetFieldQuery(facetName) }}
            });
            r.Wait();
            return r.Result.FacetFields[facetName];
        }
        private static AbstractSolrQuery BuildBasicSearchResultsSolrQuery(SearchRequest searchRequest, ref QueryOptions searchParameters)
        {
            var searchQuery = $"name:{searchRequest.SearchText} OR artist_name:{searchRequest.SearchText}";
            AbstractSolrQuery solrQuery = new SolrQuery(searchQuery);
            searchParameters.ExtraParams = new Dictionary<string, string> {
                            {"qf","name artist_name"}, //query fields are name and artist_name Solr fields
							{"hl","true"}, //enable highlighting
							{"hl.fl","name artist_name"}, //highlighted fields are name and artist_name
							{"spellcheck.q",searchRequest.SearchText } //to perform spellchecking for the searchtext entered
						};
            searchParameters.Rows = searchRequest.PageSize;
            searchParameters.StartOrCursor = new StartOrCursor.Start((searchRequest.PageNo - 1) * searchRequest.PageSize); //start will be 0 initially
            if (!string.IsNullOrEmpty(searchRequest.Sort))
                searchParameters.OrderBy = new[] { new SortOrder("name", searchRequest.Sort.Equals("asc") ? Order.ASC : Order.DESC) };//sort by name solr field
            searchParameters.SpellCheck = new SpellCheckingParameters() { };
            return solrQuery;
        }
    }
}

     