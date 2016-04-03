using System;

namespace Graphene.Core
{
    public class GraphQLQueryHandler : IGraphQLQueryHandler
    {
        private readonly IGraphQLParser _graphQLParser;

        public GraphQLQueryHandler(IGraphQLParser graphQLParser)
        {
            _graphQLParser = graphQLParser;
        }

        public string Handle(string query)
        {
            return _graphQLParser.Parse(query).ToString();
        }
    }
}