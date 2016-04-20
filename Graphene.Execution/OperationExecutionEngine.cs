using System.Collections.Generic;
using System.Linq;
using Graphene.Core;
using Graphene.Core.Exceptions;
using Graphene.Core.FieldTypes;
using Graphene.Core.Model;
using Graphene.Core.Types;
using Graphene.Core.Types.Scalar;

namespace Graphene.Execution
{
    public class OperationExecutionEngine : IOperationExecutionEngine
    {
        public object Execute(Operation operation, GraphQLSchema schema)
        {
            ValidateArguments(schema, operation);

            Validate(operation.Selections, schema.GetMergedRoot());

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

        private void Validate(Selection[] selections, IGraphQLType type)
        {
            foreach (var selection in selections)
            {
                var field = type.GetField(selection.Field.Name);

                if (field == null)
                {
                    throw new GraphQLException("Field {0} not found on {1} {2}", selection.Field.Name, type.Name, type.Kind);
                }

                Validate(selection.Field.Selections, field.Type);
            }
        }


        private static Dictionary<string, object> GetArguments(Operation operation)
        {
            var argumentsDictionary = new Dictionary<string, object>();

            if (operation.Directives.Any())
            {
                var directive = operation.Directives.First();

                if (directive.Arguments != null && directive.Arguments.Any())
                {
                    foreach (var argument in directive.Arguments)
                    {
                        argumentsDictionary.Add(argument.Name, argument.Value);
                    }
                }
            }
            return argumentsDictionary;
        }


        private static void ValidateArguments(Argument[] arguments, GraphQLObjectFieldBase objectField)
        {
            if (objectField.Arguments == null)
            {
                return;
            }

            foreach (var argument in arguments)
            {
                var arg = objectField.Arguments.FirstOrDefault(x => x.Name == argument.Name);

                if (arg == null)
                {
                    throw new GraphQLException(
                        string.Format(@"Argument '{0}' does not exist", argument.Name));
                }

                if (arg.Type is GraphQLString)
                {
                    var str = argument.Value as string;
                    if (string.IsNullOrEmpty(str))
                    {
                        throw new GraphQLException(
                            string.Format(@"Argument '{0}' has invalid value {1}. Expected type 'String'", argument.Name,
                                argument.Value));
                    }
                }
                else if (arg.Type is GraphQLInt)
                {
                    if (!(argument.Value is int))
                    {
                        throw new GraphQLException(
                            string.Format(@"Argument '{0}' has invalid value {1}. Expected type 'Int'", argument.Name,
                                argument.Value));
                    }
                }
            }
        }

        private static void ValidateArguments(GraphQLSchema schema, Operation operation)
        {
            foreach (var selection in operation.Selections)
            {
                var typeField = schema.QueryType.GetField(selection.Field.Name) as GraphQLObjectFieldBase;

                if (typeField != null)
                {
                    ValidateArguments(selection.Field.Arguments, typeField);
                }
            }
        }
    }
}