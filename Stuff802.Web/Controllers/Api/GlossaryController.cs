using Microsoft.AspNetCore.Mvc;
using Stuff802.Core.Interfaces;

namespace Stuff802.Web.Controllers.Api;

[ApiController]
[Route("umbraco/api/glossary")]
public class GlossaryController : ControllerBase
{
    private readonly IGlossaryService _glossaryService;

    public GlossaryController(IGlossaryService glossaryService)
    {
        _glossaryService = glossaryService;
    }

    [HttpGet("entries")]
    public IActionResult Entries()
    {
        return Ok(_glossaryService.GetGlossaryEntries());
    }
}
