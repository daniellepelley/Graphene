using System.Collections.Generic;
using System.Linq;

namespace Graphene.Core.Types.Introspection
{
    public class __Schema : GraphQLObjectField<GraphQLSchema>
    {
        private GraphQLSchema _schema;

        public __Schema(GraphQLSchema schema)
        {
            _schema = schema;
            SetUp(schema);
        }

        private void SetUp(GraphQLSchema schema)
        {
            var queryType = new GraphQLObjectType
            {
                Fields = new IGraphQLFieldType[]
                {
                    new GraphQLScalarField<GraphQLObjectFieldBase, string>
                    {
                        Name = "kind",
                        Description = "The type that query operations will be rooted at.",
                        OfType = new[] {"GraphQLNonNull", "__TypeKind"},
                        Resolve = context => context.Source.Kind
                    },
                    new GraphQLScalarField<GraphQLObjectFieldBase, string>
                    {
                        Name = "name",
                        OfType = new[] {"GraphQLString"},
                        Resolve = context => context.Source.Name
                    },
                    new GraphQLList<GraphQLObjectFieldBase, IGraphQLFieldType>
                    {
                        Name = "fields",
                        OfType = new[] {"GraphQLSchemaList", "__Field"},
                        GraphQLObjectType = () => new GraphQLObjectType
                        {

                            Fields = new List<IGraphQLFieldType>
                            {
                                new GraphQLScalarField<IGraphQLFieldType, string>
                                {
                                    Name = "name",
                                    OfType = new[] {"GraphQLString"},
                                    Resolve = context => context.Source.Name
                                },
                                new GraphQLScalarField<IGraphQLFieldType, string>
                                {
                                    Name = "description",
                                    OfType = new[] {"GraphQLString"},
                                    Resolve = context => context.Source.Description
                                },
                                new GraphQLScalarField<IGraphQLFieldType, string>
                                {
                                    Name = "kind",
                                    OfType = new[] {"GraphQLString"},
                                    Resolve = context => context.Source.Kind
                                }
                            }
                        },
                        Resolve = context => context.Source.GraphQLObjectType().Fields
                    }
                }
            };

            var schemaType = new GraphQLObjectType
            {
                Fields = new IGraphQLFieldType[]
                {
                    new GraphQLObjectField<GraphQLSchema, GraphQLObjectFieldBase>
                    {
                        Name = "queryType",
                        Description = "The type that query operations will be rooted at.",
                        OfType = new[] {"GraphQLNonNull", "__Type"},
                        GraphQLObjectType = () => queryType,
                        Resolve = context => schema.Query
                    },
                    new GraphQLObjectField
                    {
                        Name = "mutationType",
                        Description =
                            "If this server supports mutation, the type that mutation operations will be rooted at.",
                        OfType = new[] {"GraphQLNonNull", "__Type"},
                        Resolve = context => _schema.Query
                    },
                    new GraphQLObjectField
                    {
                        Name = "subscriptionType",
                        Description =
                            "If this server support subscription, the type that subscription operations will be rooted at.",
                        OfType = new[] {"GraphQLNonNull", "__Type"},
                        Resolve = context => schema.GetSubscriptionType()
                    },
                    new GraphQLObjectField
                    {
                        Name = "directives",
                        Description = "A list of all directives supported by this server.",
                        OfType = new[] {"GraphQLNonNull", "__Type"},
                        Resolve = context => schema.GetDirectives()
                    }
                }
            };

            Name = "__schema";
            Description = @"A GraphQL Schema defines the capabilities of a GraphQL server. It " +
                          "exposes all available types and directives on the server, as well as " +
                          "the entry points for query, mutation, and subscription operations.";

            GraphQLObjectType = () => schemaType;
            Resolve = context => _schema;
        }
    }
}
