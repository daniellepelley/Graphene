using System.Collections.Generic;
using System.Linq;
using Graphene.Core.Lexer;
using Graphene.Core.Model;

namespace Graphene.Core.Parsers
{
    public class SelectionsParser
    {
        public Selection[] Parse(IGraphQLLexerFeed feed)
        {
            var output = new List<Selection>();

            while (!feed.IsComplete())
            {
                var current = feed.Next();

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
                else if (current.Type == GraphQLTokenType.Spread)
                {
                    output.Add(new Selection
                    {
                        Field = new Field
                        {
                            Name = current.Value + feed.Next().Value
                        }
                    });
                }
                else if (current.Type == GraphQLTokenType.BraceL && output.Any())
                {
                    output.Last().Field.Selections = new SelectionsParser().Parse(feed); 
                }
                else if (current.Type == GraphQLTokenType.BraceR)
                {
                    return output.ToArray();
                }
                else if (current.Type == GraphQLTokenType.ParenL)
                {
                    output.Last().Field.Arguments = new ArgumentsParser().GetArguments(feed);
                }
            }
            return output.ToArray();
        }
    }
}