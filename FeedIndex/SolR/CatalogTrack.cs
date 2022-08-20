using SolrNet.Attributes;

namespace FeedIndex
{
   
    internal class CatalogTrack 
    {
        [SolrUniqueKey("id")]
        public long Id { get; set; }//The Deezer item id 
        [SolrField("name")]
        public string? Name { get; set; }
        [SolrField("artist_name")]
        public string? Artist_name { get; set; }
        [SolrField("album_image")]
        public string? Album_image { get; set; }
        [SolrField("duration")]
        public double? Duration { get; set; }
        [SolrField("genre")] //uninvertible
        public string? Genre { get; set; }
        [SolrField("link")]
        public string? Link { get; set; }//The url of the track on Deezer url
        [SolrField("preview")]
        public string? Preview { get; set; }//The url of track's preview file. This file contains the first 30 seconds of the track	url
    }
}
