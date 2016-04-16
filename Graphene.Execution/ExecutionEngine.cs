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
        private readonly bool _throwErrors;

        public ExecutionEngine()
        {
            _operationExecutionEngine = new OperationExecutionEngine();
        }

        public ExecutionEngine(bool throwErrors)
            :this()
        {
            _throwErrors = throwErrors;
        }

        public object Execute(IGraphQLSchema iGraphQLSchema, Document document)
        {
            var schema = (GraphQLSchema)iGraphQLSchema;
            return InternalExecute(schema, document);
        }

        private object InternalExecute(GraphQLSchema schema, Document document)
        {
            if (schema.Query == null)
            {
                throw new Exception("Query empty");
            }

            if (!document.Operations.Any())
            {
                throw new Exception("No operation");
            }

            new FragmentProcessor().Process(document, true);

            var operation = document.Operations.First();

            if (_throwErrors)
            {
                return new Dictionary<string, object>
                {
                    {"data", _operationExecutionEngine.Execute(operation, schema)}
                };
            }

            try
            {
                return new Dictionary<string, object>
                {
                    {"data", _operationExecutionEngine.Execute(operation, schema)}
                };
            }
            catch (GraphQLException ex)
            {
                if (_throwErrors)
                {
                    throw ex;
                }

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
    }
}
