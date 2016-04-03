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

                if (" ".Contains(current))
                {
                    continue;
                }

                switch (current)
                {
                    case "(":
                        directive.Name = stringBuilder.ToString();
                        directive.Arguments = new ArgumentsParser().GetArguments(characterFeed);
                        break;
                    case "{":
                        if (string.IsNullOrEmpty(directive.Name))
                            break;
                        return directive;
                    default:
                        stringBuilder.Append(current);
                        break;
                }
            }
            return directive;
        }
    }

    public class OperationParser
    {
        public Operation Parse(CharacterFeed characterFeed)
        {
            var operation = new Operation
            {
                Directives = new[] {new DirectiveParser().Parse(characterFeed)},
                Fields = new FieldsParser().Parse(characterFeed)
            };
            return operation;
        }
    }

    public class Operation
    {
        public string Name { get; set; }

        public Directive[] Directives { get; set; }

        public string[] Fields { get; set; }
    }
}