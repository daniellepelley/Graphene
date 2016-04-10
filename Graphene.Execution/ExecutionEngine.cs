using System;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core.Model;
using Graphene.Core.Types;
using GraphQLSchema = Graphene.Core.Types.GraphQLSchema;

namespace Graphene.Execution
{
    public class ExecutionEngine : IExecutionEngine
    {
        private readonly IOperationExecutionEngine _operationExecutionEngine;

        public ExecutionEngine()
        {
            _operationExecutionEngine = new OperationExecutionEngine();
        }

        public object Execute(IGraphQLSchema iGraphQLSchema, Document document)
        {
            var schema = (GraphQLSchema) iGraphQLSchema;

            if (schema.Query == null)
            {
                throw new Exception("Query empty");
            }

            if (!document.Operations.Any())
            {
                throw new Exception("No operation");
            }

            var operation = document.Operations.First();

            return new Dictionary<string, object>
            {
                {"data", _operationExecutionEngine.ProcessOperation(operation, schema)}
            };
        }
    }
}
