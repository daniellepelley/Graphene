using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Graphene
{
    public class TypeJsonBuilder : IJsonBuilder
    {
        private readonly string _kind;
        private readonly string _name;

        public List<IJsonBuilder> FieldJsonBuilders { get; set; }

        public TypeJsonBuilder(string kind, string name)
        {
            _name = name;
            _kind = kind;
            FieldJsonBuilders = new List<IJsonBuilder>();
        }

        public string Build()
        {
            var jObject = new JObject();
            jObject.Add("kind", new JValue(_kind));
            jObject.Add("name", new JValue(_name));
            jObject.Add("description", new JRaw("null"));
            jObject.Add("fields", BuildFields());
            jObject.Add("inputFields", new JArray());
            jObject.Add("interfaces", new JArray());
            jObject.Add("enumValues", new JArray());
            jObject.Add("possibleTypes", new JArray());
           
            return jObject.ToString(Newtonsoft.Json.Formatting.None);
        }

        private JToken BuildFields()
        {
            if (!FieldJsonBuilders.Any())
            {
                return new JArray();
            }

            var array = new JArray();
            foreach(var t in FieldJsonBuilders.Select(x => new JRaw(x.Build())))
            {
                array.Add(t);                
            }

            return array;
        }
    }
}
