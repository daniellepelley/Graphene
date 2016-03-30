using System.IO;
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
        public void InterfaceFieldTest()
        {
            var json = File.ReadAllText("InterfaceFieldExample.json");

            var sut = new FieldJsonBuilder("hero", null);
            sut.FieldTypeJsonBuilder = new FieldTypeJsonBuilder(new[]
            {
                new[] {"INTERFACE", "Character"}
            });

            var actual = sut.Build();
            
            Assert.AreEqual(json, actual);
        }

        [Test]
        public void FieldTypeTest()
        {
            var json = File.ReadAllText("FieldTypeExample.json");

            var sut = new FieldTypeJsonBuilder(new []
            {
                new [] { "OBJECT", "__Directive" },
                new [] { "LIST", null },
                new [] { "NON_NULL", null }
            });

            var actual = sut.Build();

            Assert.AreEqual(json, actual);
        }

        [Test]
        public void HumanFieldTest()
        {
            var json = File.ReadAllText("HumanFieldExample.json");

            var sut = new FieldJsonBuilder("human", null);
            sut.ArgsJsonBuilders.Add(new ArgsJsonBuilder("id", "idofthehuman"));
            sut.FieldTypeJsonBuilder = new FieldTypeJsonBuilder(new[]
            {
                new[] {"OBJECT", "Human"}
            });

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

            sut.FieldJsonBuilders.AddRange(new []
            {
                new FieldJsonBuilder("hero", null, new FieldTypeJsonBuilder(new[]
                {
                    new[] {"INTERFACE", "Character"}
                }),
                null                
                ), 
                new FieldJsonBuilder("human", null, new FieldTypeJsonBuilder(new[]
                {
                    new[] {"OBJECT", "Human"}
                }),
                new [] { new ArgsJsonBuilder("id", "idofthehuman") }),
                new FieldJsonBuilder("droid", null, new FieldTypeJsonBuilder(new[]
                {
                    new[] {"OBJECT", "Droid"}
                }),
                new [] { new ArgsJsonBuilder("id", "idofthedroid") })
            });

            var actual = sut.Build();

            Assert.AreEqual(json, actual);
        }
    }
}
