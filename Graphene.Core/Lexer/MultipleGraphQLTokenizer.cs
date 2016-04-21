using System.Linq;
using System.Text;

namespace Graphene.Core.Lexer
{
    public class MultipleGraphQLTokenizer : IGraphQLTokenizer
    {
        public string Characters { get; set; }

        public string TokenType { get; set; }

        public ILexerToken Handle(GraphQLLexerCursor cursor)
        {
            var current = cursor.Text[cursor.Index].ToString();
            var stringBuilder = new StringBuilder();
            while (Characters.Contains(current))
            {
                stringBuilder.Append(current);
                cursor.Index++;

                if (cursor.Index >= cursor.Text.Length)
                {
                    break;
                }

                current = cursor.Text[cursor.Index].ToString();
            }

            var output = stringBuilder.ToString();

            if (string.IsNullOrEmpty(output))
            {
                return null;
            }
            return new LexerToken(GraphQLTokenType.Name, stringBuilder.ToString());
        }
    }

    public class CommentGraphQLTokenizer : IGraphQLTokenizer
    {
        public ILexerToken Handle(GraphQLLexerCursor cursor)
        {
            var current = cursor.Text[cursor.Index].ToString();

            var lineReturns = string.Concat((char) 9, (char) 10, (char) 13);

            if (current == "#")
            {
                while (!lineReturns.Contains(current))
                {
                    cursor.Index++;

                    if (cursor.Index >= cursor.Text.Length)
                    {
                        break;
                    }

                    current = cursor.Text[cursor.Index].ToString();
                }
                return new IgnoreLexerToken();
            }
            return null;
        }
    }
}