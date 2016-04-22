using System.Text;

namespace Graphene.Core.Lexer
{
    public class SingleGraphQLTokenizer : IGraphQLTokenizer
    {
        public byte[] Characters { get; set; }

        public string TokenType {get; set;}

        public ILexerToken Handle(GraphQLLexerCursor cursor)
        {
            var current = cursor.MatchByBytes(Characters);

            if (string.IsNullOrEmpty(current))
            {
                return null;
            }

            cursor.Advance(Characters.Length);
            
            return new LexerToken(TokenType, current);
        }
    }
}