using System;
using System.Collections.Generic;
using Graphene.Core.Exceptions;
using Graphene.Core.Execution;
using Graphene.Core.Types;
using Graphene.Core.Types.Scalar;

namespace Graphene.Core.FieldTypes
{
    public abstract class GraphQLScalar<TInput> : IGraphQLScalarField
    {
        public abstract ExecutionNode ToExecutionNode(string fieldName, Func<TInput> getInput);
        public string[] Type { get; set; }
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
        public IEnumerable<IGraphQLArgument> Arguments { get; set; }
        public bool IsDeprecated { get; set; }
        public string DeprecationReason { get; set; }

        public Func<ResolveFieldContext<TInput>, TOutput> Resolve;

        public GraphQLScalarField()
        {
            Arguments = new IGraphQLArgument[0];
        }

        public override ExecutionNode ToExecutionNode(string fieldName, Func<TInput> getInput)
        {
            return new ExecutionNode<TInput, TOutput>(this, fieldName, getInput);
        }
    }
}