using Graphene.Core.Lexer;
using Graphene.Core.Model;

namespace Graphene.Core.Parsers
{
    public class OperationParser
    {
        public Operation Parse(IGraphQLLexerFeed feed)
        {
            var operation = new Operation
            {
                Directives = new[]
                {
                    new DirectiveParser().Parse(feed)
                },
                Selections = new SelectionsParser().Parse(feed)
            };
            return operation;
        }
    }
}