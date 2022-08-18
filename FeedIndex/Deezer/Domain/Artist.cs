
namespace FeedIndex
{//https://developers.deezer.com/api/artist
    internal class Artist : ApiDocument
    {

        // {"id":13953,"name":"Volbeat","link":"http:\/\/www.deezer.com\/artist\/13953","share":"https:\/\/www.deezer.com\/artist\/13953?utm_source=deezer&utm_content=artist-13953&utm_term=0_1659625865&utm_medium=web","picture":"http:\/\/api.deezer.com\/artist\/13953\/image","picture_small":"http:\/\/e-cdn-images.dzcdn.net\/images\/artist\/20e238e669876cc9a2184677106d29f9\/56x56-000000-80-0-0.jpg","picture_medium":"http:\/\/e-cdn-images.dzcdn.net\/images\/artist\/20e238e669876cc9a2184677106d29f9\/250x250-000000-80-0-0.jpg","picture_big":"http:\/\/e-cdn-images.dzcdn.net\/images\/artist\/20e238e669876cc9a2184677106d29f9\/500x500-000000-80-0-0.jpg","picture_xl":"http:\/\/e-cdn-images.dzcdn.net\/images\/artist\/20e238e669876cc9a2184677106d29f9\/1000x1000-000000-80-0-0.jpg","nb_album":30,"nb_fan":520204,"radio":true,"tracklist":"http:\/\/api.deezer.com\/artist\/13953\/top?limit=50","type":"artist"}
        public string? name { get; set; }//The artist's name	string
        public string? link { get; set; }//The url of the artist on Deezer url
        public string? share { get; set; }//The share link of the artist on Deezer  url
        public string? picture { get; set; }//The url of the artist picture.Add 'size' parameter to the url to change size.Can be 'small', 'medium', 'big', 'xl'	url
        public string? picture_small { get; set; }//The url of the artist picture in size small.	url
        //public string? picture_medium { get; internal protected set; }//The url of the artist picture in size medium.	url
        //public string? picture_big { get; internal protected set; }// The url of the artist picture in size big.	url
        //public string? picture_xl { get; internal protected set; }// The url of the artist picture in size xl.	url
        //public int? nb_album { get; internal protected set; }// The number of artist's albums	int
        //public int? nb_fan { get; internal protected set; }// The number of artist's fans	int
        //public bool? radio { get; internal protected set; }// true if the artist has a smartradio boolean
        public string? tracklist { get; set; }// API Link to the top of this artist url

    }
}
