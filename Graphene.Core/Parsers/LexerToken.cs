namespace Graphene.Core.Parsers
{
    public class LexerToken : ILexerToken
    {
        public string Type { get; set; }
        public string Value { get; set; }

        public LexerToken(string type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}