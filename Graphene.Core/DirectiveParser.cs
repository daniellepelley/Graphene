using System.Text;

namespace Graphene.Core
{
    public class DirectiveParser
    {
        public Directive Parse(CharacterFeed characterFeed)
        {
            var directive = new Directive();

            var stringBuilder = new StringBuilder();

            while (!characterFeed.IsComplete())
            {
                var current = characterFeed.Next();

                if ("{ ".Contains(current))
                {
                    continue;
                }

                switch (current)
                {
                    case "(":
                        directive.Name = stringBuilder.ToString();
                        directive.Arguments = new ArgumentsParser().GetArguments(characterFeed);
                        break;
                    default:
                        stringBuilder.Append(current);
                        break;
                }
            }
            return directive;
        }
    }
}