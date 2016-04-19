using Graphene.Core.FieldTypes;
using Graphene.Core.Types.Object;
using Graphene.Core.Types.Scalar;
using Newtonsoft.Json;

namespace Graphene.Core.Types.Introspection
{
    public class __EnumValue : GraphQLObjectType
    {
        public __EnumValue()
        {
            Name = "__EnumValue";
            Description = @"One possible value for a given Enum. Enum values are unique values, not " +
                          "a placeholder for a string or numeric value. However an Enum value is " +
                          "returned in a JSON response as a string.";
            Fields = new IGraphQLFieldType[]
            {
                new GraphQLScalarField<IGraphQLKind, string>
                {
                    Name = "name",
                    Type= new GraphQLNonNull(new GraphQLString()),
                    Resolve = context => context.Source.Name
                },
                new GraphQLScalarField<IGraphQLKind, string>
                {
                    Name = "description",
                    Type= new GraphQLString(),
                    Resolve = context => context.Source.Description
                },
                new GraphQLScalarField<IGraphQLKind, bool>
                {
                    Name = "isDeprecated",
                    Type= new GraphQLNonNull(new GraphQLBoolean()),
                    Resolve = context => false
                },
                new GraphQLScalarField<IGraphQLKind, string>
                {
                    Name = "deprecationReason",
                    Type= new GraphQLString(),
                    Resolve = context => null
                }
            };
        }
    }
}