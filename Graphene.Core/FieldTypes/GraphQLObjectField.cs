using System;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core.Execution;
using Graphene.Core.Model;

namespace Graphene.Core.Types
{
    public class GraphQLObjectField : GraphQLObjectField<object, object>
    {

    }

    public class GraphQLList<TInput, TOutput> : GraphQLObjectFieldBase, IInputField<TInput>
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
    }

    public class GraphQLObjectField<TInput, TOutput> : GraphQLObjectFieldBase, IGraphQLObject, IInputField<TInput>
    {
        public ExecutionBranch ToExecutionBranch(Selection[] selections, Func<TInput> getter)
        {
            var executionRoot = new ExecutionBranch<TInput, TOutput>(Name, Resolve, getter);

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

        public virtual Func<ResolveObjectContext<TInput>, TOutput> Resolve { get; set; }
    }

    public class GraphQLObjectField<TOutput> : GraphQLObjectFieldBase, IToExecutionBranch
    {
        public virtual Func<ResolveObjectContext, TOutput> Resolve { get; set; }
        public ExecutionBranch ToExecutionBranch(Selection[] selections, IDictionary<string, object> arguments)
        {
            var executionRoot = new ExecutionBranch<TOutput>(Name, arguments, Resolve);

            foreach (var selection in selections)
            {
                var graphQLScalar = this[selection.Field.Name] as GraphQLScalar<TOutput>;

                if (graphQLScalar != null)
                {
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
    }
}