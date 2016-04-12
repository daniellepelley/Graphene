using System;
using System.Linq;
using Graphene.Core.Types;
using Graphene.Test.Spike;

namespace Graphene.Test.Execution
{
    public static class TestSchemas
    {
        public static GraphQLSchema UserSchema()
        {
            var schema = new GraphQLSchema
            {
                Query = new GraphQLObjectType
                {
                    Name = "user",
                    Resolve = context => Data.GetData().Where(x => !context.Arguments.ContainsKey("Id") || x.Id == Convert.ToInt32(context.Arguments["Id"])),
                    Fields = new IGraphQLFieldType[]
                    {
                        new GraphQLFieldScalarType
                        {
                            Name = "Id",
                            Resolve = context => ((TestUser) context.Source).Id
                        },
                        new GraphQLFieldScalarType
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