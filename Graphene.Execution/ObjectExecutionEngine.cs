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
            var returnValue = objectContext.GraphQLObjectType.Resolve(objectContext);

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
                var graphQLFieldType = objectContext.GraphQLObjectType.Fields.FirstOrDefault(x => x.Name == selection.Field.Name);

                if (graphQLFieldType is GraphQLFieldScalarType)
                {
                    var context = BuildResolveFieldContext(objectContext, selection, graphQLFieldType);
                    var keyPairValue = new ScalarFieldExecutionEngine().Execute(context);
                    fieldValues.Add(keyPairValue.Key, keyPairValue.Value);
                }
                else
                {
                    var context = BuildResolveObjectContext(objectContext, selection, graphQLFieldType);
                    var keyPairValue = new FieldExecutionEngine(this).Execute(context);
                    fieldValues.Add(keyPairValue.Key, keyPairValue.Value);
                }
            }
            return fieldValues;
        }

        private ResolveObjectContext BuildResolveObjectContext(ResolveObjectContext objectContext, Selection selection, IGraphQLFieldType graphQLType)
        {
            objectContext = objectContext.Clone();
            objectContext.Selection = selection;
            objectContext.FieldName = selection.Field.Name;
            objectContext.Selections = selection.Field.Selections;
            objectContext.Parent = objectContext.GraphQLObjectType;
            objectContext.GraphQLObjectType = (GraphQLObjectType) graphQLType;
            return objectContext;
        }

        private ResolveFieldContext BuildResolveFieldContext(ResolveObjectContext objectContext, Selection selection, IGraphQLFieldType graphQLType)
        {
            var context = new ResolveFieldContext
            {
                FieldName = selection.Field.Name,
                Parent = objectContext.GraphQLObjectType,
                GraphQLObjectType = (GraphQLFieldScalarType) graphQLType,
                Source = objectContext.Source
            };
            return context;
        }

    }
}