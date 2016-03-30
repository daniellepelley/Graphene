using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Graphene
{
    public interface IJsonBuilder
    {
        string Build();
    }

    public class TypeJsonBuilder : IJsonBuilder
    {
        private readonly string _kind;
        private readonly string _name;

        public IJsonBuilder FieldJsonBuilder { get; set; }

        public TypeJsonBuilder(string kind, string name)
        {
            _name = name;
            _kind = kind;
        }

        public string Build()
        {
            var jObject = new JObject
            {
                {"kind", new JValue(_kind)},
                {"name", new JValue(_name)},
                {"description", new JRaw("null")},
                {"fields", FieldJsonBuilder == null ? (JToken)new JArray() : (JToken)new JRaw(FieldJsonBuilder.Build())},
                {"inputFields", new JArray()},
                {"interfaces", new JArray()},
                {"enumValues", new JArray()},
                {"possibleTypes", new JArray()}
            };

            return jObject.ToString();
        }
    }

    public class FieldJsonBuilder : IJsonBuilder
    {
        public string Build()
        {
  //            "name": "hero",
  //"description": null,
  //"args": [],
  //"type": {
  //  "kind": "INTERFACE",
  //  "name": "Character",
  //  "ofType": null
  //},
  //"isDeprecated": false,
  //"deprecationReason": null

            var jObject = new JObject
            {
                {"name", new JValue(_kind)},
                {"description", new JValue(_name)},
                {"args", new JRaw("null")},
                {"inputFields", new JArray()},
                {"interfaces", new JArray()},
                {"enumValues", new JArray()},
                {"possibleTypes", new JArray()}
            };

            return jObject.ToString();
        }
    }
}
