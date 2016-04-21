namespace Graphene.Core.Lexer
{
    public class IgnoreGraphQLTokenizer : IGraphQLTokenizer
    {
        public string Characters { get; set; }

        public ILexerToken Handle(GraphQLLexerCursor cursor)
        {
            var current = cursor.Text[cursor.Index].ToString();

            ILexerToken token = null;

            while (Characters.Contains(current))
            {
                token = new IgnoreLexerToken();
                cursor.Index++;

                if (cursor.Index >= cursor.Text.Length)
                {
                    break;
                }

                current = cursor.Text[cursor.Index].ToString();
            }

            return token;
        }
    }
}