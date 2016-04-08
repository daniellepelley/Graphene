using System.Diagnostics;
using System.Linq;
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
                Fields = new IGraphQLFieldType[]
                {
                    new GraphQLFieldType<string>
                    {
                        Name = "id"
                    },
                    new GraphQLFieldType<string>
                    {
                        Name = "name"
                    }
                }.ToList()
            };

            var schema = new GraphQLSchema
            {
                Query = new GraphQLObjectType
                {
                    Name = "Query",
                    Fields = new IGraphQLFieldType[]
                    {
                        new GraphQLFieldType<string>
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