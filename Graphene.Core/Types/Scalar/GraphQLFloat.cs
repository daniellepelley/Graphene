namespace Graphene.Core.Types
{
    public class GraphQLFloat : GraphQLScalarBase, IGraphQLType
    {
        public string Name
        {
            get { return "Float"; }
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