namespace Stuff802.Core.Models
{
    public class SearchResult
    {
        public string? Title { get; set; }
        public string? Summary { get; set; }
        public string? Link { get; set; }
        public string? Breadcrumb { get; set; }
        public int PageId { get; set; }
        public float? Score { get; set; }
        public string? NodeType { get; set; }
    }
}
