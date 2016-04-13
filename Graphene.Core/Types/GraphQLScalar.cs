using System;

namespace Graphene.Core.Types
{
    public class GraphQLScalar : IGraphQLFieldType
    {
        public string Kind { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] OfType { get; set; }

        public IGraphQLFieldType this[string name]
        {
            get { throw new GraphQLException("Can't access field on scalar value"); }
        }

        public virtual Func<ResolveFieldContext, object> Resolve { get; set; }
    }
}