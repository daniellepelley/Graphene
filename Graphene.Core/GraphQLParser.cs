using System.Collections.Generic;
using System.Text;

namespace Graphene.Core
{
    public class GraphQLParser : IGraphQLParser
    {
        public object Parse(string query)
        {
            var characterFeed = new CharacterFeed(query);
            var sb = new StringBuilder();

            var write = false;

            while (!characterFeed.IsComplete())
            {
                var current = characterFeed.Next();

                switch (current)
                {
                    case "{":
                        write = true;
                        break;
                    case "}":
                        write = false;
                        break;
                    default:
                        if (write)
                        {
                            sb.Append(current);
                        }
                        break;
                }
            }
            return sb.ToString();
        }
    }
}