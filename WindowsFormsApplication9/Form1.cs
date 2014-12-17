using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
