using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedIndex
{
    //https://developers.deezer.com/api/album
    internal class Album : ApiDocument
    {
        public string? title { get; set; }//The album title string
        //public string? upc { get; internal protected set; }//The album UPC string
        public string? link { get;  set; }//The url of the album on Deezer url
        public string? share { get; set; }//The share link of the album on Deezer url
        //public string? cover { get; internal protected set; }//The url of the album's cover. Add 'size' parameter to the url to change size. Can be 'small', 'medium', 'big', 'xl'	url
        public string? cover_small { get;  set; }//The url of the album's cover in size small.	url
        //public string? cover_medium { get; internal protected set; }//The url of the album's cover in size medium.	url
        //public string? cover_big { get; internal protected set; }//The url of the album's cover in size big.	url
        //public string? cover_xl { get; internal protected set; }//The url of the album's cover in size xl.	url
        //public string? md5_image { get; internal protected set; }//string
        public int? genre_id { get;  set; }// The album's first genre id (You should use the genre list instead). NB : -1 for not found	int
        // genres List of genre object list
        public string? label { get; set; }//The album's label name	string
        public int? nb_tracks { get; set; }//int
        public int? duration { get; set; }//The album's duration (seconds)	int
        //public int? fans { get; internal protected set; }//The number of album's Fans	int
        //public DateTime? release_date { get; internal protected set; }//The album's release date	date
        //public string? record_type { get; internal protected set; }//The record type of the album(EP / ALBUM / etc..)   string
        public bool? available { get; set; }//boolean
        //alternative Return an alternative album object if the current album is not available    object
        public string? tracklist { get; set; }//API Link to the tracklist of this album url
        //public int? explicit_lyrics { get; internal protected set; }//Whether the album contains explicit lyrics boolean
        //public int? explicit_content_lyrics { get; internal protected set; }//The explicit content lyrics values(0:Not Explicit; 1:Explicit; 2:Unknown; 3:Edited; 4:Partially Explicit(Album "lyrics" only); 5:Partially Unknown(Album "lyrics" only); 6:No Advice Available; 7:Partially No Advice Available(Album "lyrics" only))	int
        //public int? explicit_content_cover { get; internal protected set; }// The explicit cover values (0:Not Explicit; 1:Explicit; 2:Unknown; 3:Edited; 4:Partially Explicit(Album "lyrics" only); 5:Partially Unknown(Album "lyrics" only); 6:No Advice Available; 7:Partially No Advice Available(Album "lyrics" only))	int
        //public string? contributors { get; internal protected set; }// Return a list of contributors on the album  list
    }
}
