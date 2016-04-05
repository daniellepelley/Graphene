using Graphene.Core.Parsers;

namespace Graphene.Core
{
    public class GraphQLQueryHandler : IGraphQLQueryHandler
    {
        private readonly IDocumentParser _documentParser;

        public GraphQLQueryHandler(IDocumentParser documentParser)
        {
            _documentParser = documentParser;
        }

        public string Handle(string query)
        {
            return _documentParser.Parse(query).ToString();
        }
    }
}