using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphene.Core.Parsers
{
    public class GraphQLLexer
    {
        private readonly GraphQLLexerCursor _cursor = new GraphQLLexerCursor();

        private readonly List<IGraphQLTokenizer> tokenizers = new List<IGraphQLTokenizer>();

        public GraphQLLexer(string text)
        {
            tokenizers.Add(new SingleGraphQLTokenizer
            {
                Characters = "({",
                TokenType = GraphQLTokenType.Open
            });

            tokenizers.Add(new SingleGraphQLTokenizer
            {
                Characters = "})",
                TokenType = GraphQLTokenType.Close
            });

            tokenizers.Add(new SingleGraphQLTokenizer
            {
                Characters = ":," + (char)10 + (char)13,
                TokenType = GraphQLTokenType.Seperator
            });

            tokenizers.Add(new MultipleGraphQLTokenizer
            {
                Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_",
                TokenType = GraphQLTokenType.Name
            });

            tokenizers.Add(new IgnoreGraphQLTokenizer
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
            return tokenizers
                .Select(tokenizer => tokenizer.Handle(_cursor))
                .FirstOrDefault(token => token != null);
        }

        public bool IsComplete()
        {
            return _cursor.Index >= _cursor.Text.Length;
        }
    }
}