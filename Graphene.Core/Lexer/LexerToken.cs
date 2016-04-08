namespace Graphene.Core.Lexer
{
    public class LexerToken : ILexerToken
    {
        public string Type { get; private set; }
        public string Value { get; private set; }

        public LexerToken(string type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}