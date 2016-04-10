using Graphene.Core.Model;
using Graphene.Core.Types;

namespace Graphene.Execution
{
    public interface IOperationExecutionEngine
    {
        object ProcessOperation(Operation operation, GraphQLSchema schema);
    }
}