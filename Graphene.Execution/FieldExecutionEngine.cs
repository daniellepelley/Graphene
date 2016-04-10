using System;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core.Model;
using Graphene.Core.Types;

namespace Graphene.Execution
{
    public class FieldExecutionEngine
    {
        private readonly IObjectExecutionEngine _objectExecutionEngine;

        public FieldExecutionEngine(IObjectExecutionEngine objectExecutionEngine)
        {
            _objectExecutionEngine = objectExecutionEngine;
        }

        public KeyValuePair<string, object> ProcessField(GraphQLObjectType graphQLObject, Selection selection, object item)
        {
            var schemaField = graphQLObject.Fields.FirstOrDefault(x => x.Name == selection.Field.Name);

            if (schemaField == null)
            {
                throw new Exception(string.Format("Field {0} does not exist", selection.Field.Name));
            }

            var context = new ResolveFieldContext
            {
                //Schema = graphQLObject,
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

                return new KeyValuePair<string, object>(schemaField.Name, _objectExecutionEngine.Execute(selection.Field.Selections, schemaField as GraphQLObjectType, objectContext));
            }

            return new KeyValuePair<string, object>(schemaField.Name, schemaField.ResolveToObject(context));
        }        
    }
}