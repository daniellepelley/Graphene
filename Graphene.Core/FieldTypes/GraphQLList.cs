using System;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core.Execution;
using Graphene.Core.Model;
using Graphene.Core.Types;

namespace Graphene.Core.FieldTypes
{
    public class GraphQLList<TInput, TOutput> : GraphQLObjectFieldBase, IInputField<TInput>
    {
        public ExecutionBranch ToExecutionBranch(Field field, Func<TInput> getter, ITypeList typeList)
        {
            var executionRoot = new ExecutionBranchList<TInput, TOutput>(Name, Resolve, getter);

            var type = typeList.LookUpType(Type.Last());

            foreach (var selection in field.Selections)
            {
                var graphQLField = type.GetField(selection.Field.Name);

                if (graphQLField is GraphQLScalar<TOutput>)
                {
                    var graphQLScalar = (GraphQLScalar<TOutput>)graphQLField;

                    var node = graphQLScalar.ToExecutionNode(selection.Field.GetFieldOrAliasName(), executionRoot.GetOutput);
                    executionRoot.AddNode(node);
                }
                else if (graphQLField is IInputField<TOutput>)
                {
                    var graphQLObject = (IInputField<TOutput>)graphQLField;

                    var branch = graphQLObject.ToExecutionBranch(selection.Field, executionRoot.GetOutput, typeList);
                    executionRoot.AddNode(branch);
                }
            }

            return executionRoot;
        }

        public virtual Func<ResolveObjectContext<TInput>, IEnumerable<TOutput>> Resolve { get; set; }
    }

    public class GraphQLList<TOutput> : GraphQLObjectFieldBase, IToExecutionBranch
    {
        public ExecutionBranch ToExecutionBranch(Field field, ITypeList typeList)
        {
            var executionRoot = new ExecutionBranchList<TOutput>(Name, Resolve);

            var type = typeList.LookUpType(Type.Last());

            foreach (var selection in field.Selections)
            {
                if (type.GetField(selection.Field.Name) is GraphQLScalar<TOutput>)
                {
                    var graphQLScalar = (GraphQLScalar<TOutput>)type.GetField(selection.Field.Name);

                    var node = graphQLScalar.ToExecutionNode(selection.Field.GetFieldOrAliasName(), executionRoot.GetOutput);
                    executionRoot.AddNode(node);
                }
                else if (type.GetField(selection.Field.Name) is IInputField<TOutput>)
                {
                    var graphQLObject = (IInputField<TOutput>)type.GetField(selection.Field.Name);

                    var branch = graphQLObject.ToExecutionBranch(selection.Field, executionRoot.GetOutput, typeList);
                    executionRoot.AddNode(branch);
                }
            }

            return executionRoot;
        }

        public virtual Func<ResolveObjectContext, IEnumerable<TOutput>> Resolve { get; set; }
    }
}