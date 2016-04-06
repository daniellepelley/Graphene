using System.Linq;
using Graphene.Core.Parsers;
using NUnit.Framework;

namespace Graphene.Test.Parser
{
    public class GraphQLLexerTest
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

        [TestCase(0, "person")]
        [TestCase(1, "{")]
        [TestCase(2, "name")]
        [TestCase(3, ",")]
        [TestCase(4, "address")]
        [TestCase(5, "{")]
        [TestCase(6, "street")]
        [TestCase(7, ",")]
        [TestCase(8, "town")]
        [TestCase(9, "}")]
        [TestCase(10, "}")]
        public void Test3(int index, string expected)
        {
            var parserFeed = new GraphQLLexer("person {name, address { street, town }}");
            var output = parserFeed.All().ToArray();
            Assert.AreEqual(expected, ((LexerToken)output[index]).Value);
        }
    }
}