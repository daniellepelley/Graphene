using Graphene.Core.Execution;
using Graphene.Core.FieldTypes;
using Graphene.Core.Model;

namespace Graphene.Execution
{
    public class ExecutionBranchBuilder
    {
        public ExecutionBranch Build(IToExecutionBranch graphQLType, Field field)
        {
            var generator = graphQLType.ToExecutionBranch(field);
            return generator;
        }
    }
}