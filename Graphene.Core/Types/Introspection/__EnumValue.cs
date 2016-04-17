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
                    Type= new GraphQLString(),
                    //OfType = typeof (GraphQLNonNull<GraphQLString>),
                    Resolve = context => context.Source.Name
                },
                new GraphQLScalarField<IGraphQLKind, string>
                {
                    Name = "description",
                    Type= new GraphQLString(),
                    //OfType = typeof (GraphQLString),
                    Resolve = context => context.Source.Description
                },
                new GraphQLScalarField<IGraphQLKind, bool>
                {
                    Name = "isDeprecated",
                    Type= new GraphQLBoolean(),
                    //OfType = typeof (GraphQLNonNull<GraphQLString>),
                    Resolve = context => false
                },
                new GraphQLScalarField<IGraphQLKind, string>
                {
                    Name = "deprecationReason",
                    Type= new GraphQLString(),
                    //OfType = typeof (GraphQLString),
                    Resolve = context => null
                }
            };
        }
    }
}