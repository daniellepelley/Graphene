using Owin;

namespace Graphene.Owin
{
    public static class Extensions
    {
        public static void UseGraphQL(this IAppBuilder app)
        {
            app.Use<GraphQLComponent>();
        }
    }
}