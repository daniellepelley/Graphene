using System.Linq;
using Graphene.Core.Parsers;
using Graphene.Core.Types;
using Graphene.Core.Types.Introspection;
using Graphene.Execution;
using Graphene.Test.Data;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Graphene.Test.Introspection
{
    public class TypeIntrospectionExecutionEngineTests
    {
        [Test]
        //[Ignore("Introspection to be done")]
        public void StringDescription()
        {
            var sut = new ExecutionEngine(true);

            var schema = CreateIntrospectionSchema(TestSchemas.CreateUserType());

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

            var expected = @"{""data"":{""kind"":""OBJECT"",""name"":""User"",""ofType"":null}}";

            var result = JsonConvert.SerializeObject(sut.Execute(schema, document));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void StringNameAndDescription()
        {
            var sut = new ExecutionEngine(true);

            var schema = CreateIntrospectionSchema(new GraphQLString());

            var query = @"{__type(name:""String""){name,description}}";
            var document = new DocumentParser().Parse(query); ;

            var expected = @"{""data"":{""name"":""String"",""description"":""The `String` scalar type represents textual data, represented as UTF-8 character sequences. The String type is most often used by GraphQL to represent free-form human-readable text.""}}";

            var result = JsonConvert.SerializeObject(sut.Execute(schema, document));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void IntNameAndDescription()
        {
            var sut = new ExecutionEngine(true);

            var schema = CreateIntrospectionSchema(new GraphQLInt());

            var query = @"{__type(name:""Int""){name,description,kind}}";
            var document = new DocumentParser().Parse(query); ;

            var expected = @"{""data"":{""name"":""Int"",""description"":""The `Int` scalar type represents non-fractional signed whole numeric values. Int can represent values between -(2^31) and 2^31 - 1. '"",""kind"":""SCALAR""}}";

            var result = JsonConvert.SerializeObject(sut.Execute(schema, document));

            //DoLots(schema, document, sut);
            
            Assert.AreEqual(expected, result);
        }

        //private static void DoLots(IGraphQLSchema schema, Document document, ExecutionEngine sut)
        //{
        //    for (int i = 0; i < 1000; i++)
        //    {
        //        sut.Execute(schema, document);
        //    }
        //}

        private static GraphQLSchema CreateIntrospectionSchema(IGraphQLType type)
        {
            var query = ((GraphQLSchema) TestSchemas.UserSchema()).Query;

            var newSchema = new GraphQLSchema
            {
                Query = new GraphQLObjectField<IGraphQLType>
                {
                    Name = "__type",
                    GraphQLObjectType = new __Type(),
                    Resolve = _ => type
                }
            };
            return newSchema;
        }
    }
}