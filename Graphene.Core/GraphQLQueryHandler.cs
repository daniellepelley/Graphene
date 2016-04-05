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
            var document = _documentParser.Parse(query);

            return document.ToString();
        }
    }
}