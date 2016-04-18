using Graphene.Core.Execution;
using Graphene.Core.Model;

namespace Graphene.Core.Types
{
    public interface IToExecutionBranch
    {
        ExecutionBranch ToExecutionBranch(Field field);
    }
}