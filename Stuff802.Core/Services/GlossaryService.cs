using Stuff802.Core.Models;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services.Navigation;
using Umbraco.Cms.Core.Web;
using Stuff802.Core.Interfaces;

namespace Stuff802.Core.Services;

public class GlossaryService : IGlossaryService
{
    private readonly IUmbracoContextAccessor _umbracoContextAccessor;
    private readonly IDocumentNavigationQueryService _navigationQueryService;
    private readonly IPublishedUrlProvider _publishedUrlProvider;

    public GlossaryService(
        IUmbracoContextAccessor umbracoContextAccessor,
        IDocumentNavigationQueryService navigationQueryService,
        IPublishedUrlProvider publishedUrlProvider)
    {
        _umbracoContextAccessor = umbracoContextAccessor;
        _navigationQueryService = navigationQueryService;
        _publishedUrlProvider = publishedUrlProvider;
    }

    public IEnumerable<GlossaryEntryDto> GetGlossaryEntries()
    {
        if (!_umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext))
            return Enumerable.Empty<GlossaryEntryDto>();

        var contentCache = umbracoContext.Content;
        if (contentCache == null)
            return Enumerable.Empty<GlossaryEntryDto>();

        var results = new List<GlossaryEntryDto>();

        if (!_navigationQueryService.TryGetRootKeys(out IEnumerable<Guid> rootKeys))
            return results;

        var allRoots = rootKeys
            .Select(key => contentCache.GetById(key))
            .Where(x => x != null)
            .Cast<IPublishedContent>()
            .ToList();

        var glossaryPages = new List<IPublishedContent>();
        foreach (var root in allRoots)
        {
            CollectPages(root, "subContentWithLeftNavPage", glossaryPages, contentCache);
        }

        foreach (var page in glossaryPages)
        {
            var pageUrl = _publishedUrlProvider.GetUrl(page);

            var blockListProperty = page.GetProperty("mainContent");
            if (blockListProperty == null) continue;

            var blockList = blockListProperty.GetValue() as BlockListModel;
            if (blockList == null) continue;

            foreach (var block in blockList)
            {
                var title = block.Content.GetProperty("title")?.GetValue()?.ToString();
                var anchor = block.Content.GetProperty("anchorName")?.GetValue()?.ToString();

                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(anchor))
                    continue;

                results.Add(new GlossaryEntryDto
                {
                    Title = title,
                    Url = pageUrl,
                    Anchor = anchor
                });
            }
        }

        return results.OrderBy(x => x.Title);
    }

    private void CollectPages(
        IPublishedContent node,
        string alias,
        List<IPublishedContent> results,
        IPublishedContentCache contentCache)
    {
        if (node.ContentType.Alias == alias)
            results.Add(node);

        // Use navigation query service to get children keys
        if (!_navigationQueryService.TryGetChildrenKeys(node.Key, out IEnumerable<Guid> childKeys))
            return;

        foreach (var childKey in childKeys)
        {
            var child = contentCache.GetById(childKey);
            if (child != null)
                CollectPages(child, alias, results, contentCache);
        }
    }
}
