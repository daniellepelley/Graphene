using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Graphene.Core
{
    public interface IGraphQLParser
    {
        object Parse(string query);
    }

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
    }

    public class GraphQLParser : IGraphQLParser
    {
        public object Parse(string query)
        {
            var characterFeed = new CharacterFeed(query);
            var sb = new StringBuilder();

            var write = false;

            while (!characterFeed.IsComplete())
            {
                var current = characterFeed.Next();

                switch (current)
                {
                    case "{":
                        write = true;
                        break;
                    case "}":
                        write = false;
                        break;
                    default:
                        if (write)
                        {
                            sb.Append(current);
                        }
                        break;
                }
            }
            return sb.ToString();
        }
    }


    public class Directive
    {
        public string Name { get; set; }

        public Argument[] Arguments { get; set; }
    }
}