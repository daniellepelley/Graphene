using Graphene.Core.Constants;

namespace Graphene.Core.Types.Scalar
{
    public class GraphQLInt : GraphQLScalarBase, IGraphQLType
    {
        public string Name
        {
            get { return GraphQLTypes.Int; }
        }

        public string Description
        {
            get
            {
                return
                    "The `Int` scalar type represents non-fractional signed whole numeric values. Int can represent values between -(2^31) and 2^31 - 1. '";
            }
        }
    }
}