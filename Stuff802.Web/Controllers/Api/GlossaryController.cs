using Microsoft.AspNetCore.Mvc;
using Stuff802.Core.Interfaces;
using Umbraco.Cms.Web.Common.Controllers;

namespace Stuff802.Web.Controllers.Api;

public class GlossaryController : UmbracoApiController
{
    private readonly IGlossaryService _glossaryService;

    public GlossaryController(IGlossaryService glossaryService)
    {
        _glossaryService = glossaryService;
    }

    [HttpGet]
    public IActionResult Entries()
    {
        return Ok(_glossaryService.GetGlossaryEntries());
    }
}
