using System.Collections.Generic;
using System.Linq;

namespace Graphene.Core.Lexer
{
    public class GraphQLLexerFeed : IGraphQLLexerFeed
    {
        private readonly ILexerToken[] _tokens;

        private int index;

        public GraphQLLexerFeed(IEnumerable<ILexerToken> tokens)
        {
            _tokens = tokens.ToArray();
        }

        public IEnumerable<ILexerToken> All()
        {
            return _tokens;
        }

        public ILexerToken Next()
        {
            var current = _tokens[index];
            index++;
            return current;
        }

        public bool IsComplete()
        {
            return index >= _tokens.Length;
        }
    }
}