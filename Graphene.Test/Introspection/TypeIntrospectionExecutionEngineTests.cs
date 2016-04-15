using Graphene.Core.Model;
using Graphene.Core.Parsers;
using Graphene.Core.Types;
using Graphene.Core.Types.Introspection;
using Graphene.Execution;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Graphene.Test.Execution
{
    public class TypeIntrospectionExecutionEngineTests
    {
        [Test]
        //[Ignore("Introspection to be done")]
        public void StringDescription()
        {
            var sut = new ExecutionEngine(true, new IntrospectionSchemaFactory(CreateIntrospectionSchema()));

            var schema = CreateIntrospectionSchema();

            var query = @"__type{
                            kind
                            name
                            ofType {
                              kind
                              name
                            }
                        }";

            //                              ofType {
            //                                kind
            //                                name
            //                                ofType {
            //                                  kind
            //                                  name
            //                                }
            //                              }


            //var query = @"{__type(name:""String""){description}}";
            var document = new DocumentParser().Parse(query); ;

            var expected = @"{""data"":{""kind"":""OBJECT"",""name"":""user""}}";

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

            var query = @"{__type(name:""Int""){name,description,kind}}";
            var document = new DocumentParser().Parse(query); ;

            var expected = @"{""data"":{""name"":""Int"",""description"":""The `Int` scalar type represents non-fractional signed whole numeric values. Int can represent values between -(2^31) and 2^31 - 1. '"",""kind"":""SCALAR""}}";

            var result = JsonConvert.SerializeObject(sut.Execute(schema, document));

            //DoLots(schema, document, sut);
            
            Assert.AreEqual(expected, result);
        }

        //private static void DoLots(IGraphQLSchema schema, Document document, ExecutionEngine sut)
        //{
        //    for (int i = 0; i < 100000; i++)
        //    {
        //        sut.Execute(schema, document);
        //    }
        //}

        private static GraphQLSchema CreateIntrospectionSchema()
        {
            var query = ((GraphQLSchema) TestSchemas.UserSchema()).Query;

            var types = new IGraphQLType[]
            {
                query.GraphQLObjectType,
                new GraphQLString(),
                new GraphQLInt() 
            };

            var newSchema = new GraphQLSchema
            {
                Query = new __Type(types)
            };
            return newSchema;
        }
    }
}