using System.Collections.Generic;
using Graphene.Core.Types;
using Graphene.Core.Types.Scalar;

namespace Graphene.Core.FieldTypes
{
    public interface IGraphQLFieldType
    {
        string Name { get; set; }
        string Kind { get; }
        string Description { get; set; }
        IEnumerable<IGraphQLArgument> Arguments { get; set; }
        string[] Type { get; set; }
        bool IsDeprecated { get; set; }
        string DeprecationReason { get; set; }
    }
}