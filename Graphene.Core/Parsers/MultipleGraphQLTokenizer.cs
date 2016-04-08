using System.Text;
using Graphene.Core.Lexer;

namespace Graphene.Core.Parsers
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
}