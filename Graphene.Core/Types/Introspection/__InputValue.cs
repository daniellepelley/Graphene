namespace Graphene.Core.Types.Introspection
{
    public class __InputValue
    {
        public string Kind { get; private set; }

        public string Name
        {
            get { return "__InputValue"; }
        }

        public string Description
        {
            get
            {
                return @"Arguments provided to Fields or Directives and the input fields of an " +
                       "InputObject are represented as Input Values which describe their type " +
                       "and optionally a default value.";
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
                        Resolve = schema => string.Empty
                    },
                    new GraphQLSchemaFieldType
                    {
                        Name = "description",
                        OfType = typeof (GraphQLString),
                        Resolve = schema => string.Empty
                    },
                    new GraphQLSchemaFieldType
                    {
                        Name = "type",
                        OfType = typeof (GraphQLNonNull<GraphQLString>),
                        Resolve = schema => string.Empty
                    },
                    new GraphQLSchemaFieldType
                    {
                        Name = "defaultValue",
                        OfType = typeof (GraphQLString),
                        Description =
                            "A GraphQL-formatted string representing the default value for this input value.",
                        Resolve = schema => string.Empty
                    }
                };
            }
        }
    }
}