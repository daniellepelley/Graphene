using System.Diagnostics;
using System.Linq;
using Graphene.Core.FieldTypes;
using Graphene.Core.Types.Object;
using Graphene.Core.Types.Scalar;

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
                    Type = new GraphQLNonNull(new GraphQLString()),
                    Resolve = context => context.Source.Name
                },
                new GraphQLScalarField<IGraphQLFieldType, string>
                {
                    Name = "description",
                    Type = new GraphQLString(),
                    Resolve = context => context.Source.Description
                },
                new GraphQLList<IGraphQLFieldType, IGraphQLArgument>
                {
                    Name = "args",
                    Type = new GraphQLNonNull(new GraphQLList(new GraphQLNonNull(new __InputValue()))),
                    Resolve = context => context.Source.Arguments
                },
                new GraphQLObjectField<IGraphQLFieldType, IGraphQLType>
                {
                    Name = "type",
                    Type = new ChainType("NonNull", "__Type"),
                    Resolve = context => context.Source.Type
                },
                new GraphQLScalarField<IGraphQLFieldType, bool>
                {
                    Name = "isDeprecated",
                    Type = new GraphQLNonNull(new GraphQLBoolean()),
                    Resolve = context => context.Source.IsDeprecated
                },
                new GraphQLScalarField<IGraphQLFieldType, string>
                {
                    Name = "deprecationReason",
                    Type = new GraphQLString(),
                    Resolve = context => context.Source.DeprecationReason
                }
            };
        }
    }
}