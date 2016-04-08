namespace Graphene.Core.Lexer
{
    public interface IGraphQLTokenizer
    {
        ILexerToken Handle(GraphQLLexerCursor cursor);
    }
}