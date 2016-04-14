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
        ExecutionRoot ToExecutionBranch(Selection[] selections, Func<TInput> getInput);
    }

    //public interface IOutputField<TOutput> : IGraphQLFieldType
    //{
    //    ExecutionRoot ToExecutionBranch(Selection[] selections, Func<TOutput> getInput);
    //}

    //public interface IGraphQLFieldType<TInput, TOutput> : IGraphQLFieldType<TInput>
    //{

    //}
}