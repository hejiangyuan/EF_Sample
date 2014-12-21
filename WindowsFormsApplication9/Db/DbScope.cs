using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Numero3.EntityFramework.Implementation;
using Numero3.EntityFramework.Interfaces;

namespace WindowsFormsApplication9.Db
{
    public class DbScope
    {
        const string CONNECTION_CACHE_KEY_PRE = "EF_CONNECTION_STRING_CACHE_KEY_PREFIX_";

        private static DbContextScopeFactory GetDbContextScopeFactory(string domainCode)
        {
            var connectString = "Db1";

            return CreateDbContextScopeFactory(connectString);
        }

        private static DbContextScopeFactory GetDbContextScopeFactory(DbConnection conn)
        {
            return CreateDbContextScopeFactory(conn);
        }

        private static DbContextScopeFactory CreateDbContextScopeFactory(DbConnection conn)
        {
            var dbcontextFactory = new DbContextFactory(conn);

            var dbContextScopeFactory = new DbContextScopeFactory(dbcontextFactory);

            return dbContextScopeFactory;
        }

        private static DbContextScopeFactory CreateDbContextScopeFactory(string connectString)
        {
            var dbcontextFactory = new DbContextFactory(connectString);

            var dbContextScopeFactory = new DbContextScopeFactory(dbcontextFactory);

            return dbContextScopeFactory;
        }
        
        public static IDbContextScope Create(DbContextScopeOption joiningOption = DbContextScopeOption.JoinExisting, string domainCode = null)
        {
            return GetDbContextScopeFactory(domainCode).Create(joiningOption);
        }


        public static IDbContextReadOnlyScope CreateReadOnly(DbContextScopeOption joiningOption = DbContextScopeOption.JoinExisting, string domainCode = null)
        {
            return GetDbContextScopeFactory(domainCode).CreateReadOnly(joiningOption);
        }

        public static IDbContextScope Create(DbConnection conn, DbContextScopeOption joiningOption = DbContextScopeOption.JoinExisting)
        {
            return GetDbContextScopeFactory(conn).Create(joiningOption);
        }

        public static T Parse<T>(DbContext dbContext, T entity) where T : class
        {
            var objContext = ((IObjectContextAdapter)dbContext).ObjectContext;
            var objSet = objContext.CreateObjectSet<T>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            object original = null;

            objContext.TryGetObjectByKey(entityKey, out original);

            T model = null;
            if (original != null)
            {
                model = (T) original;
                dbContext.Entry(model).CurrentValues.SetValues(entity);
            }
            else
            {
                model = entity;
                objContext.AddObject(entityKey.EntitySetName, model);
            }

            return model;
        }
    }
}
