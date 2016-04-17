using Graphene.Core.Lexer;
using Graphene.Core.Model;

namespace Graphene.Core.Parsers
{
    public class DirectiveParser
    {
        public Directive Parse(IGraphQLLexerFeed feed)
        {
            var directive = new Directive();

            if (feed.Peek().Value == "query" || feed.PeekAhead(1).Value == "query")
            {
                while (!feed.IsComplete())
                {
                    var current = feed.Next();

                    if (current.Type == GraphQLTokenType.Name)
                    {
                        directive.Name = current.Value;
                    }
                    else if (current.Type == GraphQLTokenType.ParenL)
                    {
                        directive.Arguments = new ArgumentsParser().GetArguments(feed);
                    }
                    else if (current.Type == GraphQLTokenType.BraceL)
                    {
                        if (!string.IsNullOrEmpty(directive.Name))
                        {
                            return directive;
                        }
                    }
                }
            }
            return directive;
        }
    }
}