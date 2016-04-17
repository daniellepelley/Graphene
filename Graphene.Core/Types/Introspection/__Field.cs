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
                },
                new GraphQLList<IGraphQLFieldType, IGraphQLArgument>
                {
                    Name = "args",
                    OfType  = new[] {"GraphQLString"},
                    GraphQLObjectType = () => new __InputValue(),
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
                    OfType  = new[] {"GraphQLString"},
                    Resolve = context => false
                },
                new GraphQLScalarField<IGraphQLFieldType, string>
                {
                    Name = "deprecationReason",
                    OfType  = new[] {"GraphQLString"},
                    Resolve = context => null
                }
            };
        }
    }
}