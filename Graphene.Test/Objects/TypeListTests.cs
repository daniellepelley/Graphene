using Graphene.Core.Exceptions;
using Graphene.Core.Types;
using Graphene.Core.Types.Scalar;
using NUnit.Framework;

namespace Graphene.Test.Objects
{
    public class TypeListTests
    {
        private readonly TypeList _typeProvider;

        public TypeListTests()
        {
            _typeProvider = new TypeList();
            _typeProvider.AddType("String", new GraphQLString());
            _typeProvider.AddType("Boolean", new GraphQLBoolean());
        }

        [Test]
        public void String()
        {
            var type = _typeProvider.LookUpType("String");
            Assert.AreEqual("String", type.Name);
        }

        [Test]
        public void Boolean()
        {
            var type = _typeProvider.LookUpType("Boolean");
            Assert.AreEqual("Boolean", type.Name);
        }

        [Test]
        public void ThrowsExceptionOnDuplicateTypeNames()
        {
            Assert.Throws<GraphQLException>(() => _typeProvider.AddType("String", new GraphQLString()));
        }
    }
}
