using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core.Model;
using Graphene.Core.Types;

namespace Graphene.Execution
{
    public class ObjectExecutionEngine : IObjectExecutionEngine
    {
        public object Execute(Selection[] selections, GraphQLObjectType graphQLObject, ResolveFieldContext objectContext)
        {
            var returnValue = graphQLObject.Resolve(objectContext);

            var enumerable = returnValue as IEnumerable;
            if (enumerable != null)
            {
                return enumerable
                    .Cast<object>()
                    .Select(item => GetFieldValues(selections, graphQLObject, item));
            }

            return GetFieldValues(selections, graphQLObject, returnValue);
        }

        private Dictionary<string, object> GetFieldValues(Selection[] selections, GraphQLObjectType graphQLObject, object item)
        {
            var fieldValues = new Dictionary<string, object>();

            foreach (var selection in selections)
            {
                var keyPairValue = new FieldExecutionEngine(this).ProcessField(graphQLObject, selection, item);
                fieldValues.Add(keyPairValue.Key, keyPairValue.Value);
            }
            return fieldValues;
        }


    }
}