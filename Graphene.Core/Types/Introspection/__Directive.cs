using Graphene.Core.FieldTypes;
using Graphene.Core.Types.Object;
using Graphene.Core.Types.Scalar;

namespace Graphene.Core.Types.Introspection
{
    public class __Directive: GraphQLObjectType
    {
        private ITypeList _typeList;

        public __Directive(ITypeList typeList)
        {
            _typeList = typeList;
            Name = "__Directive";
            Description =
                @"A Directive provides a way to describe alternate runtime execution and type validation behavior in a GraphQL document.
In some cases, you need to provide options to alter GraphQL’s execution behavior in ways field arguments will not suffice, such as conditionally including or skipping a field. Directives provide this by describing additional information to the executor.";

            Fields = new IGraphQLFieldType[]
            {
                new GraphQLScalarField<IGraphQLFieldType, string>
                {
                    Name = "name",
                    Type = new ChainType(_typeList, "NonNull", "String"),
                    Resolve = context => context.Source.Name
                },
                new GraphQLScalarField<IGraphQLFieldType, string>
                {
                    Name = "description",
                    Type = new ChainType(_typeList, "String"),
                    Resolve = context => context.Source.Description
                },
                new GraphQLList<IGraphQLFieldType, IGraphQLArgument>
                {
                    Name = "args",
                    Type = new ChainType(_typeList, "NonNull", "List", "NonNull", "__InputValue"),
                    Resolve = context => context.Source.Arguments
                },
                new GraphQLScalarField<IGraphQLFieldType, bool>
                {
                    Name = "onOperation",
                    Type = new ChainType(_typeList, "NonNull", "Boolean"),
                    Resolve = context => false
                },                new GraphQLScalarField<IGraphQLFieldType, bool>
                {
                    Name = "onFragment",
                    Type = new ChainType(_typeList, "NonNull", "Boolean"),
                    Resolve = context => false
                },
                new GraphQLScalarField<IGraphQLFieldType, bool>
                {
                    Name = "onField",
                    Type = new ChainType(_typeList, "NonNull", "Boolean"),
                    Resolve = context => false
                }
            };
        }
    }
}