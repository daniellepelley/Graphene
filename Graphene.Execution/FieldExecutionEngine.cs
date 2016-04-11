using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core;
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

        public KeyValuePair<string, object> Execute(ResolveObjectContext resolveObjectContext)
        {
            var executionObject = GetExecutionObject(resolveObjectContext);
            return new KeyValuePair<string, object>(executionObject.Name, executionObject.Execute());
        }

        private ExecuteCommand GetExecutionObject(ResolveObjectContext resolveObjectContext1)
        {
            if (resolveObjectContext1.Current == null)
            {
                throw new GraphQLException("Current object is null");
            }

            if (resolveObjectContext1.Current.Fields == null)
            {
                throw new GraphQLException("Current object fields is null");
            }

            var fieldName = resolveObjectContext1.Selection.Field.Name;

            var schemaField = resolveObjectContext1.Current.Fields.FirstOrDefault(x => x.Name == fieldName);

            if (schemaField == null)
            {
                throw new GraphQLException(string.Format("Field {0} does not exist", resolveObjectContext1.Selection.Field.Name));
            }

            if (schemaField is GraphQLObjectType)
            {
                var resolveObjectContext = new ResolveObjectContext
                {
                    FieldName = resolveObjectContext1.Selection.Field.Name,
                    Source = resolveObjectContext1.Source,
                    Selections = resolveObjectContext1.Selection.Field.Selections,
                    Current = (GraphQLObjectType) schemaField,
                    Schema = resolveObjectContext1.Schema
                };

                return new ExecuteObjectCommand
                {
                    Name = schemaField.Name,
                    ResolveObjectContext = resolveObjectContext,
                    Func = context => _objectExecutionEngine.Execute(context)
                };
            }

            var resolveFieldContext = new ResolveFieldContext
            {
                FieldName = resolveObjectContext1.Selection.Field.Name,
                Source = resolveObjectContext1.Source,
                Schema = resolveObjectContext1.Schema
            };

            return new ExecuteFieldCommand
            {
                Name = schemaField.Name,
                //ResolveFieldContext = resolveFieldContext,
                Func = x => ((GraphQLFieldScalarType)schemaField).Resolve(resolveFieldContext)
            };
        }
    }
}