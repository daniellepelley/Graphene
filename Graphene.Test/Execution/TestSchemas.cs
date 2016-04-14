using System;
using System.Linq;
using Graphene.Core.Types;
using Graphene.Test.Spike;
using Graphene.TypeProvider;

namespace Graphene.Test.Execution
{
    public static class TestSchemas
    {
        public static IGraphQLSchema UserSchema()
        {
            //var type = new SimpleTypeBuilder().Build(typeof(TestUser));
            //type.Name = "user";
            //type.Resolve =
            //    context => Data.GetData().First();
                    //Data.GetData()
                    //    .Where(
                    //        x =>
                    //            !context.Arguments.ContainsKey("Id") || x.Id == Convert.ToInt32(context.Arguments["Id"]));

            //return new GraphQLSchema
            //{
            //    Query = type
            //};

            var schema = new GraphQLSchema
            {
                Query = new GraphQLObject<TestUser>
                {
                    Name = "user",
                    Resolve = context => Data.GetData().FirstOrDefault(x => !context.Arguments.ContainsKey("Id") || x.Id == Convert.ToInt32(context.Arguments["Id"])),
                    Fields = new IGraphQLFieldType[]
                    {
                        new GraphQLScalar<TestUser, object>
                        {
                            Name = "Id",
                            Resolve = context => context.Source.Id
                        },
                        new GraphQLScalar<TestUser, object>
                        {
                            Name = "Name",
                            Resolve = context => context.Source.Name
                        },
                        new GraphQLObject<TestUser, Boss>
                        {
                            Name = "Boss",
                            Resolve = context => context.Source.Boss,
                            Fields = new IGraphQLFieldType[]
                            {
                                new GraphQLScalar<Boss, int>
                                {
                                    Name = "Id",
                                    Resolve = context => context.Source.Id
                                },
                                new GraphQLScalar<Boss, string>
                                {
                                    Name = "Name",
                                    Resolve = context => context.Source.Name
                                }
                            }.ToList()
                        }
                    }.ToList()
                }
            };
            return schema;
        }
    }
}