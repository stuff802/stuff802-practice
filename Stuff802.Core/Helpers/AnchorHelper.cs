using System.Text.RegularExpressions;

namespace Stuff802.Core.Helpers;

public static class AnchorHelper
{
    public static string ToAnchorId(string rawAnchor)
    {
        if (string.IsNullOrWhiteSpace(rawAnchor))
            return string.Empty;

        return Regex.Replace(
            Regex.Replace(rawAnchor.TrimEnd(), @"[^a-zA-Z0-9 -]", ""),
            @"\s+", "-");
    }
}
