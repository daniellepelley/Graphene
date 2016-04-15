using Graphene.Owin;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace Graphene.Owin
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseGraphQL();
            app.UseGraphiQL();
        }
    }
}