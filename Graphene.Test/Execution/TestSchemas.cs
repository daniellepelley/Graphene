using System;
using System.ComponentModel;
using System.Linq;
using Graphene.Core.Types;
using Graphene.Test.Spike;
using Graphene.TypeProvider;

namespace Graphene.Test.Execution
{
    public static class TestSchemas
    {
        public static GraphQLSchema UserSchema()
        {
            var type = new SimpleTypeBuilder().Build(typeof (TestUser));
            type.Name = "user";
            type.Resolve =
                context =>
                    Data.GetData()
                        .Where(
                            x =>
                                !context.Arguments.ContainsKey("Id") || x.Id == Convert.ToInt32(context.Arguments["Id"]));

            return new GraphQLSchema
            {
                Query = type
            };

            var schema = new GraphQLSchema
            {
                Query = new GraphQLObject
                {
                    Name = "user",
                    Resolve = context => Data.GetData().Where(x => !context.Arguments.ContainsKey("Id") || x.Id == Convert.ToInt32(context.Arguments["Id"])),
                    Fields = new IGraphQLFieldType[]
                    {
                        new GraphQLScalar
                        {
                            Name = "Id",
                            Resolve = context => ((TestUser) context.Source).Id
                        },
                        new GraphQLScalar
                        {
                            Name = "Name",
                            Resolve = context => ((TestUser) context.Source).Name
                        }
                    }.ToList()
                }
            };
            return schema;
        }
    }
}