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
            var userType = new GraphQLObject
            {
                Name = "User",
                Fields = new IGraphQLFieldType[]
                {
                    new GraphQLScalar
                    {
                        Name = "id"
                    },
                    new GraphQLScalar
                    {
                        Name = "name"
                    }
                }.ToList()
            };

            var schema = new GraphQLSchema
            {
                Query = new GraphQLObject<object>
                {
                    Name = "Query",
                    Fields = new IGraphQLFieldType[]
                    {
                        new GraphQLScalar
                        {
                            Name = "user",
                        }
                    }.ToList()
                }
            };

            var document =
                new DocumentParser().Parse(
                    @"{__schema{queryType{name},mutationType{name},subscriptionType{name},types{kind,name,description}}}");

            var expected = @"{""data"":{""__schema"":{""types"":[";
        }
    }
}