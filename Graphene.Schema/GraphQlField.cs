namespace Graphene.Schema
{
    public class GraphQLField
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public GraphQLArg[] Args { get; set; }
        public GraphQLFieldType Type { get; set; }
        public bool IsDeprecated { get; set; }
        public string DeprecationReason { get; set; }

        public GraphQLField()
        {
            Args = new GraphQLArg[0];
        }
    }
}