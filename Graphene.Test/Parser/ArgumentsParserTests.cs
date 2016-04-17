using System.Linq;
using Graphene.Core.Lexer;
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
            var result = sut.GetArguments(new GraphQLLexerFeed(@"(id:""1"")"));
            Assert.AreEqual("id", result.First().Name);
            Assert.AreEqual("1", result.First().Value);
        }

        [Test]
        public void ParseWithoutOpeningBracket()
        {
            var sut = new ArgumentsParser();
            var result = sut.GetArguments(new GraphQLLexerFeed(@"id:""1"")"));
            Assert.AreEqual("id", result.First().Name);
            Assert.AreEqual("1", result.First().Value);
        }

        [Test]
        public void ParseWithoutBracket()
        {
            var sut = new ArgumentsParser();
            var result = sut.GetArguments(new GraphQLLexerFeed(@"id:""1"""));
            Assert.AreEqual("id", result.First().Name);
            Assert.AreEqual("1", result.First().Value);
        }

        [Test]
        public void ParseWithWhiteSpace()
        {
            var sut = new ArgumentsParser();
            var result = sut.GetArguments(new GraphQLLexerFeed(@"id : ""1"""));
            Assert.AreEqual("id", result.First().Name);
            Assert.AreEqual("1", result.First().Value);
        }

        [Test]
        public void Int()
        {
            var sut = new ArgumentsParser();
            var result = sut.GetArguments(new GraphQLLexerFeed("(id:1)"));
            Assert.AreEqual("id", result.First().Name);
            Assert.AreEqual(1, result.First().Value);
        }
    }
}