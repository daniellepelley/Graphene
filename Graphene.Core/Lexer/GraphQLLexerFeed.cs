using System.Collections.Generic;
using System.Linq;

namespace Graphene.Core.Lexer
{
    public class GraphQLLexerFeed : IGraphQLLexerFeed
    {
        private readonly ILexerToken[] _tokens;

        private int _index;

        public GraphQLLexerFeed(IEnumerable<ILexerToken> tokens)
        {
            _tokens = tokens.ToArray();
        }

        public GraphQLLexerFeed(string query)
        {
            _tokens = new GraphQLLexer(query).All().ToArray();
        }

        public IEnumerable<ILexerToken> All()
        {
            return _tokens;
        }

        public ILexerToken Peek()
        {
            return _tokens[_index];
        }

        public ILexerToken PeekAhead(int number)
        {
            return _tokens[_index + number];
        }

        public ILexerToken Next()
        {
            var current = _tokens[_index];
            _index++;
            return current;
        }

        public bool IsComplete()
        {
            return _index >= _tokens.Length;
        }
    }
}