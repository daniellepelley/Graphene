using Graphene.Core.Model;

namespace Graphene.Execution
{
    public interface IExecutionEngine
    {
        string Execute(Document document);
    }
}