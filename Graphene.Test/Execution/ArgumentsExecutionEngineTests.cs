using System;
using System.Linq;
using Graphene.Core.Model;
using Graphene.Core.Parsers;
using Graphene.Core.Types;
using Graphene.Execution;
using Graphene.Test.Spike;
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

            var schema = CreateGraphQLSchema();

            var query = @"{user(id:""1"") {id, name}}";
            var document = new DocumentParser().Parse(query); ;

            var expected =
                @"{""data"":[{""id"":1,""name"":""Dan_Smith""}]}";
            var result = Execute(sut, schema, document);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void WhenArgumentIsNotFound()
        {
            AssertArgumentTypeHandling(new GraphQLString(), "1", "Argument 'id' has invalid value 1. Expected type 'String'");
        }

        [Test]
        public void WhenArgumentIsNotFound2()
        {
            AssertArgumentTypeHandling(new GraphQLString(), "2", "Argument 'id' has invalid value 2. Expected type 'String'");
        }

        [Test]
        public void WhenArgumentIsNotFound3()
        {
            AssertArgumentTypeHandling(new GraphQLInt(), @"""42""", "Argument 'id' has invalid value 42. Expected type 'Int'");
        }

        private void AssertArgumentTypeHandling(IGraphQLType graphQLType, string queryValue, string expectedErrorMessage)
        {
            var sut = new ExecutionEngine();

            var schema = CreateGraphQLSchema(new IGraphQLFieldType[] {
                    new GraphQLFieldType<string>
                    {
                        Name = "id",
                        OfType = graphQLType
                    }});

            var query = "{user(id:" + queryValue + ") {id, name}}";
            var document = new DocumentParser().Parse(query); ;

            var expected =
                @"{""errors"":[{""message"":""" + expectedErrorMessage + @"""}]}";
            var result = Execute(sut, schema, document);
            Assert.AreEqual(expected, result);
        }

        private static object Execute(ExecutionEngine sut, GraphQLSchema schema, Document document)
        {
            return JsonConvert.SerializeObject(sut.Execute(schema, document));
        }

        private static GraphQLSchema CreateGraphQLSchema(IGraphQLFieldType[] arguments = null)
        {
            var schema = new GraphQLSchema
            {
                Query = new GraphQLObjectType
                {
                    Name = "user",
                    Arguments = arguments,
                    Resolve = context => Data.GetData().Where(x => !context.Arguments.ContainsKey("id") || x.Id == Convert.ToInt32(context.Arguments["id"])),
                    Fields = new IGraphQLFieldType[]
                    {
                        new GraphQLFieldType<int>
                        {
                            Name = "id",
                            Resolve = context => ((TestUser) context.Source).Id
                        },
                        new GraphQLFieldType<string>
                        {
                            Name = "name",
                            Resolve = context => ((TestUser) context.Source).Name
                        }
                    }.ToList()
                }
            };
            return schema;
        }
    }
}