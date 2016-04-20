using Graphene.Core.Types;

namespace Owin.Graphene
{
    public static class Extensions
    {
        public static void UseGraphQL(this IAppBuilder app, GraphQLSchema schema, GraphQLSchema introspectionSchema)
        {
            app.Use<GraphQLComponent>(schema, introspectionSchema);
        }
    }
}