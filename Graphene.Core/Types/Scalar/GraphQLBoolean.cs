using Graphene.Core.Constants;

namespace Graphene.Core.Types.Scalar
{
    public class GraphQLBoolean : GraphQLScalarBase, IGraphQLType
    {
        public string Name
        {
            get { return GraphQLTypes.Boolean; }
        }

        public string Description
        {
            get { return "The `Boolean` scalar type represents `true` or `false`."; }
        }
    }
}