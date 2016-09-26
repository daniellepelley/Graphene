using System;
using Graphene.Core.Execution;
using Graphene.Core.Model;
using Graphene.Core.Types;

namespace Graphene.Core.FieldTypes
{
    public interface IInputField<TInput> : IGraphQLFieldType
    {
        ExecutionBranch ToExecutionBranch(Field field, Func<TInput> getInput, ITypeList typeList);
    }
}