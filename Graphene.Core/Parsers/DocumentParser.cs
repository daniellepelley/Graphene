using Graphene.Core.Model;

namespace Graphene.Core.Parsers
{
    public class DocumentParser : IDocumentParser
    {
        public Document Parse(string query)
        {
            var parserFeed = new ParserFeed(query);

            var document = new Document
            {
                Operations = new[] {new OperationParser().Parse(parserFeed)}
            };
            return document;
        }
    }
}