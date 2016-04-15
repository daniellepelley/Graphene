using Graphene.Core.Model;
using Graphene.Core.Parsers;
using Graphene.Core.Types;
using Graphene.Execution;
using Graphene.Test.Data;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Graphene.Test.Execution
{
    public class NestedExecutionEngineTests
    {
        [Test]
        public void RunsExecute()
        {
            var sut = new ExecutionEngine();

            var schema = CreateGraphQLSchema();

            var query = "{user(Id: 1) {Id, Name, Boss {Id, Name}}}";
            var document = new DocumentParser().Parse(query);

            var expected =
                @"{""data"":{""Id"":1,""Name"":""Dan_Smith"",""Boss"":{""Id"":4,""Name"":""Boss_Smith""}}}";
            var result = Execute(sut, schema, document);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void RunsExecute2()
        {
            var sut = new ExecutionEngine();

            var schema = CreateGraphQLSchema();

            var query = "{user(Id: 1) {Name, Boss {Name}}}";
            var document = new DocumentParser().Parse(query);

            var expected =
                @"{""data"":{""Name"":""Dan_Smith"",""Boss"":{""Name"":""Boss_Smith""}}}";
            var result = Execute(sut, schema, document);
            Assert.AreEqual(expected, result);
        }

        private static object Execute(ExecutionEngine sut, IGraphQLSchema schema, Document document)
        {
            return JsonConvert.SerializeObject(sut.Execute(schema, document));
        }

        private static IGraphQLSchema CreateGraphQLSchema()
        {
            return TestSchemas.UserSchema();
        }
    }
}