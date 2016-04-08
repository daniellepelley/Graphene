using System.Collections.Generic;
using Graphene.Core.Lexer;
using Graphene.Core.Model;

namespace Graphene.Core.Parsers
{
    public class ArgumentsParser
    {
        public Argument[] GetArguments(GraphQLLexer graphQLLexer)
        {
            var output = new List<Argument>();

            while (!graphQLLexer.IsComplete())
            {
                var current = graphQLLexer.Next();

                if (current.Type == GraphQLTokenType.Name)
                {
                    var argument = new Argument();

                    argument.Name = current.Value;
                    graphQLLexer.Next();
                    argument.Value = graphQLLexer.Next().Value;

                    output.Add(argument);
                }

                if (current.Type == GraphQLTokenType.ParenR)
                {
                    break;
                }
            }
            return output.ToArray();
        }
    }
}