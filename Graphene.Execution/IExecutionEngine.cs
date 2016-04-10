using Graphene.Core.Model;
using Graphene.Core.Types;

namespace Graphene.Execution
{
    public interface IExecutionEngine
    {
        object Execute(IGraphQLSchema schema, Document document);
    }
}