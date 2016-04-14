using System;
using Graphene.Core.Execution;

namespace Graphene.Core.Types
{
    public class GraphQLScalar : GraphQLScalar<object, object>
    {

    }

    public abstract class GraphQLScalar<TInput>
    {
        public abstract ExecutionNode ToExecutionNode(Func<TInput> getInput);
    }

    public class GraphQLScalar<TInput, TOutput> : GraphQLScalar<TInput>, IGraphQLFieldType
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

        public Func<ResolveFieldContext<TInput>, TOutput> Resolve;

        public override ExecutionNode ToExecutionNode(Func<TInput> getInput)
        {
            return new ExecutionNode<TInput, TOutput>(this, getInput);
        }
    }
}