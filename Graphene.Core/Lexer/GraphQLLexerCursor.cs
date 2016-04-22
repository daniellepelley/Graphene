using System.Text;

namespace Graphene.Core.Lexer
{
    public class GraphQLLexerCursor
    {
        private string _text;
        private int _index;
        private char[] _charArray;

        public GraphQLLexerCursor(string text)
        {
            _text = text;
            _charArray = text.ToCharArray();
        }

        private string GetCurrent()
        {
            return _text[_index].ToString();
        }

        public string Match(string characters)
        {
            if (_text.Length < _index + characters.Length)
            {
                return null;
            }

            var current = _text.Substring(_index, characters.Length);

            if (!characters.Contains(current))
            {
                return null;
            }

            return current;
        }

        public bool IsComplete()
        {
            return _index >= _text.Length;
        }

        public void Advance()
        {
            _index++;
        }

        public void WhileDoesNotContain(string lineReturns)
        {
            var current = GetCurrent();

            while (!lineReturns.Contains(current))
            {
                Advance();

                if (IsComplete())
                {
                    break;
                }

                current = GetCurrent();
            }
        }

        public string TakeWhile(string characters)
        {
            var current = GetCurrent();
            var stringBuilder = new StringBuilder();
            while (characters.Contains(current))
            {
                stringBuilder.Append(current);
                Advance();

                if (IsComplete())
                {
                    break;
                }

                current = GetCurrent();
            }

            return stringBuilder.ToString();
        }
    }
}