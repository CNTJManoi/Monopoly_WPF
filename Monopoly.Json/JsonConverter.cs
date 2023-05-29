using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Monopoly.Json
{
    public static class JsonConverter<T>
    {
        public static string SerializeObject(T entity)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            return JsonConvert.SerializeObject(entity, Formatting.Indented, settings);
        }
        public static T? DeserializeObject(string json)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var test = JsonConvert.DeserializeObject<T>(json, settings);
            return test;
        }
    }
}
