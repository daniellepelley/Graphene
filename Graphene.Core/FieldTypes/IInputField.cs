using System;
using Graphene.Core.Execution;
using Graphene.Core.Model;

namespace Graphene.Core.FieldTypes
{
    public interface IInputField<TInput> : IGraphQLFieldType
    {
        ExecutionBranch ToExecutionBranch(Field field, Func<TInput> getInput);
    }
}