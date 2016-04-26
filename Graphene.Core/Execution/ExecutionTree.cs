using System;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core.Model;

namespace Graphene.Core.Execution
{
    public class ExecutionBranch<TOutput> : ExecutionBranch
    {
        private readonly Func<ResolveObjectContext, TOutput> _getter;
        private TOutput _value;
        private readonly Argument[] _arguments;

        public TOutput GetOutput()
        {
            return _value;
        }

        public ExecutionBranch(string fieldName, Argument[] arguments, Func<ResolveObjectContext, TOutput> getter)
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

            if (_value == null)
            {
                return new KeyValuePair<string, object>(FieldName, null);
            }

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