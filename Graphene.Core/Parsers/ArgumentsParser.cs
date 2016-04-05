using System.Collections.Generic;
using Graphene.Core.Model;

namespace Graphene.Core.Parsers
{
    public class ArgumentsParser
    {
        public Argument[] GetArguments(ParserFeed parserFeed)
        {
            var output = new List<Argument>();

            while (!parserFeed.IsComplete())
            {
                var current = parserFeed.Next();

                if (current.ParseType == ParseType.Name)
                {
                    var argument = new Argument();

                    argument.Name = current.Value;
                    parserFeed.Next();
                    argument.Value = parserFeed.Next().Value;

                    output.Add(argument);
                }
               
                if (current.ParseType == ParseType.Close)
                {
                    break;
                }
            }
            return output.ToArray();
        }
    }
}