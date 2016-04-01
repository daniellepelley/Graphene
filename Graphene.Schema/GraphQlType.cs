namespace Graphene.Schema
{
    public class GraphQLType
    {
        public string Kind { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public GraphQLField[] Fields { get; set; }
        public string[] InputFields { get; set; }
        public string[] Interfaces { get; set; }
        public string[] EnumValues { get; set; }
        public string[] PossibleTypes { get; set; }

        public GraphQLType()
        {
            Description = null;
            Fields = new GraphQLField[0];
            InputFields = new string[0];
            Interfaces = new string[0];
            EnumValues = new string[0];
            PossibleTypes = new string[0];
        }
    }
}