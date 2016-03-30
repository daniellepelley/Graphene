namespace Graphene.Schema
{
    public class GraphQlArg
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public GraphQlFieldType Type { get; set; }
        public string DefaultValue { get; set; }
    }
}