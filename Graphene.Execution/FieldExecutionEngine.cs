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

        private ExecuteCommand GetExecutionObject(ResolveObjectContext resolveObjectContext)
        {
            if (resolveObjectContext.Parent == null)
            {
                throw new GraphQLException("ScalarType object is null");
            }

            if (resolveObjectContext.Parent.ObjectType.Fields == null)
            {
                throw new GraphQLException("ScalarType object fields is null");
            }

            if (resolveObjectContext.ObjectType == null)
            {
                throw new GraphQLException(string.Format("Field {0} does not exist",
                    resolveObjectContext.Selection.Field.Name));
            }

            return new ExecuteObjectCommand
            {
                Name = resolveObjectContext.FieldName,
                ResolveObjectContext = resolveObjectContext,
                Func = context => _objectExecutionEngine.Execute(context)
            };
        }
    }
}