using System.Collections.Generic;
using System.Text;

namespace Graphene.Core
{
    public class FieldsParser
    {
        public string[] Parse(CharacterFeed characterFeed)
        {
            var output = new List<string>();

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
                    case "}":
                        output.Add(stringBuilder.ToString());
                        break;
                    case ",":
                        output.Add(stringBuilder.ToString());
                        stringBuilder = new StringBuilder();
                        break;
                    default:
                        stringBuilder.Append(current);
                        break;
                }
            }

            return output.ToArray();
        }
    }
}