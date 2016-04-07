using System;

namespace Graphene.Core.Types.Introspection
{
    public class GraphQLSchemaFieldType
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Type OfType { get; set; }
        public Func<GraphQLSchema, string> Resolve { get; set; }
        public string Args { get; set; }
    }
}