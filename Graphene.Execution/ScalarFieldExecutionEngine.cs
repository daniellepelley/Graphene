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

        private ExecuteScalarCommand GetExecutionObject(ResolveFieldContext resolveObjectContext1)
        {
            if (resolveObjectContext1.Parent == null)
            {
                throw new GraphQLException("GraphQLObjectType object is null");
            }

            if (resolveObjectContext1.Parent.Fields == null)
            {
                throw new GraphQLException("GraphQLObjectType object fields is null");
            }

            if (resolveObjectContext1.GraphQLObjectType == null)
            {
                throw new GraphQLException(string.Format("Field {0} does not exist", resolveObjectContext1.FieldName));
            }

            return new ExecuteScalarCommand
            {
                Name = resolveObjectContext1.FieldName,
                ResolveFieldContext = resolveObjectContext1,
                Func = context => context.GetValue()
            };
        }
    }
}