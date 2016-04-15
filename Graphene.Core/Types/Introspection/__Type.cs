using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace Graphene.Core.Types.Introspection
{
    public class __Type : GraphQLObjectField<IGraphQLType>
    {
        private readonly IGraphQLType[] _types;

        public __Type(IGraphQLType[] types)
            : this()
        {
            _types = types;
        }

        public __Type()
        {
            var typeType = new GraphQLObjectType
            {
                Fields = new IGraphQLFieldType[]
                {
                    new GraphQLScalarField<IGraphQLType, string>
                    {
                        Name = "kind",
                        Description = "The type that query operations will be rooted at.",
                        Resolve = context => context.Source.Kind
                    },
                    new GraphQLScalarField<IGraphQLType, string>
                    {
                        Name = "name",
                        Resolve = context => context.Source.Name
                    },
                    new GraphQLScalarField<IGraphQLType, string>
                    {
                        Name = "description",
                        Resolve = context => context.Source.Description
                    },
                    new GraphQLObjectField<IGraphQLType, IGraphQLType>
                    {
                        Name = "ofType",
                        Resolve = context =>
                        {
                            if (context.Source == null ||
                                context.Source.OfType == null)
                            {
                                return null;
                            }
                            return _types.FirstOrDefault(x => x.Name == context.Source.OfType.FirstOrDefault());
                        }
                    },
                    new GraphQLSchemaFieldType
                    {
                        Name = "fields",
                        OfType = typeof (GraphQLSchemaList<GraphQLNonNull<__Field>>),
                        Args = "includeDeprecated: { type: GraphQLBoolean, defaultValue: false }",
                        Resolve = schema => string.Empty
                    },
                    new GraphQLSchemaFieldType
                    {
                        Name = "interfaces",
                        OfType = typeof (GraphQLSchemaList<GraphQLNonNull<__Type>>),
                        Resolve = schema => string.Empty
                    },
                    new GraphQLSchemaFieldType
                    {
                        Name = "possibleTypes",
                        OfType = typeof (GraphQLSchemaList<GraphQLNonNull<__Type>>),
                        Resolve = schema => string.Empty
                    },
                    new GraphQLSchemaFieldType
                    {
                        Name = "possibleTypes",
                        Args = "includeDeprecated: { type: GraphQLBoolean, defaultValue: false }",
                        OfType = typeof (GraphQLSchemaList<GraphQLNonNull<__EnumValue>>),
                        Resolve = schema => string.Empty
                    },
                    new GraphQLSchemaFieldType
                    {
                        Name = "inputFields",
                        Args = "includeDeprecated: { type: GraphQLBoolean, defaultValue: false }",
                        OfType = typeof (GraphQLSchemaList<GraphQLNonNull<__InputValue>>),
                        Resolve = schema => string.Empty
                    }
                }
            };


            Name = "__type";
            Description = @"The fundamental unit of any GraphQL Schema is the type. There are " +
                          "many kinds of types in GraphQL as represented by the `__TypeKind` enum." +
                          "\n\nDepending on the kind of a type, certain fields describe " +
                          "information about that type. Scalar types provide no information " +
                          "beyond a name and description, while Enum types provide their values. " +
                          "Object and Interface types provide the fields they describe. Abstract " +
                          "types, Union and Interface, provide the Object types possible " +
                          "at runtime. List and NonNull types compose other types.'";
            GraphQLObjectType = typeType;
            Resolve =
                context =>
                    _types.FirstOrDefault(
                        x => !context.Arguments.ContainsKey("name") || x.Name == context.Arguments["name"].ToString());
        }
    }
}