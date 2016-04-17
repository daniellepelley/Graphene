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
                new GraphQLScalarField<IGraphQLType, string>
                {
                    Name = "name",
                    //OfType = typeof (GraphQLNonNull<GraphQLString>),
                    Resolve = schema => string.Empty
                },
                new GraphQLScalarField<IGraphQLType, string>
                {
                    Name = "description",
                    //OfType = typeof (GraphQLString),
                    Resolve = schema => string.Empty
                },
                new GraphQLScalarField<IGraphQLType, bool>
                {
                    Name = "isDeprecated",
                    //OfType = typeof (GraphQLNonNull<GraphQLString>),
                    Resolve = schema => false
                },
                new GraphQLScalarField<IGraphQLType, string>
                {
                    Name = "deprecationReason",
                    //OfType = typeof (GraphQLString),
                    Resolve = schema => string.Empty
                }
            };
        }
    }
}