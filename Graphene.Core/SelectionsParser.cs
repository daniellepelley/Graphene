using System.Collections.Generic;
using System.Text;

namespace Graphene.Core
{
    public class SelectionsParser
    {
        public Selection[] Parse(CharacterFeed characterFeed)
        {
            var output = new List<Selection>();

            var stringBuilder = new StringBuilder();

            while (!characterFeed.IsComplete())
            {
                var current = characterFeed.Next();

                if (" ".Contains(current))
                {
                    continue;
                }

                var name = stringBuilder.ToString();

                if (current == "{")
                {
                    if (string.IsNullOrEmpty(name))
                    {
                        continue;
                    }

                    var selection = new Selection
                    {
                        Field = new Field
                        {
                            Name = name,
                            Selections = Parse(characterFeed)
                        }
                    };

                    output.Add(selection);
                }

                else if (current == "}")
                {
                    if (!string.IsNullOrEmpty(name))
                    {
                        output.Add(new Selection{ Field = new Field { Name = name }});
                        return output.ToArray();
                    }
                }
                else if (current == "," ||
                         current == ((char)10).ToString() ||
                         current == ((char)13).ToString())
                {
                    if (!string.IsNullOrEmpty(name))
                    {
                        output.Add(new Selection { Field = new Field { Name = name } });
                    }
                    stringBuilder = new StringBuilder();
                }
                else
                {
                    stringBuilder.Append(current);
                }
            }

            return output.ToArray();
        }
    }
}