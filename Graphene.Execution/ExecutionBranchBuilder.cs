using Graphene.Core.Execution;
using Graphene.Core.FieldTypes;
using Graphene.Core.Model;
using Graphene.Core.Types;

namespace Graphene.Execution
{
    public class ExecutionBranchBuilder
    {
        public ExecutionBranch Build(IToExecutionBranch graphQLType, Field field, ITypeList typeList)
        {
            var generator = graphQLType.ToExecutionBranch(field, typeList);
            return generator;
        }
    }
}