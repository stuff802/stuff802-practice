using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using Umbraco.Cms.Core.Models.Blocks;
using Umbraco.Extensions;

namespace Stuff802.Web.Helpers
{
    public static class HtmlExtensions
    {
        public static IHtmlContent WMDCGetBlockListHtml(this IHtmlHelper helper, BlockListModel blocklist)
        {
            string c = GetString(helper.GetBlockListHtml(blocklist)) ?? "";
            c = c.Trim().Trim('\r', '\n').TrimStart('\r', '\n');
            c = Regex.Replace(c, @"^<[^>^<.]*>", "");
            c = Regex.Replace(c, @"<[^>^<.]*>$", "");
            c = c.Trim().Trim('\r', '\n').TrimStart('\r', '\n');

            return new HtmlString(c);
        }

        public static string GetHtmlString(this IHtmlHelper helper, IHtmlContent content)
        {
            if (content == null)
            {
                return string.Empty;
            }
            return GetString(content);
        }

        private static string GetString(IHtmlContent content)
        {
            using (var writer = new System.IO.StringWriter())
            {
                content.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }
    }
}
