using System.Collections.Generic;

namespace Graphene.Core.Lexer
{
    public interface IGraphQLLexerFeed
    {
        IEnumerable<ILexerToken> All();
        ILexerToken Next();
        bool IsComplete();
    }
}