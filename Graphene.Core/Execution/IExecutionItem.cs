using System.Collections.Generic;

namespace Graphene.Core.Execution
{
    public interface IExecutionItem
    {
        KeyValuePair<string, object> Execute();
    }
}