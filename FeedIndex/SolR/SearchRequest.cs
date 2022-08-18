namespace FeedIndex.SolR
{
    public class SearchRequest
    {
        public SearchRequest()
        {
            SearchText = "*:*";
        }
        public SearchRequest(string searchText)
        {
            SearchText = searchText;
        }
        public string SearchText { get; set; }

        public string? Language { get; set; }

        public int PageNo { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public List<string> FacetFields { get; set; } = new List<string>();

        public Dictionary<string, List<string>> Filters { get; set; } = new Dictionary<string, List<string>>();

        public string? Sort { get; set; } 

    }

}
