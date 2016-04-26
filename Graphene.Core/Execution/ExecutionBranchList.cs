using System;
using System.Collections.Generic;

namespace Graphene.Core.Execution
{
    public class ExecutionBranchList<TInput, TOutput> : ExecutionBranch
    {
        private IEnumerable<TOutput> _values;
        private TOutput _value;
        private readonly Func<TInput> _getInput;
        private readonly Func<ResolveObjectContext<TInput>, IEnumerable<TOutput>> _func;

        public TOutput GetOutput()
        {
            return _value;
        }

        public ExecutionBranchList(string fieldName, Func<ResolveObjectContext<TInput>, IEnumerable<TOutput>> func, Func<TInput> getInput)
        {
            FieldName = fieldName;
            _func = func;
            _getInput = getInput;
        }

        public override KeyValuePair<string, object> Execute()
        {
            var parent = _getInput();
            var context = new ResolveObjectContext<TInput>
            {
                Source = parent
            };

            _values = _func(context);
            
            if (_values == null)
            {
                return new KeyValuePair<string, object>(FieldName, null);
            }

            var list = new List<object>();

            foreach (var value in _values)
            {
                _value = value;
                list.Add(base.Execute().Value);
            }

            return new KeyValuePair<string, object>(FieldName, list);
        }
    }

    public class ExecutionBranchList<TOutput> : ExecutionBranch
    {
        private IEnumerable<TOutput> _values;
        private TOutput _value;
        private readonly Func<ResolveObjectContext, IEnumerable<TOutput>> _func;

        public TOutput GetOutput()
        {
            return _value;
        }

        public ExecutionBranchList(string fieldName, Func<ResolveObjectContext, IEnumerable<TOutput>> func)
        {
            FieldName = fieldName;
            _func = func;
        }

        public override KeyValuePair<string, object> Execute()
        {
            var context = new ResolveObjectContext
            {
               
            };

            _values = _func(context);

            if (_values == null)
            {
                return new KeyValuePair<string, object>(FieldName, null);
            }

            var list = new List<object>();

            foreach (var value in _values)
            {
                _value = value;
                list.Add(base.Execute().Value);
            }

            return new KeyValuePair<string, object>(FieldName, list);
        }
    }

}