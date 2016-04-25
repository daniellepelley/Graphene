using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphene.Example.Data.EntityFramework
{
    public class GraphQLDemoDataContext : DbContext
    {
        static GraphQLDemoDataContext()
        {
            // ROLA - This is a hack to ensure that Entity Framework SQL Provider is copied across to the output folder.
            // As it is installed in the GAC, Copy Local does not work. It is required for probing.
            // Fixed "Provider not loaded" error
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public GraphQLDemoDataContext()
            : base("GraphQLDemoData")
        {
            Configuration.AutoDetectChangesEnabled = true;
        }

        public GraphQLDemoDataContext(IDatabaseInitializer<GraphQLDemoDataContext> contextInitialiser)
            : this()
        {
            Database.SetInitializer(contextInitialiser);

        }

        public GraphQLDemoDataContext(IDatabaseInitializer<GraphQLDemoDataContext> contextInitialiser, string connection)
            : base(connection)
        {
            Database.SetInitializer(contextInitialiser);
            Configuration.AutoDetectChangesEnabled = false;
        }

        public GraphQLDemoDataContext(DbConnection existingConnection)
            : base(existingConnection, true)
        {
            Configuration.AutoDetectChangesEnabled = true;
        }

        public virtual IDbSet<Person> Persons { get; set; }

        public virtual IDbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
