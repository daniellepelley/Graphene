using Graphene.Core.Lexer;
using Graphene.Core.Model;

namespace Graphene.Core.Parsers
{
    public class OperationParser
    {
        public Operation Parse(GraphQLLexer graphQLLexer)
        {
            var operation = new Operation
            {
                Directives = new[] {new DirectiveParser().Parse(graphQLLexer)},
                Selections = new SelectionsParser().Parse(graphQLLexer)
            };
            return operation;
        }
    }
}