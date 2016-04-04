﻿using System.Collections.Generic;
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

        public CharacterFeed ToCharacterFeed()
        {
            return new CharacterFeed(_text.Substring(_index, _text.Length - _index));
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