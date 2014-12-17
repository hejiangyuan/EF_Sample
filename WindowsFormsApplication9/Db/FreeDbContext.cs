using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication9.Model;

namespace WindowsFormsApplication9.Db
{
    public class FreeDbContext : DbContext
    {
        public DbSet<Shop> Shop { get; set; }

        public FreeDbContext(string nameOrConnectionString) :
            base(nameOrConnectionString)
        {
            Database.SetInitializer<FreeDbContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("");

            // Overrides for the convention-based mappings.
            // We're assuming that all our fluent mappings are declared in this assembly.
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(FreeDbContext)));
        }
        
    }
}
