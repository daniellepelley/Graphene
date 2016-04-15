using Graphene.Core.Model;
using Graphene.Core.Parsers;
using Graphene.Core.Types;
using Graphene.Execution;
using Graphene.Test.Data;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Graphene.Test.Execution
{
    public class SimpleExecutionEngineTests
    {
        [Test]
        public void RunsExecute()
        {
            var sut = new ExecutionEngine();

            var schema = CreateGraphQLSchema();

            var query = "{user {Id, Name}}";
            var document = new DocumentParser().Parse(query); ;

            var expectedWhenList =
                @"{""data"":[{""Id"":1,""Name"":""Dan_Smith""},{""Id"":2,""Name"":""Lee_Smith""},{""Id"":3,""Name"":""Nick_Smith""}]}";

            var expected =
                @"{""data"":{""Id"":1,""Name"":""Dan_Smith""}}";
            var result = Execute(sut, schema, document);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void RunsExecute2()
        {
            var sut = new ExecutionEngine();

            var schema = CreateGraphQLSchema();

            var query = "{user {Name}}";
            var document = new DocumentParser().Parse(query); ;

            var expectedWhenList = @"{""data"":[{""Name"":""Dan_Smith""},{""Name"":""Lee_Smith""},{""Name"":""Nick_Smith""}]}";

            var expected = @"{""data"":{""Name"":""Dan_Smith""}}";

            var result = Execute(sut, schema, document);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void RunsExecute3()
        {
            var sut = new ExecutionEngine();

            var schema = CreateGraphQLSchema();

            var query = "{user(Id :1) {Name}}";
            var document = new DocumentParser().Parse(query); ;

            var expected = @"{""data"":{""Name"":""Dan_Smith""}}";
            var result = Execute(sut, schema, document);
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void RunsExecuteWithUnknownType()
        {
            var sut = new ExecutionEngine();

            var schema = CreateGraphQLSchema();

            var query = "{foo(Id :1) {Name}}";
            var document = new DocumentParser().Parse(query); ;

            var expected = @"{""errors"":[{""message"":""Object foo does not exist""}]}";
            var result = Execute(sut, schema, document);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void RunsExecuteWithUnknownField()
        {
            var sut = new ExecutionEngine();

            var schema = CreateGraphQLSchema();

            var query = "{user(Id :1) {foo}}";
            var document = new DocumentParser().Parse(query); ;

            var expected = @"{""errors"":[{""message"":""Field foo does not exist""}]}";
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