using Graphene.Core.Constants;
using Graphene.Core.FieldTypes;
using Graphene.Core.Types.Object;
using Graphene.Core.Types.Scalar;

namespace Graphene.Core.Types.Introspection
{
    public class __Type : GraphQLObjectType
    {
        private readonly ITypeList _typeList;

        public __Type(ITypeList typeList)
        {
            _typeList = typeList;

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
                    Type = new [] {GraphQLTypes.NonNull, "GraphQLEnum"},
                    Resolve = context => context.Source.Kind
                },
                new GraphQLScalarField<IGraphQLType, string>
                {
                    Name = "name",
                    Type = new [] {GraphQLTypes.String},
                    Resolve = context => context.Source.Name
                },
                new GraphQLScalarField<IGraphQLType, string>
                {
                    Name = "description",
                    Type = new [] {GraphQLTypes.String},
                    Resolve = context => context.Source.Description
                },
                new GraphQLList<IGraphQLType, IGraphQLFieldType>
                {
                    Name = "fields",
                    Type = new [] {GraphQLTypes.List, GraphQLTypes.NonNull, "__Field"},
                    Arguments = new [] 
                    {
                      new GraphQLArgument
                      {
                          Name = "includeDeprecated",
                          Type = new [] { GraphQLTypes.Boolean },
                          DefaultValue = "false"
                      }                        
                    },
                    Resolve = context =>
                    {
                        var type = context.Source as GraphQLObjectType;
                        return type != null ? type.Fields : null;
                    }
                },
                new GraphQLScalarField<IGraphQLType, string[]>
                {
                    Name = "interfaces",
                    Type = new [] {GraphQLTypes.List, GraphQLTypes.NonNull, GraphQLTypes.__Type},
                    Resolve = context => context.Source.Kind != "OBJECT" ? null : new string[0]
                },
                new GraphQLObjectField<IGraphQLType, string>
                {
                    Name = "possibleTypes",
                    Type =  new [] {GraphQLTypes.List, GraphQLTypes.NonNull, "__Type"},
                    Resolve = context => null 
                },
                new GraphQLList<IGraphQLType, IGraphQLKind>
                {
                    Name = "enumValues",
                    Type = new [] {GraphQLTypes.List, GraphQLTypes.NonNull, "__EnumValue"},
                    Arguments = new [] 
                    {
                      new GraphQLArgument
                      {
                          Name = "includeDeprecated",
                          Type = new [] { GraphQLTypes.Boolean },
                          DefaultValue = "false"
                      }                        
                    },
                    Resolve = context =>
                    {
                        if (context.Source is GraphQLEnum<IGraphQLKind>)
                        {
                            return ((GraphQLEnum<IGraphQLKind>)context.Source).Values;
                        }
                        return null;
                    }
                },
                new GraphQLScalarField<IGraphQLType, string>
                {
                    Name = "inputFields",
                    Type = new [] {GraphQLTypes.List, GraphQLTypes.NonNull, "__InputValue"},
                    Resolve = context => null
                },
                new GraphQLObjectField<IGraphQLType, IGraphQLType>
                {
                    Name = "ofType",
                    Type = new [] {"__Type"},
                    Resolve = context =>
                    {
                        var chainType = context.Source as IGraphQLChainType;

                        if (chainType != null)
                        {
                            return chainType.OfType;
                        }

                        return null;
                    }
                }
            };
        }
    }
}