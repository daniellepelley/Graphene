using System.Linq;
using Graphene.Core.Lexer;
using NUnit.Framework;

namespace Graphene.Test.Lexer
{
    public class GraphQLLexerTests
    {
        [TestCase(0, "name")]
        [TestCase(1, "{")]
        [TestCase(2, "}")]
        public void Test1(int index, string expected)
        {
            var parserFeed = new GraphQLLexer("name{}");
            var output = parserFeed.All().ToArray();
            Assert.AreEqual(expected, ((LexerToken)output[index]).Value);
        }

        [TestCase(0, "person")]
        [TestCase(1, "{")]
        [TestCase(2, "name")]
        [TestCase(3, "}")]
        public void Test2(int index, string expected)
        {
            var parserFeed = new GraphQLLexer("person {name}");
            var output = parserFeed.All().ToArray();
            Assert.AreEqual(expected, ((LexerToken)output[index]).Value);
        }

        [TestCase(0, "person", GraphQLTokenType.Name)]
        [TestCase(1, "(", GraphQLTokenType.ParenL)]
        [TestCase(2, "id", GraphQLTokenType.Name)]
        [TestCase(3, ":", GraphQLTokenType.Colon)]
        [TestCase(4, "1", GraphQLTokenType.Name)]
        [TestCase(5, ")", GraphQLTokenType.ParenR)]
        [TestCase(6, "{", GraphQLTokenType.BraceL)]
        [TestCase(7, "name", GraphQLTokenType.Name)]
        [TestCase(8, "address", GraphQLTokenType.Name)]
        [TestCase(9, "{", GraphQLTokenType.BraceL)]
        [TestCase(10, "street", GraphQLTokenType.Name)]
        [TestCase(11, "town", GraphQLTokenType.Name)]
        [TestCase(12, "}", GraphQLTokenType.BraceR)]
        [TestCase(13, "}", GraphQLTokenType.BraceR)]
        public void Test3(int index, string expectedValue, string expectedType)
        {
            var parserFeed = new GraphQLLexer("person(id :  1)   {name, address { street, town }}");
            var output = parserFeed.All().ToArray();
            Assert.AreEqual(expectedValue, ((LexerToken)output[index]).Value);
            Assert.AreEqual(expectedType, ((LexerToken)output[index]).Type);
        }

        [TestCase(0, "should", GraphQLTokenType.Name)]
        [TestCase(1, "pick", GraphQLTokenType.Name)]
        [TestCase(2, "up", GraphQLTokenType.Name)]
        [TestCase(3, "...", GraphQLTokenType.Spread)]
        [TestCase(4, "{", GraphQLTokenType.BraceL)]
        [TestCase(5, "spreads", GraphQLTokenType.Name)]
        public void Test4(int index, string expectedValue, string expectedType)
        {
            var parserFeed = new GraphQLLexer("should pick up ...{ spreads");
            var output = parserFeed.All().ToArray();
            Assert.AreEqual(expectedValue, ((LexerToken)output[index]).Value);
            Assert.AreEqual(expectedType, ((LexerToken)output[index]).Type);
        }
    }

    public class LexicalTreeTests
    {

        public void TreeBuilderTest()
        {
            
        }
    }
}