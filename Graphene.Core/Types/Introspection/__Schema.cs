using System.Collections.Generic;
using Graphene.Core.FieldTypes;
using Graphene.Core.Types.Object;
using Graphene.Core.Types.Scalar;

namespace Graphene.Core.Types.Introspection
{
    public class __Schema : GraphQLObjectType
    {
        private readonly ITypeList _typeList;

        public __Schema(ITypeList typeList)
        {
            _typeList = typeList;
//            var queryType = new GraphQLObjectType
//            {
//                Name = "__Type",
//                Fields = new IGraphQLFieldType[]
//                {
//                    new GraphQLScalarField<GraphQLObjectFieldBase, string>
//                    {
//                        Name = "kind",
//                        Description = "The type that query operations will be rooted at.",
//                        Resolve = context => context.Source.Kind
//                    },
//                    new GraphQLScalarField<GraphQLObjectFieldBase, string>
//                    {
//                        Name = "name",
//                        Resolve = context => context.Source.Name
//                    },
//                    new GraphQLList<GraphQLObjectFieldBase, IGraphQLFieldType>
//                    {
//                        Name = "fields",
//                        Type = new GraphQLObjectType
//                        {
//                            Fields = new List<IGraphQLFieldType>
//                            {
//                                new GraphQLScalarField<IGraphQLFieldType, string>
//                                {
//                                    Name = "name",
//                                    Resolve = context => context.Source.Name
//                                },
//                                new GraphQLScalarField<IGraphQLFieldType, string>
//                                {
//                                    Name = "description",
//                                    Resolve = context => context.Source.Description
//                                },
//                                new GraphQLScalarField<IGraphQLFieldType, string>
//                                {
//                                    Name = "kind",
//                                    Resolve = context => context.Source.Kind
//                                }
//                            }
//                        },
//                        Resolve = context => ((GraphQLObjectType)context.Source.Type).Fields
//                    }
//                }
//            };

            Fields = new IGraphQLFieldType[]
            {
                new GraphQLList<GraphQLSchema, IGraphQLType>
                {
                    Name = "types",
                    Description = "A list of all types supported by this server.",
                    Type = new [] { "NonNull", "List", "NonNull", "__Type"},
                    Resolve = context => context.Source.GetTypes()
                },
                new GraphQLObjectField<GraphQLSchema, IGraphQLType>
                {
                    Name = "queryType",
                    Description = "The type that query operations will be rooted at.",
                    Type = new [] {"__Type"},
                    Resolve = context => context.Source.QueryType
                },
                new GraphQLObjectField<GraphQLSchema, IGraphQLType>
                {
                    Name = "mutationType",
                    Description =
                        "If this server supports mutation, the type that mutation operations will be rooted at.",
                    Type = new [] {"__Type"},
                    Resolve = context => context.Source.MutationType
                },
                new GraphQLObjectField<GraphQLSchema, GraphQLObjectFieldBase>
                {
                    Name = "subscriptionType",
                    Description =
                        "If this server support subscription, the type that subscription operations will be rooted at.",
                    Type = new [] {"__Type"},
                    Resolve = context => null
                },
                new GraphQLList<GraphQLSchema, GraphQLDirective>
                {
                    Name = "directives",
                    Description = "A list of all directives supported by this server.",
                    Type = new [] {"NonNull", "List", "NonNull", "__Directive"},
                    Resolve = context => context.Source.GetDirectives()
                }
            };

            Name = "__schema";
            Description = @"A GraphQL Schema defines the capabilities of a GraphQL server. It " +
                          "exposes all available types and directives on the server, as well as " +
                          "the entry points for query, mutation, and subscription operations.";
        }
    }
}
