namespace Graphene.Core.Lexer
{
    public class IgnoreLeverToken : ILexerToken
    {
        public string Type { get; private set; }
        public string Value { get; private set; }

        public IgnoreLeverToken()
        {
            Type = "Ignore";
            Value = null;
        }
    }
}