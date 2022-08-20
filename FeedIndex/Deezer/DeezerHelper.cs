using FeedIndex.Deezer;
using System.Text.Json;

namespace FeedIndex
{
    internal class DeezerHelper
    {
        private const string CONNECT_ENDPOINT = @"https://connect.deezer.com/oauth/access_token.php";

        private readonly HttpClient _httpClient;

        public DeezerHelper(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(DeezerHelper));
        }

        public async Task<T?> Load<T>(string token, string urlRequest) where T : ApiDocument
       => JsonSerializer.Deserialize<T>(await ExecuteHttpGet(token, urlRequest));

        public async Task<T[]?> LoadArray<T>(string token, string urlRequest) where T : ApiDocument
        {
            var responseContent = await ExecuteHttpGet(token, urlRequest);
            var result = JsonSerializer.Deserialize<ArrayResponse<T>>(responseContent);
            return result?.data;
        }
        private async Task<string> ExecuteHttpGet(string token, string method)
        {
                var target = $"{method}{(method.Contains('?') ? "&" : "?")}access_token={token}";
                return _httpClient.GetStringAsync(target).Result;
        }

        public async Task<TokenResponse> GetAccessToken(string appId, string appSecret)
        {
            using var httpClient = new HttpClient();
            var form = new Dictionary<string, string> {{"grant_type", "client_credentials"},
                                                       {"client_id", appId},
                                                       {"client_secret", appSecret}};
            var tokenResponse = await httpClient.PostAsync(CONNECT_ENDPOINT, new FormUrlEncodedContent(form));
            return new TokenResponse(await tokenResponse.Content.ReadAsStringAsync());
        }

        public readonly Dictionary<string, string> GENRES = new (){ { "0",        "Tous"},
                                                                    { "132",      "Pop"},
                                                                    { "457",      "Livres audio"},
                                                                    { "116",      "Rap/Hip Hop"},
                                                                    { "152",      "Rock"},
                                                                    { "113",      "Dance"},
                                                                    { "165",      "R&B"},
                                                                    { "85",       "Alternative"},
                                                                    { "106",      "Electro"},
                                                                    { "466",      "Folk"},
                                                                    { "144",      "Reggae"},
                                                                    { "129",      "Jazz"},
                                                                    { "52",       "Chanson française"},
                                                                    { "84",       "Country"},
                                                                    { "98",       "Classique"},
                                                                    { "173",      "Films/Jeux vidéo"},
                                                                    { "464",      "Metal"},
                                                                    { "169",      "Soul & Funk"},
                                                                    { "153",      "Blues"},
                                                                    { "95",       "Jeunesse"},
                                                                    { "197",      "Latino"},
                                                                    { "2",        "Musique africaine"},
                                                                    { "12",       "Musique arabe"},
                                                                    { "16",       "Musique asiatique"},
                                                                    { "75",       "Musique brésilienne"},
                                                                    { "81",       "Musique indienne"}};
    }
}
