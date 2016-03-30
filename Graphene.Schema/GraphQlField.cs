namespace Graphene.Schema
{
    public class GraphQlField
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public GraphQlArg[] Args { get; set; }
        public GraphQlFieldType Type { get; set; }
        public bool IsDeprecated { get; set; }
        public string DeprecationReason { get; set; }

        public GraphQlField()
        {
            Args = new GraphQlArg[0];
        }
    }
}