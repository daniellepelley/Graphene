namespace Graphene.Core.Types
{
    public class GraphQLBoolean : GraphQLScalarBase, IGraphQLType
    {
        public string Name
        {
            get { return "Boolean"; }
        }

        public string Description
        {
            get { return "This is a boolean"; }
        }
    }
}