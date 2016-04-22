using System.Collections.Generic;
using System.Linq;

namespace Graphene.Core.Lexer
{
    public class GraphQLLexer
    {
        private readonly GraphQLLexerCursor _cursor;

        private readonly List<IGraphQLTokenizer> _tokenizers;

        public GraphQLLexer(string text)
        {
            _tokenizers = new GraphQLTokenizerBuilder().Build();
            _cursor = new GraphQLLexerCursor(text);
        }

        public IEnumerable<ILexerToken> All()
        {
            var output = new List<ILexerToken>();
            while (!_cursor.IsComplete())
            {
                var next = GetLexerToken();

                if (next != null)
                {
                    if (next is LexerToken)
                    {
                        output.Add(next);
                    }
                }
                else
                {
                    _cursor.Advance();
                }
            }
            return output;
        }

        private ILexerToken GetLexerToken()
        {
            return _tokenizers
                .Select(tokenizer => tokenizer.Handle(_cursor))
                .FirstOrDefault(token => token != null);
        }
    }
}