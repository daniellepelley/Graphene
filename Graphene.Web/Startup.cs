using System.Linq;
using Graphene.Example.Data.EntityFramework;
using Graphene.Spike;
using Graphene.Web.Models;
using Microsoft.Owin;
using Owin;
using Owin.Graphene;

[assembly: OwinStartupAttribute(typeof(Graphene.Web.Startup))]
namespace Graphene.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.UseGraphQL(new SchemaBuilder().Build());

            //app.UseGraphQL(ExampleSchemas.CreateSchema2());

            app.UseGraphQL(ExampleSchemas.CreateGraphQLDemoDataContextSchema());
            
        }
    }
}
