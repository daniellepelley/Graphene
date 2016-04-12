using System;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core;
using Graphene.Core.Model;
using Graphene.Core.Types;

namespace Graphene.Execution
{
    public class ExecutionEngine : IExecutionEngine
    {
        private readonly IOperationExecutionEngine _operationExecutionEngine;
        private readonly bool _throwErrors;

        private readonly IIntrospectionSchemaFactory _introspectionSchemaFactory =
            new IntrospectionSchemaFactory(new GraphQLSchema());

        public ExecutionEngine()
        {
            _operationExecutionEngine = new OperationExecutionEngine();
        }

        public ExecutionEngine(bool throwErrors)
            : this(throwErrors, new IntrospectionSchemaFactory(new GraphQLSchema()))
        {
        }

        public ExecutionEngine(bool throwErrors, IIntrospectionSchemaFactory introspectionSchemaFactory)
            :this()
        {
            _introspectionSchemaFactory = introspectionSchemaFactory;
            _throwErrors = throwErrors;
        }

        public object Execute(IGraphQLSchema iGraphQLSchema, Document document)
        {
            var schema = (GraphQLSchema)iGraphQLSchema;

            var introspectionSchema = _introspectionSchemaFactory.Create();

            if (_throwErrors)
            {
                var result = InternalExecute(introspectionSchema, document);

                if (result != null)
                {
                    return result;
                }
            }

            try
            {
                var result = InternalExecute(introspectionSchema, document);

                if (result != null)
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                if (_throwErrors)
                {
                    throw ex;
                }
            }

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

            var operation = document.Operations.First();

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

    public interface IIntrospectionSchemaFactory
    {
        GraphQLSchema Create();
    }

    public class IntrospectionSchemaFactory : IIntrospectionSchemaFactory
    {
        private readonly GraphQLSchema _schema;

        public IntrospectionSchemaFactory(GraphQLSchema schema)
        {
            _schema = schema;
        }

        public GraphQLSchema Create()
        {
            return _schema;
        }
    }
}
