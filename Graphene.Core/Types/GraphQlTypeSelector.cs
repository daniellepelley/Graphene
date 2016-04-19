using System;
using System.Collections.Generic;
using Graphene.Core.Types.Scalar;

namespace Graphene.Core.Types
{
    public class GraphQlTypeSelector : Mapper<IGraphQLType>
    {
        public GraphQlTypeSelector()
        {
            var dictionary = new Dictionary<string, Func<IGraphQLType, string>>
            {
                {"name", x => x.Name},
                {"description", x => x.Description},
                {"kind", x => x.Kind}
            };

            SetUp(dictionary);
        }
    }
}