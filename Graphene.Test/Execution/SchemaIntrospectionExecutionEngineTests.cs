using System;
using System.Linq;
using Graphene.Core.Parsers;
using Graphene.Core.Types;
using Graphene.Core.Types.Introspection;
using Graphene.Execution;
using Graphene.Test.Spike;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Graphene.Test.Execution
{
    public class SchemaIntrospectionExecutionEngineTests
    {
        [Test]
        public void StringDescription()
        {
            var sut = new ExecutionEngine(true, new IntrospectionSchemaFactory(CreateIntrospectionSchema()));

            var schema = CreateGraphQLSchema();

            var query = @"{__schema{queryType{name}}}";
            var document = new DocumentParser().Parse(query); ;

            var expected = @"{""data"":{""queryType"":{""name"":null}}}";

            var result = JsonConvert.SerializeObject(sut.Execute(schema, document));
            Assert.AreEqual(expected, result);
        }

        private static GraphQLSchema CreateIntrospectionSchema()
        {
            var newSchema = new GraphQLSchema
            {
                Query = new __Schema()
            };
            return newSchema;
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