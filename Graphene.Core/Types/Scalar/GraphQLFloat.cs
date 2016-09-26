using Graphene.Core.Constants;

namespace Graphene.Core.Types.Scalar
{
    public class GraphQLFloat : GraphQLScalarBase, IGraphQLType
    {
        public string Name
        {
            get { return GraphQLTypes.Float; }
        }

        public string Description
        {
            get
            {
                return
                    "The `Float` scalar type represents signed double-precision fractional values as specified by [IEEE 754](http://en.wikipedia.org/wiki/IEEE_floating_point).";
            }
        }
    }
}