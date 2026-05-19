using Stuff802.Core.Interfaces;
using Stuff802.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace Stuff802.Web.Controllers.Render
{
    public class SearchPageController : RenderController
    {
        private IPublishedValueFallback _publishedValueFallback;
        private readonly ISearchService _searchService;

        public SearchPageController(
                ILogger<SearchPageController> logger,
                ICompositeViewEngine compositeViewEngine,
                IUmbracoContextAccessor umbracoContextAccessor,
                IPublishedValueFallback publishedValueFallback,
                ISearchService searchService
             )
            : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _publishedValueFallback = publishedValueFallback;
            _searchService = searchService;
        }

        [HttpGet]
        public IActionResult Index([FromQuery(Name = "query")] string query, [FromQuery(Name = "page")] int page = 1, [FromQuery(Name = "searchType")] int searchType = 0, [FromQuery(Name = "pageSize")] int pageSize = 10)
        {
            if (string.IsNullOrEmpty(query))
            {
                var searchPage = new SearchPage(CurrentPage, _publishedValueFallback);
                return View(new SearchResultsModel(searchPage, _publishedValueFallback));
            }
            else
                return SearchResults(query, page);
        }
        [HttpGet]
        public ActionResult SearchResults(string searchText, int page)
        {
            var searchPage = new SearchPage(CurrentPage, _publishedValueFallback);
            var res = _searchService.GetSearchResults(searchText, page, searchPage);
            return View("~/Views/searchPage.cshtml", res);
        }
    }
}
