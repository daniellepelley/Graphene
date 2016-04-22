using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphene.Core.Lexer
{
    public class GraphQLLexerCursor
    {
        private readonly string _text;
        private int _index;
        private readonly byte[] _byteArray;

        public GraphQLLexerCursor(string text)
        {
            _text = text;
            _byteArray = Encoding.ASCII.GetBytes(text);
        }

        public string MatchByBytes(byte[] characters)
        {
            if (_byteArray.Length < _index + characters.Length)
            {
                return null;
            }

            return characters.Where((t, i) => t != _byteArray[_index + i]).Any()
                ? null
                : _text.Substring(_index, characters.Length);
        }

        public bool IsComplete()
        {
            return _index >= _byteArray.Length;
        }

        public void Advance()
        {
            _index++;
        }

        public void Advance(int number)
        {
            _index += number;
        }

        public void WhileDoesNotContain(byte[] lineReturns)
        {
            while (!lineReturns.Contains(_byteArray[_index]))
            {
                Advance();

                if (IsComplete())
                {
                    break;
                }
            }
        }

        public string TakeWhile(byte[] characters)
        {
            var start = _index;
            while (characters.Contains(_byteArray[_index]))
            {
                Advance();

                if (IsComplete())
                {
                    break;
                }
            }

            return _text.Substring(start, _index - start);
        }
    }
}