using System.Collections.Generic;
using System.Linq;
using Graphene.Core.Exceptions;
using Graphene.Core.FieldTypes;
using Graphene.Core.Lexer;
using Graphene.Core.Model;

namespace Graphene.Core.Parsers
{
    public class DocumentParser : IDocumentParser
    {
        public Document Parse(string query)
        {
            var graphQLLexer = new GraphQLLexer(query);

            var tokens = graphQLLexer.All();

            var feed = new GraphQLLexerFeed(tokens);

            Validate(feed);

            var operation = new OperationParser().Parse(feed);
            var fragments = new FragmentParser().Parse(feed);

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

        private void Validate(GraphQLLexerFeed feed)
        {
            if (feed.All().Count(x => x.Type == GraphQLTokenType.ParenL) !=
                feed.All().Count(x => x.Type == GraphQLTokenType.ParenR))
            {
                throw new GraphQLException("Missing Paren");
            }

            if (feed.All().Count(x => x.Type == GraphQLTokenType.BraceL) !=
                feed.All().Count(x => x.Type == GraphQLTokenType.BraceR))
            {
                throw new GraphQLException("Missing Brace");
            }
        }
    }

    public class DocumentValidator
    {
        public string[] Validate(Document document)
        {
            return new string[0];
        }
    }
}