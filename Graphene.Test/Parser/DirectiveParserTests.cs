using Graphene.Core.Lexer;
using Graphene.Core.Parsers;
using NUnit.Framework;

namespace Graphene.Test.Parser
{
    public class DirectiveParserTests
    {
        [Test]
        public void ParseWithoutBrackets()
        {
            var sut = new DirectiveParser();
            var result = sut.Parse(new GraphQLLexerFeed("query customer(id : 1)"));
            Assert.AreEqual("customer", result.Name);
        }
    }
}