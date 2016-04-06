namespace Graphene.Core.Parsers
{
    public class IgnoreLeverToken : ILexerToken
    {
        public string Type { get; set; }
        public string Value { get; set; }

        public IgnoreLeverToken()
        {
            Type = "Ignore";
        }
    }
}