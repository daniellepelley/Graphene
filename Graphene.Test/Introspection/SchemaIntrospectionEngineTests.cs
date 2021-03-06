using Graphene.Core.Parsers;
using Graphene.Execution;
using Graphene.Test.Data;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Graphene.Test.Introspection
{
    public class SchemaIntrospectionEngineTests
    {
        [Test]
        public void QueryType()
        {
            var sut = new ExecutionEngine();

            var schema = TestSchemas.UserSchema();

            var query = @"{query IntrospectionQuery {__schema{queryType{name, kind}}}}";
            var document = new DocumentParser().Parse(query); ;

            var expected = @"{""data"":{""__schema"":{""queryType"":{""name"":""Root"",""kind"":""OBJECT""}}}}";

            var result = JsonConvert.SerializeObject(sut.Execute(schema, document));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void QueryTypeWithFields()
        {
            var sut = new ExecutionEngine();

            var schema = TestSchemas.UserSchema();

            var query = @"{query IntrospectionQuery {__schema{queryType{name, kind, fields{name}}}}}";
            var document = new DocumentParser().Parse(query); ;

            var expected = @"{""data"":{""__schema"":{""queryType"":{""name"":""Root"",""kind"":""OBJECT"",""fields"":[{""name"":""user""}]}}}}";

            var result = JsonConvert.SerializeObject(sut.Execute(schema, document));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void QueryTypeWithFieldsUsingEntityFramework()
        {
            var sut = new ExecutionEngine();

            var schema = TestSchemas.EntityFrameworkSchema();

            var query = @"{query IntrospectionQuery {__schema{types{kind name}}}}";
            var document = new DocumentParser().Parse(query); ;

            var expected = @"{""kind"":""OBJECT"",""name"":""Company""";

            var result = JsonConvert.SerializeObject(sut.Execute(schema, document));
            StringAssert.Contains(expected, result);
        }

        [Test]
        [Ignore("Keeps changing")]
        public void Types()
        {
            var sut = new ExecutionEngine();

            var schema = TestSchemas.UserSchema();

            var query = @"{query IntrospectionQuery {__schema{types{name, kind, fields{name, kind, fields{name}}}}}}";
            var document = new DocumentParser().Parse(query); ;

            var expected = @"{""data"":{""__schema"":{""types"":[
                    {""name"":""String"",""kind"":""SCALAR"",""fields"":null},
                    {""name"":""Boolean"",""kind"":""SCALAR"",""fields"":null},
                    {""name"":null,""kind"":""OBJECT"",""fields"":[                    
                        {""name"":""user"",""kind"":""OBJECT""}]}
                ]}}}".Replace(System.Environment.NewLine, string.Empty).Replace(" ", string.Empty);

            var result = JsonConvert.SerializeObject(sut.Execute(schema, document));
            Assert.AreEqual(expected, result);
        }

        //private static GraphQLSchema CreateIntrospectionSchema()
        //{
        //    return new GraphQLSchema
        //    {
        //        QueryType = new GraphQLObjectField<object>
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