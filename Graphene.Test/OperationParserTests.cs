using System.Linq;
using Graphene.Core;
using NUnit.Framework;

namespace Graphene.Test
{
    public class OperationParserTests
    {
        [Test]
        public void Parse()
        {
            var sut = new OperationParser();
            var result = sut.Parse(new CharacterFeed(@"{user(id:1){name}}"));
            Assert.AreEqual(null, result.Name);
            Assert.AreEqual("user", result.Directives.First().Name);
            Assert.AreEqual("name", result.Fields.First());
        }

        [Test]
        public void WithoutBrackets()
        {
            var sut = new OperationParser();
            var result = sut.Parse(new CharacterFeed(@"user(id:1){name}"));
            Assert.AreEqual(null, result.Name);
            Assert.AreEqual("user", result.Directives.First().Name);
            Assert.AreEqual("name", result.Fields.First());
        }

        [Test]
        public void Without2Fields()
        {
            var sut = new OperationParser();
            var result = sut.Parse(new CharacterFeed(@"user(id:1){name,age}"));
            Assert.AreEqual(null, result.Name);
            Assert.AreEqual("user", result.Directives.First().Name);
            Assert.AreEqual("name", result.Fields.ElementAt(0));
            Assert.AreEqual("age", result.Fields.ElementAt(1));
        }
    }
}