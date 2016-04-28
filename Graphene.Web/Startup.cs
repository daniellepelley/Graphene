using System.Data.Entity;
using System.Linq;
using Graphene.Core.Types;
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

            //app.UseGraphQL(ExampleSchemas.CreateGraphQLDemoDataContextSchema());
            //app.UseGraphQL(Build);


            app.UseGraphQL(builder =>
            {
                var context = new GraphQLDemoDataContext();

                var schema = builder
                    .AsAggregateRoot(context.Companies.Include(x => x.Address), "companies")
                    .AsAggregateRoot(context.Persons.Include(x => x.Address), "people")
                    .Build();

                return schema;                
            });
        }

        private GraphQLSchema Build(SchemaBuilder builder)
        {
            var context = new GraphQLDemoDataContext();

            var schema = builder
                .AsAggregateRoot(context.Companies.Include(x => x.Address), "companies")
                .AsAggregateRoot(context.Persons.Include(x => x.Address), "people")
                .Build();

            return schema;
        }
    }
}
