using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core;
using Graphene.Core.Model;
using Graphene.Core.Types;

namespace Graphene.Execution
{
    //public class ObjectExecutionEngine : IObjectExecutionEngine
    //{
    //    public object Execute(ResolveObjectContext<object> objectContext)
    //    {

    //        var returnValue = objectContext.ObjectType.Resolve(objectContext);

    //        var enumerable = returnValue as IEnumerable;
    //        if (enumerable != null)
    //        {
    //            return ProcessEnumerable(objectContext, enumerable);
    //        }

    //        var singleObjectContext = objectContext.Clone();
    //        singleObjectContext.Source = returnValue;
    //        return GetFieldValues(singleObjectContext);
    //    }

    //    private object ProcessEnumerable(ResolveObjectContext<object> objectContext, IEnumerable enumerable)
    //    {
    //        var output = new List<Dictionary<string, object>>();
    //        foreach (var item in enumerable)
    //        {
    //            var newObjectContext = objectContext.Clone();
    //            newObjectContext.Source = item;
    //            output.Add(GetFieldValues(newObjectContext));
    //        }

    //        return output;
    //    }

    //    private Dictionary<string, object> GetFieldValues(ResolveObjectContext<object> objectContext)
    //    {
    //        var fieldValues = new Dictionary<string, object>();

    //        foreach (var selection in objectContext.Selections)
    //        {
    //            var graphQLFieldType = objectContext.ObjectType[selection.Field.Name];

    //            if (graphQLFieldType is GraphQLScalar)
    //            {
    //                var executionNodeBuilder = new ExecutionNodeBuilder();
    //                var node = executionNodeBuilder.Build(selection, graphQLFieldType as GraphQLScalar, () => objectContext.Source);
    //                var keyPairValue = node.Execute();
    //                fieldValues.Add(keyPairValue.Key, keyPairValue.Value);
    //            }
    //            else
    //            {
    //                var context = BuildResolveObjectContext(objectContext, selection, graphQLFieldType);
    //                var keyPairValue = new FieldExecutionEngine(this).Execute(context);
    //                fieldValues.Add(keyPairValue.Key, keyPairValue.Value);
    //            }
    //        }
    //        return fieldValues;
    //    }

    //    private ResolveObjectContext<object> BuildResolveObjectContext(ResolveObjectContext<object> objectContext, Selection selection, IGraphQLFieldType graphQLType)
    //    {
    //        objectContext = objectContext.Clone();
    //        objectContext.Selection = selection;
    //        objectContext.FieldName = selection.Field.Name;
    //        objectContext.Selections = selection.Field.Selections;
    //        objectContext.ObjectType = (GraphQLObject) graphQLType;
    //        return objectContext;
    //    }

    //    private ResolveFieldContext<object> BuildResolveFieldContext(ResolveObjectContext<object> objectContext, Selection selection, IGraphQLFieldType graphQLType)
    //    {
    //        var context = new ResolveFieldContext<object> 
    //        {
    //            FieldName = selection.Field.Name,
    //            Parent = objectContext,
    //            ScalarType = (GraphQLScalar) graphQLType,
    //            Source = objectContext.Source,
    //            Schema = objectContext.Schema
    //        };
    //        return context;
    //    }
    //}
}