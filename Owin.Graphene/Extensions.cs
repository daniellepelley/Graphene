using Graphene.Core.Types;

namespace Owin.Graphene
{
    public static class Extensions
    {
        public static void UseGraphQL(this IAppBuilder app, GraphQLSchema schema)
        {
            app.Use<GraphQLComponent>(schema);
        }
    }
}