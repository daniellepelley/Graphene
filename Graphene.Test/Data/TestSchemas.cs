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
                    Resolve = context => Test.Data.Data.GetData().FirstOrDefault(x => !context.Arguments.ContainsKey("id") || x.Id == Convert.ToInt32(context.Arguments["id"])),
                    OfType = new [] {"user" },
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


    }
}