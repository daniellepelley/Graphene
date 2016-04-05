using System.Collections.Generic;
using System.Linq;

namespace Graphene.Core.Parsers
{
    public class SelectionsParser
    {
        public Selection[] Parse(ParserFeed parserFeed)
        {
            var output = new List<Selection>();

            while (!parserFeed.IsComplete())
            {
                var current = parserFeed.Next();

                if (current.ParseType == ParseType.Name)
                {
                    output.Add(new Selection
                    {
                        Field = new Field
                        {
                            Name = current.Value
                        }
                    });
                }
                else if (current.ParseType == ParseType.Open && output.Any())
                {
                    output.Last().Field.Selections = new SelectionsParser().Parse(parserFeed); 
                }
                else if (current.ParseType == ParseType.Close)
                {
                    return output.ToArray();
                }
            }
            return output.ToArray();
        }
    }
}