using System;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core;
using Graphene.Core.Parsers;
using Graphene.Core.Types;

namespace Graphene.Test.Objects
{
    public static class TestHelpers
    {
        public static IDictionary<string, object> QueryAType<T>(GraphQLObjectType type, string fieldName, string queryPart, Func<ResolveObjectContext, T> resolve)
        {
            var field = new GraphQLObjectField<T>
            {
                Name = fieldName,
                GraphQLObjectType = () => type,
                Resolve = resolve
            };

            var document = new DocumentParser().Parse(queryPart);
            var selections = document.Operations.First().Selections;
            var executionBranch = field.ToExecutionBranch(selections, new Dictionary<string, object>());
            var result = executionBranch.Execute();

            return (IDictionary<string, object>)result.Value;
        }
    }
}