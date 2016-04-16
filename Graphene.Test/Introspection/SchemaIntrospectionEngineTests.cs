using Graphene.Core.Parsers;
using Graphene.Core.Types;
using Graphene.Core.Types.Introspection;
using Graphene.Execution;
using Graphene.Test.Data;
using Graphene.Test.Execution;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Graphene.Test.Introspection
{
    public class SchemaIntrospectionEngineTests
    {
        [Test]
        public void QueryType()
        {
            var sut = new ExecutionEngine(true);

            var schema = TestSchemas.CreateIntrospectionSchema();

            var query = @"{IntrospectionQuery {__schema{queryType{name, kind}}}}";
            var document = new DocumentParser().Parse(query); ;

            var expected = @"{""data"":{""__schema"":{""queryType"":{""name"":""user"",""kind"":""OBJECT""}}}}";

            var result = JsonConvert.SerializeObject(sut.Execute(schema, document));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void QueryTypeWithFriends()
        {
            var sut = new ExecutionEngine(true);

            var schema = TestSchemas.CreateIntrospectionSchema();

            var query = @"{IntrospectionQuery {__schema{queryType{name, kind, fields{name}}}}}";
            var document = new DocumentParser().Parse(query); ;

            var expected = @"{""data"":{""__schema"":{""queryType"":{""name"":""user"",""kind"":""OBJECT"",""fields"":[{""name"":""id""},{""name"":""name""},{""name"":""boss""}]}}}}";

            var result = JsonConvert.SerializeObject(sut.Execute(schema, document));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Types()
        {
            var sut = new ExecutionEngine(true);

            var schema = TestSchemas.CreateIntrospectionSchema();

            var query = @"{IntrospectionQuery {__schema{types{name, kind, fields{name}}}}}";
            var document = new DocumentParser().Parse(query); ;

            var expected = @"{""data"":{""__schema"":{""types"":[{""name"":""String"",""kind"":""SCALAR"",""fields"":null},{""name"":""Boolean"",""kind"":""SCALAR"",""fields"":null}]}}}";

            var result = JsonConvert.SerializeObject(sut.Execute(schema, document));
            Assert.AreEqual(expected, result);
        }

        //private static GraphQLSchema CreateIntrospectionSchema()
        //{
        //    return new GraphQLSchema
        //    {
        //        Query = new GraphQLObjectField<object>
        //        {
        //            Name = "IntrospectionQuery",
        //            GraphQLObjectType = () => new GraphQLObjectType
        //            {
        //                Fields = new IGraphQLFieldType[]
        //                {
        //                    new GraphQLObjectField<object, GraphQLSchema>
        //                    {
        //                        Name = "__schema",
        //                        GraphQLObjectType = () => new __Schema(),
        //                        Resolve = _ => TestSchemas.UserSchema()
        //                    }
        //                }
        //            },
        //            Resolve = _ => TestSchemas.UserSchema(),
        //        }
        //    };
        //}
    }
}