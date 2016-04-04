using System.Linq;
using System.Text;

namespace Graphene.Core
{
    public class DirectiveParser
    {
        private Directive _directive;
        private StringBuilder _stringBuilder;
        private string _current;

        public Directive Parse(CharacterFeed characterFeed)
        {
            _directive = new Directive();

            _stringBuilder = new StringBuilder();

            while (!characterFeed.IsComplete())
            {
                _current = characterFeed.Next();

                if (" ".Contains(_current))
                {
                    continue;
                }

                switch (_current)
                {
                    case "(":
                        _directive.Name = _stringBuilder.ToString();
                        _directive.Arguments = new ArgumentsParser().GetArguments(characterFeed);
                        break;
                    case "{":
                        if (string.IsNullOrEmpty(_directive.Name))
                            break;
                        return _directive;
                    default:
                        _stringBuilder.Append(_current);
                        break;
                }
            }
            return _directive;
        }
    }
}