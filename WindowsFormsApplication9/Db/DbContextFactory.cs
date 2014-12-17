using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Numero3.EntityFramework.Interfaces;

namespace WindowsFormsApplication9.Db
{
    public class DbContextFactory : IDbContextFactory
    {
        private string _nameOrConnectionString;

        public DbContextFactory(string nameOrConnectionString)
        {
            _nameOrConnectionString = nameOrConnectionString;
        }

        public TDbContext CreateDbContext<TDbContext>() where TDbContext : DbContext
        {
            return Activator.CreateInstance(typeof(TDbContext), _nameOrConnectionString) as TDbContext;
        }
    }
}
