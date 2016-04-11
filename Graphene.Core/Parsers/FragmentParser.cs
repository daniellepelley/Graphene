using System.Collections.Generic;
using Graphene.Core.Lexer;
using Graphene.Core.Model;

namespace Graphene.Core.Parsers
{
    public class FragmentParser
    {
        public Fragment[] Parse(GraphQLLexer graphQLLexer)
        {
            var output = new List<Fragment>();

            while (!graphQLLexer.IsComplete())
            {
                var current = graphQLLexer.Next();

                if (current.Type == GraphQLTokenType.Name &&
                    current.Value == "fragment")
                {
                    var currentFragment = new Fragment();
                    output.Add(currentFragment);
                    currentFragment.Name = graphQLLexer.Next().Value;

                    if (current.Type == GraphQLTokenType.Name &&
                        graphQLLexer.Next().Value == "on")
                    {
                        currentFragment.Type = graphQLLexer.Next().Value;
                        currentFragment.Selections = new SelectionsParser().Parse(graphQLLexer);
                    }
                }
            }

            return output.ToArray();
        }
    }
}