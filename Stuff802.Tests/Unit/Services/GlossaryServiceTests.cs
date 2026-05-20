using FluentAssertions;
using NSubstitute;
using Stuff802.Core.Services;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services.Navigation;
using Umbraco.Cms.Core.Web;

namespace Stuff802.Tests.Unit.Services;

public class GlossaryServiceTests
{
    private readonly IUmbracoContextAccessor _contextAccessor = Substitute.For<IUmbracoContextAccessor>();
    private readonly IDocumentNavigationQueryService _navigationService = Substitute.For<IDocumentNavigationQueryService>();
    private readonly IPublishedUrlProvider _urlProvider = Substitute.For<IPublishedUrlProvider>();

    private GlossaryService CreateService() =>
        new(_contextAccessor, _navigationService, _urlProvider);

    [Fact]
    public void GetGlossaryEntries_ReturnsEmpty_WhenContextUnavailable()
    {
        IUmbracoContext? ctx = null;
        _contextAccessor.TryGetUmbracoContext(out ctx).Returns(false);

        CreateService().GetGlossaryEntries().Should().BeEmpty();
    }

    [Fact]
    public void GetGlossaryEntries_ReturnsEmpty_WhenContentCacheIsNull()
    {
        var context = Substitute.For<IUmbracoContext>();
        context.Content.Returns((IPublishedContentCache?)null);

        IUmbracoContext? ctx = context;
        _contextAccessor
            .TryGetUmbracoContext(out Arg.Any<IUmbracoContext?>())
            .Returns(callInfo => { callInfo[0] = context; return true; });

        CreateService().GetGlossaryEntries().Should().BeEmpty();
    }

    [Fact]
    public void GetGlossaryEntries_ReturnsEmpty_WhenNoRootKeys()
    {
        var context = Substitute.For<IUmbracoContext>();
        var contentCache = Substitute.For<IPublishedContentCache>();
        context.Content.Returns(contentCache);

        IEnumerable<Guid> rootKeys = [];
        _navigationService.TryGetRootKeys(out Arg.Any<IEnumerable<Guid>>())
            .Returns(callInfo => { callInfo[0] = rootKeys; return false; });

        IUmbracoContext? ctx = context;
        _contextAccessor
            .TryGetUmbracoContext(out Arg.Any<IUmbracoContext?>())
            .Returns(callInfo => { callInfo[0] = context; return true; });

        CreateService().GetGlossaryEntries().Should().BeEmpty();
    }
}
