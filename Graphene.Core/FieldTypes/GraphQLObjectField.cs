using System;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core.Execution;
using Graphene.Core.Model;
using Graphene.Core.Types.Introspection;

namespace Graphene.Core.Types
{
    public class GraphQLObjectField : GraphQLObjectField<object>, IGraphQLFieldType
    {

    }

    public class GraphQLObjectField<TInput, TOutput> : GraphQLObjectFieldBase, IInputField<TInput>
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
        public IGraphQLType Type { get; set; }
    }

    public class GraphQLObjectField<TOutput> : GraphQLObjectFieldBase, IToExecutionBranch, IGraphQLFieldType
    {
        public virtual Func<ResolveObjectContext, TOutput> Resolve { get; set; }
        public ExecutionBranch ToExecutionBranch(Field field)
        {
            var executionRoot = new ExecutionBranch<TOutput>(Name, field.Arguments, Resolve);

            foreach (var selection in field.Selections)
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

                    if (selection.Field.Selections == null)
                    {
                        throw new GraphQLException("Selections cannot be null. This relates to not having field on an object.");
                    }

                    var branch = graphQLObject.ToExecutionBranch(selection.Field.Selections, executionRoot.GetOutput);
                    executionRoot.AddNode(branch);
                }
            }

            return executionRoot;
        }

        public IGraphQLType Type { get; set; }
    }
}