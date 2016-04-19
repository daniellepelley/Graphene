namespace Graphene.Core.Types.Scalar
{
    public interface IGraphQLType
    {
        string Kind { get; }
        string Name { get; }
        string Description { get; }
        IGraphQLType OfType { get; }
    }

    public class GraphQLList : IGraphQLType
    {
        private readonly IGraphQLType _graphQLType;

        public GraphQLList(IGraphQLType graphQLType)
        {
            _graphQLType = graphQLType;
        }

        public string Kind
        {
            get { return GraphQLKinds.List; }
        }

        public string Name
        {
            get { return null; }
        }

        public string Description
        {
            get { return "Describes a list"; }
        }

        public IGraphQLType OfType
        {
            get { return _graphQLType; }
        }
    }
}