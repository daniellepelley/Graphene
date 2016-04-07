namespace Graphene.Core.Types.Introspection
{
    public class __EnumValue : IGraphObjectType
    {
        public string Kind { get; private set; }

        public string Name
        {
            get { return "__EnumValue"; }
        }

        public string Description
        {
            get
            {
                return @"One possible value for a given Enum. Enum values are unique values, not " +
                       "a placeholder for a string or numeric value. However an Enum value is " +
                       "returned in a JSON response as a string.";
            }
        }

        public GraphQLSchemaFieldType[] Fields
        {
            get
            {
                return new[]
                {
                    new GraphQLSchemaFieldType
                    {
                        Name = "name",
                        OfType = typeof (GraphQLNonNull<GraphQLString>),
                        Resolve = schema => schema.GetQueryType()
                    },
                    new GraphQLSchemaFieldType
                    {
                        Name = "description",
                        OfType = typeof (GraphQLString),
                        Resolve = schema => schema.GetQueryType()
                    },
                    new GraphQLSchemaFieldType
                    {
                        Name = "isDeprecated",
                        OfType = typeof (GraphQLNonNull<GraphQLString>),
                        Resolve = schema => schema.GetQueryType()
                    },
                    new GraphQLSchemaFieldType
                    {
                        Name = "deprecationReason",
                        OfType = typeof (GraphQLString),
                        Resolve = schema => schema.GetQueryType()
                    }
                };
            }
        }
    }
}