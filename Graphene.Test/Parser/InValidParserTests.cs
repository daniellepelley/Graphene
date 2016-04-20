using System.Linq;
using Graphene.Core;
using Graphene.Core.Exceptions;
using Graphene.Core.FieldTypes;
using Graphene.Core.Model;
using Graphene.Core.Parsers;
using NUnit.Framework;

namespace Graphene.Test.Parser
{
    public class InValidParserTests
    {
        [Test]
        public void MissingParen()
        {
            var sut = new DocumentParser();
            Assert.Throws<GraphQLException>(() => sut.Parse(@"{user(id:1{name}}"));
        }

        [Test]
        public void MissingBrace()
        {
            var sut = new DocumentParser();
            Assert.Throws<GraphQLException>(() => sut.Parse(@"{user(id:1){name}"));
        }
    }
}