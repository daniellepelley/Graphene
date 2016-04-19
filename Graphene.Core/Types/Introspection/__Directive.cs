using Graphene.Core.FieldTypes;
using Graphene.Core.Types.Object;
using Graphene.Core.Types.Scalar;

namespace Graphene.Core.Types.Introspection
{
    public class __Directive: GraphQLObjectType
    {
        public __Directive()
        {
            Name = "__Directive";
            Description =
                @"A Directive provides a way to describe alternate runtime execution and type validation behavior in a GraphQL document.
In some cases, you need to provide options to alter GraphQL’s execution behavior in ways field arguments will not suffice, such as conditionally including or skipping a field. Directives provide this by describing additional information to the executor.";

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
                new GraphQLScalarField<IGraphQLFieldType, bool>
                {
                    Name = "onOperation",
                    Type = new GraphQLNonNull(new GraphQLBoolean()),
                    Resolve = context => false
                },                new GraphQLScalarField<IGraphQLFieldType, bool>
                {
                    Name = "onFragment",
                    Type = new GraphQLNonNull(new GraphQLBoolean()),
                    Resolve = context => false
                },
                new GraphQLScalarField<IGraphQLFieldType, bool>
                {
                    Name = "onField",
                    Type = new GraphQLNonNull(new GraphQLBoolean()),
                    Resolve = context => false
                }
            };
        }
    }
}