using System;
using System.Collections.Generic;
using Graphene.Core.Execution;
using Graphene.Core.Types.Introspection;

namespace Graphene.Core.Types
{
    public interface IGraphQLScalarField : IHasType
    {
        IGraphQLType Type { get; set; }
    }

    public abstract class GraphQLScalar<TInput> : IGraphQLScalarField
    {
        public abstract ExecutionNode ToExecutionNode(Func<TInput> getInput);
        public IGraphQLType Type { get; set; }
    }

    public class GraphQLScalarField<TInput, TOutput>
        : GraphQLScalar<TInput>, IGraphQLFieldType
    {
        public string Name { get; set; }

        public string Kind
        {
            get { return "SCALAR"; }
        }

        public string Description { get; set; }
        public string[] OfType { get; set; }
        public IEnumerable<IGraphQLArgument> Arguments { get; set; } 

        public IGraphQLFieldType this[string name]
        {
            get { throw new GraphQLException("Can't access field on scalar value"); }
        }

        public Func<ResolveFieldContext<TInput>, TOutput> Resolve;

        public GraphQLScalarField()
        {
            Arguments = new IGraphQLArgument[0];
        }

        public override ExecutionNode ToExecutionNode(Func<TInput> getInput)
        {
            return new ExecutionNode<TInput, TOutput>(this, getInput);
        }
    }
}