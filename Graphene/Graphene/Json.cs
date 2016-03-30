using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Graphene
{
    public static class Json
    {
        public static string Serialize(object value)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return JsonConvert.SerializeObject(value, Formatting.None, settings);
        }
    }
}