namespace Graphene.Schema
{
    public class GraphQLFieldType
    {
        public string Kind { get; set; }
        public string Name { get; set; }
        public GraphQLFieldType OfType { get; set; }
    }
}