using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core.Model;
using Graphene.Core.Types;
using Newtonsoft.Json;
using GraphQLSchema = Graphene.Core.Types.GraphQLSchema;

namespace Graphene.Execution
{
    public class ExecutionEngine : IExecutionEngine
    {
        public string Execute(IGraphQLSchema iGraphQLSchema, Document document)
        {
            GraphQLSchema schema = (GraphQLSchema)iGraphQLSchema;

            if (schema.Query == null)
            {
                throw new Exception("Query empty");    
            }

            var output = new List<object>();

            foreach (var operation in document.Operations)
            {
                var returnValue = ProcessOperation(operation, schema);

                IEnumerable enumerable = null;

                if (returnValue is IEnumerable)
                {
                    enumerable = (IEnumerable)returnValue;
                }
                else
                {
                    enumerable = new[] { returnValue };
                }

                foreach (var item in enumerable)
                {
                    output.Add(item);
                } 
            }
            return JsonConvert.SerializeObject(output);
        }

        private static object ProcessOperation(Operation operation, GraphQLSchema schema)
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

            return ProcessObject(operation.Selections, query, objectContext);
        }

        private static object ProcessObject(Selection[] selections, GraphQLObjectType query, ResolveFieldContext objectContext)
        {
            List<object> output = new List<object>();

            var returnValue = query.Resolve(objectContext);

            IEnumerable enumerable = null;

            if (returnValue is IEnumerable)
            {
                enumerable = (IEnumerable) returnValue;
                foreach (var item in enumerable)
                {
                    var fieldValues = new Dictionary<string, object>();
                    output.Add(fieldValues);

                    foreach (var selection in selections)
                    {
                        var keyPairValue = ProcessField(query, selection, item);
                        fieldValues.Add(keyPairValue.Key, keyPairValue.Value);
                    }
                }
                return output;
            }
            else
            {
                var fieldValues = new Dictionary<string, object>();
                foreach (var selection in selections)
                {
                    var keyPairValue = ProcessField(query, selection, returnValue);
                    fieldValues.Add(keyPairValue.Key, keyPairValue.Value);
                }
                return fieldValues;
            }
        }

        private static KeyValuePair<string, object> ProcessField(GraphQLObjectType schema, Selection selection, object item)
        {
            var schemaField = schema.Fields.FirstOrDefault(x => x.Name == selection.Field.Name);

            if (schemaField == null)
            {
                throw new Exception(string.Format("Field {0} does not exist", selection.Field.Name));
            }

            var context = new ResolveFieldContext
            {
                //Schema = schema,
                FieldName = selection.Field.Name,
                //Operation = operation,
                Source = item
            };

            if (schemaField is GraphQLObjectType)
            {
                var objectContext = new ResolveFieldContext
                {
                    Source = item
                };

                return new KeyValuePair<string, object>(schemaField.Name, ProcessObject(selection.Field.Selections, schemaField as GraphQLObjectType, objectContext));
            }

            return new KeyValuePair<string, object>(schemaField.Name, schemaField.ResolveToObject(context));
        }
    }
}
