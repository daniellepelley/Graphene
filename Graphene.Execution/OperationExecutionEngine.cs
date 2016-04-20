using System.Linq;
using Graphene.Core;
using Graphene.Core.Exceptions;
using Graphene.Core.FieldTypes;
using Graphene.Core.Model;
using Graphene.Core.Types;

namespace Graphene.Execution
{
    public class OperationExecutionEngine : IOperationExecutionEngine
    {
        public object Execute(Operation operation, GraphQLSchema schema)
        {
            var errors = new OperationValidator().Validate(schema, operation);

            if (errors.Any())
            {
                return errors;
            }

            if (!operation.Selections.Any())
            {
                return null;
            }

            var executionBranch =
                new ExecutionBranchBuilder().Build(
                    schema.GetMergedRoot().GetField(operation.Selections.First().Field.Name) as IToExecutionBranch,
                    operation.Selections.First().Field);

            return new[] {executionBranch.Execute()}.ToDictionary(x => x.Key, x => x.Value);

        }
    }
}