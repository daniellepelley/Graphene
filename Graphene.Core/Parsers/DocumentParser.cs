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

            var operation = new OperationParser().Parse(graphQLLexer);
            var fragments = new FragmentParser().Parse(graphQLLexer);

            var document = new Document
            {
                Operations = new[]
                {
                    operation
                },
                Fragments = fragments
            };
            return document;
        }
    }
}