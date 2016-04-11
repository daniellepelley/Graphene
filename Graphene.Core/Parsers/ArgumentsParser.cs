using System;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core.Lexer;
using Graphene.Core.Model;

namespace Graphene.Core.Parsers
{
    public class ArgumentsParser
    {
        public Argument[] GetArguments(IGraphQLLexerFeed feed)
        {
            var output = new List<Argument>();

            while (!feed.IsComplete())
            {
                var current = feed.Next();

                if (current.Type == GraphQLTokenType.Name)
                {
                    var argument = new Argument();

                    argument.Name = current.Value;
                    feed.Next();
                    argument.Value = GetValue(feed.Next().Value);

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
            
            if (value.Contains("."))
            {
                return Convert.ToDecimal(value);
            }

            if (value.ToCharArray().All(char.IsNumber))
            {
                return Convert.ToInt32(value);
            }

            if (value.ToLower() == "false")
            {
                return false;
            }

            if (value.ToLower() == "true")
            {
                return true;
            }

            return value;
        }
    }
}