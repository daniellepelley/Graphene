using System;
using System.Linq;
using Graphene.Core.Parsers;
using Graphene.Core.Types;
using Graphene.Core.Types.Introspection;
using Graphene.Execution;
using Graphene.Test.Execution;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Graphene.Test.Introspection
{
    public class SchemaIntrospectionExecutionEngineTests
    {
        [Test]
        public void QueryType()
        {
            var sut = new ExecutionEngine(true, new IntrospectionSchemaFactory(CreateIntrospectionSchema()));

            var schema = CreateIntrospectionSchema();

            var query = @"{__schema{queryType{name, kind}}}";
            var document = new DocumentParser().Parse(query); ;

            var expected = @"{""data"":{""queryType"":{""name"":""user"",""kind"":""OBJECT""}}}";

            var result = JsonConvert.SerializeObject(sut.Execute(schema, document));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void QueryTypeWithFriends()
        {
            //Need an object list and a scalar list
            //Possible to have a field list wrapper

            var sut = new ExecutionEngine(true);

            var schema = CreateIntrospectionSchema();

            var query = @"{__schema{queryType{name, kind, fields{name}}}}";
            var document = new DocumentParser().Parse(query); ;

            var expected = @"{""data"":{""queryType"":{""name"":""user"",""kind"":""OBJECT"",""fields"":[{""name"":""Id""},{""name"":""Name""},{""name"":null}]}}}";

            var result = JsonConvert.SerializeObject(sut.Execute(schema, document));
            Assert.AreEqual(expected, result);
        }

        private static GraphQLSchema CreateIntrospectionSchema()
        {
            var newSchema = new GraphQLSchema
            {
                Query = new __Schema(TestSchemas.UserSchema())
            };
            return newSchema;
        }
    }

    public class AsIntrospectiveTests
    {
        [Test]
        public void Test1()
        {
            var introspective = TestSchemas.UserSchema().Query.AsIntrospective();

            Assert.IsNotNull(introspective["name"]);
            Assert.IsNotNull(introspective["kind"]);
            Assert.IsNotNull(introspective["description"]);
            Assert.IsNotNull(introspective["fields"]);
        }
    }
}