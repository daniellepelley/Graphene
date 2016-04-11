using System.Collections.Generic;
using System.Linq;
using Graphene.Core;
using Graphene.Core.Types;

namespace Graphene.Execution
{
    public class ScalarFieldExecutionEngine
    {
        public KeyValuePair<string, object> Execute(ResolveFieldContext resolveFieldContext)
        {
            var executionObject = GetExecutionObject(resolveFieldContext);
            return new KeyValuePair<string, object>(executionObject.Name, executionObject.Execute());
        }

        private ExecuteScalarCommand GetExecutionObject(ResolveFieldContext resolveObjectContext1)
        {
            if (resolveObjectContext1.GraphQLObjectType == null)
            {
                throw new GraphQLException("Current object is null");
            }

            if (resolveObjectContext1.GraphQLObjectType.Fields == null)
            {
                throw new GraphQLException("Current object fields is null");
            }

            var fieldName = resolveObjectContext1.FieldName;

            var schemaField = resolveObjectContext1.GraphQLObjectType.Fields.FirstOrDefault(x => x.Name == fieldName);

            if (schemaField == null)
            {
                throw new GraphQLException(string.Format("Field {0} does not exist", resolveObjectContext1.FieldName));
            }

            return new ExecuteScalarCommand
            {
                Name = schemaField.Name,
                ResolveFieldContext = resolveObjectContext1,
                Func = context => context.GetValue()
            };
        }
    }
}