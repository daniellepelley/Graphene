using Graphene.Schema;
using NUnit.Framework;

namespace Graphene.Test
{
    public class QueryTest
    {
        [Test]
        public void CanBuildAQuery()
        {
            var schema = new GraphQLSchema
            {
                Query = new GraphQLType
                {
                    Name= "Query",
                    Fields = new []
                    {
                        new GraphQLField
                        {
                            Name = "user",
                            Type = new GraphQLFieldType
                            {
                                Kind = "STRING"
                            }
                        }
                    }
                }
            };
        }
    }
}