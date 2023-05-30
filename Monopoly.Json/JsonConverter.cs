using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Logic;
using Newtonsoft.Json;

namespace Monopoly.Json
{
    public static class JsonConverter
    {
        public static string SerializeObject(Game entity)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects

            };
            return JsonConvert.SerializeObject(entity, Formatting.Indented, settings);
        }
        public static Game? DeserializeObject(string json)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
            var test = JsonConvert.DeserializeObject<Game>(File.ReadAllText(json), settings);
            return test;
        }
    }
}
