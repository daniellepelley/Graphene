using Graphene.Core.FieldTypes;
using Graphene.Core.Types.Object;

namespace Graphene.Core.Types.Introspection
{
    public class __Directive : GraphQLObjectType
    {
        private ITypeList _typeList;

        public __Directive(ITypeList typeList)
        {
            _typeList = typeList;
            Name = "__Directive";
            Description =
                @"A Directive provides a way to describe alternate runtime execution and type validation behavior in a GraphQL document." +
                @"In some cases, you need to provide options to alter GraphQL�s execution behavior in ways field arguments will not suffice, such as conditionally including or skipping a field. Directives provide this by describing additional information to the executor.";

            Fields = new IGraphQLFieldType[]
            {
                new GraphQLScalarField<GraphQLDirective, string>
                {
                    Name = "name",
                    Type = new[] {"NonNull", "String"},
                    Resolve = context => context.Source.Name
                },
                new GraphQLScalarField<GraphQLDirective, string>
                {
                    Name = "description",
                    Type = new[] {"String"},
                    Resolve = context => context.Source.Description
                },
                new GraphQLList<GraphQLDirective, IGraphQLArgument>
                {
                    Name = "args",
                    Type = new[] {"NonNull", "List", "NonNull", "__InputValue"},
                    Resolve = context => context.Source.Arguments
                },
                new GraphQLScalarField<GraphQLDirective, bool>
                {
                    Name = "onOperation",
                    Type = new[] {"NonNull", "Boolean"},
                    Resolve = context => context.Source.OnOperation
                },
                new GraphQLScalarField<GraphQLDirective, bool>
                {
                    Name = "onFragment",
                    Type = new[] {"NonNull", "Boolean"},
                    Resolve = context => context.Source.OnFragment
                },
                new GraphQLScalarField<GraphQLDirective, bool>
                {
                    Name = "onField",
                    Type = new[] {"NonNull", "Boolean"},
                    Resolve = context => context.Source.OnField
                }
            };
        }
    }
}