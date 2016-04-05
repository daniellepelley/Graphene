namespace Graphene.Core.Parsers
{
    public class ParsedPart
    {
        public string Value { get; set; }
        public ParseType ParseType { get; set; }

        public ParsedPart(string value, ParseType parseType)
        {
            Value = value;
            ParseType = parseType;
        }
    }
}