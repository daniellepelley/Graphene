using System.Collections.Generic;
using System.Linq;

namespace Graphene.Core.Types.Introspection
{
    public class __Schema : GraphQLObject<GraphQLSchema>
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

            Fields = new IGraphQLFieldType[]
            {
                new GraphQLObject<GraphQLSchema, GraphQLObjectBase>
                {
                    Name = "queryType",
                    Description = "The type that query operations will be rooted at.",
                    OfType = new[] { "GraphQLNonNull", "__Type" },
                    Fields = new IGraphQLFieldType[]
                    {
                        new GraphQLScalar<GraphQLObjectBase, string>
                        {
                            Name = "kind",
                            Description = "The type that query operations will be rooted at.",
                            OfType = new[] { "GraphQLNonNull", "__TypeKind" },
                            Resolve = context => context.Source.Kind
                        },
                        new GraphQLScalar<GraphQLObjectBase, string>
                        {
                            Name = "name",
                            OfType = new[] {"GraphQLString" },
                            Resolve = context => context.Source.Name
                        },
                        new GraphQLList<GraphQLObjectBase, IGraphQLFieldType>
                        {
                            Name = "fields",
                            OfType = new[] {"GraphQLSchemaList", "__Field"},
                            Fields = new List<IGraphQLFieldType>
                            {
                                new GraphQLScalar<IGraphQLFieldType, string>
                                {
                                    Name = "name",
                                    OfType = new[] {"GraphQLString"},
                                    Resolve = context => context.Source.Name
                                },
                                new GraphQLScalar<IGraphQLFieldType, string>
                                {
                                    Name = "description",
                                    OfType = new[] {"GraphQLString"},
                                    Resolve = context => context.Source.Description
                                },
                                new GraphQLScalar<IGraphQLFieldType, string>
                                {
                                    Name = "kind",
                                    OfType = new[] {"GraphQLString"},
                                    Resolve = context => context.Source.Kind
                                }   
                            },
                            Resolve = context => context.Source.Fields
                        }
                    }.ToList(),
                    Resolve = context => schema.Query
                },
                new GraphQLObject
                {
                    Name = "mutationType",
                    Description =
                        "If this server supports mutation, the type that mutation operations will be rooted at.",
                    OfType = new [] { "GraphQLNonNull", "__Type" },
                    Resolve = context => _schema.Query
                },
                new GraphQLObject
                {
                    Name = "subscriptionType",
                    Description =
                        "If this server support subscription, the type that subscription operations will be rooted at.",
                    OfType = new [] { "GraphQLNonNull", "__Type" },
                    Resolve = context => schema.GetSubscriptionType()
                },
                new GraphQLObject
                {
                    Name = "directives",
                    Description = "A list of all directives supported by this server.",
                    OfType = new [] { "GraphQLNonNull", "__Type"},
                    Resolve = context => schema.GetDirectives()
                }
            }.ToList();
            Resolve = context => _schema;
        }
    }
}
