using System.Collections.Generic;
using Graphene.Core;

namespace Graphene.Execution
{
    public class ScalarFieldExecutionEngine
    {
        public KeyValuePair<string, object> Execute(ResolveFieldContext resolveFieldContext)
        {
            Validate(resolveFieldContext);
            var executionNode = resolveFieldContext.ScalarType.ToExecutionNode(() => resolveFieldContext);
            return executionNode.Execute();
        }

        private void Validate(ResolveFieldContext resolveFieldContext)
        {
            if (resolveFieldContext.Parent == null)
            {
                throw new GraphQLException("ScalarType object is null");
            }

            if (resolveFieldContext.Parent.ObjectType.Fields == null)
            {
                throw new GraphQLException("ScalarType object fields is null");
            }

            if (resolveFieldContext.ScalarType == null)
            {
                throw new GraphQLException(string.Format("Field {0} does not exist", resolveFieldContext.FieldName));
            }
        }
    }
}