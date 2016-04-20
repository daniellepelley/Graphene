using Graphene.Web.Models;
using Microsoft.Owin;
using Owin;
using Owin.Graphene;

[assembly: OwinStartupAttribute(typeof(Graphene.Web.Startup))]
namespace Graphene.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseGraphQL(ExampleSchemas.CreateSchema());
        }
    }
}
