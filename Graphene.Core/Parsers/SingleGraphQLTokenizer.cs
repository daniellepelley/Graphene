namespace Graphene.Core.Parsers
{
    public class SingleGraphQLTokenizer : IGraphQLTokenizer
    {
        public string Characters { get; set; }

        public string TokenType {get; set;}

        public ILexerToken Handle(GraphQLLexerCursor cursor)
        {
            var current = cursor.Text[cursor.Index].ToString();

            if (!Characters.Contains(current))
            {
                return null;
            }
            cursor.Index++;
            return new LexerToken(TokenType, current);
        }
    }
}