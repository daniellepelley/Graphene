namespace Graphene.Schema
{
    public class GraphQlFieldType
    {
        public string Kind { get; set; }
        public string Name { get; set; }
        public GraphQlFieldType OfType { get; set; }
    }
}