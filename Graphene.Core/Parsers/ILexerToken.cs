namespace Graphene.Core.Parsers
{
    public interface ILexerToken
    {
        string Type { get; set; }
        string Value { get; set; }
    }
}