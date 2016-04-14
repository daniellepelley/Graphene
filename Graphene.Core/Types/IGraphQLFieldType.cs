using System;
using Graphene.Core.Execution;
using Graphene.Core.Model;

namespace Graphene.Core.Types
{
    public interface IGraphQLFieldType
    {
        string Name { get; set; }
        string Kind { get; }
        string Description { get; set; }
        string[] OfType { get; set; }
        IGraphQLFieldType this[string name] { get; }
    }

    public interface IInputField<TInput> : IGraphQLFieldType
    {
        ExecutionBranch ToExecutionBranch(Selection[] selections, Func<TInput> getInput);
    }
}