using System.Linq;
using System.Text;

namespace Graphene.Core.Lexer
{
    public class MultipleGraphQLTokenizer : IGraphQLTokenizer
    {
        public byte[] Characters { get; set; }

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
        private readonly byte[] _characters;
        private readonly byte[] _lineReturns;

        public CommentGraphQLTokenizer()
        {
            _characters = Encoding.ASCII.GetBytes("#");
            _lineReturns = Encoding.ASCII.GetBytes(string.Concat((char) 9, (char) 10, (char) 13));
        }

        public ILexerToken Handle(GraphQLLexerCursor cursor)
        {
            while (!string.IsNullOrEmpty(cursor.MatchByBytes(_characters)))
            {
                cursor.WhileDoesNotContain(_lineReturns);
                return new IgnoreLexerToken();
            }
            return null;
        }
    }
}