namespace Graphene.Core
{
    public class CharacterFeed
    {
        private int _index;

        private readonly string _text;

        public CharacterFeed(string text)
        {
            _text = text;
        }

        public bool IsComplete()
        {
            return _index >= _text.Length;
        }

        public string Next()
        {
            var output = _text[_index].ToString();
            _index++;
            return output;
        }

        public string Peek()
        {
            return _text[_index].ToString();
        }

        public void Advance()
        {
            _index++;            
        }
    }

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

    public enum ParseType
    {
        Name,
        Open,
        Close,
        Seperator
    }
}