namespace Graphene.Core.Lexer
{
    public interface ILexerToken
    {
        string Type { get; }
        string Value { get; }
    }
}