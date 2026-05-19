using Examine;
using Examine.Search;
using Stuff802.Core.Interfaces;
using Stuff802.Core.Models;
using CoreSearchResult = Stuff802.Core.Models.SearchResult;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace Stuff802.Web.Services
{
    public class SearchService : ISearchService
    {
        private readonly IExamineManager _examineManager;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private readonly IPublishedValueFallback _publishedValueFallback;

        public SearchService(IExamineManager examineManager, IUmbracoContextAccessor umbracoContextAccessor, IPublishedValueFallback publishedValueFallback)
        {
            _examineManager = examineManager;
            _umbracoContextAccessor = umbracoContextAccessor;
            _publishedValueFallback = publishedValueFallback;
        }

        public SearchResultsModel GetSearchResults(string searchTerm, int page, SearchPage searchPage)
        {
            var model = new SearchResultsModel(searchPage, _publishedValueFallback)
            {
                SearchTerm = searchTerm,
                CurrentPage = page,
                DisplayResults = !string.IsNullOrWhiteSpace(searchTerm)
            };

            if (string.IsNullOrWhiteSpace(searchTerm))
                return model;

            try
            {
                if (!_examineManager.TryGetIndex("ExternalIndex", out var index))
                    return model;

                var searcher = index.Searcher;
                var query = searcher.CreateQuery("content")
                    .GroupedOr(new[] { "nodeName", "metaTitle", "pageTitle", "metaDescription" },
                        searchTerm.MultipleCharacterWildcard());

                var results = query.Execute();
                model.TotalItemCount = (int)results.TotalItemCount;

                var umbracoContext = _umbracoContextAccessor.GetRequiredUmbracoContext();
                var pagedResults = results.Skip((page - 1) * model.PageSize).Take(model.PageSize);

                foreach (var result in pagedResults)
                {
                    if (!int.TryParse(result.Id, out int nodeId)) continue;
                    var content = umbracoContext.Content?.GetById(nodeId);
                    if (content == null) continue;

                    model.SearchResults.Add(new CoreSearchResult
                    {
                        PageId = nodeId,
                        Title = content.Name ?? result.GetValues("nodeName").FirstOrDefault(),
                        Summary = result.GetValues("metaDescription").FirstOrDefault()
                                  ?? result.GetValues("pageTitle").FirstOrDefault(),
                        Link = content.Url(),
                        Score = result.Score
                    });
                }
            }
            catch
            {
                model.HasErrors = true;
            }

            return model;
        }
    }
}
