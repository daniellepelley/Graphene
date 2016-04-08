using System.Collections.Generic;
using System.Linq;
using Graphene.Core.Lexer;
using Graphene.Core.Model;

namespace Graphene.Core.Parsers
{
    public class SelectionsParser
    {
        public Selection[] Parse(GraphQLLexer parserFeed)
        {
            var output = new List<Selection>();

            while (!parserFeed.IsComplete())
            {
                var current = parserFeed.Next();

                if (current.Type == GraphQLTokenType.Name)
                {
                    output.Add(new Selection
                    {
                        Field = new Field
                        {
                            Name = current.Value
                        }
                    });
                }
                else if (current.Type == GraphQLTokenType.BraceL && output.Any())
                {
                    output.Last().Field.Selections = new SelectionsParser().Parse(parserFeed); 
                }
                else if (current.Type == GraphQLTokenType.BraceR)
                {
                    return output.ToArray();
                }
            }
            return output.ToArray();
        }
    }
}