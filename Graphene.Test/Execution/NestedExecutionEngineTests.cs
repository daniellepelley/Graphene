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

            var query = "{user(id: 1) {id, name, boss {id, name}}}";
            var document = new DocumentParser().Parse(query);

            var expected =
                @"{""data"":{""id"":1,""name"":""Dan_Smith"",""boss"":{""id"":4,""name"":""Boss_Smith""}}}";
            var result = Execute(sut, schema, document);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void RunsExecute2()
        {
            var sut = new ExecutionEngine();

            var schema = CreateGraphQLSchema();

            var query = "{user(id: 1) {name, boss {name}}}";
            var document = new DocumentParser().Parse(query);

            var expected =
                @"{""data"":{""name"":""Dan_Smith"",""boss"":{""name"":""Boss_Smith""}}}";
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