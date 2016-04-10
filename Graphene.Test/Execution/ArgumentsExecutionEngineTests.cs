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
            var sut = new ExecutionEngine();

            var schema = CreateGraphQLSchema();

            var query = "{user(id:1) {id, name}}";
            var document = new DocumentParser().Parse(query); ;

            var expected =
                @"{""errors"":[{""message"":""Argument 'id' has invalid value 1. Expected type 'String'""}]}";
            var result = Execute(sut, schema, document);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void WhenArgumentIsNotFound2()
        {
            var sut = new ExecutionEngine();

            var schema = CreateGraphQLSchema();

            var query = "{user(id:2) {id, name}}";
            var document = new DocumentParser().Parse(query); ;

            var expected =
                @"{""errors"":[{""message"":""Argument 'id' has invalid value 2. Expected type 'String'""}]}";
            var result = Execute(sut, schema, document);
            Assert.AreEqual(expected, result);
        }

        private static object Execute(ExecutionEngine sut, GraphQLSchema schema, Document document)
        {
            return JsonConvert.SerializeObject(sut.Execute(schema, document));
        }

        private static GraphQLSchema CreateGraphQLSchema()
        {
            var schema = new GraphQLSchema
            {
                Query = new GraphQLObjectType
                {
                    Name = "user",
                    Arguments = new [] { new GraphQLFieldType<string>
                        {
                            Name = "id",
                            OfType = new GraphQLString()
                    }},
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