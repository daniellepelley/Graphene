using Graphene.Core.Parsers;
using Graphene.Core.Types;
using Graphene.Core.Types.Introspection;
using Graphene.Execution;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Graphene.Test.Execution
{
    public class IntrospectionExecutionEngineTests
    {
        [Test]
        public void StringDescription()
        {
            var sut = new ExecutionEngine(true);

            var schema = CreateIntrospectionSchema();

            var query = @"{__Type(name:""String""){description}}";
            var document = new DocumentParser().Parse(query); ;

            var expected = @"{""data"":{""description"":""The `String` scalar type represents textual data, represented as UTF-8 character sequences. The String type is most often used by GraphQL to represent free-form human-readable text.""}}";

            var result = JsonConvert.SerializeObject(sut.Execute(schema, document));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void StringNameAndDescription()
        {
            var sut = new ExecutionEngine(true);

            var schema = CreateIntrospectionSchema();

            var query = @"{__Type(name:""String""){name,description}}";
            var document = new DocumentParser().Parse(query); ;

            var expected = @"{""data"":{""name"":""String"",""description"":""The `String` scalar type represents textual data, represented as UTF-8 character sequences. The String type is most often used by GraphQL to represent free-form human-readable text.""}}";

            var result = JsonConvert.SerializeObject(sut.Execute(schema, document));
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void IntNameAndDescription()
        {
            var sut = new ExecutionEngine(true);

            var schema = CreateIntrospectionSchema();

            var query = @"{__Type(name:""Int""){name,description}}";
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
    }
}