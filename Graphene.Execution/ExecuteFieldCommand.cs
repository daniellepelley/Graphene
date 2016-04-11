using System;
using Graphene.Core;

namespace Graphene.Execution
{
    public class ExecuteFieldCommand : ExecuteCommand
    {
        public ResolveObjectContext ResolveFieldContext { get; set; }
        public Func<ResolveObjectContext, object> Func { get; set; }

        public override object Execute()
        {
            return Func(ResolveFieldContext);
        }
    }
}