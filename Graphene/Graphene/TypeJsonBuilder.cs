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
}
