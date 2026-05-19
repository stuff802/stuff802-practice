using Microsoft.AspNetCore.Html;
using Stuff802.Core.Models;
using System.Text;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace Stuff802.Core.Models
{
    public class SearchResultsModel(IPublishedContent content, IPublishedValueFallback publishedValueFallback) : SearchPage(content, publishedValueFallback)
    {
        public List<SearchResult> SearchResults { get; set; } = [];
        public bool HasResults => SearchResults.Count > 0;
        public string? SearchTerm { get; set; }
        public int TotalItemCount { get; set; }
        public bool HasErrors { get; set; } = false;
        public bool DisplayResults { get; set; } = false;
        public int PageSize { get; set; } = 10;
        public int CurrentPage { get; set; } = 0;
        public int Pages
        {
            get
            {
                int p = 0;
                try
                {
                    p = (int)Math.Ceiling(TotalItemCount / (decimal)PageSize);
                }
                catch { }
                return p;
            }
        }
        public int PaginationViewPort { get; set; } = 10;
        public IEnumerable<int> Pagination
        {
            get
            {
                if (TotalItemCount == 0 || Pages == 1)
                    return [];

                decimal half = Math.Floor(PaginationViewPort / 2m);
                decimal start = Math.Floor(CurrentPage - half + 1 - (PageSize % 2));
                decimal end = Math.Ceiling(CurrentPage + half);

                if (Pages > PaginationViewPort)
                {
                    int visiblePages = PaginationViewPort;
                    if (start <= 0) { start = 1; end = visiblePages; }
                    if (end >= Pages) { start = Pages - visiblePages + 1; end = Pages; }
                }
                else
                {
                    start = 1; end = Pages;
                }

                return Enumerable.Range((int)start, (int)end + 1 - (int)start);
            }
        }
        public HtmlString RenderPagination
        {
            get
            {
                StringBuilder pager = new();

                if (Pages > PaginationViewPort)
                {
                    _ = pager.AppendLine($"<li class=\"page-item {(CurrentPage == 1 ? "disabled" : "")}\">");
                    _ = pager.AppendLine($"<a class=\"page-link {(CurrentPage == 1 ? "disabled" : "")}\" href=\"#\" aria-label=\"First\" data-page=\"1\">");
                    _ = pager.AppendLine("<span aria-hidden=\"true\">First</span>");
                    _ = pager.AppendLine("<span class=\"u-sr-only\">First</span></a></li>");

                    _ = pager.AppendLine($"<li class=\"page-item {(CurrentPage == 1 ? "disabled" : "")}\">");
                    _ = pager.AppendLine($"<a class=\"page-link {(CurrentPage == 1 ? "disabled" : "")}\" href=\"#\" aria-label=\"Previous\" data-page=\"{(CurrentPage == 1 ? 1 : CurrentPage - 1)}\">");
                    _ = pager.AppendLine("<span aria-hidden=\"true\">&laquo;</span>");
                    _ = pager.AppendLine("<span class=\"u-sr-only\">Previous</span></a></li>");
                }

                foreach (int page in Pagination)
                {
                    _ = pager.AppendLine($"<li class=\"page-item {(CurrentPage == page ? "active" : "")}\">");
                    _ = pager.AppendLine($"<a class=\"page-link {(CurrentPage == page ? "active" : "")}\" href=\"#\" aria-label=\"{page}\" data-page=\"{page}\">");
                    _ = pager.AppendLine($"<span>{page}</span></a></li>");
                }

                if (Pages > PaginationViewPort)
                {
                    _ = pager.AppendLine($"<li class=\"page-item {(CurrentPage == Pages ? "disabled" : "")}\">");
                    _ = pager.AppendLine($"<a class=\"page-link {(CurrentPage == Pages ? "disabled" : "")}\" href=\"#\" aria-label=\"Next\" data-page=\"{(CurrentPage == Pages ? Pages : CurrentPage + 1)}\">");
                    _ = pager.AppendLine("<span aria-hidden=\"true\">&raquo;</span>");
                    _ = pager.AppendLine("<span class=\"u-sr-only\">Next</span></a></li>");

                    _ = pager.AppendLine($"<li class=\"page-item {(CurrentPage == Pages ? "disabled" : "")}\">");
                    _ = pager.AppendLine($"<a class=\"page-link {(CurrentPage == Pages ? "disabled" : "")}\" href=\"#\" aria-label=\"Last\" data-page=\"{Pages}\">");
                    _ = pager.AppendLine("<span aria-hidden=\"true\">Last</span>");
                    _ = pager.AppendLine("<span class=\"u-sr-only\">Last</span></a></li>");
                }

                return new HtmlString(pager.ToString());
            }
        }
    }
}
