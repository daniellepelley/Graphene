using System.Linq;
using Graphene.Core.Lexer;
using Graphene.Core.Parsers;
using NUnit.Framework;

namespace Graphene.Test.Parser
{
    public class FragmentParserTests
    {
        [Test]
        public void SimpleFragment()
        {
            var sut = new FragmentParser();

            var query = @"fragment InputValue on __InputValue {
                            name
                            description
                            type { ...TypeRef }
                            defaultValue
                          }";

            var fragments = sut.Parse(new GraphQLLexer( query));

            Assert.AreEqual("InputValue", fragments.First().Name);
            Assert.AreEqual("__InputValue", fragments.First().Type);
        }
    }
}