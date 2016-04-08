namespace Graphene.Core.Lexer
{
    public static class GraphQLTokenType
    {
        public const string Ignore = "Ignore";
        public const string Bang = "Bang";
        public const string Dollar = "Dollar";
        public const string ParenL = "ParenL";
        public const string ParenR = "ParenR";
        public const string Spread = "Spread";
        public const string Colon = "BColonang";
        public const string Eq = "Equals";
        public const string At = "At";
        public const string BracketL = "BracketL";
        public const string BracketR = "BracketR";
        public const string BraceL = "BraceL";
        public const string Pipe = "Pipe";
        public const string BraceR = "BraceR";
        public const string Int = "Int";
        public const string Float = "Float";
        public const string String = "String";
        public const string Name = "Name";
        
        public static string Seperator = "Seperator";
    }
}