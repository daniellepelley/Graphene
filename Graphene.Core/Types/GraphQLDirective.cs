using System.Collections.Generic;

namespace Graphene.Core.Types
{
    public class GraphQLDirective
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<IGraphQLArgument> Arguments { get; set; }
        public bool OnOperation { get; set; }
        public bool OnFragment { get; set; }
        public bool OnField { get; set; }
    }
}