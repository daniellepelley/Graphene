using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphene.Execution
{
    public interface IExecutionItem
    {
        KeyValuePair<string, object> Execute();
    }

    public class ExecutionBranch<TInput, TOutput> : ExecutionBranch
    {
        private Func<TInput, TOutput> _func;
        private Func<TInput> _getInput;
        private TOutput _value;

        public TOutput GetOutput()
        {
            return _value;
        }

        public ExecutionBranch(string fieldName, Func<TInput, TOutput> func, Func<TInput> getInput)
        {
            _getInput = getInput;
            _func = func;
            _fieldName = fieldName;
        }

        public override KeyValuePair<string, object> Execute()
        {
            var input = _getInput();
            _value = _func(input);
            return base.Execute();
        }
    }

    public class ExecutionBranch<TOutput> : ExecutionBranch
    {
        private readonly Func<TOutput> _getter;
        private TOutput _value;

        public TOutput GetOutput()
        {
            return _value;
        }

        public ExecutionBranch(string fieldName, Func<TOutput> getter)
        {
            _fieldName = fieldName;
            _getter = getter;
        }

        public override KeyValuePair<string, object> Execute()
        {
            _value = _getter();
            return base.Execute();
        }
    }



    public abstract class ExecutionBranch : IExecutionItem
    {
        protected string _fieldName;

        private readonly List<IExecutionItem> _nodes = new List<IExecutionItem>();
        
        public void AddNode(IExecutionItem node)
        {
            _nodes.Add(node);
        }

        public virtual KeyValuePair<string, object> Execute()
        {
            var value = _nodes.Select(x => x.Execute())
                .ToDictionary(x => x.Key, x => x.Value); 
           
            return new KeyValuePair<string, object>(_fieldName, value);
        }
    }
}