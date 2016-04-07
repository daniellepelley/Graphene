using System.Diagnostics;
using Graphene.Core.Model;
using Graphene.Core.Parsers;
using Graphene.Core.Types;
using Graphene.Execution;
using NUnit.Framework;

namespace Graphene.Test
{
    public class QueryTest
    {
        [Test]
        public void CanBuildAQuery()
        {
            var userType = new GraphQLObjectType
            {
                Name = "User",
                Fields = new[]
                {
                    new GraphQLFieldType
                    {
                        Name = "id",
                        OfType = new GraphQLFieldType
                        {
                            Kind = "STRING"
                        }
                    },
                    new GraphQLFieldType
                    {
                        Name = "name",
                        OfType = new GraphQLFieldType
                        {
                            Kind = "STRING"
                        }
                    }
                }
            };

            var schema = new GraphQLSchema
            {
                Query = new GraphQLObjectType
                {
                    Name = "Query",
                    Fields = new[]
                    {
                        new GraphQLFieldType
                        {
                            Name = "user",
                            OfType = new GraphQLFieldType
                            {
                                Kind = "STRING"
                            }
                        }
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