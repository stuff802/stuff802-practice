using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stuff802.Core.Helpers;

namespace Stuff802.Web.Controllers.Api
{
    [ApiController]
    public class SessionController : Controller
    {
        [HttpGet]
        [Route("umbraco/api/scalefilter/set")]
        public IActionResult SetArea(string filter)
        {
            if (HttpContext.Session.SetScaleFilter<string>("scaleFilter", filter))
            {
                return Ok();
            }
            else
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("umbraco/api/scalefilter/get")]
        public IActionResult GetArea()
        {
            return Ok(HttpContext.Session.GetScaleFilter<string>("scaleFilter") ?? string.Empty);
        }

        [HttpGet]
        [Route("umbraco/api/scalefilter/clear")]
        public IActionResult ClearArea()
        {
            HttpContext.Session.RemoveScaleFilter("scaleFilter");
            return Ok();
        }
    }
}
