using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace Graphene.Core.Types.Introspection
{
    public class __Type : GraphQLObjectType
    {
        public __Type()
        {
            //var ofTypeType = GraphQLObjectType();

            //((GraphQLObjectField<IGraphQLType, IGraphQLType>) ofTypeType.Fields.ElementAt(3)).GraphQLObjectType =
            //    ofTypeType;

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
                    //GraphQLObjectType = ofTypeType,
                    Resolve = context =>
                    {
                        if (context.Source == null)
                        {
                            return null;
                        }

                        if (context.Source.OfType == null)
                        {
                            return null;
                        }
                        return new GraphQLBoolean();
                    }
                },
                new GraphQLList<IGraphQLType, IGraphQLFieldType>
                {
                    Name = "fields",
                    OfType = new[] {"GraphQLSchemaList", "__Field"},
                    GraphQLObjectType = () => new __Field(),
                    Resolve = context =>
                    {
                        if (context.Source is GraphQLObjectType)
                        {
                            return ((GraphQLObjectType) context.Source).Fields;
                        }
                        return null;
                    }
                }

                //new GraphQLSchemaFieldType
                //{
                //    Name = "interfaces",
                //    OfType = typeof (GraphQLSchemaList<GraphQLNonNull<__Type>>),
                //    Resolve = schema => string.Empty
                //},
                //new GraphQLSchemaFieldType
                //{
                //    Name = "possibleTypes",
                //    OfType = typeof (GraphQLSchemaList<GraphQLNonNull<__Type>>),
                //    Resolve = schema => string.Empty
                //},
                //new GraphQLSchemaFieldType
                //{
                //    Name = "possibleTypes",
                //    Args = "includeDeprecated: { type: GraphQLBoolean, defaultValue: false }",
                //    OfType = typeof (GraphQLSchemaList<GraphQLNonNull<__EnumValue>>),
                //    Resolve = schema => string.Empty
                //},
                //new GraphQLSchemaFieldType
                //{
                //    Name = "inputFields",
                //    Args = "includeDeprecated: { type: GraphQLBoolean, defaultValue: false }",
                //    OfType = typeof (GraphQLSchemaList<GraphQLNonNull<__InputValue>>),
                //    Resolve = schema => string.Empty
                //}
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
        }

        private static GraphQLObjectType GraphQLObjectType()
        {
            var ofTypeType = new GraphQLObjectType
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
                            return new GraphQLBoolean();
                        }
                    }
                }
            };
            return ofTypeType;
        }
    }
}