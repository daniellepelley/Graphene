using System.Diagnostics;
using System.Linq;

namespace Graphene.Core.Types.Introspection
{
    public class __Field : GraphQLObjectType
    {
        public __Field()
        {
            Name = "__Field";
            Description = @"Object and Interface types are described by a list of Fields, each of " +
                          "which has a name, potentially a list of arguments, and a return type.";

            Fields = new IGraphQLFieldType[]
            {
                new GraphQLScalarField<IGraphQLFieldType, string>
                {
                    Name = "name",
                    OfType = new[] {"GraphQLString"},
                    Type = new GraphQLString(),
                    Resolve = context => context.Source.Name
                },
                new GraphQLScalarField<IGraphQLFieldType, string>
                {
                    Name = "description",
                    Type = new GraphQLString(),
                    OfType = new[] {"GraphQLString"},
                    Resolve = context => context.Source.Description
                },
                new GraphQLList<IGraphQLFieldType, IGraphQLArgument>
                {
                    Name = "args",
                    OfType  = new[] {"GraphQLString"},
                    GraphQLObjectType = () => new __InputValue(),
                    Type = new __InputValue(),
                    Resolve = context =>
                    {
                        return context.Source.Arguments;
                    }
                },
                new GraphQLObjectField<IGraphQLFieldType, IGraphQLType>
                {
                    Name = "type",
                    OfType  = new[] {"GraphQLString"},
                    GraphQLObjectType = () => new __Type(),
                    Resolve = context =>
                    {
                        var graphQLScalarField = context.Source as IHasType;

                        if (graphQLScalarField != null)
                        {
                            return graphQLScalarField.Type;
                        }

                        var graphQLObjectField = context.Source as GraphQLObjectFieldBase;

                        if (graphQLObjectField != null)
                        {
                            return graphQLObjectField.GraphQLObjectType();
                        }

                        return null;
                    }
                },
                new GraphQLScalarField<IGraphQLFieldType, bool>
                {
                    Name = "isDeprecated",
                    Type = new GraphQLBoolean(),
                    OfType  = new[] {"GraphQLString"},
                    Resolve = context => false
                },
                new GraphQLScalarField<IGraphQLFieldType, string>
                {
                    Name = "deprecationReason",
                    Type = new GraphQLString(),
                    OfType  = new[] {"GraphQLString"},
                    Resolve = context => null
                }
            };
        }
    }
}