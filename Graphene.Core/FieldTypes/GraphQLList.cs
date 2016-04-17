using System;
using System.Collections.Generic;
using Graphene.Core.Execution;
using Graphene.Core.Model;
using Graphene.Core.Types.Introspection;

namespace Graphene.Core.Types
{
    public class GraphQLList<TInput, TOutput> : GraphQLObjectFieldBase, IInputField<TInput>, IHasType
    {
        public ExecutionBranch ToExecutionBranch(Selection[] selections, Func<TInput> getter)
        {
            var executionRoot = new ExecutionBranchList<TInput, TOutput>(Name, Resolve, getter);

            foreach (var selection in selections)
            {
                if (this[selection.Field.Name] is GraphQLScalar<TOutput>)
                {
                    var graphQLScalar = (GraphQLScalar<TOutput>)this[selection.Field.Name];

                    var node = graphQLScalar.ToExecutionNode(executionRoot.GetOutput);
                    executionRoot.AddNode(node);
                }
                else if (this[selection.Field.Name] is IInputField<TOutput>)
                {
                    var graphQLObject = (IInputField<TOutput>)this[selection.Field.Name];

                    var branch = graphQLObject.ToExecutionBranch(selection.Field.Selections, executionRoot.GetOutput);
                    executionRoot.AddNode(branch);
                }
            }

            return executionRoot;
        }

        public virtual Func<ResolveObjectContext<TInput>, IEnumerable<TOutput>> Resolve { get; set; }

        public IGraphQLType Type { get; set; }
    }

    public interface IHasType
    {
        IGraphQLType Type { get; set; }
    }
}