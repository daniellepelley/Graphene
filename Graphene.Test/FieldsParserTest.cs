using System.Linq;
using Graphene.Core;
using NUnit.Framework;

namespace Graphene.Test
{
    public class FieldsParserTest
    {
        [Test]
        public void Parse1()
        {
            var sut = new FieldsParser();
            var result = sut.Parse(new CharacterFeed(@"{name}"));
            Assert.AreEqual("name", result.First());
        }

        [Test]
        public void Parse2()
        {
            var sut = new FieldsParser();
            var result = sut.Parse(new CharacterFeed(@"{name,age}"));
            Assert.AreEqual("name", result.ElementAt(0));
            Assert.AreEqual("age", result.ElementAt(1));
        }  
    }
}