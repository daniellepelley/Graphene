namespace Graphene.Core.Types
{
    public class GraphQLObjectType : IGraphObjectType
    {
        public string Kind
        {
            get { return GraphQLKinds.Object; }
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public GraphQLFieldType[] Fields { get; set; }
    }
}