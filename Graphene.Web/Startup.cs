using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Graphene.Web.Startup))]
namespace Graphene.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

        }
    }
}
