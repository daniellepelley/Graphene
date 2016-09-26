using System;
using System.Linq;
using Graphene.Core.Constants;
using Graphene.Core.FieldTypes;
using Graphene.Core.Types;
using Graphene.Core.Types.Introspection;
using Graphene.Core.Types.Object;
using Graphene.Core.Types.Scalar;
using Graphene.Example.Data.EntityFramework;
using Graphene.Spike;
using Graphene.TypeProvider;

namespace Graphene.Test.Data
{
    public static class TestSchemas
    {
        public static GraphQLSchema UserSchema()
        {
            return UserSchema(CreateUserType());
        }

        public static GraphQLSchema UserSchemaWithBoss()
        {
            return UserSchema(CreateUserTypeWithBoss());
        }

        public static GraphQLSchema EntityFrameworkSchema()
        {
            var builder = new SchemaBuilder();

            var context = new GraphQLDemoDataContext();
            var field = new FieldBuilder().CreateAggregateRoot(context.Companies, "companies", builder.TypeList);

            return builder.WithField(field).Build();
        }

        private static ITypeList _typeList;

        public static ITypeList GetTypeList()
        {
            if (_typeList == null)
            {
                _typeList = new TypeList();
                _typeList.AddType("__Schema", new __Schema(_typeList));
                _typeList.AddType("__Type", new __Type(_typeList));
                _typeList.AddType("__TypeKind", new __TypeKind());
                _typeList.AddType("GraphQLBoolean", new GraphQLBoolean());
                _typeList.AddType("__Field", new __Field(_typeList));
                _typeList.AddType("__InputValue", new __InputValue(_typeList));
                _typeList.AddType("__EnumValue", new __EnumValue(_typeList));
                _typeList.AddType("__Directive", new __Directive(_typeList));
                _typeList.AddType(GraphQLTypes.NonNull, new GraphQLNonNull());
                _typeList.AddType(GraphQLTypes.List, new GraphQLList());
                _typeList.AddType(GraphQLTypes.String, new GraphQLString());
                _typeList.AddType(GraphQLTypes.Boolean, new GraphQLBoolean());
                _typeList.AddType("GraphQLEnum", new GraphQLEnum());
                _typeList.AddType("Int", new GraphQLInt());
            }
            return _typeList;
        }

        private static GraphQLSchema UserSchema(GraphQLObjectType userType)
        {
            var typeList = new TypeList();

            var schema = new GraphQLSchema(typeList)
            {
                QueryType = new GraphQLObjectType
                {
                    Name = "Root",
                    Fields = new IGraphQLFieldType[]
                    {
                        new GraphQLObjectField<User>
                        {
                            Name = "user",
                            Arguments = new[]
                            {
                                new GraphQLArgument {Name = "id", Type = new [] { "String" }}
                            },
                            Type = new []{"User"},
                            Resolve =
                                context =>
                                    Data.GetData()
                                        .FirstOrDefault(
                                            x =>
                                                context.Arguments.All(arg => arg.Name != "id") ||
                                                x.Id ==
                                                Convert.ToInt32(context.Arguments.First(arg => arg.Name == "id").Value)),
                        }
                    }
                }
            };

            typeList.AddType("Root", schema.QueryType);
            typeList.AddType(GraphQLTypes.String, new GraphQLString());
            typeList.AddType("User", userType);
            typeList.AddType("__Schema", new __Schema(typeList));
            typeList.AddType("__Type", new __Type(typeList));
            typeList.AddType("Enum", new GraphQLEnum<IGraphQLKind> { Name = "__TypeKind" });
            typeList.AddType("__TypeKind", new __TypeKind());
            typeList.AddType("__Field", new __Field(typeList));
            typeList.AddType("__InputValue", new __InputValue(typeList));
            typeList.AddType("__EnumValue", new __EnumValue(typeList));
            typeList.AddType("__Directive", new __Directive(typeList));
            typeList.AddType(GraphQLTypes.NonNull, new GraphQLNonNull());
            typeList.AddType(GraphQLTypes.List, new GraphQLList());
            typeList.AddType("GraphQLEnum", new GraphQLEnum());
            typeList.AddType(GraphQLTypes.Boolean, new GraphQLBoolean());
            typeList.AddType("Int", new GraphQLInt());
            typeList.AddType("Boss", CreateBossType());
            //schema.Types.AddType("QueryType", schema.QueryType.Type);
            //schema.Types.AddType("GraphQLString", new GraphQLString());
            //schema.Types.AddType("User", userType);
            //schema.Types.AddType("__Schema", new __Schema(schema.Types));
            //schema.Types.AddType("__Type", new __Type(schema.Types));
            //schema.Types.AddType("__TypeKind", new __TypeKind());
            //schema.Types.AddType("GraphQLBoolean", new GraphQLBoolean());
            //schema.Types.AddType("__Field", new __Field(schema.Types));
            //schema.Types.AddType("__InputValue", new __InputValue(schema.Types));
            //schema.Types.AddType("__EnumValue", new __EnumValue());
            //schema.Types.AddType("__Directive", new __Directive(schema.Types));
            //schema.Types.AddType(GraphQLTypes.NonNull, new GraphQLNonNull(new ChainType(schema.Types)));
            //schema.Types.AddType(GraphQLTypes.List, new GraphQLList(new ChainType(schema.Types)));
            //schema.Types.AddType(GraphQLTypes.String, new GraphQLString());

            return schema;
        }

