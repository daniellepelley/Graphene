using Graphene.Core.Lexer;

namespace Graphene.Core.Parsers
{
    public interface IGraphQLTokenizer
    {
        ILexerToken Handle(GraphQLLexerCursor cursor);
    }
}