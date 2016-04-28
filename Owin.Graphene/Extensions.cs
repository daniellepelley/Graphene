using System;
using Graphene.Core.Types;
using Graphene.Spike;

namespace Owin.Graphene
{
    public static class Extensions
    {
        public static void UseGraphQL(this IAppBuilder app, GraphQLSchema schema)
        {
            app.Use<GraphQLComponent>(schema);
        }

        public static void UseGraphQL(this IAppBuilder app, Func<SchemaBuilder, GraphQLSchema> buildFunc)
        {
            var builder = new SchemaBuilder();
            var schema = buildFunc(builder);
            app.Use<GraphQLComponent>(schema);
        }
    }
}