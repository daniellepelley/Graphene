using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Graphene.Schema
{
    public static class Json
    {
        public static string Serialize(object value, Formatting formatting = Formatting.Indented)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return JsonConvert.SerializeObject(value, formatting, settings);
        }
    }
}