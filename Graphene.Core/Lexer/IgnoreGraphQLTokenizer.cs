namespace Graphene.Core.Lexer
{
    public class IgnoreGraphQLTokenizer : IGraphQLTokenizer
    {
        public string Characters { get; set; }

        public ILexerToken Handle(GraphQLLexerCursor cursor)
        {
            var result = cursor.TakeWhile(Characters);

            if (string.IsNullOrEmpty(result))
            {
                return null;
            }

            return new IgnoreLexerToken();
        }
    }
}