using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Graphene.Core.FieldTypes;
using Graphene.Core.Types.Object;
using Graphene.Core.Types.Scalar;

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
                    Type = new GraphQLNonNull(new GraphQLEnum { Name = "__TypeKind"}),
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
                    Type = new GraphQLList(new GraphQLNonNull(new __Field())),
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
                    Type = new GraphQLList(new GraphQLNonNull(new GraphQLString())),
                    Resolve = context => context.Source.Kind != "OBJECT" ? null : new string[0]
                },
                new GraphQLObjectField<IGraphQLType, string>
                {
                    Name = "possibleTypes",
                    Type =  new ChainType("List", "NonNull", "__Type"),
                    Resolve = context => null 
                },
                new GraphQLList<IGraphQLType, IGraphQLKind>
                {
                    Name = "enumValues",
                    Type = new GraphQLList(new GraphQLNonNull(new __EnumValue())),
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
                    Type = new GraphQLList(new GraphQLNonNull(new __InputValue())),
                    Resolve = context => null
                },
                new GraphQLObjectField<IGraphQLType, IGraphQLType>
                {
                    Name = "ofType",
                    Type = new ChainType("__Type"),
                    Resolve = context => context.Source == null ? null : context.Source.OfType
                }
            };
        }

        //private static GraphQLObjectType GraphQLObjectType()
        //{
        //    var ofTypeType = new GraphQLObjectType
        //    {
        //        Fields = new IGraphQLFieldType[]
        //        {
        //            new GraphQLScalarField<IGraphQLType, string>
        //            {
        //                Name = "kind",
        //                //Description = "The type that query operations will be rooted at.",
        //                Resolve = context => context.Source.Kind
        //            },
        //            new GraphQLScalarField<IGraphQLType, string>
        //            {
        //                Name = "name",
        //                Resolve = context => context.Source.Name
        //            },
        //            new GraphQLScalarField<IGraphQLType, string>
        //            {
        //                Name = "description",
        //                Resolve = context => context.Source.Description
        //            },
        //            new GraphQLObjectField<IGraphQLType, IGraphQLType>
        //            {
        //                Name = "ofType",
        //                Resolve = context =>
        //                {
        //                    if (context.Source == null ||
        //                        context.Source.OfType == null)
        //                    {
        //                        return null;
        //                    }
        //                    return new GraphQLBoolean();
        //                }
        //            }
        //        }
        //    };
        //    return ofTypeType;
        //}
    }
}