using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Stuff802.Core.Helpers
{
    public static class SessionExtensions // Changed to static class to fix CS1106
    {
        public static bool SetScaleFilter<T>(this ISession session, string key, T value)
        {
            bool ret = true;

            try
            {
                session.SetString(key, JsonSerializer.Serialize(value));
            }
            catch (Exception)
            {
                ret = false;
            }

            return ret;
        }

        public static T? GetScaleFilter<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }

        public static void RemoveScaleFilter(this ISession session, string key)
        {
            session.Remove(key);
        }
    }
}
