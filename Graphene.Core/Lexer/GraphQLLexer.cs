using System.Collections.Generic;
using System.Linq;

namespace Graphene.Core.Lexer
{
    public class GraphQLLexer
    {
        private readonly GraphQLLexerCursor _cursor = new GraphQLLexerCursor();

        private readonly List<IGraphQLTokenizer> _tokenizers;

        public GraphQLLexer(string text)
        {
            _tokenizers = new GraphQLTokenizerBuilder().Build();
            _cursor.Text = text;
        }

        public IEnumerable<ILexerToken> All()
        {
            var output = new List<ILexerToken>();
            while (_cursor.Index < _cursor.Text.Length)
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
                    _cursor.Index++;
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