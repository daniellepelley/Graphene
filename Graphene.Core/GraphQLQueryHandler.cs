using System;

namespace Graphene.Core
{
    public class GraphQLQueryHandler : IGraphQLQueryHandler
    {
        private readonly IGraphQLParser _graphQLParser;
        private readonly Func<int, string> _func;

        public GraphQLQueryHandler(IGraphQLParser graphQLParser, Func<int, string> func)
        {
            _func = func;
            _graphQLParser = graphQLParser;
        }

        public string Handle(string query)
        {
            _graphQLParser.Parse(query);
            return _func(1);
        }
    }
}