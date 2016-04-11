using System.Collections.Generic;
using Graphene.Core.Lexer;
using Graphene.Core.Model;

namespace Graphene.Core.Parsers
{
    public class FragmentParser
    {
        public Fragment[] Parse(IGraphQLLexerFeed feed)
        {
            var output = new List<Fragment>();

            while (!feed.IsComplete())
            {
                var current = feed.Next();

                if (current.Type == GraphQLTokenType.Name &&
                    current.Value == "fragment")
                {
                    var currentFragment = new Fragment();
                    output.Add(currentFragment);
                    currentFragment.Name = feed.Next().Value;

                    if (current.Type == GraphQLTokenType.Name &&
                        feed.Next().Value == "on")
                    {
                        currentFragment.Type = feed.Next().Value;
                        currentFragment.Selections = new SelectionsParser().Parse(feed);
                    }
                }
            }

            return output.ToArray();
        }
    }
}