
namespace FeedIndex
{
    internal class TokenResponse
    {
        public string? Token { get; set; }
        public DateTime Expires { get; set; }
        public TokenResponse(string? jsonToken)
        {
            //ex : access_token=fr79BszwC9d6Mpnpj9PAh61KjjBDZyWkf1DbEDJohm0hOkoDlYo&expires=3600
            var tokArray = jsonToken?.Split("&expires=");
            Expires = DateTime.UtcNow.AddSeconds(double.TryParse(tokArray?[1]??"0",out var sec)? sec : 0);
            Token = tokArray?[0]?.Split("_token=")?[1];
        }
    }
}
