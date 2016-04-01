using System.Collections.Generic;
using System.Linq;
using Graphene.Core;
using NUnit.Framework;

namespace Graphene.Test
{
    public class GraphQLParserTests
    {
        [Test]
        public void Test1()
        {
            var sut = new GraphQLParser();

            var result = sut.Parse(@"{user(id:""1""){name}}");

            Assert.AreEqual(result.Args.First().Value, "1");
        }
    }
}