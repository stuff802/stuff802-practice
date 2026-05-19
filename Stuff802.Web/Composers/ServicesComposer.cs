using Stuff802.Core.Interfaces;
using Stuff802.Core.Services;
using Umbraco.Cms.Core.Composing;

namespace Stuff802.Web.Composers;

public class ServicesComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.AddScoped<IGlossaryService, GlossaryService>();
    }
}
