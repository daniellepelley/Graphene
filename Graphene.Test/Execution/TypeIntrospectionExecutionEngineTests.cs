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
    public class TypeIntrospectionExecutionEngineTests
    {
        [Test]
        public void StringDescription()
        {
            var sut = new ExecutionEngine(true, new IntrospectionSchemaFactory(CreateIntrospectionSchema()));

            var schema = CreateGraphQLSchema();

            var query = @"{__type(name:""String""){description}}";
            var document = new DocumentParser().Parse(query); ;

            var expected = @"{""data"":{""description"":""The `String` scalar type represents textual data, represented as UTF-8 character sequences. The String type is most often used by GraphQL to represent free-form human-readable text.""}}";

            var result = JsonConvert.SerializeObject(sut.Execute(schema, document));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void StringNameAndDescription()
        {
            var sut = new ExecutionEngine(true, new IntrospectionSchemaFactory(CreateIntrospectionSchema()));

            var schema = CreateIntrospectionSchema();

            var query = @"{__type(name:""String""){name,description}}";
            var document = new DocumentParser().Parse(query); ;

            var expected = @"{""data"":{""name"":""String"",""description"":""The `String` scalar type represents textual data, represented as UTF-8 character sequences. The String type is most often used by GraphQL to represent free-form human-readable text.""}}";

            var result = JsonConvert.SerializeObject(sut.Execute(schema, document));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void IntNameAndDescription()
        {
            var sut = new ExecutionEngine(true, new IntrospectionSchemaFactory(CreateIntrospectionSchema()));

            var schema = CreateIntrospectionSchema();

            var query = @"{__type(name:""Int""){name,description}}";
            var document = new DocumentParser().Parse(query); ;

            var expected = @"{""data"":{""name"":""Int"",""description"":""The `Int` scalar type represents non-fractional signed whole numeric values. Int can represent values between -(2^31) and 2^31 - 1. '""}}";

            var result = JsonConvert.SerializeObject(sut.Execute(schema, document));
            Assert.AreEqual(expected, result);
        }

        private static GraphQLSchema CreateIntrospectionSchema()
        {
            var types = new IGraphQLType[]
            {
                new GraphQLString(),
                new GraphQLInt() 
            };

            var newSchema = new GraphQLSchema
            {
                Query = new __Type(types)
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