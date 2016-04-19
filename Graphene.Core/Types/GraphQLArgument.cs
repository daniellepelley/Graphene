using Graphene.Core.Types.Scalar;

namespace Graphene.Core.Types
{
    public class GraphQLArgument : IGraphQLArgument
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IGraphQLType Type { get; set; }
        public string DefaultValue { get; set; }
    }
}