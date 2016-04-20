using System;
using System.Collections.Generic;
using Graphene.Core.Execution;
using Graphene.Core.Model;

namespace Graphene.Core.FieldTypes
{
    public class GraphQLList<TInput, TOutput> : GraphQLObjectFieldBase, IInputField<TInput>
    {
        public ExecutionBranch ToExecutionBranch(Field field, Func<TInput> getter)
        {
            var executionRoot = new ExecutionBranchList<TInput, TOutput>(Name, Resolve, getter);

            foreach (var selection in field.Selections)
            {
                if (this[selection.Field.Name] is GraphQLScalar<TOutput>)
                {
                    var graphQLScalar = (GraphQLScalar<TOutput>)this[selection.Field.Name];

                    var node = graphQLScalar.ToExecutionNode(selection.Field.GetFieldOrAliasName(), executionRoot.GetOutput);
                    executionRoot.AddNode(node);
                }
                else if (this[selection.Field.Name] is IInputField<TOutput>)
                {
                    var graphQLObject = (IInputField<TOutput>)this[selection.Field.Name];

                    var branch = graphQLObject.ToExecutionBranch(selection.Field, executionRoot.GetOutput);
                    executionRoot.AddNode(branch);
                }
            }

            return executionRoot;
        }

        public virtual Func<ResolveObjectContext<TInput>, IEnumerable<TOutput>> Resolve { get; set; }
    }
}