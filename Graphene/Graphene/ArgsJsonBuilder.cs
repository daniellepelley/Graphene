using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Graphene
{
    public class ArgsJsonBuilder : IJsonBuilder
    {
        private string _name;
        private string _description;

        public ArgsJsonBuilder(string name, string description)
        {
            _description = description;
            _name = name;
        }

        public string Build()
        {
            var jObject = new JObject
            {
                {"name", new JValue(_name)},
                {"description", new JValue(_description)},
                {
                    "type", new JObject
                    {
                        {"kind", "NON_NULL"},
                        {"name", new JRaw("null")},
                        {
                            "ofType", new JObject
                            {
                                {"kind", "SCALAR"},
                                {"name", "String"},
                                {"ofType", new JRaw("null")}
                            }
                        }
                    }
                },
                {"defaultValue", new JRaw("null")}
            };

            return jObject.ToString(Formatting.None);
        }
    }
}