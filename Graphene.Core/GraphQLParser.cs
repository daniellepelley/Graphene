using System.Collections.Generic;

namespace Graphene.Core
{
    public class GraphQLParser : IGraphQLParser
    {
        public QueryObject Parse(string query)
        {
            return new QueryObject
            {
                Name = "User",
                Args = new List<QueryFieldArgs>
                {
                    new QueryFieldArgs
                    {
                        Field = "id",
                        Value = "1"
                    }
                }
            };
        }
    }
}