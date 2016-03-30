using System.CodeDom;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;

namespace Graphene.Test
{
    public class TypeSerializationTests
    {
        [Test]
        public void StringTypeTest()
        {
            var json = File.ReadAllText("StringType.json");

            var sut = new GraphQlType
            {
                Kind = "SCALAR",
                Name = "String"
            };
            var actual = Json.Serialize(sut);

            Assert.AreEqual(json, actual);
        }

        [Test]
        public void BooleanTypeTest()
        {
            var json = File.ReadAllText("BooleanType.json");

            var sut = new GraphQlType
            {
                Kind = "SCALAR",
                Name = "Boolean"
            };
            var actual = Json.Serialize(sut);
            
            Assert.AreEqual(json, actual);
        }

        [Test]
        public void InterfaceFieldTest()
        {
            var json = File.ReadAllText("InterfaceFieldExample.json");

            //var sut = new FieldJsonBuilder("hero", null);
            //sut.FieldTypeJsonBuilder = new FieldTypeJsonBuilder(new[]
            //{
            //    new[] {"INTERFACE", "Character"}
            //});

            //{"name":"hero","description":null,"args":[],"type":{"kind":"INTERFACE","name":"Character","ofType":null},"isDeprecated":false,"deprecationReason":null}

            var sut = new GraphQlType
            {
                Kind = "INTERFACE",
                Name = "Character"
            };
            var actual = Json.Serialize(sut);

            Assert.AreEqual(json, actual);
        }
    }
}
