using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using WindowsFormsApplication9.Db;
using WindowsFormsApplication9.Model;
using WindowsFormsApplication9.Service;

namespace WindowsFormsApplication9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var scope = DbScope.Create())
            {
                var list = scope.DbContexts.Get<FreeDbContext>().Shop.ToList();

                txtName.Text = list.Count.ToString();

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                var shop = new Shop();

                shop.ShCode = txtCode.Text;
                shop.ShName = txtName.Text;

                var ss = new Shops();

                ss.Save(shop);

                var list = ss.GetList();

                dg.DataSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnStateTest_Click(object sender, EventArgs e)
        {

            var shop = new Shop();

            shop.ShId = Guid.NewGuid();
            shop.ShCode = txtCode.Text;
            shop.ShName = txtName.Text;
            shop.ShDomainId = Guid.Empty;
            shop.ShIdentityCode = DateTime.Now.ToString("yyyyMMddHHmmss");

            var state = shop.State;

            Shop modelInContext = null;

            using (var scope = DbScope.Create())
            {
                var context = scope.DbContexts.Get<FreeDbContext>();

                modelInContext = DbScope.Parse<Shop>(context, shop);

                state = modelInContext.State;

                scope.SaveChanges();
            }

            state = modelInContext.State;

        }

        private void btnHunhe_Click(object sender, EventArgs e)
        {

            var shop = new Shop();
            shop.ShId = Guid.NewGuid();

            Shop modelInContext = null;

            using (var trans = new TransactionScope())
            {
                using (var scope = DbScope.Create())
                {

                    var context = scope.DbContexts.Get<FreeDbContext>();

                    //DbScope.Parse(context, shop);

                    modelInContext = context.Shop.Add(shop);

                    modelInContext.ShCode = txtCode.Text;
                    modelInContext.ShName = txtName.Text;
                    modelInContext.ShDomainId = Guid.Empty;
                    modelInContext.ShIdentityCode = DateTime.Now.ToString("yyyyMMddHHmmss");


                    scope.SaveChanges();

                    trans.Complete();
                }
            }


            using (var scope = DbScope.CreateReadOnly())
            {
                var list = scope.DbContexts.Get<FreeDbContext>().Shop.Where(s => s.ShDomainId == Guid.Empty).ToList();

                dg.DataSource = list;
            }

        }

    }
}
