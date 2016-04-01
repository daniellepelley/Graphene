namespace Graphene.Schema
{
    public class GraphQLArg
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public GraphQLFieldType Type { get; set; }
        public string DefaultValue { get; set; }
    }
}