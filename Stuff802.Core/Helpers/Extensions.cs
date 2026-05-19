using System.Globalization;

namespace Stuff802.Core.Helpers
{
    public static class Extensions
    {
        public static string ToTitleCase(this string str) => 
            new CultureInfo("en-GB", false).TextInfo.ToTitleCase(str);
    }
}
