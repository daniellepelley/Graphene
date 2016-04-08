using Graphene.Core.Lexer;

namespace Graphene.Core.Parsers
{
    public class SingleGraphQLTokenizer : IGraphQLTokenizer
    {
        public string Characters { get; set; }

        public string TokenType {get; set;}

        public ILexerToken Handle(GraphQLLexerCursor cursor)
        {
            if (cursor.Text.Length < cursor.Index + Characters.Length)
            {
                return null;
            }

            var current = cursor.Text.Substring(cursor.Index, Characters.Length);

            if (!Characters.Contains(current))
            {
                return null;
            }
            cursor.Index++;
            return new LexerToken(TokenType, current);
        }
    }
}