namespace Graphene.Core.Lexer
{
    public class CommentGraphQLTokenizer : IGraphQLTokenizer
    {
        public ILexerToken Handle(GraphQLLexerCursor cursor)
        {
            var current = cursor.GetCurrent();

            var lineReturns = string.Concat((char) 9, (char) 10, (char) 13);

            if (current == "#")
            {
                while (!lineReturns.Contains(current))
                {
                    cursor.Index++;

                    if (!cursor.IsLive())
                    {
                        break;
                    }

                    current = cursor.GetCurrent();
                }
                return new IgnoreLexerToken();
            }
            return null;
        }
    }
}