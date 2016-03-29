using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Graphene
{
    public class TypeJsonBuilder
    {
        public string Build(string kind, string name)
        {
            var jObject = new JObject
            {
                {"kind", new JValue(kind)},
                {"name", new JValue(name)},
                {"description", new JRaw("null")},
                {"fields", new JArray()},
                {"inputFields", new JArray()},
                {"interfaces", new JArray()},
                {"enumValues", new JArray()},
                {"possibleTypes", new JArray()}
            };
            return jObject.ToString();
        }
    }
}
