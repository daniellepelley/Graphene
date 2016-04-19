using Graphene.Core.Execution;
using Graphene.Core.Model;

namespace Graphene.Core.FieldTypes
{
    public interface IToExecutionBranch
    {
        ExecutionBranch ToExecutionBranch(Field field);
    }
}