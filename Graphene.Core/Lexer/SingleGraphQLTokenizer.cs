namespace Graphene.Core.Lexer
{
    public class SingleGraphQLTokenizer : IGraphQLTokenizer
    {
        public string Characters { get; set; }

        public string TokenType {get; set;}

        public ILexerToken Handle(GraphQLLexerCursor cursor)
        {


            var current = cursor.Match(Characters);

            if (string.IsNullOrEmpty(current))
            {
                return null;
            }
            cursor.Advance();
            return new LexerToken(TokenType, current);
        }
    }
}