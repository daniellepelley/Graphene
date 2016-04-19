using Graphene.Core.FieldTypes;
using Graphene.Core.Types.Object;
using Graphene.Core.Types.Scalar;

namespace Graphene.Core.Types.Introspection
{
    public class __InputValue : GraphQLObjectType
    {
        private readonly ITypeList _typeList;

        public __InputValue(ITypeList typeList)
        {
            _typeList = typeList;
            Name = "__InputValue";
            Description = @"Arguments provided to Fields or Directives and the input fields of an " +
                          "InputObject are represented as Input Values which describe their type " +
                          "and optionally a default value.";

            Fields = new IGraphQLFieldType[]
            {
                new GraphQLScalarField<IGraphQLArgument, string>
                {
                    Name = "name",
                    Type = new ChainType(_typeList, "NonNull", "String"),
                    Resolve = context => context.Source.Name
                },
                new GraphQLScalarField<IGraphQLArgument, string>
                {
                    Name = "description",
                    Type = new ChainType(_typeList, "String"),
                    Resolve = context => context.Source.Description
                },
                new GraphQLObjectField<IGraphQLArgument, IGraphQLType>
                {
                    Name = "type",
                    Type = new ChainType(_typeList, "NonNull", "__Type"),
                    Resolve = context => context.Source.Type
                },
                new GraphQLScalarField<IGraphQLArgument, string>
                {
                    Name = "defaultValue",
                    Description = "A GraphQL-formatted string representing the default value for this input value.",
                    Type = new ChainType(_typeList, "String"),
                    Resolve = context => context.Source.DefaultValue
                }
            };
        }
    }
}