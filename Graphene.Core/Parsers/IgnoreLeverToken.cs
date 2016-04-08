using Graphene.Core.Lexer;

namespace Graphene.Core.Parsers
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