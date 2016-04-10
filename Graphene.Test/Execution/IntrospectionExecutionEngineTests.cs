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
    public class IntrospectionExecutionEngineTests
    {
        [Test]
        public void RunsExecute()
        {
            var sut = new ExecutionEngine();

            var schema = CreateIntrospectionSchema();

            var query = @"{__type(name:""String""){description}}";
            var document = new DocumentParser().Parse(query); ;

            var expected = @"{""data"":{""description"":""The `String` scalar type represents textual data, represented as UTF-8 character sequences. The String type is most often used by GraphQL to represent free-form human-readable text.""}}";

            var result = JsonConvert.SerializeObject(sut.Execute(schema, document));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void RunsExecute2()
        {
            var sut = new ExecutionEngine();

            var schema = CreateIntrospectionSchema();

            var query = @"{__type(name:""String""){name,description}}";
            var document = new DocumentParser().Parse(query); ;

            var expected = @"{""data"":{""name"":""String"",""description"":""The `String` scalar type represents textual data, represented as UTF-8 character sequences. The String type is most often used by GraphQL to represent free-form human-readable text.""}}";

            var result = JsonConvert.SerializeObject(sut.Execute(schema, document));
            Assert.AreEqual(expected, result);
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

        private static GraphQLSchema CreateIntrospectionSchema()
        {
            var types = new[]
            {
                new GraphQLString()
            };

            var newSchema = new GraphQLSchema
            {
                Query = new GraphQLObjectType
                {
                    Name = "__type",
                    Resolve = context => types.FirstOrDefault(x => !context.Arguments.ContainsKey("name") || x.Name == context.Arguments["name"].ToString()),
                    Fields = new IGraphQLFieldType[]
                    {
                        new GraphQLFieldType<string>
                        {
                            Name = "name",
                            Resolve = context => ((IGraphQLType) context.Source).Name
                        },
                        new GraphQLFieldType<string>
                        {
                            Name = "description",
                            Resolve = context => ((IGraphQLType) context.Source).Description
                        }
                    }.ToList()
                }
            };
            return newSchema;
        }
    }
}