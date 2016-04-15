using System;
using Graphene.Core.Execution;
using Graphene.Core.Model;

namespace Graphene.Core.Types
{
    public interface IInputField<TInput> : IGraphQLFieldType
    {
        ExecutionBranch ToExecutionBranch(Selection[] selections, Func<TInput> getInput);
    }
}