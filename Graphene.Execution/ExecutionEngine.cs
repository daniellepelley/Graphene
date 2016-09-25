using System.Collections.Generic;
using System.Linq;
using Graphene.Core.Exceptions;
using Graphene.Core.Model;
using Graphene.Core.Parsers;
using Graphene.Core.Types;

namespace Graphene.Execution
{
    public class ExecutionEngine : IExecutionEngine
    {
        private readonly IOperationExecutionEngine _operationExecutionEngine;
        private readonly ExecutionValidator _executionValidator;

        public ExecutionEngine()
        {
            _operationExecutionEngine = new OperationExecutionEngine();
            _executionValidator = new ExecutionValidator();
        }

        public object Execute(IGraphQLSchema schema, Document document)
        {
            return InternalExecute((GraphQLSchema)schema, document);
        }

        private object InternalExecute(GraphQLSchema schema, Document document)
        {
            var errors = _executionValidator.Validate(schema, document);

            if (errors.Any())
            {
                return errors;
            }

            
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


    }

    public class ExecutionValidator
    {
        public string[] Validate(GraphQLSchema schema, Document document)
        {
            var output = new List<string>();

            if (schema.QueryType == null)
            {
                throw new GraphQLException("QueryType empty");
            }

            if (!document.Operations.Any())
            {
                throw new GraphQLException("No operation");
            }

            return output.ToArray();
        }

    }
}
