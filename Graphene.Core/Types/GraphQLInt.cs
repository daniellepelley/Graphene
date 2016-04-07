namespace Graphene.Core.Types
{
    public class GraphQLInt : GraphQLScalar, IGraphQLType
    {
        public string Name
        {
            get { return "Int"; }
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