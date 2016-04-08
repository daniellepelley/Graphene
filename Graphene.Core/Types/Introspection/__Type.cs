namespace Graphene.Core.Types.Introspection
{
    public class __Type : IGraphObjectType
    {
        public string Kind { get; private set; }

        public string Name
        {
            get { return "__Type"; }
        }

        public string Description
        {
            get
            {
                return @"The fundamental unit of any GraphQL Schema is the type. There are " +
                       "many kinds of types in GraphQL as represented by the `__TypeKind` enum." +
                       "\n\nDepending on the kind of a type, certain fields describe " +
                       "information about that type. Scalar types provide no information " +
                       "beyond a name and description, while Enum types provide their values. " +
                       "Object and Interface types provide the fields they describe. Abstract " +
                       "types, Union and Interface, provide the Object types possible " +
                       "at runtime. List and NonNull types compose other types.'";
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
                        Name = "kind",
                        Description = "The type that query operations will be rooted at.",
                        OfType = typeof (GraphQLNonNull<__TypeKind>),
                        Resolve = schema => schema.GetQueryType()
                    },
                    new GraphQLSchemaFieldType
                    {
                        Name = "name",
                        OfType = typeof (GraphQLString),
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
                        Name = "fields",
                        OfType = typeof (GraphQLList<GraphQLNonNull<__Field>>),
                        Args = "includeDeprecated: { type: GraphQLBoolean, defaultValue: false }",
                        Resolve = schema => schema.GetQueryType()
                    },
                    new GraphQLSchemaFieldType
                    {
                        Name = "interfaces",
                        OfType = typeof (GraphQLList<GraphQLNonNull<__Type>>),
                        Resolve = schema => schema.GetQueryType()
                    },
                    new GraphQLSchemaFieldType
                    {
                        Name = "possibleTypes",
                        OfType = typeof (GraphQLList<GraphQLNonNull<__Type>>),
                        Resolve = schema => schema.GetQueryType()
                    },
                    new GraphQLSchemaFieldType
                    {
                        Name = "possibleTypes",
                        Args = "includeDeprecated: { type: GraphQLBoolean, defaultValue: false }",
                        OfType = typeof (GraphQLList<GraphQLNonNull<__EnumValue>>),
                        Resolve = schema => schema.GetQueryType()
                    },
                    new GraphQLSchemaFieldType
                    {
                        Name = "inputFields",
                        Args = "includeDeprecated: { type: GraphQLBoolean, defaultValue: false }",
                        OfType = typeof (GraphQLList<GraphQLNonNull<__InputValue>>),
                        Resolve = schema => schema.GetQueryType()
                    }
                };
            }
        }
    }
}