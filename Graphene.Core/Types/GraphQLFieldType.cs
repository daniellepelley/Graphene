using System;

namespace Graphene.Core.Types
{
    public class GraphQLFieldType
    {
        public string Kind { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Func<string> Resolve { get; set; }

        public GraphQLFieldType OfType { get; set; }
    }
}