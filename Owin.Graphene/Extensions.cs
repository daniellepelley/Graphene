namespace Owin.Graphene
{
    public static class Extensions
    {
        public static void UseGraphQL(this IAppBuilder app)
        {
            app.Use<GraphQLComponent>();
        }
    }
}