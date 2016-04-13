using System.Linq;
using System.Runtime.Remoting.Messaging;
using Graphene.Core.FieldTypes;

namespace Graphene.Core.Types.Introspection
{
    public class __Type : GraphQLObjectType
    {
        private readonly IGraphQLType[] _types;

        public __Type(IGraphQLType[] types)
            : this()
        {
            _types = types;
        }

        public __Type()
        {
            Name = "__type";
            Description = @"The fundamental unit of any GraphQL Schema is the type. There are " +
                          "many kinds of types in GraphQL as represented by the `__TypeKind` enum." +
                          "\n\nDepending on the kind of a type, certain fields describe " +
                          "information about that type. Scalar types provide no information " +
                          "beyond a name and description, while Enum types provide their values. " +
                          "Object and Interface types provide the fields they describe. Abstract " +
                          "types, Union and Interface, provide the Object types possible " +
                          "at runtime. List and NonNull types compose other types.'";

            Fields = new IGraphQLFieldType[]
            {
                new GraphQLFieldScalarType
                {
                    Name = "kind",
                    Description = "The type that query operations will be rooted at.",
                    Resolve = context => ((IGraphQLType)context.Source).Kind
                },
                new GraphQLFieldScalarType
                {
                    Name = "name",
                    Resolve = context => ((IGraphQLType)context.Source).Name
                },
                new GraphQLFieldScalarType
                {
                    Name = "description",
                    Resolve = context => ((IGraphQLType)context.Source).Description
                },
                new GraphQLSchemaFieldType
                {
                    Name = "fields",
                    OfType = typeof (GraphQLList<GraphQLNonNull<__Field>>),
                    Args = "includeDeprecated: { type: GraphQLBoolean, defaultValue: false }",
                    Resolve = schema => string.Empty
                },
                new GraphQLSchemaFieldType
                {
                    Name = "interfaces",
                    OfType = typeof (GraphQLList<GraphQLNonNull<__Type>>),
                    Resolve = schema => string.Empty
                },
                new GraphQLSchemaFieldType
                {
                    Name = "possibleTypes",
                    OfType = typeof (GraphQLList<GraphQLNonNull<__Type>>),
                    Resolve = schema => string.Empty
                },
                new GraphQLSchemaFieldType
                {
                    Name = "possibleTypes",
                    Args = "includeDeprecated: { type: GraphQLBoolean, defaultValue: false }",
                    OfType = typeof (GraphQLList<GraphQLNonNull<__EnumValue>>),
                    Resolve = schema => string.Empty
                },
                new GraphQLSchemaFieldType
                {
                    Name = "inputFields",
                    Args = "includeDeprecated: { type: GraphQLBoolean, defaultValue: false }",
                    OfType = typeof (GraphQLList<GraphQLNonNull<__InputValue>>),
                    Resolve = schema => string.Empty
                }
            }.ToList();
            Resolve =
                context =>
                    _types.FirstOrDefault(
                        x => !context.Arguments.ContainsKey("name") || x.Name == context.Arguments["name"].ToString());
        }

        public string Kind { get; private set; }
    }
}