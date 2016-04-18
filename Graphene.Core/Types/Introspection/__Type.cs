using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace Graphene.Core.Types.Introspection
{
    public class __Type : GraphQLObjectType
    {
        public __Type()
        {
            Name = "__Type";
            Description = @"The fundamental unit of any GraphQL Schema is the type. There are " +
                          "many kinds of types in GraphQL as represented by the `__TypeKind` enum." +
                          "\n\nDepending on the kind of a type, certain fields describe " +
                          "information about that type. Scalar types provide no information " +
                          "beyond a name and description, while Enum types provide their values. " +
                          "Object and Interface types provide the fields they describe. Abstract " +
                          "types, Union and Interface, provide the Object types possible " +
                          "at runtime. List and NonNull types compose other types.";
            Fields = new IGraphQLFieldType[]
            {
                new GraphQLScalarField<IGraphQLType, string>
                {
                    Name = "kind",
                    //Description = "The type that query operations will be rooted at.",
                    Type = new GraphQLEnum { Name = "__TypeKind"},
                    Resolve = context => context.Source.Kind
                },
                new GraphQLScalarField<IGraphQLType, string>
                {
                    Name = "name",
                    Type = new GraphQLString(),
                    Resolve = context => context.Source.Name
                },
                new GraphQLScalarField<IGraphQLType, string>
                {
                    Name = "description",
                    Type = new GraphQLString(),
                    Resolve = context => context.Source.Description
                },
                new GraphQLList<IGraphQLType, IGraphQLFieldType>
                {
                    Name = "fields",
                    OfType = new[] {"GraphQLSchemaList", "__Field"},
                    GraphQLObjectType = () => new __Field(),
                    Type = new __Field(),
                    Arguments = new [] 
                    {
                      new GraphQLArgument
                      {
                          Name = "includeDeprecated",
                          Type = new GraphQLBoolean(),
                          DefaultValue = "false"
                      }                        
                    },
                    Resolve = context =>
                    {
                        if (context.Source is GraphQLObjectType)
                        {
                            return ((GraphQLObjectType) context.Source).Fields;
                        }
                        return null;
                    }
                },
                new GraphQLScalarField<IGraphQLType, string[]>
                {
                    Name = "interfaces",
                    Type = new GraphQLString(),
                    //OfType = typeof (GraphQLSchemaList<GraphQLNonNull<__Type>>),
                    Resolve = context => context.Source.Kind != "OBJECT" ? null : new string[0]
                },
                new GraphQLObjectField<IGraphQLType, string>
                {
                    Name = "possibleTypes",
                    GraphQLObjectType = () => new __Type(),
                    //OfType = typeof (GraphQLSchemaList<GraphQLNonNull<__Type>>),
                    Resolve = context => null
                },
                new GraphQLList<IGraphQLType, IGraphQLKind>
                {
                    Name = "enumValues",
                    Type = new __EnumValue(),
                    GraphQLObjectType = () => new __EnumValue(),
                    Arguments = new [] 
                    {
                      new GraphQLArgument
                      {
                          Name = "includeDeprecated",
                          Type = new GraphQLBoolean(),
                          DefaultValue = "false"
                      }                        
                    },
                    Resolve = context =>
                    {
                        if (context.Source is GraphQLEnum)
                        {
                            return ((GraphQLEnum<IGraphQLKind>)context.Source).Values;
                        }
                        return null;
                    }
                },
                new GraphQLScalarField<IGraphQLType, string>
                {
                    Name = "inputFields",
                    Type = new __InputValue(),
                    //Args = "includeDeprecated: { type: GraphQLBoolean, defaultValue: false }",
                    //OfType = typeof (GraphQLSchemaList<GraphQLNonNull<__InputValue>>),
                    Resolve = context => null
                },
                new GraphQLObjectField<IGraphQLType, IGraphQLType>
                {
                    Name = "ofType",
                    GraphQLObjectType = () => new __Type(),
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
            };
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
                        //Description = "The type that query operations will be rooted at.",
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