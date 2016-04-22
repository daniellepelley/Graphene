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
            var result = cursor.TakeWhile(Characters);

            if (string.IsNullOrEmpty(result))
            {
                return null;
            }

            return new LexerToken(GraphQLTokenType.Name, result);
        }
    }

    public class CommentGraphQLTokenizer : IGraphQLTokenizer
    {
        public ILexerToken Handle(GraphQLLexerCursor cursor)
        {
            while (!string.IsNullOrEmpty(cursor.Match("#")))
            {
                cursor.WhileDoesNotContain(string.Concat((char) 9, (char) 10, (char) 13));
                return new IgnoreLexerToken();
            }
            return null;
        }
    }
}