using System.Collections.Generic;
using System.Linq;

namespace Graphene.Core.Types.Introspection
{
    public class __Schema : GraphQLObjectType
    {
        public __Schema()
        {
            var queryType = new GraphQLObjectType
            {
                Name = "__Type",
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

            Fields = new IGraphQLFieldType[]
            {
                new GraphQLList<GraphQLSchema, IGraphQLType>
                {
                    Name = "types",
                    Description = "A list of all types supported by this server.",
                    GraphQLObjectType = () => new __Type(),
                    Type = new __Type(),
                    OfType = new[] {"GraphQLNonNull", "__Type"},
                    Resolve = context => context.Source.GetTypes()
                },
                new GraphQLObjectField<GraphQLSchema, GraphQLObjectFieldBase>
                {
                    Name = "queryType",
                    Description = "The type that query operations will be rooted at.",
                    OfType = new[] {"GraphQLNonNull", "__Type"},
                    GraphQLObjectType = () => queryType,
                    Resolve = context => context.Source.Query
                },
                new GraphQLObjectField<GraphQLSchema, GraphQLObjectFieldBase>
                {
                    Name = "mutationType",
                    Description =
                        "If this server supports mutation, the type that mutation operations will be rooted at.",
                    OfType = new[] {"GraphQLNonNull", "__Type"},
                    GraphQLObjectType = () => new __Type(),
                    Resolve = context => null
                },
                new GraphQLObjectField<GraphQLSchema, GraphQLObjectFieldBase>
                {
                    Name = "subscriptionType",
                    Description =
                        "If this server support subscription, the type that subscription operations will be rooted at.",
                    OfType = new[] {"GraphQLNonNull", "__Type"},
                    GraphQLObjectType = () => new __Type(),
                    Resolve = context => null
                },
                new GraphQLObjectField<GraphQLSchema, GraphQLObjectFieldBase>
                {
                    Name = "directives",
                    Description = "A list of all directives supported by this server.",
                    OfType = new[] {"GraphQLNonNull", "__Type"},
                    GraphQLObjectType = () => new __Directive(),
                    Resolve = context => null
                },
            };

            Name = "__schema";
            Description = @"A GraphQL Schema defines the capabilities of a GraphQL server. It " +
                          "exposes all available types and directives on the server, as well as " +
                          "the entry points for query, mutation, and subscription operations.";
        }
    }
}
