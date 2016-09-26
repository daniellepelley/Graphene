using System;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core;
using Graphene.Core.FieldTypes;
using Graphene.Core.Parsers;
using Graphene.Core.Types;
using Graphene.Core.Types.Object;

namespace Graphene.Test.Objects
{
    public static class TestHelpers
    {
        public static IDictionary<string, object> QueryAType<T>(GraphQLObjectType type, string fieldName, string queryPart, Func<ResolveObjectContext, T> resolve, ITypeList typeList = null)
        {
            if (typeList == null)
            {
                typeList = TypeList.Create();
            }

            var field = new GraphQLObjectField<T>
            {
                Name = fieldName,
                Type = new [] { type.Name },
                Resolve = resolve
            };

            var document = new DocumentParser().Parse(queryPart);
            var executionBranch = field.ToExecutionBranch(document.Operations.First().Selections.First().Field, typeList);
            var result = executionBranch.Execute();

            return (IDictionary<string, object>)result.Value;
        }
    }
}