using System.Collections.Generic;
using Graphene.Core;

namespace Graphene.Execution
{
    public class ScalarFieldExecutionEngine
    {
        public KeyValuePair<string, object> Execute(ResolveFieldContext resolveFieldContext)
        {
            var executionObject = GetExecutionObject(resolveFieldContext);
            return new KeyValuePair<string, object>(executionObject.Name, executionObject.Execute());
        }

        private ExecuteScalarCommand GetExecutionObject(ResolveFieldContext resolveObjectContext)
        {
            if (resolveObjectContext.Parent == null)
            {
                throw new GraphQLException("ScalarType object is null");
            }

            if (resolveObjectContext.Parent.ObjectType.Fields == null)
            {
                throw new GraphQLException("ScalarType object fields is null");
            }

            if (resolveObjectContext.ScalarType == null)
            {
                throw new GraphQLException(string.Format("Field {0} does not exist", resolveObjectContext.FieldName));
            }

            return new ExecuteScalarCommand
            {
                Name = resolveObjectContext.FieldName,
                ResolveFieldContext = resolveObjectContext,
                Func = context => context.GetValue()
            };
        }
    }
}