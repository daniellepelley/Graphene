using System;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core;
using Graphene.Core.Execution;
using Graphene.Core.Model;
using Graphene.Core.Types;

namespace Graphene.Execution
{
    public class ExecutionNodeBuilder
    {
        public ExecutionNode Build<TInput>(Selection selection, GraphQLScalar<TInput> graphQLType, Func<TInput> getInput)
        {
            return graphQLType.ToExecutionNode(getInput);
        }

        public ExecutionRoot Build(GraphQLObjectBase graphQLType, Selection[] selections, IDictionary<string, object> arguments)
        {
            var generator = graphQLType.ToExecutionRoot(selections.ToArray(), arguments);
            return generator;
        }
    }

    public class ScalarFieldExecutionEngine
    {
        public KeyValuePair<string, object> Execute(ResolveFieldContext<object> resolveFieldContext)
        {
            Validate(resolveFieldContext);
            var executionNode = resolveFieldContext.ScalarType.ToExecutionNode(() => resolveFieldContext.Source);
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