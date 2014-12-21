using System;
using System.Collections.Generic;
using System.Data.Common;
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

        private DbConnection _conn;

        public DbContextFactory(string nameOrConnectionString)
        {
            _nameOrConnectionString = nameOrConnectionString;
        }

        public DbContextFactory(DbConnection conn)
        {
            _conn = conn;
        }

        public TDbContext CreateDbContext<TDbContext>() where TDbContext : DbContext
        {
            if (_conn != null)
            {
                return Activator.CreateInstance(typeof(TDbContext), _conn) as TDbContext;
            }

            return Activator.CreateInstance(typeof(TDbContext), _nameOrConnectionString) as TDbContext;
        }
    }
}
