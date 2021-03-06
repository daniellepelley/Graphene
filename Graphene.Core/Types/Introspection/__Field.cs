using Graphene.Core.Constants;
using Graphene.Core.FieldTypes;
using Graphene.Core.Types.Object;
using Graphene.Core.Types.Scalar;

namespace Graphene.Core.Types.Introspection
{
    public class __Field : GraphQLObjectType
    {
        private ITypeList _typeList;

        public __Field(ITypeList typeList)
        {
            _typeList = typeList;
            Name = "__Field";
            Description = @"Object and Interface types are described by a list of Fields, each of " +
                          "which has a name, potentially a list of arguments, and a return type.";

            Fields = new IGraphQLFieldType[]
            {
                new GraphQLScalarField<IGraphQLFieldType, string>
                {
                    Name = "name",
                    Type = new[] {GraphQLTypes.NonNull, GraphQLTypes.String},
                    Resolve = context => context.Source.Name
                },
                new GraphQLScalarField<IGraphQLFieldType, string>
                {
                    Name = "description",
                    Type = new[] {GraphQLTypes.String},
                    Resolve = context => context.Source.Description
                },
                new GraphQLList<IGraphQLFieldType, IGraphQLArgument>
                {
                    Name = "args",
                    Type = new[] {GraphQLTypes.NonNull, GraphQLTypes.List, GraphQLTypes.NonNull, "__InputValue"},
                    Resolve = context => context.Source.Arguments
                },
                new GraphQLObjectField<IGraphQLFieldType, IGraphQLType>
                {
                    Name = "type",
                    Type = new[] {GraphQLTypes.NonNull, "__Type"},
                    Resolve = context => new ChainType(typeList, context.Source.Type)
                },
                new GraphQLScalarField<IGraphQLFieldType, bool>
                {
                    Name = "isDeprecated",
                    Type = new[] {GraphQLTypes.NonNull, GraphQLTypes.Boolean},
                    Resolve = context => context.Source.IsDeprecated
                },
                new GraphQLScalarField<IGraphQLFieldType, string>
                {
                    Name = "deprecationReason",
                    Type = new[] {GraphQLTypes.String},
                    Resolve = context => context.Source.DeprecationReason
                }
            };
        }
    }
}