using Graphene.Core.Types.Scalar;

namespace Graphene.Core.Types
{
    public interface IGraphQLArgument
    {
        string Name { get; set; }
        string Description { get; set; }
        IGraphQLType Type { get; set; }
        string DefaultValue { get; set; }
    }
}