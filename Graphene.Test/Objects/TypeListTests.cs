using Graphene.Core.Constants;
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
            _typeProvider.AddType(GraphQLTypes.String, new GraphQLString());
            _typeProvider.AddType(GraphQLTypes.Boolean, new GraphQLBoolean());
        }

        [Test]
        public void String()
        {
            var type = _typeProvider.LookUpType(GraphQLTypes.String);
            Assert.AreEqual(GraphQLTypes.String, type.Name);
        }

        [Test]
        public void Boolean()
        {
            var type = _typeProvider.LookUpType(GraphQLTypes.Boolean);
            Assert.AreEqual(GraphQLTypes.Boolean, type.Name);
        }

        [Test]
        public void ThrowsExceptionOnDuplicateTypeNames()
        {
            Assert.Throws<GraphQLException>(() => _typeProvider.AddType(GraphQLTypes.String, new GraphQLString()));
        }
    }
}
