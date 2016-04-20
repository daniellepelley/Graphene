using Graphene.Core;
using Graphene.Core.Exceptions;
using Graphene.Core.FieldTypes;
using Graphene.Core.Types.Object;
using NUnit.Framework;

namespace Graphene.Test.Spec
{
    public class SpecRules
    {
        [Test]
        public void FieldNamesOnTypeMustBeUnique()
        {
            var sut = new GraphQLObjectType();

            Assert.DoesNotThrow(() => sut.Fields = new IGraphQLFieldType[0]);

            Assert.DoesNotThrow(() =>
                sut.Fields = new IGraphQLFieldType[]
                {
                    new GraphQLObjectField {Name = "foo"},
                    new GraphQLObjectField {Name = "bar"}
                });

            Assert.Throws<GraphQLException>(() =>
                sut.Fields = new IGraphQLFieldType[]
                    {
                        new GraphQLObjectField {Name = "foo"},
                        new GraphQLObjectField {Name = "foo"}
                    });
        }
    }
}