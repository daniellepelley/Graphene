using System;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core;
using Graphene.Core.Execution;
using Graphene.Core.FieldTypes;
using Graphene.Core.Model;
using Graphene.Core.Types;
using Graphene.Core.Types.Object;
using Graphene.Core.Types.Scalar;

namespace Graphene.Execution
{
    public class OperationExecutionEngine : IOperationExecutionEngine
    {
        public object Execute(Operation operation, GraphQLSchema schema)
        {
            //var argumentsDictionary = GetArguments(operation);

            ValidateArguments(schema, operation);

            var directive = operation.Directives.First().Name;

            //if (!string.IsNullOrEmpty(directive) &&
            //    schema.QueryType.Name != directive)
            //{
            //    throw new GraphQLException(string.Format("Object {0} does not exist", directive));
            //}

            foreach (var selection in operation.Selections)
            {
                var typeField = schema.QueryType[selection.Field.Name];

                if (typeField == null)
                {
                    throw new GraphQLException("Type {0} not known", selection.Field.Name);
                }

                Validate(operation.Selections.First().Field.Selections, typeField as GraphQLObjectFieldBase);

                if (!(typeField is IToExecutionBranch))
                {
                    throw new GraphQLException("Field type must inherite from IToExecutionBranch");
                }


                var executionBranch = new ExecutionBranchBuilder().Build(typeField as IToExecutionBranch, operation.Selections.First().Field);

                return new[] {executionBranch.Execute()}.ToDictionary(x => x.Key, x => x.Value);
            }

            //var baseType = schema.QueryType.GraphQLObjectType();



            //Validate(operation.Selections.First().Field.Selections, (GraphQLObjectFieldBase)baseType.Fields.First());

            //var executionBranch = new ExecutionBranchBuilder().Build(schema.QueryType as IToExecutionBranch, operation.Selections.First().Field.Selections, argumentsDictionary);
            //return executionBranch.Execute().Name;
            return null;
        }

        private void Validate(Selection[] selections, GraphQLObjectFieldBase fieldType)
        {
            if (!(fieldType is IToExecutionBranch))
            {
                throw new GraphQLException("Field type must inherite from IToExecutionBranch");
            }
            
            foreach (var selection in selections)
            {
                var graphQLObjectType = (GraphQLObjectType) fieldType.Type;

                if (graphQLObjectType.Fields == null)
                {
                    throw new GraphQLException(string.Format("Field {0} does not exist", selection.Field.Name));
                }

                if (selection.Field.Name == null)
                {

                }

                if (!graphQLObjectType.HasField(selection.Field.Name))
                {

                }


                var field = fieldType[selection.Field.Name];

                if (field == null)
                {
                    throw new GraphQLException(string.Format("Field {0} does not exist", selection.Field.Name));
                }

                if (field is IGraphQLObject)
                {
                    Validate(selection.Field.Selections, (GraphQLObjectFieldBase) field);
                }
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
                var typeField = schema.QueryType[selection.Field.Name] as GraphQLObjectFieldBase;

                if (typeField != null)
                {
                    ValidateArguments(selection.Field.Arguments, typeField);
                }
            }
        }
    }
}