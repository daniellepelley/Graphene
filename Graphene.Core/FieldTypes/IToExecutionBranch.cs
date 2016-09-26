using Graphene.Core.Execution;
using Graphene.Core.Model;
using Graphene.Core.Types;

namespace Graphene.Core.FieldTypes
{
    public interface IToExecutionBranch
    {
        ExecutionBranch ToExecutionBranch(Field field, ITypeList typeList);
    }
}