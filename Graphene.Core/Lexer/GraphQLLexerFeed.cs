using System.Collections.Generic;
using System.Linq;

namespace Graphene.Core.Lexer
{
    public class GraphQLLexerFeed : IGraphQLLexerFeed
    {
        private ILexerToken[] _tokens;

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
            if (!_tokens.Any())
            {
                return new LexerToken(null, null);
            }

            return _tokens.First();
        }

        public ILexerToken PeekAhead(int number)
        {
            if (number >= _tokens.Length)
            {
                return new LexerToken(null, null);
            }

            return _tokens[number];
        }

        public ILexerToken Next()
        {
            var current = _tokens.First();
            _tokens = _tokens.Skip(1).ToArray();
            return current;
        }

        public bool IsComplete()
        {
            return !_tokens.Any();
        }

        public ILexerToken[] Match(params string[] tokens)
        {
            if (tokens.Length > _tokens.Length)
            {
                return new ILexerToken[0];
            }

            if (tokens.Where((t, i) => _tokens[i].Type != t).Any())
            {
                return new ILexerToken[0];
            }
            
            var output = _tokens.Take(tokens.Length).ToArray();

            _tokens = _tokens.Skip(tokens.Length).ToArray();

            return output;
        }
    }
}