using System;
using System.Collections.Generic;
using Graphene.Core.FieldTypes;

namespace Graphene.Core.Execution
{
    public abstract class ExecutionNode : IExecutionItem
    {
        public abstract KeyValuePair<string, object> Execute();
    }

    public class ExecutionNode<TInput, TOutput> : ExecutionNode 
    {
        private readonly string _fieldName;
        private readonly Func<ResolveFieldContext<TInput>, TOutput> _func;
        private readonly Func<TInput> _getInput;

        public ExecutionNode(GraphQLScalarField<TInput, TOutput> scalarField, string fieldName, Func<TInput> getInput)
        {
            _fieldName = fieldName;
            _getInput = getInput;
            _func = scalarField.Resolve;
        }

        public override KeyValuePair<string, object> Execute()
        {
            var input = _getInput();
            var context = new ResolveFieldContext<TInput>
            {
                Source = input
            };
            return new KeyValuePair<string, object>(_fieldName, _func(context));
        }
    }
}