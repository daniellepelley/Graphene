namespace Graphene.Core
{
    public class OperationParser
    {
        public Operation Parse(CharacterFeed characterFeed)
        {
            var operation = new Operation
            {
                Directives = new[] {new DirectiveParser().Parse(characterFeed)},
                Selections = new SelectionsParser().Parse(characterFeed)
            };
            return operation;
        }
    }
}