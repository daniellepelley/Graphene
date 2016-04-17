using System.IO;
using System.Text;
using Graphene.Core.Model;
using Graphene.Core.Parsers;
using Graphene.Core.Types;
using Graphene.Core.Types.Introspection;
using Graphene.Execution;
using Graphene.Schema;
using Graphene.Test.Data;
using Graphene.Test.Execution;
using Newtonsoft.Json;
using NUnit.Framework;
using GraphQLSchema = Graphene.Core.Types.GraphQLSchema;

namespace Graphene.Test.Introspection
{

    public class GraphiqlTest
    {
        private string _queryWithoutFragments = @"{""query"":""" + TestSchemas.GraphiQlQueryWithoutFragments + @""",""variables"":null}";

        private string _queryWithFragments = @"{""query"":""" + TestSchemas.GraphiQlQueryWithFragments + @""",""variables"":null}";

        [Test]
        public void WithFragments()
        {
            var json = RunQuery(_queryWithFragments);

            Assert.IsFalse(json.Contains("errors"));
        }

        [Test]
        public void WithFragmentsShould()
        {
            var actual = RunQuery(_queryWithFragments);

            var expected = FormatJson(File.ReadAllText(@"Introspection\Response.json"));

            var start = 0;//2658 + 2275;


            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(expected.Substring(start, expected.Length - start));
            stringBuilder.AppendLine(actual.Substring(start, actual.Length - start));
            File.WriteAllText(@"C:\Users\Danny\Source\Repos\Graphene\Graphene.Test\Introspection\Actual.json", stringBuilder.ToString());
             
        
            Assert.AreEqual(expected, actual);
        }

        private static string FormatJson(string json)
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(parsedJson, Formatting.None);
        }

        [Test]
        public void WithoutFragments()
        {
            var json = RunQuery(_queryWithoutFragments);

            Assert.IsFalse(json.Contains("errors"));
        }

        [Test]
        public void CompareDocuments()
        {
            var documentWithoutFragments = CreateDocument(_queryWithoutFragments);
            var jsonWithoutFragments = JsonConvert.SerializeObject(documentWithoutFragments);

            var documentWithFragments = CreateDocument(_queryWithFragments);

            new FragmentProcessor().Process(documentWithFragments, true);
            
            var jsonWithFragments = JsonConvert.SerializeObject(documentWithFragments, new JsonSerializerSettings { Formatting = Formatting.Indented});

            Assert.AreEqual(jsonWithoutFragments, jsonWithFragments);
        }

        public string RunQuery(string query)
        {
            var document = CreateDocument(query);

            var result = new ExecutionEngine().Execute(TestSchemas.CreateIntrospectionSchema(), document);

            var json = JsonConvert.SerializeObject(result);

            return json;            
        }

        private static Document CreateDocument(string query)
        {
            var queryObject = JsonConvert.DeserializeObject<QueryObject>(query);

            var document = new DocumentParser().Parse(queryObject.Query);
            return document;
        }
    }

    public class QueryObject
    {
        public string Query { get; set; }
        public string Variables { get; set; }
    }
}