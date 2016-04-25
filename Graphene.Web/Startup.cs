using System.Linq;
using Graphene.Example.Data.EntityFramework;
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
            app.UseGraphQL(ExampleSchemas.CreateSchema());
            var context = new GraphQLDemoDataContext();

            var seeder = new Seeder();
            seeder.Seed(context);

            var people = context.Persons.ToArray();
        }
    }
}
