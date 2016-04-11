using System.Collections.Generic;
using System.Linq;
using Graphene.Core.Parsers;

namespace Graphene.Core.Lexer
{
    public class GraphQLLexer : IGraphQLLexerFeed
    {
        private readonly GraphQLLexerCursor _cursor = new GraphQLLexerCursor();

        private readonly List<IGraphQLTokenizer> _tokenizers;

        public GraphQLLexer(string text)
        {
            _tokenizers = new GraphQLTokenizerBuilder().Build();

            _tokenizers.Add(new IgnoreGraphQLTokenizer
            {
                Characters = " " + (char)13
            });

            _cursor.Text = text;
        }

        public IEnumerable<ILexerToken> All()
        {
            while (_cursor.Index < _cursor.Text.Length)
            {
                yield return Next();
            }
        }

        public ILexerToken Next()
        {
            while (_cursor.Index < _cursor.Text.Length)
            {
                var next = GetLexerToken();

                if (next != null)
                {
                    if (next is LexerToken)
                    {
                        return next;
                    }
                }
                else
                {
                    _cursor.Index++;
                }
            }
            return null;
        }

        private ILexerToken GetLexerToken()
        {
            return _tokenizers
                .Select(tokenizer => tokenizer.Handle(_cursor))
                .FirstOrDefault(token => token != null);
        }

        public bool IsComplete()
        {
            return _cursor.Index >= _cursor.Text.Length;
        }
    }
}