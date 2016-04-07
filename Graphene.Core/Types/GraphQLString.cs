namespace Graphene.Core.Types
{
    public class GraphQLString : GraphQLScalar, IGraphQLType
    {
        public string Name
        {
            get { return "String"; }
        }

        public string Description
        {
            get { return "This is a string"; }
        }
    }
}