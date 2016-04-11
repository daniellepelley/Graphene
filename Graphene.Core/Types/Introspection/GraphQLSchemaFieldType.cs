using System;

namespace Graphene.Core.Types.Introspection
{
    public class GraphQLSchemaFieldType : IGraphQLFieldType
    {
        public string Name { get; set; }
        public string Description { get; set; }
        IGraphQLType IGraphQLFieldType.OfType { get; set; }
        public object ResolveToObject(ResolveFieldContext context)
        {
            return Resolve(context.Schema);
        }

        public Type OfType { get; set; }
        public Func<GraphQLSchema, string> Resolve { get; set; }
        public string Args { get; set; }
    }
}