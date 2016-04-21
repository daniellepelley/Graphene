namespace Graphene.Core.Lexer
{
    public class IgnoreLexerToken : ILexerToken
    {
        public string Type { get; private set; }
        public string Value { get; private set; }

        public IgnoreLexerToken()
        {
            Type = "Ignore";
            Value = null;
        }
    }
}