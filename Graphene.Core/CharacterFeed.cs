using System.Collections.Generic;
using System.Text;

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

    public class ParserFeed
    {
        private int _index;

        private readonly string _text;

        private string _namingLetters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
        private string _symbols = "({}),";
        private string _ignore = " ";

        public ParserFeed(string text)
        {
            _text = text;
        }

        public IEnumerable<string> Next()
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
                    yield return GetName();
                }
                else if (_symbols.Contains(current))
                {
                    _index++;
                    yield return current;
                }
                else
                {
                    _index++;
                }
            }
        }

        private string GetName()
        {
            var current = _text[_index].ToString();
            var stringBuilder = new StringBuilder();
            while (_namingLetters.Contains(current))
            {
                stringBuilder.Append(current);
                _index++;
                current = _text[_index].ToString();
            }
            return stringBuilder.ToString();
        }
    }
}