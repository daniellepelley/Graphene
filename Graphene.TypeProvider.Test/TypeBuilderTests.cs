using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Graphene.TypeProvider.Test
{
    public class TypeBuilderTests
    {
        [Test]
        public void CreatesAType()
        {
            var typeProvider = new SimpleTypeBuilder();

            var graphQlType = typeProvider.Build(typeof (SimpleType));

            Assert.AreEqual("SimpleType", graphQlType.Name);

            Assert.IsNotNull(graphQlType["Name"]);
            Assert.IsNotNull(graphQlType["Amount"]);
            Assert.IsNotNull(graphQlType["AnotherType"]);
            Assert.IsNotNull(graphQlType["AnotherType"]["Name"]);
        }
    }

    public class SimpleType
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public AnotherType AnotherType { get; set; }
    }

    public class AnotherType
    {
        public string Name { get; set; }
    }
}
