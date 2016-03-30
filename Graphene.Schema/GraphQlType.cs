namespace Graphene.Schema
{
    public class GraphQlType
    {
        public string Kind { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public GraphQlField[] Fields { get; set; }
        public string[] InputFields { get; set; }
        public string[] Interfaces { get; set; }
        public string[] EnumValues { get; set; }
        public string[] PossibleTypes { get; set; }

        public GraphQlType()
        {
            Description = null;
            Fields = new GraphQlField[0];
            InputFields = new string[0];
            Interfaces = new string[0];
            EnumValues = new string[0];
            PossibleTypes = new string[0];
        }
    }
}