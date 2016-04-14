using System.Collections.Generic;

namespace Graphene.Execution
{
    public interface IExecutionItem
    {
        KeyValuePair<string, object> Execute();
    }
}