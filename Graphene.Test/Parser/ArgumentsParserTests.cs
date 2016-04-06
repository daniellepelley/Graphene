using System.Linq;
using Graphene.Core.Parsers;
using NUnit.Framework;

namespace Graphene.Test.Parser
{
    public class ArgumentsParserTests
    {
        [Test]
        public void ParseWithOpeningBracket()
        {
            var sut = new ArgumentsParser();
            var result = sut.GetArguments(new GraphQLLexer("(id:1)"));
            Assert.AreEqual("id", result.First().Name);
            Assert.AreEqual("1", result.First().Value);
        }

        [Test]
        public void ParseWithoutOpeningBracket()
        {
            var sut = new ArgumentsParser();
            var result = sut.GetArguments(new GraphQLLexer("id:1)"));
            Assert.AreEqual("id", result.First().Name);
            Assert.AreEqual("1", result.First().Value);
        }

        [Test]
        public void ParseWithoutBracket()
        {
            var sut = new ArgumentsParser();
            var result = sut.GetArguments(new GraphQLLexer("id:1"));
            Assert.AreEqual("id", result.First().Name);
            Assert.AreEqual("1", result.First().Value);
        }

        [Test]
        public void ParseWithWhiteSpace()
        {
            var sut = new ArgumentsParser();
            var result = sut.GetArguments(new GraphQLLexer("id : 1"));
            Assert.AreEqual("id", result.First().Name);
            Assert.AreEqual("1", result.First().Value);
        }
    }
}