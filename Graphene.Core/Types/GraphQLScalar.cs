using System;

namespace Graphene.Core.Types
{
    public class GraphQLScalar : GraphQLScalar<ResolveFieldContext, object>
    {

    }

    public class GraphQLScalar<TInput, TOutput> : IGraphQLFieldType
    {
        public string Name { get; set; }

        public string Kind
        {
            get { return "SCALAR"; }
        }

        public string Description { get; set; }
        public string[] OfType { get; set; }

        public IGraphQLFieldType this[string name]
        {
            get { throw new GraphQLException("Can't access field on scalar value"); }
        }

        public Func<TInput, TOutput> Resolve;
    }
}