using System;
using System.Collections.Generic;

namespace Graphene.Core.Execution
{
    public class ExecutionBranch<TInput, TOutput> : ExecutionBranch
    {
        private readonly Func<ResolveObjectContext<TInput>, TOutput> _func;
        private readonly Func<TInput> _getInput;
        private TOutput _value;

        public TOutput GetOutput()
        {
            return _value;
        }

        public ExecutionBranch(string fieldName, Func<ResolveObjectContext<TInput>, TOutput> func, Func<TInput> getInput)
        {
            _getInput = getInput;
            _func = func;
            FieldName = fieldName;
        }

        public override KeyValuePair<string, object> Execute()
        {
            var input = _getInput();
            var context = new ResolveObjectContext<TInput>
            {
                Source = input
            };
            _value = _func(context);

            if (_value == null)
            {
                return new KeyValuePair<string, object>(FieldName, null);
            }

            return base.Execute();
        }
    }
}