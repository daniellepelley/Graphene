namespace Graphene.Core.Types.Introspection
{
    public class __Schema : IGraphObjectType
    {
        public string Kind { get; private set; }

        public string Name
        {
            get { return "__Schema"; }
        }

        public string Description
        {
            get
            {
                return @"A GraphQL Schema defines the capabilities of a GraphQL server. It " +
                       "exposes all available types and directives on the server, as well as " +
                       "the entry points for query, mutation, and subscription operations.";
            }
        }

        public GraphQLSchemaFieldType[] Fields
        {
            get
            {
                object __Type = null;
                return new[]
                {
                    new GraphQLSchemaFieldType
                    {
                        Name = "queryType",
                        Description = "The type that query operations will be rooted at.",
                        OfType = typeof (GraphQLNonNull<__Type>),
                        Resolve = schema => schema.GetQueryType()
                    },
                    new GraphQLSchemaFieldType
                    {
                        Name = "mutationType",
                        Description =
                            "If this server supports mutation, the type that mutation operations will be rooted at.",
                        OfType = typeof (GraphQLNonNull<__Type>),
                        Resolve = schema => schema.GetMutationType()
                    },
                    new GraphQLSchemaFieldType
                    {
                        Name = "subscriptionType",
                        Description =
                            "If this server support subscription, the type that subscription operations will be rooted at.",
                        OfType = typeof (GraphQLNonNull<__Type>),
                        Resolve = schema => schema.GetSubscriptionType()
                    },
                    new GraphQLSchemaFieldType
                    {
                        Name = "directives",
                        Description = "A list of all directives supported by this server.",
                        OfType = typeof (GraphQLNonNull<__Type>),
                        Resolve = schema => schema.GetDirectives()
                    }
                };
            }
        }
    }
}
