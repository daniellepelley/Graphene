using System;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core;
using Graphene.Core.Execution;
using Graphene.Core.Model;
using Graphene.Core.Types;

namespace Graphene.Execution
{
    public class OperationExecutionEngine : IOperationExecutionEngine
    {
        //private readonly ObjectExecutionEngine _objectExecutionEngine;

        //public OperationExecutionEngine()
        //{
            //_objectExecutionEngine = new ObjectExecutionEngine();
        //}

        public object Execute(Operation operation, GraphQLSchema schema)
        {
            var argumentsDictionary = GetArguments(operation);

            ValidateArguments(schema, argumentsDictionary);

            var directive = operation.Directives.First().Name;

            if (schema.Query.Name != directive)
            {
                throw new GraphQLException(string.Format("Object {0} does not exist", directive));
            }

            Validate(operation.Selections, schema.Query);

            var executionBranch = new ExecutionNodeBuilder().Build(schema.Query, operation.Selections, argumentsDictionary);
            return executionBranch.Execute().Value;
        }

        private void Validate(Selection[] selections, IGraphQLObject fieldType)
        {
            foreach (var selection in selections)
            {
                var field = fieldType[selection.Field.Name];

                if (field == null)
                {
                    throw new GraphQLException(string.Format("Field {0} does not exist", selection.Field.Name));
                }

                if (field is IGraphQLObject)
                {
                    Validate(selection.Field.Selections, (IGraphQLObject)field);
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

        private static void ValidateArguments(GraphQLSchema schema, Dictionary<string, object> argumentsDictionary)
        {
            if (schema.Query.Arguments != null)
            {
                foreach (var argument in schema.Query.Arguments)
                {
                    if (argumentsDictionary.ContainsKey(argument.Name))
                    {
                        var value = argumentsDictionary[argument.Name];

                        if (argument.OfType.Contains("GraphQLString"))
                        {
                            var str = value as string;
                            if (string.IsNullOrEmpty(str))
                            {
                                throw new GraphQLException(
                                    string.Format(@"Argument 'id' has invalid value {0}. Expected type 'String'", value));
                            }
                        }
                        else if (argument.OfType.Contains("GraphQLInt"))
                        {
                            if (!(value is int))
                            {
                                throw new GraphQLException(
                                    string.Format(@"Argument 'id' has invalid value {0}. Expected type 'Int'", value));
                            }
                        }
                    }
                }
            }
        }
    }
}