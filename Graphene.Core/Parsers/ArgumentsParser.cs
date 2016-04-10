using System;
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
                    argument.Value = GetValue(graphQLLexer.Next().Value);

                    output.Add(argument);
                }

                if (current.Type == GraphQLTokenType.ParenR)
                {
                    break;
                }
            }
            return output.ToArray();
        }

        private object GetValue(string value)
        {
            if (value.StartsWith(@"""") && value.EndsWith(@"""") && value.Length >= 2)
            {
                return value.Substring(1, value.Length - 2);
            }
            else if (value.Contains("."))
            {
                return Convert.ToDecimal(value);
            }
            return Convert.ToInt32(value);
        }
    }
}