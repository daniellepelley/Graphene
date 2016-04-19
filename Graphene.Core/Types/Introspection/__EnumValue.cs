using Graphene.Core.FieldTypes;
using Graphene.Core.Types.Object;

namespace Graphene.Core.Types.Introspection
{
    public class __EnumValue : GraphQLObjectType
    {
        private readonly ITypeList _typeList;

        public __EnumValue(ITypeList typeList)
        {
            _typeList = typeList;
            Name = "__EnumValue";
            Description = @"One possible value for a given Enum. Enum values are unique values, not " +
                          "a placeholder for a string or numeric value. However an Enum value is " +
                          "returned in a JSON response as a string.";
            Fields = new IGraphQLFieldType[]
            {
                new GraphQLScalarField<IGraphQLKind, string>
                {
                    Name = "name",
                    Type= new ChainType(_typeList, "NonNull", "String"),
                    Resolve = context => context.Source.Name
                },
                new GraphQLScalarField<IGraphQLKind, string>
                {
                    Name = "description",
                    Type= new ChainType(_typeList, "String"),
                    Resolve = context => context.Source.Description
                },
                new GraphQLScalarField<IGraphQLKind, bool>
                {
                    Name = "isDeprecated",
                    Type= new ChainType(_typeList, "NonNull", "Boolean"),
                    Resolve = context => false
                },
                new GraphQLScalarField<IGraphQLKind, string>
                {
                    Name = "deprecationReason",
                    Type= new ChainType(_typeList, "String"),
                    Resolve = context => null
                }
            };
        }
    }
}