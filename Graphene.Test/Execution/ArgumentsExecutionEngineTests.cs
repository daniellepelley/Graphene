using Graphene.Core.Model;
using Graphene.Core.Parsers;
using Graphene.Core.Types;
using Graphene.Execution;
using Graphene.Test.Data;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Graphene.Test.Execution
{
    public class ArgumentsExecutionEngineTests
    {
        [Test]
        public void WhenArgumentIsFound()
        {
            var sut = new ExecutionEngine();

            var schema = TestSchemas.UserSchema();

            var query = @"{user(id:""1"") {id, name}}";
            var document = new DocumentParser().Parse(query);

            var expected =
                @"{""data"":{""user"":{""id"":1,""name"":""Dan_Smith""}}}";
            var result = Execute(sut, schema, document);
            Assert.AreEqual(expected, result);
        }

        private static object Execute(ExecutionEngine sut, GraphQLSchema schema, Document document)
        {
            return JsonConvert.SerializeObject(sut.Execute(schema, document));
        }
    }
}