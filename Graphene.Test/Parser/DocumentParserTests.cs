using System.Linq;
using Graphene.Core.Parsers;
using NUnit.Framework;

namespace Graphene.Test.Parser
{
    public class DocumentParserTests
    {
        [Test]
        public void Parse()
        {
            var sut = new DocumentParser();
            var result = sut.Parse(@"{user(id:1){name}}");
            var operation = result.Operations.First();

            Assert.AreEqual("user", operation.Directives.First().Name);
            Assert.AreEqual("name", operation.Selections.First().Field.Name);
        }

        [Test]
        public void WithoutBrackets()
        {
            var sut = new DocumentParser();
            var result = sut.Parse(@"user(id:1){name}");
            var operation = result.Operations.First();

            Assert.AreEqual("user", operation.Directives.First().Name);
            Assert.AreEqual("name", operation.Selections.First().Field.Name);
        }

        [Test]
        public void Without2Fields()
        {
            var sut = new DocumentParser();
            var result = sut.Parse(@"user(id:1){name,age}");
            var operation = result.Operations.First();

            Assert.AreEqual("user", operation.Directives.First().Name);
            Assert.AreEqual("name", operation.Selections.ElementAt(0).Field.Name);
            Assert.AreEqual("age", operation.Selections.ElementAt(1).Field.Name);
        }
    }
}