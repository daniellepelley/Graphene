using System.CodeDom;

namespace Graphene
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

    public class GraphQlFieldType
    {
        public string Kind { get; set; }
        public string Name { get; set; }
        public GraphQlFieldType OfType { get; set; }
    }

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

    public class GraphQlArg
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public GraphQlFieldType Type { get; set; }
        public string DefaultValue { get; set; }
    }
}