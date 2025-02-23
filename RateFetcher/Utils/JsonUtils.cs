using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RateFetcher.Utils
{
    public class JsonUtils
    {
        public static T? Deserialize<T>(string json, JsonSerializerOptions? options = null)
        {
            if (string.IsNullOrWhiteSpace(json))
                return default;

            return JsonSerializer.Deserialize<T>(json, options);
        }
        public static string Serialize<T>(T obj, JsonSerializerOptions? options = null)
        {
            return JsonSerializer.Serialize(obj, options);
        }
    }
}
