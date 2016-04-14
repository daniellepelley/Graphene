using System;
using System.Collections.Generic;
using System.Linq;

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
            
            var list = new List<object>();

            foreach (var value in _values)
            {
                _value = value;
                list.Add(base.Execute().Value);
            }

            return new KeyValuePair<string, object>(FieldName, list);
        }
    }

    public class ExecutionBranch<TOutput> : ExecutionBranch
    {
        private readonly Func<ResolveObjectContext, TOutput> _getter;
        private TOutput _value;
        private readonly IDictionary<string, object> _arguments;

        public TOutput GetOutput()
        {
            return _value;
        }

        public ExecutionBranch(string fieldName, IDictionary<string, object> arguments, Func<ResolveObjectContext, TOutput> getter)
        {
            _arguments = arguments;
            FieldName = fieldName;
            _getter = getter;
        }

        public override KeyValuePair<string, object> Execute()
        {
            var context = new ResolveObjectContext<TOutput>
            {
                Arguments = _arguments
            };
            _value = _getter(context);
            return base.Execute();
        }
    }

    public abstract class ExecutionBranch : IExecutionItem
    {
        protected string FieldName;

        private readonly List<IExecutionItem> _nodes = new List<IExecutionItem>();
        
        public void AddNode(IExecutionItem node)
        {
            _nodes.Add(node);
        }

        public virtual KeyValuePair<string, object> Execute()
        {
            var value = _nodes.Select(x => x.Execute())
                .ToDictionary(x => x.Key, x => x.Value); 
           
            return new KeyValuePair<string, object>(FieldName, value);
        }
    }
}