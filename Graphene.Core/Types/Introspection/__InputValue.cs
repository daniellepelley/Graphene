using Graphene.Core.FieldTypes;
using Graphene.Core.Types.Object;
using Graphene.Core.Types.Scalar;

namespace Graphene.Core.Types.Introspection
{
    public class __InputValue : GraphQLObjectType
    {
        public __InputValue()
        {
            Name = "__InputValue";
            Description = @"Arguments provided to Fields or Directives and the input fields of an " +
                          "InputObject are represented as Input Values which describe their type " +
                          "and optionally a default value.";

            Fields = new IGraphQLFieldType[]
            {
                new GraphQLScalarField<IGraphQLArgument, string>
                {
                    Name = "name",
                    Type = new GraphQLNonNull(new GraphQLString()),
                    OfType = new[] {"GraphQLString"},
                    Resolve = context => context.Source.Name
                },
                new GraphQLScalarField<IGraphQLArgument, string>
                {
                    Name = "description",
                    Type = new GraphQLString(),
                    OfType = new[] {"GraphQLString"},
                    Resolve = context => context.Source.Description
                },
                new GraphQLObjectField<IGraphQLArgument, IGraphQLType>
                {
                    Name = "type",
                    Type = new ChainType("NonNull", "__Type"),
                    //GraphQLObjectType = () => new __Type(),
                    Resolve = context => context.Source.Type
                },
                new GraphQLScalarField<IGraphQLArgument, string>
                {
                    Name = "defaultValue",
                    Description = "A GraphQL-formatted string representing the default value for this input value.",
                    Type= new GraphQLString(),
                    OfType = new[] {"GraphQLString"},
                    Resolve = context => context.Source.DefaultValue
                }
            };
        }
    }

    public interface IGraphQLArgument
    {
        string Name { get; set; }
        string Description { get; set; }
        IGraphQLType Type { get; set; }
        string DefaultValue { get; set; }
    }

    public class GraphQLArgument : IGraphQLArgument
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IGraphQLType Type { get; set; }
        public string DefaultValue { get; set; }
    }
}