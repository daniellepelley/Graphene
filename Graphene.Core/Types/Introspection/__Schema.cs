using System.Collections.Generic;
using System.Linq;
using Graphene.Core.FieldTypes;

namespace Graphene.Core.Types.Introspection
{
    public class __Schema : GraphQLObjectType
    {
        private GraphQLSchema _schema;

        public __Schema(GraphQLSchema schema)
        {
            _schema = schema;
            SetUp(schema);
        }

        private void SetUp(GraphQLSchema schema)
        {
            Name = "__schema";
            Description = @"A GraphQL Schema defines the capabilities of a GraphQL server. It " +
                          "exposes all available types and directives on the server, as well as " +
                          "the entry points for query, mutation, and subscription operations.";

            Fields = new List<IGraphQLFieldType>(new IGraphQLFieldType[]
            {
                new GraphQLObjectType
                {
                    Name = "queryType",
                    Description = "The type that query operations will be rooted at.",
                    OfType = new[] { "GraphQLNonNull", "__Type" },
                    Fields = new IGraphQLFieldType[]
                    {
                        new GraphQLFieldScalarType
                        {
                            Name = "kind",
                            Description = "The type that query operations will be rooted at.",
                            //OfType = new GraphQLNonNull<__TypeKind>>(),
                            Resolve = context => _schema.Query.Kind
                        },
                        new GraphQLFieldScalarType
                        {
                            Name = "name",
                            //OfType = new GraphQLString(),
                            Resolve = context => _schema.Query.Name
                        }
                    }.ToList(),
                    Resolve = context => schema.Query
                },
                new GraphQLObjectType
                {
                    Name = "mutationType",
                    Description =
                        "If this server supports mutation, the type that mutation operations will be rooted at.",
                    //OfType = typeof (GraphQLNonNull<__Type>),
                    Resolve = context => context.Schema.Query.AsIntrospective()
                },
                new GraphQLSchemaFieldType
                {
                    Name = "subscriptionType",
                    Description =
                        "If this server support subscription, the type that subscription operations will be rooted at.",
                    OfType = typeof (GraphQLNonNull<__Type>),
                    Resolve = context => schema.GetSubscriptionType()
                },
                new GraphQLSchemaFieldType
                {
                    Name = "directives",
                    Description = "A list of all directives supported by this server.",
                    OfType = typeof (GraphQLNonNull<__Type>),
                    Resolve = context => schema.GetDirectives()
                }
            });
            Resolve = context => context.Schema;
        }

        public string Kind { get; private set; }

    }
}
