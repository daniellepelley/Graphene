using System.Collections.Generic;
using System.Text;

namespace Graphene.Core
{
    public class ArgumentsParser
    {
        public Argument[] GetArguments(CharacterFeed characterFeed)
        {
            var output = new List<Argument>();

            while (!characterFeed.IsComplete())
            {
                if (characterFeed.Peek() == "{")
                {
                    return output.ToArray();
                }

                var argument = GetArgument(characterFeed);
                output.Add(argument);
            }
            return output.ToArray();
        }

        private Argument GetArgument(CharacterFeed characterFeed)
        {
            var argument = new Argument();

            var stringBuilder = new StringBuilder();

            while (!characterFeed.IsComplete())
            {
                var current = characterFeed.Next();

                if ("( ".Contains(current))
                {
                    continue;
                }

                if (current == ":")
                {
                    argument.Name = stringBuilder.ToString();
                    stringBuilder = new StringBuilder();
                }
                else if (current == ")")
                {
                    argument.Value = stringBuilder.ToString();
                    return argument;
                }
                else
                {
                    stringBuilder.Append(current);
                }
            }

            argument.Value = stringBuilder.ToString();
            return argument;
        }
    }
}