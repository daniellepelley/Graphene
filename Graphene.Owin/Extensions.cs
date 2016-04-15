using Owin;

namespace Graphene.Owin
{
    public static class Extensions
    {
        public static void UseGraphQL(this IAppBuilder app)
        {
            app.Use<GraphQLComponent>();
        }

        public static void UseGraphiQL(this IAppBuilder app)
        {
            app.Use<GraphiQLComponent>();
        }

        
    }
}