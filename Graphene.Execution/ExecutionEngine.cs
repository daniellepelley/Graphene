using System;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core;
using Graphene.Core.Model;
using Graphene.Core.Parsers;
using Graphene.Core.Types;

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
            var schema = (GraphQLSchema)iGraphQLSchema;
            return InternalExecute(schema, document);
        }

        private object InternalExecute(GraphQLSchema schema, Document document)
        {
            Validate(schema, document);

            new FragmentProcessor().Process(document, true);

            var operation = document.Operations.First();

            try
            {
                var output = _operationExecutionEngine.Execute(operation, schema);
                return new Dictionary<string, object> { {"data", output } };
            }
            catch (GraphQLException ex)
            {
                return new Dictionary<string, object>
                {
                    {
                        "errors", 
                        new [] {
                            new Dictionary<string, object>
                            {
                                { "message", ex.Message }
                            }
                        }
                    }
                };
            }
        }

        private static void Validate(GraphQLSchema schema, Document document)
        {
            if (schema.QueryType == null)
            {
                throw new Exception("QueryType empty");
            }

            if (!document.Operations.Any())
            {
                throw new Exception("No operation");
            }
        }
    }
}
