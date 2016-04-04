namespace Graphene.Core
{
    public class OperationParser
    {
        public Operation Parse(ParserFeed parserFeed)
        {
            var operation = new Operation
            {
                Directives = new[] {new DirectiveParser().Parse(parserFeed)},
                Selections = new SelectionsParser().Parse(parserFeed)
            };
            return operation;
        }
    }
}