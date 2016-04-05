using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphene.Core.Parsers
{
    public class FieldsParser
    {
        public Field[] Parse(CharacterFeed characterFeed)
        {
            var output = new List<Field>();

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

                    var field = new Field
                    {
                        Name = name,
                        Selections = new[]
                        {
                            new Selection
                            {
                                Field = new FieldsParser().Parse(characterFeed).First()
                            }
                        }
                    };

                    output.Add(field); 
                }
                    
                else if (current == "}")
                {
                    if (!string.IsNullOrEmpty(name))
                    {
                        output.Add(new Field { Name = name });                                    
                    }
                }
                else if (current == "," ||
                    current == ((char)10).ToString() ||
                    current == ((char)13).ToString())
                {
                    if (!string.IsNullOrEmpty(name))
                    {
                        output.Add(new Field { Name = name });
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

    public class Field
    {
        public string Name { get; set; }
        public Selection[] Selections { get; set; }
    }

    public class Selection
    {
        public Field Field { get; set; }
    }
}