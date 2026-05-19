using Microsoft.AspNetCore.Mvc;
using Stuff802.Core.Interfaces;

namespace Stuff802.Web.Controllers.Api;

[ApiController]
[Route("api/glossary")]
public class GlossaryApiController : ControllerBase
{
    private readonly IGlossaryService _glossaryService;

    public GlossaryApiController(IGlossaryService glossaryService)
    {
        _glossaryService = glossaryService;
    }

    [HttpGet]
    public IActionResult GetEntries()
    {
        var entries = _glossaryService.GetGlossaryEntries();
        return Ok(entries);
    }
}
