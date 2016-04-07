namespace Graphene.Core.Types.Introspection
{
    public class __Field
    {
        public string Kind { get; private set; }

        public string Name
        {
            get { return "__Field"; }
        }

        public string Description
        {
            get
            {
                return @"Object and Interface types are described by a list of Fields, each of " +
                       "which has a name, potentially a list of arguments, and a return type.";
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
                        Name = "args",
                        OfType = typeof (GraphQLNonNull<GraphQLList<GraphQLNonNull<__InputValue>>>),
                        Resolve = schema => schema.GetQueryType()
                    },
                    new GraphQLSchemaFieldType
                    {
                        Name = "type",
                        OfType = typeof (GraphQLNonNull<__Type>),
                        Resolve = schema => schema.GetQueryType()
                    },
                    new GraphQLSchemaFieldType
                    {
                        Name = "isDeprecated",
                        OfType = typeof (GraphQLNonNull<GraphQLBoolean>),
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