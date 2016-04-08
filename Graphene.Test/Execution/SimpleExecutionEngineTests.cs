using System;
using System.Linq;
using Graphene.Core.Parsers;
using Graphene.Core.Types;
using Graphene.Execution;
using Graphene.Test.Spike;
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
            
            var expected =
                @"[{""Id"":1,""Name"":""Dan_Smith""},{""Id"":2,""Name"":""Lee_Smith""},{""Id"":3,""Name"":""Nick_Smith""}]";
            var result = sut.Execute(schema, document);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void RunsExecute2()
        {
            var sut = new ExecutionEngine();

            var schema = CreateGraphQLSchema();

            var query = "{user {Name}}";
            var document = new DocumentParser().Parse(query); ;

            var expected = @"[{""Name"":""Dan_Smith""},{""Name"":""Lee_Smith""},{""Name"":""Nick_Smith""}]";
            var result = sut.Execute(schema, document);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void RunsExecute3()
        {
            var sut = new ExecutionEngine();

            var schema = CreateGraphQLSchema();

            var query = "{user(Id :1) {Name}}";
            var document = new DocumentParser().Parse(query); ;

            var expected = @"[{""Name"":""Dan_Smith""}]";
            var result = sut.Execute(schema, document);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void RunsExecuteWithUnknownType()
        {
            var sut = new ExecutionEngine();

            var schema = CreateGraphQLSchema();

            var query = "{foo(Id :1) {Name}}";
            var document = new DocumentParser().Parse(query); ;

            var expected = @"[{""Name"":""Dan_Smith""}]";

            Assert.Throws<Exception>(() => sut.Execute(schema, document));
        }

        [Test]
        public void RunsExecuteWithUnknownField()
        {
            var sut = new ExecutionEngine();

            var schema = CreateGraphQLSchema();

            var query = "{foo(Id :1) {foo}}";
            var document = new DocumentParser().Parse(query); ;

            var expected = @"[{""Name"":""Dan_Smith""}]";

            Assert.Throws<Exception>(() => sut.Execute(schema, document));
        }

        private static GraphQLSchema CreateGraphQLSchema()
        {
            var schema = new GraphQLSchema
            {
                Query = new GraphQLObjectType
                {
                    Name = "user",
                    Resolve = context => Data.GetData().Where(x => !context.Arguments.ContainsKey("Id") || x.Id == Convert.ToInt32(context.Arguments["Id"])),
                    Fields = new IGraphQLFieldType[]
                    {
                        new GraphQLFieldType<int>
                        {
                            Name = "Id",
                            Resolve = context => ((TestUser) context.Source).Id
                        },
                        new GraphQLFieldType<string>
                        {
                            Name = "Name",
                            Resolve = context => ((TestUser) context.Source).Name
                        }
                    }.ToList()
                }
            };
            return schema;
        }
    }
}