using System.Linq;
using Graphene.Core.Lexer;
using Graphene.Core.Model;

namespace Graphene.Core.Parsers
{
    public class DocumentParser : IDocumentParser
    {
        public Document Parse(string query)
        {
            var graphQLLexer = new GraphQLLexer(query);

            var document = new Document
            {
                Operations = new[]
                {
                    new OperationParser().Parse(graphQLLexer)
                },
                Fragments = new FragmentParser().Parse(graphQLLexer)
            };
            return document;
        }
    }
}