        public static GraphQLObjectType CreateUserType()
        {
            var userType = new GraphQLObjectType
            {
                Name = "User",
                Fields = new IGraphQLFieldType[]
                {
                    new GraphQLScalarField<User, int>
                    {
                        Name = "id",
                        Resolve = context => context.Source.Id,
                        Type = new []{GraphQLTypes.String}
                    },
                    new GraphQLScalarField<User, string>
                    {
                        Name = "name",
                        Resolve = context => context.Source.Name,
                        Type = new []{GraphQLTypes.String}
                    },
                    //new GraphQLObjectField<User, Boss>
                    //{
                    //    Name = "boss",
                    //    Resolve = context => context.Source.Boss,
                    //    GraphQLObjectType = () => CreateBossType()
                    //}
                }
            };
            return userType;
        }

        private static GraphQLObjectType CreateUserTypeWithBoss()
        {
            var userType = new GraphQLObjectType
            {
                Name = "User",
                Fields = new IGraphQLFieldType[]
                {
                    new GraphQLScalarField<User, int>
                    {
                        Name = "id",
                        Resolve = context => context.Source.Id,
                        Type = new []{GraphQLTypes.String}
                    },
                    new GraphQLScalarField<User, string>
                    {
                        Name = "name",
                        Resolve = context => context.Source.Name,
                        Type = new []{GraphQLTypes.String}
                    },
                    new GraphQLObjectField<User, Boss>
                    {
                        Name = "boss",
                        Resolve = context => context.Source.Boss,
                        Type =  new [] { CreateBossType().Name }
                    }
                }
            };

            return userType;
        }

        public static GraphQLObjectType CreateBossType()
        {
            var bossType = new GraphQLObjectType
            {
                Name = "Boss",
                Description = "This is the boss",
                Fields = new IGraphQLFieldType[]
                {
                    new GraphQLScalarField<Boss, int>
                    {
                        Name = "id",
                        Type = new [] {"Int"},
                        Resolve = context => context.Source.Id
                    },
                    new GraphQLScalarField<Boss, string>
                    {
                        Name = "name",
                        Type = new [] {GraphQLTypes.String},
                        Resolve = context => context.Source.Name
                    }
                }
            };
            return bossType;
        }

        //public static GraphQLSchema CreateIntrospectionSchema()
        //{
        //    return UserSchema();
        //}

        public static string GraphiQlQueryWithFragments
        {
            get { return @"
                query IntrospectionQuery {
    __schema {
      queryType { name }
      mutationType { name }
      subscriptionType { name }
      types {
        ...FullType
      }
      directives {
        name
        description
        args {
          ...InputValue
        }
        onOperation
        onFragment
        onField
      }
    }
  }

  fragment FullType on __Type {
    kind
    name
    description
    fields(includeDeprecated: true) {
      name
      description
      args {
        ...InputValue
      }
      type {
        ...TypeRef
      }
      isDeprecated
      deprecationReason
    }
    inputFields {
      ...InputValue
    }
    interfaces {
      ...TypeRef
    }
    enumValues(includeDeprecated: true) {
      name
      description
      isDeprecated
      deprecationReason
    }
    possibleTypes {
      ...TypeRef
    }
  }

  fragment InputValue on __InputValue {
    name
    description
    type { ...TypeRef }
    defaultValue
  }

  fragment TypeRef on __Type {
    kind
    name
    ofType {
      kind
      name
      ofType {
        kind
        name
        ofType {
          kind
          name
        }
      }
    }
  }"; }
        }


        public static string GraphiQlQueryWithoutFragments
        {
            get { return @"
                query IntrospectionQuery {
    __schema {
      queryType { name }
      mutationType { name }
      subscriptionType { name }
      types {
   kind
    name
    description
    fields(includeDeprecated: true) {
      name
      description
      args {
    name
    description
    type {     kind
    name
    ofType {
      kind
      name
      ofType {
        kind
        name
        ofType {
          kind
          name
        }
      }
    } }
    defaultValue
      }
      type {
            kind
    name
    ofType {
      kind
      name
      ofType {
        kind
        name
        ofType {
          kind
          name
        }
      }
    }
      }
      isDeprecated
      deprecationReason
    }
    inputFields {
    name
    description
    type {     kind
    name
    ofType {
      kind
      name
      ofType {
        kind
        name
        ofType {
          kind
          name
        }
      }
    } }
    defaultValue
    }
    interfaces {
          kind
    name
    ofType {
      kind
      name
      ofType {
        kind
        name
        ofType {
          kind
          name
        }
      }
    }
    }
    enumValues(includeDeprecated: true) {
      name
      description
      isDeprecated
      deprecationReason
    }
    possibleTypes {
          kind
    name
    ofType {
      kind
      name
      ofType {
        kind
        name
        ofType {
          kind
          name
        }
      }
    }
    }
      }
      directives {
        name
        description
        args {    name
    description
    type {     kind
    name
    ofType {
      kind
      name
      ofType {
        kind
        name
        ofType {
          kind
          name
        }
      }
    } }
    defaultValue
        }
        onOperation
        onFragment
        onField
      }
    }
  }";
            }
        }
    }
}