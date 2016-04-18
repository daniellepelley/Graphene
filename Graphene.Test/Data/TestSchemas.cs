using System;
using System.Collections.Generic;
using System.Linq;
using Graphene.Core;
using Graphene.Core.Types;
using Graphene.Core.Types.Introspection;

namespace Graphene.Test.Data
{
    public class SchemaBuilder
    {
        private IEnumerable<IGraphQLArgument> _arguments;
        private Func<ResolveObjectContext, User> _resolve;

        public SchemaBuilder()
        {
            _resolve = context =>
                Data.GetData()
                    .FirstOrDefault(
                        x =>
                            context.Arguments.All(arg => arg.Name != "id") ||
                             x.Id == Convert.ToInt32(context.Arguments.First(arg => arg.Name == "id").Value));
        }

        public SchemaBuilder WithArguments(IEnumerable<IGraphQLArgument> arguments)
        {
            _arguments = arguments;
            return this;
        }

        public SchemaBuilder WithResolve(Func<ResolveObjectContext, User> resolve)
        {
            _resolve = resolve;
            return this;
        }

        public GraphQLSchema Build()
        {
            var userType = TestSchemas.CreateUserType();

            return new GraphQLSchema
            {
                Query = new GraphQLObjectField<object>
                {
                    Name = "Query",
                    Type
                    GraphQLObjectType = () => ,
                    Resolve = _ => null
                }
            };
        }
    }


    public static class TestSchemas
    {

        private static GraphQLObjectType GetUserType()
        {
            return CreateUserType();
        }

        public static GraphQLSchema UserSchema()
        {
            return UserSchema(CreateUserType());
        }

        public static GraphQLSchema UserSchemaWithBoss()
        {
            return UserSchema(CreateUserTypeWithBoss());
        }

        public static GraphQLSchema UserSchema(GraphQLObjectType userType)
        {
            var schema = new GraphQLSchema
            {
                Query = new GraphQLObjectField
                {
                    Name = "Query",
                    GraphQLObjectType = () => new GraphQLObjectType
                    {
                        Name = "Query",
                        Fields = new IGraphQLFieldType[]
                        {
                            new GraphQLObjectField<User>
                            {
                                Name = "user",
                                Arguments = new []
                                {
                                    new GraphQLArgument { Name = "id", Type = new GraphQLString() }
                                },
                                Resolve =
                                    context =>
                                        Data.GetData()
                                            .FirstOrDefault(
                                        x =>
                                            context.Arguments.All(arg => arg.Name != "id") ||
                                             x.Id == Convert.ToInt32(context.Arguments.First(arg => arg.Name == "id").Value)),
                                                OfType = new[] {"user"},
                                GraphQLObjectType = () => userType
                            }
                        }
                    },
                    Resolve = _ => string.Empty
                }
            };

            schema.Types = new IGraphQLType[]
            {
                schema.Query.GraphQLObjectType(),
                new GraphQLString(),
                userType,
                new __Schema(), 
                new __Type(),
                new __TypeKind(),
                new GraphQLBoolean(),
                new __Field(),
                new __InputValue(),
                new __EnumValue(),
                new __Directive()
            };

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
                        Type = new GraphQLString()
                    },
                    new GraphQLScalarField<User, string>
                    {
                        Name = "name",
                        Resolve = context => context.Source.Name,
                        Type = new GraphQLString()
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

        public static GraphQLObjectType CreateUserTypeWithBoss()
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
                        Type = new GraphQLString()
                    },
                    new GraphQLScalarField<User, string>
                    {
                        Name = "name",
                        Resolve = context => context.Source.Name,
                        Type = new GraphQLString()
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
            return CreateIntrospectionSchema(new GraphQLObjectField<GraphQLSchema>
            {
                Name = "__schema",
                GraphQLObjectType = () => new __Schema(),
                Resolve = _ => UserSchema()
            });
        }

        public static GraphQLSchema CreateIntrospectionSchema(IGraphQLFieldType type)
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
                            type
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