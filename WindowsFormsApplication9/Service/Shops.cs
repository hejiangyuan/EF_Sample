using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication9.Db;
using WindowsFormsApplication9.Model;

namespace WindowsFormsApplication9.Service
{
    public class Shops
    {
        private Guid domainId = Guid.Empty;

        public void Save(IShop shop)
        {
            using (var scope = DbScope.Create())
            {
                var context = scope.DbContexts.Get<FreeDbContext>();

                if (shop.ShId == null)
                {
                    shop.ShId = Guid.NewGuid();
                }

                var model = DbScope.Parse(context, shop as Shop);

                model.ShDomainId = domainId;
                model.ShIdentityCode = DateTime.Now.ToString("yyyyMMddHHmmss");

                scope.SaveChanges();
            }
        }

        public List<IShop> GetList()
        {
            using (var scope = DbScope.Create())
            {
                return scope.DbContexts.Get<FreeDbContext>().Shop.Where(n=>n.ShDomainId == domainId).ToList().Cast<IShop>().ToList();
            }
        }

        /*
         * Save(Shop)  已经有了
         * 
         * GetList(condition)
         * 
         * Update(field, value, condition)
         * 
         * Delete(condition)
         * 
         * Delete(id)
         * 
         * GetModel(id)
         * 
         * GetModel(condition)
         * 
         * Query(condition) 返回IQueryable
         * 
         * 新增时给特殊字段赋值
         * 
         * 修改时给特殊字段赋值
         * 
         * 新增修改删除后事件
         * 
         * 取原值
         * 
         * 取Model当前状态
         * 
         * AcceptChange
         * 
         * 
         * 
         */

    }
}
