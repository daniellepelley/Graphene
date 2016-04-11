using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core;
using Graphene.Core.Model;
using Graphene.Core.Types;

namespace Graphene.Execution
{
    public class ObjectExecutionEngine : IObjectExecutionEngine
    {
        public object Execute(ResolveObjectContext objectContext)
        {
            var returnValue = objectContext.Current.Resolve(objectContext);

            var enumerable = returnValue as IEnumerable;
            if (enumerable != null)
            {
                var output = new List<Dictionary<string, object>>();
                foreach (var item in enumerable)
                {
                    var newObjectContext  = objectContext.Clone();
                    newObjectContext.Source = item;
                    output.Add(GetFieldValues(newObjectContext));
                }

                return output;
            }

            var singleObjectContext = objectContext.Clone();
            singleObjectContext.Source = returnValue;
            return GetFieldValues(singleObjectContext);
        }

        private Dictionary<string, object> GetFieldValues(ResolveObjectContext objectContext)
        {
            var fieldValues = new Dictionary<string, object>();

            foreach (var selection in objectContext.Selections)
            {
                var fieldContext = objectContext.Clone();
                fieldContext.Selection = selection;
                fieldContext.FieldName = selection.Field.Name;
    
                var keyPairValue = new FieldExecutionEngine(this).Execute(fieldContext);
                fieldValues.Add(keyPairValue.Key, keyPairValue.Value);
            }
            return fieldValues;
        }


    }
}