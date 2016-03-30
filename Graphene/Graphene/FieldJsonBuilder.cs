using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Graphene
{
    public class FieldJsonBuilder : IJsonBuilder
    {
        private string _name;
        private string _description;

        public List<IJsonBuilder> ArgsJsonBuilders { get; set; }
        public IJsonBuilder FieldTypeJsonBuilder { get; set; }

        public FieldJsonBuilder(string name, string description)
            : this(name, description, null, null)
        { }

        public FieldJsonBuilder(string name, string description, IJsonBuilder fieldTypeJsonBuilder, IEnumerable<IJsonBuilder> argsJsonBuilders)
        {
            _description = description;
            _name = name;
            FieldTypeJsonBuilder = fieldTypeJsonBuilder;
            ArgsJsonBuilders = new List<IJsonBuilder>();

            if (argsJsonBuilders != null)
                ArgsJsonBuilders.AddRange(argsJsonBuilders);
        }

        public string Build()
        {
            var jObject = new JObject
            {
                {"name", new JValue(_name)},
                {"description", new JValue(_description)},
                {"args", BuildArgs()},
                {
                    "type", BuildFieldType()
                },
                {"isDeprecated", new JValue(false)},
                {"deprecationReason", new JRaw("null")}
            };

            return jObject.ToString(Formatting.None);
        }

        private JToken BuildFieldType()
        {
            if (FieldTypeJsonBuilder != null)
            {
                return new JRaw(FieldTypeJsonBuilder.Build());
            }

            return new JRaw("null");
        }

        private JToken BuildArgs()
        {
            if (!ArgsJsonBuilders.Any())
            {
                return new JArray();
            }

            var array = new JArray();
            foreach (var t in ArgsJsonBuilders.Select(x => new JRaw(x.Build())))
            {
                array.Add(t);
            }

            return array;
        }
    }

    public class FieldTypeJsonBuilder : IJsonBuilder
    {
        private string[][] _pairs;

        public FieldTypeJsonBuilder(string[][] pairs)
        {
            _pairs = pairs;
        }

        public string Build()
        {
            JObject output = null;
            foreach (var pair in _pairs)
            {
                output = AddType(pair[0], pair[1], output);
            }

            return output.ToString(Formatting.None);
        }

        private JObject AddType(string kind, string name, JObject jObject)
        {
            var output = new JObject();
            output.Add("kind", new JValue(kind));
            output.Add("name", new JValue(name));
            output.Add("ofType", jObject != null ? (JToken)jObject : new JRaw("null"));
            return output;
        }
    }
}