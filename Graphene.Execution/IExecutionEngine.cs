using Graphene.Core.Model;
using Graphene.Core.Types;

namespace Graphene.Execution
{
    public interface IExecutionEngine
    {
        string Execute(IGraphQLSchema schema, Document document);
    }
}