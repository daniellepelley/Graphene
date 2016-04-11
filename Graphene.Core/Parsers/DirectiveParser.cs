using Graphene.Core.Lexer;
using Graphene.Core.Model;

namespace Graphene.Core.Parsers
{
    public class DirectiveParser
    {
        public Directive Parse(GraphQLLexer parserFeed)
        {
            var directive = new Directive();

            while (!parserFeed.IsComplete())
            {
                var current = parserFeed.Next();

                if (current.Type == GraphQLTokenType.Name)
                {
                    directive.Name = current.Value;
                }
                else if (current.Type == GraphQLTokenType.ParenL)
                {
                    directive.Arguments = new ArgumentsParser().GetArguments(parserFeed);
                }
                else if (current.Type == GraphQLTokenType.BraceL)
                {
                    if (!string.IsNullOrEmpty(directive.Name))
                    {
                        return directive;
                    }
                }
            }
            return directive;
        }
    }
}