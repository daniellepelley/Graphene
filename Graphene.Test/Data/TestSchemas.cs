using System;
using System.Linq;
using Graphene.Core.Types;
using Graphene.Core.Types.Introspection;

namespace Graphene.Test.Data
{
    public static class TestSchemas
    {
        public static GraphQLSchema UserSchema()
        {
            var userType = CreateUserType();

            var schema = new GraphQLSchema
            {
                Query = new GraphQLObjectField<User>
                {
                    Name = "user",
                    Resolve =
                        context =>
                            Test.Data.Data.GetData()
                                .FirstOrDefault(
                                    x =>
                                        !context.Arguments.ContainsKey("id") ||
                                        x.Id == Convert.ToInt32(context.Arguments["id"])),
                    OfType = new[] {"user"},
                    GraphQLObjectType = () => userType
                }
            };
            return schema;
        }

        public static GraphQLObjectType CreateUserType()
        {
            var userType = new GraphQLObjectType
            {
                Name = "User",
                Description = "This is a user",
                Fields = new IGraphQLFieldType[]
                {
                    new GraphQLScalarField<User, int>
                    {
                        Name = "id",
                        Resolve = context => context.Source.Id
                    },
                    new GraphQLScalarField<User, string>
                    {
                        Name = "name",
                        Resolve = context => context.Source.Name
                    },
                    new GraphQLObjectField<User, Boss>
                    {
                        Name = "boss",
                        Resolve = context => context.Source.Boss,
                        GraphQLObjectType = () => CreateBossType()
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
                        Resolve = context => context.Source.Id
                    },
                    new GraphQLScalarField<Boss, string>
                    {
                        Name = "name",
                        Resolve = context => context.Source.Name
                    }
                }
            };
            return bossType;
        }

        public static GraphQLSchema CreateIntrospectionSchema()
        {
            return new GraphQLSchema
            {
                Query = new GraphQLObjectField<object>
                {
                    Name = "IntrospectionQuery",
                    GraphQLObjectType = () => new GraphQLObjectType
                    {
                        Fields = new IGraphQLFieldType[]
                        {
                            new GraphQLObjectField<object, GraphQLSchema>
                            {
                                Name = "__schema",
                                GraphQLObjectType = () => new __Schema(),
                                Resolve = _ => UserSchema()
                            }
                        }
                    },
                    Resolve = _ => UserSchema()
                }
            };
        }



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