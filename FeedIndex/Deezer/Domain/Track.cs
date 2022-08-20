

namespace FeedIndex
{
    internal class Track : ApiDocument
    {
        public bool? readable { get; set; }//if the track is readable in the player for the current user boolean
        public string? title { get; set; }//The track's fulltitle	string
        public string? title_short { get; set; }//The track's short title	string
        //public string? title_version { get; internal protected set; }//The track version   string
        //public bool? unseen { get; internal protected set; }//The track unseen status boolean
        //public string? isrc { get; internal protected set; }//The track isrc string
        public string? link { get; set; }//The url of the track on Deezer url
        //public string? share { get; internal protected set; }//The share link of the track on Deezer url
        public int? duration { get; set; }//The track's duration in seconds	int
        //public int? track_position { get; internal protected set; }//The position of the track in its album  int
        //public int? disk_number { get; internal protected set; }// The track's album's disk number int
        //public int? rank { get; internal protected set; }//The track's Deezer rank	int
        //public DateTime? release_date { get; internal protected set; }//The track's release date	date
        //public bool? explicit_lyrics { get; internal protected set; }//Whether the track contains explicit lyrics boolean
        //public int? explicit_content_lyrics { get; internal protected set; }//The explicit content lyrics values(0:Not Explicit; 1:Explicit; 2:Unknown; 3:Edited; 6:No Advice Available)	int
        //public int? explicit_content_cover { get; internal protected set; }//The explicit cover value (0:Not Explicit; 1:Explicit; 2:Unknown; 3:Edited; 6:No Advice Available)	int
        public string? preview { get; set; }//The url of track's preview file. This file contains the first 30 seconds of the track	url
        //public float? bpm { get; internal protected set; }//Beats per minute    float
        //public float? gain { get; internal protected set; }//Signal strength float
        //public string? available_countries { get; internal protected set; }//List of countries where the track is available list
        //public string? alternative { get; internal protected set; }//Return an alternative readable track if the current track is not readable   track
        //public string? contributors { get; internal protected set; }//Return a list of contributors on the track  list
        //public string? md5_image { get; internal protected set; }//string
        public Artist? artist { get; set; }//object containing : id, name, link, share, picture, picture_small, picture_medium, picture_big, picture_xl, nb_album, nb_fan, radio, tracklist, role object
        public Album? album { get; set; }//object containing : id, title, link, cover, cover_small, cover_medium, cover_big, cover_xl, release_date object

        public CatalogTrack ToCatalogTrack() => new()
        {
            Id = this.id,
            Album_image = this.album?.cover_small,
            Artist_name = this.artist?.name,
            Duration = this.duration,
            Name = this.title,
            Genre = this.album?.genre_id?.ToString(),
            Link = this.link,
            Preview = this.preview
        };

    }
}
