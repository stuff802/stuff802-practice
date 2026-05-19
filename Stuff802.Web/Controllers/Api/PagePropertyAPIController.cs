using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Common.Controllers;

namespace Stuff802.Core.Controllers
{
    public class PagePropertyAPIController : UmbracoApiController
    {
        private readonly UmbracoHelper _umbracoHelper;

        public PagePropertyAPIController(UmbracoHelper umbracoHelper)
        {
            _umbracoHelper = umbracoHelper;
        }

        [HttpGet]
        [Route("umbraco/api/getpageproperty")]
        public IActionResult GetPageProperty(int pageId, string pageProperty)
        {
            var propertyValue = _umbracoHelper.Content(pageId)?.GetProperty(pageProperty)?.GetValue() ?? "";

            return Ok(propertyValue);
        }
    }
}
