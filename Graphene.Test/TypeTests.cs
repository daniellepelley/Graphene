using System.IO;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Graphene.Test
{
    public class TypeTests
    {
        [Test]
        public void StringTypeTest()
        {
            var json = File.ReadAllText("StringType.json");

            var sut = new TypeJsonBuilder("SCALAR", "String");

            var actual = sut.Build();

            Assert.AreEqual(json, actual);
        }

        [Test]
        public void BooleanTypeTest()
        {
            var json = File.ReadAllText("BooleanType.json");

            var sut = new TypeJsonBuilder("SCALAR", "Boolean");

            var actual = sut.Build();

            Assert.AreEqual(json, actual);
        }

        [Test]
        public void FieldTest()
        {
            var json = File.ReadAllText("InterfaceFieldExample.json");

            var sut = new TypeJsonBuilder("OBJECT", "Query");

            var actual = sut.Build();

            Assert.AreEqual(json, actual);
        }

        //[Test]
        //public void ObjectTypeTest()
        //{
        //    var json = File.ReadAllText("ObjectType.json");

        //    var sut = new TypeJsonBuilder("OBJECT", "__Type");

        //    var actual = sut.Build();

        //    Assert.AreEqual(json, actual);
        //}

        [Test]
        public void QueryTypeTest()
        {
            var json = File.ReadAllText("QueryExample.json");

            var sut = new TypeJsonBuilder("OBJECT", "Query");

            var actual = sut.Build();

            Assert.AreEqual(json, actual);
        }
    }
}
