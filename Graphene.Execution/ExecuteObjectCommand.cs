using System;
using Graphene.Core;

namespace Graphene.Execution
{
    public class ExecuteObjectCommand : ExecuteCommand
    {
        public ResolveObjectContext ResolveObjectContext { get; set; }
        public Func<ResolveObjectContext, object> Func { get; set; }

        public override object Execute()
        {
            return Func(ResolveObjectContext);
        }
    }
}