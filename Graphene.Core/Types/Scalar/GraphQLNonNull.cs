namespace Graphene.Core.Types.Scalar
{
    public class GraphQLNonNull : IGraphQLType
    {
        public string Kind
        {
            get { return GraphQLKinds.NonNull; }
        }

        public string Name
        {
            get { return null; }
        }

        public string Description
        {
            get { return "This is a NonNull"; }
        }
    }
}