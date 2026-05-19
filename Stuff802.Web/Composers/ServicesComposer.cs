using Stuff802.Core.Interfaces;
using Stuff802.Core.Services;
using Stuff802.Web.Services;
using Umbraco.Cms.Core.Composing;

namespace Stuff802.Web.Composers;

public class ServicesComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.AddScoped<IGlossaryService, GlossaryService>();
        builder.Services.AddScoped<ISearchService, SearchService>();
    }
}
