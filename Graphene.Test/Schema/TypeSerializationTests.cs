using System.IO;
using Graphene.Core.FieldTypes;
using Graphene.Core.Parsers;
using Graphene.Core.Types;
using Graphene.Core.Types.Introspection;
using Graphene.Core.Types.Scalar;
using Graphene.Execution;
using Graphene.Test.Data;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Graphene.Test.Schema
{
    public class TypeSerializationTests
    {
        [Test]
        public void StringTypeTest()
        {
            var sut = new ExecutionEngine();

            var schema = CreateIntrospectionSchema(new GraphQLString());

            var query = @"{__type{kind,name,description,fields,inputFields,interfaces,enumValues,possibleTypes}}";
            var document = new DocumentParser().Parse(query); ;

            var expected =
                @"{""data"":{""__type"":" +
                File.ReadAllText(@"Schema\StringType.json")
                + "}}";

            var result = JsonConvert.SerializeObject(sut.Execute(schema, document));
            
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void BooleanTypeTest()
        {
            var json = File.ReadAllText(@"Schema\BooleanType.json");
           
        }

        [Test]
        public void InterfaceFieldTest()
        {
            var json = File.ReadAllText(@"Schema\InterfaceFieldExample.json");
        }

        [Test]
        public void FieldTypeTest()
        {
            var json = File.ReadAllText(@"Schema\FieldTypeExample.json");
        }

        [Test]
        public void HumanFieldTest()
        {
            var json = File.ReadAllText(@"Schema\HumanFieldExample.json");
        }

        [Test]
        public void QueryTypeTest()
        {
            var json = File.ReadAllText(@"Schema\QueryExample.json");

        }

        private static GraphQLSchema CreateIntrospectionSchema(IGraphQLType type)
        {
            return TestSchemas.CreateIntrospectionSchema(new GraphQLObjectField<IGraphQLType>
            {
                Name = "__type",
                Type = new __Type(TestSchemas.GetTypeList()),
                Resolve = _ => type
            });
        }
    }
}
