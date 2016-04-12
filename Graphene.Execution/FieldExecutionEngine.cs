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
                throw new GraphQLException(string.Format("Field {0} does not exist",
                    resolveObjectContext1.Selection.Field.Name));
            }

            return new ExecuteObjectCommand
            {
                Name = resolveObjectContext1.FieldName,
                ResolveObjectContext = resolveObjectContext1,
                Func = context => _objectExecutionEngine.Execute(context)
            };

        }
    }
}