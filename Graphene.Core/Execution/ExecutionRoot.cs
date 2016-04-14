using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphene.Core.Execution
{
    public class ExecutionBranch<TInput, TOutput> : ExecutionRoot
    {
        private Func<ResolveObjectContext<TInput>, TOutput> _func;
        private Func<TInput> _getInput;
        private TOutput _value;

        public TOutput GetOutput()
        {
            return _value;
        }

        public ExecutionBranch(string fieldName, Func<ResolveObjectContext<TInput>, TOutput> func, Func<TInput> getInput)
        {
            _getInput = getInput;
            _func = func;
            _fieldName = fieldName;
        }

        public override KeyValuePair<string, object> Execute()
        {
            var input = _getInput();
            var context = new ResolveObjectContext<TInput>();
            context.Source = input;
            context.Arguments = new Dictionary<string, object>
            {
                { "Id", 1 }
            };
            _value = _func(context);
            return base.Execute();
        }
    }

    public class ExecutionRoot<TOutput> : ExecutionRoot
    {
        private readonly Func<ResolveObjectContext, TOutput> _getter;
        private TOutput _value;
        private readonly IDictionary<string, object> _arguments;

        public TOutput GetOutput()
        {
            return _value;
        }

        public ExecutionRoot(string fieldName, IDictionary<string, object> arguments, Func<ResolveObjectContext, TOutput> getter)
        {
            _arguments = arguments;
            _fieldName = fieldName;
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

    public abstract class ExecutionRoot : IExecutionItem
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