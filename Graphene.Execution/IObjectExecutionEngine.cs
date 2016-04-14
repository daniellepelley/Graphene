using Graphene.Core;
using Graphene.Core.Model;
using Graphene.Core.Types;

namespace Graphene.Execution
{
    public interface IObjectExecutionEngine
    {
        object Execute(ResolveObjectContext<object> objectContext);
    }
}