using Graphene.Core.FieldTypes;
using Graphene.Core.Types.Scalar;

namespace Graphene.Core.Types.Object
{
    public interface IGraphQLObject : IGraphQLType
    {
        IGraphQLFieldType this[string name] { get; }
    }
}