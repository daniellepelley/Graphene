using System.Collections.Generic;

namespace Graphene.Core.Lexer
{
    public interface IGraphQLLexerFeed
    {
        IEnumerable<ILexerToken> All();
        ILexerToken Next();
        bool IsComplete();
        ILexerToken Peek();
        ILexerToken PeekAhead(int i);
        ILexerToken[] Match(params string[] tokens);
    }
}