using System.Collections.Generic;
using System.Text;

namespace Graphene.Core.Parsers
{
    public class ParserFeed
    {
        private int _index;

        private readonly string _text;

        private string _namingLetters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
        private string _open = "({";
        private string _close = "})";
        private string _seperator = ":,";
        private string _ignore = " ";

        public ParserFeed(string text)
        {
            _text = text;
            _seperator = _seperator + (char)13;
        }

        public IEnumerable<ParsedPart> All()
        {
            while (_index < _text.Length)
            {
                yield return Next();
            }
        }

        public ParsedPart Next()
        {
            while (_index < _text.Length)
            {
                var current = _text[_index].ToString();

                if (_ignore.Contains(current))
                {
                    while (_ignore.Contains(current))
                    {
                        _index++;
                        current = _text[_index].ToString();
                    }
                }
                else if (_namingLetters.Contains(current))
                {
                    return GetName();
                }
                else if (_open.Contains(current))
                {
                    _index++;
                    return new ParsedPart(current, ParseType.Open);
                }
                else if (_close.Contains(current))
                {
                    _index++;
                    return new ParsedPart(current, ParseType.Close);
                }
                else if (_seperator.Contains(current))
                {
                    _index++;
                    return new ParsedPart(current, ParseType.Seperator);
                }
                else
                {
                    _index++;
                }
            }
            return null;
        }

        public bool IsComplete()
        {
            return _index >= _text.Length;
        }

        private ParsedPart GetName()
        {
            var current = _text[_index].ToString();
            var stringBuilder = new StringBuilder();
            while (_namingLetters.Contains(current))
            {
                stringBuilder.Append(current);
                _index++;

                if (_index >= _text.Length)
                {
                    break;
                }

                current = _text[_index].ToString();
            }
            return new ParsedPart(stringBuilder.ToString(), ParseType.Name);
        }
    }
}