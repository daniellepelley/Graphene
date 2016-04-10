using System;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core.Model;
using Graphene.Core.Types;

namespace Graphene.Execution
{
    public class OperationExecutionEngine : IOperationExecutionEngine
    {
        private readonly ObjectExecutionEngine _objectExecutionEngine;

        public OperationExecutionEngine()
        {
            _objectExecutionEngine = new ObjectExecutionEngine();
        }

        public object ProcessOperation(Operation operation, GraphQLSchema schema)
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

            var objectContext = new ResolveFieldContext
            {
                Schema = schema,
                Operation = operation,
                Arguments = argumentsDictionary
            };

            if (schema.Query.Name != operation.Directives.First().Name)
            {
                throw new Exception(string.Format("Object {0} does not exist", operation.Directives.First().Name));
            }

            var query = schema.Query;

            return _objectExecutionEngine.Execute(operation.Selections, query, objectContext);
        }
    }
}