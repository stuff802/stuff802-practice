using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Stuff802.Web.Controllers.Render;

public class ErrorController : Controller
{
    [Route("error")]
    public IActionResult Index()
    {
        if (Response.StatusCode == StatusCodes.Status500InternalServerError)
        {
            return Redirect("/500");
        }

        return Redirect("/");
    }
}