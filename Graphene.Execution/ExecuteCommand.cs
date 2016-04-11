using System;
using Graphene.Core;

namespace Graphene.Execution
{
    public abstract class ExecuteCommand
    {
        public string Name { get; set; }
        public abstract object Execute();
    }

    public class ExecuteScalarCommand
    {
        public string Name { get; set; }
        public ResolveFieldContext ResolveFieldContext { get; set; }
        public Func<ResolveFieldContext, object> Func { get; set; }

        public object Execute()
        {
            return Func(ResolveFieldContext);
        }
    }
}