using System;
using System.Linq;
using Graphene.Core.Types;
using Graphene.Test.Data;
using Graphene.Test.Spike;

namespace Graphene.Test.Execution
{
    public static class TestSchemas
    {
        public static IGraphQLSchema UserSchema()
        {
            var bossType = new GraphQLObjectType
            {
                Name = "Boss",
                Description = "This is the boss",
                Fields = new IGraphQLFieldType[]
                {
                    new GraphQLScalarField<Boss, int>
                    {
                        Name = "Id",
                        Resolve = context => context.Source.Id
                    },
                    new GraphQLScalarField<Boss, string>
                    {
                        Name = "Name",
                        Resolve = context => context.Source.Name
                    }
                }
            };

            var userType = new GraphQLObjectType
            {
                Name = "User",
                Description = "This is a user",
                Fields = new IGraphQLFieldType[]
                {
                    new GraphQLScalarField<User, int>
                    {
                        Name = "Id",
                        Resolve = context => context.Source.Id
                    },
                    new GraphQLScalarField<User, string>
                    {
                        Name = "Name",
                        Resolve = context => context.Source.Name
                    },
                    new GraphQLObjectField<User, Boss>
                    {
                        Name = "Boss",
                        Resolve = context => context.Source.Boss,
                        GraphQLObjectType = bossType
                    }
                }
            };

            var schema = new GraphQLSchema
            {
                Query = new GraphQLObjectField<User>
                {
                    Name = "user",
                    Resolve = context => Data.Data.GetData().FirstOrDefault(x => !context.Arguments.ContainsKey("Id") || x.Id == Convert.ToInt32(context.Arguments["Id"])),
                    OfType = new [] {"user" },
                    GraphQLObjectType = userType
                }
            };
            return schema;
        }
    }
}