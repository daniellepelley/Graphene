using System.Linq;
using Graphene.Core.Parsers;
using Graphene.Core.Types;
using NUnit.Framework;

namespace Graphene.Test
{
    public class QueryTest
    {
        [Test]
        public void CanBuildAQuery()
        {
            var userType = new GraphQLObjectField
            {
                Name = "User",
                GraphQLObjectType = new GraphQLObjectType
                {
                    Fields = new IGraphQLFieldType[]
                    {
                        new GraphQLScalarField<object, object>
                        {
                            Name = "id"
                        },
                        new GraphQLScalarField<object, object>
                        {
                            Name = "name"
                        }
                    }
                }
            };

            var schema = new GraphQLSchema
            {
                Query = new GraphQLObjectField<object>
                {
                    Name = "Query",
                    GraphQLObjectType = new GraphQLObjectType
                    {
                        Fields = new IGraphQLFieldType[]
                        {
                            new GraphQLScalarField<object, object>
                            {
                                Name = "user",
                            }
                        }.ToList()
                    }
                }
            };

            var document =
                new DocumentParser().Parse(
                    @"{__schema{queryType{name},mutationType{name},subscriptionType{name},types{kind,name,description}}}");

            var expected = @"{""data"":{""__schema"":{""types"":[";
        }
    }
}