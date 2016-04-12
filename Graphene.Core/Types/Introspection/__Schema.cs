using System.Collections.Generic;
using System.Linq;

namespace Graphene.Core.Types.Introspection
{
    public class __Schema : GraphQLObjectType
    {
        public __Schema()
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
                    //OfType = typeof (GraphQLNonNull<__Type>),
                    Fields = new IGraphQLFieldType[]
                    {
                        new GraphQLFieldScalarType
                        {
                            Name = "kind",
                            Description = "The type that query operations will be rooted at.",
                            //OfType = new GraphQLNonNull<__TypeKind>>(),
                            Resolve = context => context.Schema.Query.AsIntrospective()["kind"].Name
                        },
                        new GraphQLFieldScalarType
                        {
                            Name = "name",
                            //OfType = typeof (GraphQLString),
                            Resolve = context => context.Schema.Query.Name
                        }
                    }.ToList(),
                    Resolve = context => context.Schema.Query
                },
                new GraphQLFieldObjectType
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
                    Resolve = schema => schema.GetSubscriptionType()
                },
                new GraphQLSchemaFieldType
                {
                    Name = "directives",
                    Description = "A list of all directives supported by this server.",
                    OfType = typeof (GraphQLNonNull<__Type>),
                    Resolve = schema => schema.GetDirectives()
                }
            });
            Resolve = context => context.Schema;
        }

        public string Kind { get; private set; }

    }
}
