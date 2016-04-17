using System.Linq;
using Graphene.Core.Lexer;
using Graphene.Core.Parsers;
using NUnit.Framework;

namespace Graphene.Test.Parser
{
    public class OperationParserTests
    {
        [Test]
        public void Parse()
        {
            var sut = new OperationParser();
            var result = sut.Parse(new GraphQLLexerFeed(@"query Operation{user(id:1){name}}"));
            Assert.AreEqual(null, result.Name);
            Assert.AreEqual("Operation", result.Directives.First().Name);
            Assert.AreEqual("user", result.Selections.First().Field.Name);
        }

        [Test]
        public void ParseWithBrackets()
        {
            var sut = new OperationParser();
            var result = sut.Parse(new GraphQLLexerFeed(@"{query Operation{user(id:1){name,age}}}"));
            Assert.AreEqual(null, result.Name);

            Assert.AreEqual("Operation", result.Directives.First().Name);
            Assert.AreEqual("user", result.Selections.First().Field.Name);
            Assert.AreEqual("name", result.Selections.First().Field.Selections.ElementAt(0).Field.Name);
            Assert.AreEqual("age", result.Selections.First().Field.Selections.ElementAt(1).Field.Name);
        }
    }
}