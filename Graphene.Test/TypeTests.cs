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

            var sut = new TypeJsonBuilder();

            var actual = sut.Build("SCALAR", "String");

            Assert.AreEqual(json, actual);
        }

        [Test]
        public void BooleanTypeTest()
        {
            var json = File.ReadAllText("BooleanType.json");

            var sut = new TypeJsonBuilder();

            var actual = sut.Build("SCALAR", "Boolean");

            Assert.AreEqual(json, actual);
        }

        [Test]
        public void ObjectTypeTest()
        {
            var json = File.ReadAllText("ObjectType.json");

            var sut = new TypeJsonBuilder();

            var actual = sut.Build("OBJECT", "__Type");

            Assert.AreEqual(json, actual);
        }
    }
}
