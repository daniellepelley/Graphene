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

                switch (current)
                {
                    case ":":
                        argument.Name = stringBuilder.ToString();
                        stringBuilder = new StringBuilder();
                        break;
                    case ")":
                        argument.Value = stringBuilder.ToString();
                        return argument;
                    default:
                        stringBuilder.Append(current);
                        break;
                }
            }

            argument.Value = stringBuilder.ToString();

            //if (string.IsNullOrEmpty(argument.Name) ||
            //    string.IsNullOrEmpty(argument.Value))
            //{
            //    throw new Exception();
            //}

            return argument;
        }
    }
}