using Graphene.Core.Model;

namespace Graphene.Core.Parsers
{
    public class DirectiveParser
    {
        private Directive _directive;
        //private StringBuilder _stringBuilder;
        private ILexerToken _current;

        public Directive Parse(GraphQLLexer parserFeed)
        {
            _directive = new Directive();

            while (!parserFeed.IsComplete())
            {
                _current = parserFeed.Next();

                if (_current.Type == GraphQLTokenType.Name)
                {
                    _directive.Name = _current.Value;
                }
                else if (_current.Type == GraphQLTokenType.ParenL)
                {
                    _directive.Arguments = new ArgumentsParser().GetArguments(parserFeed);
                }
                else if (_current.Type == GraphQLTokenType.BraceL)
                {
                    if (!string.IsNullOrEmpty(_directive.Name))
                    {
                        return _directive;
                    }
                }
            }
            return _directive;
        }
    }
}