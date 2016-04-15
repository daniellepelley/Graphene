namespace Graphene.Core.Types
{
    public abstract class GraphQLScalarBase
    {
        public string Kind
        {
            get { return GraphQLKinds.Scalar; }
        }

        public string[] OfType { get; set; }

    }
}
