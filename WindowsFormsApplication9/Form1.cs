using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using WindowsFormsApplication9.Db;
using WindowsFormsApplication9.Model;
using WindowsFormsApplication9.Service;
using Numero3.EntityFramework.Interfaces;
using Oracle.ManagedDataAccess.Client;
using TSoft.Frame.Utility.SqlDB;

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
            TransactionTest3();


            using (var scope = DbScope.CreateReadOnly())
            {
                var list = scope.DbContexts.Get<FreeDbContext>().Shop.Where(s => s.ShDomainId == Guid.Empty).ToList();

                dg.DataSource = list;
            }

        }



        /// <summary>
        /// Suppress 事务需要另外创建connection才有效，否则会一起回滚
        /// 而且此时不会有DTC问题，应该是因为不在一个事务中。
        /// </summary>
        private void TransactionTest1()
        {
            var shop = new Shop();
            shop.ShId = Guid.NewGuid();

            Shop modelInContext = null;

            using (var trans = new TransactionScope())
            {
                //using (var conn = new OracleConnection("DATA SOURCE=//192.168.0.11/YUNSH;PASSWORD=jooge2012;PERSIST SECURITY INFO=True;USER ID=TERRY_1116;"))
                //{
                using (var scope = DbScope.Create())
                {
                    var context = scope.DbContexts.Get<FreeDbContext>();

                    modelInContext = DbScope.Parse(context, shop);

                    //modelInContext = context.Shop.Add(shop);

                    modelInContext.ShCode = txtCode.Text;
                    modelInContext.ShName = txtName.Text;
                    modelInContext.ShDomainId = Guid.Empty;

                    string sql = "select getidentitycode() from dual";

                    var conn = context.Database.Connection as OracleConnection;
                    var cmd = new OracleCommand(sql, conn);

                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    var obj = cmd.ExecuteScalar();

                    modelInContext.ShIdentityCode = obj.ToString();

                    scope.SaveChanges();

                    using (var trans2 = new TransactionScope(TransactionScopeOption.Suppress))
                    {
                        try
                        {
                            sql = string.Format("insert into t1 values('{0}')", DateTime.Now);

                            var conn2 =
                                new OracleConnection(
                                    "DATA SOURCE=//192.168.0.11/YUNSH;PASSWORD=jooge2012;PERSIST SECURITY INFO=True;USER ID=TERRY_1116;");

                            if (conn2.State == ConnectionState.Closed)
                                conn2.Open();

                            cmd = new OracleCommand(sql, conn2);

                            var t = cmd.ExecuteNonQuery();

                            trans2.Complete();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                }

                trans.Complete();
            }
            //}

        }


        /// <summary>
        /// 测试EF对Suppress事务的效果, OK
        /// 注意：除了用Suppress事务选项，还必须用DbContextScopeOption.ForceCreateNew 创建Scope
        /// </summary>
        private void TransactionTest2()
        {
            var shop = new Shop();
            shop.ShId = Guid.NewGuid();

            Shop modelInContext = null;

            using (var trans = new TransactionScope())
            {
                using (var scope = DbScope.Create())
                {
                    var context = scope.DbContexts.Get<FreeDbContext>();

                    modelInContext = DbScope.Parse(context, shop);

                    //modelInContext = context.Shop.Add(shop);

                    modelInContext.ShCode = txtCode.Text;
                    modelInContext.ShName = txtName.Text;
                    modelInContext.ShDomainId = Guid.Empty;

                    modelInContext.ShIdentityCode = DateTime.Now.ToLongTimeString();

                    using (var trans2 = new TransactionScope(TransactionScopeOption.Suppress))
                    {
                        using (var scope2 = DbScope.Create(DbContextScopeOption.ForceCreateNew))
                        {
                            shop = new Shop();
                            shop.ShId = Guid.NewGuid();

                            context = scope2.DbContexts.Get<FreeDbContext>();

                            modelInContext = DbScope.Parse(context, shop);

                            //modelInContext = context.Shop.Add(shop);

                            modelInContext.ShCode = txtCode.Text;
                            modelInContext.ShName = txtName.Text;
                            modelInContext.ShDomainId = Guid.Empty;

                            modelInContext.ShIdentityCode = DateTime.Now.ToString("yyyyMMddHHmmssfff");

                            scope2.SaveChanges();
                        }

                        trans2.Complete();
                    }

                    scope.SaveChanges();

                }

                //trans.Complete();
            }

        }

        /// <summary>
        /// 测试EF 和 普通ado.net同时在一个事务中
        /// 在修改了OracleAcessDataBase中的代码，对连接进行及时关闭后，已实现事务中EF和EntLib共存。但必须保证2种方式的连接字符串一样。
        /// 
        /// </summary>
        private void TransactionTest3()
        {
            var shop = new Shop();
            shop.ShId = Guid.NewGuid();

            using (var trans = new TransactionScope())
            {
                using (var scope = DbScope.Create())
                {
                    var context = scope.DbContexts.Get<FreeDbContext>();

                    shop.ShCode = txtCode.Text;
                    shop.ShName = txtName.Text;
                    shop.ShDomainId = Guid.Empty;

                    string sql = "select getidentitycode() from dual";
                    var connStr = "DATA SOURCE=//192.168.0.11/YUNSH;PASSWORD=jooge2012;PERSIST SECURITY INFO=True;USER ID=TERRY_1116;PROMOTABLE TRANSACTION=LOCAL;";

                    OracleCommand cmd;
                    using (var conn = new OracleConnection(connStr))
                    {
                        cmd = new OracleCommand(sql, conn);

                        if (conn.State == ConnectionState.Closed)
                            conn.Open();

                        var obj = cmd.ExecuteScalar();
                        shop.ShIdentityCode = obj.ToString();
                    }

                    sql = string.Format("insert into t1 values('{0}')", DateTime.Now);

                    OracleHelper.ExecuteNonQuery(connStr, sql);

                    DbScope.Parse(context, shop);

                    scope.SaveChanges();

                }

                trans.Complete();
            }

        }


        /// <summary>
        /// 测试在一个事务中有2次创建连接的情况
        /// 如果每个连接都正常关闭，就没有问题。
        /// 如果2个连接字符串不同，则会有问题。
        /// </summary>
        private void TransactionTest4()
        {
            var shop = new Shop();
            shop.ShId = Guid.NewGuid();

            using (var trans = new TransactionScope())
            {

                var sql = string.Format("insert into t1 values('{0}')", DateTime.Now);

                var conn = new OracleConnection("DATA SOURCE=//192.168.0.11/YUNSH;PASSWORD=jooge2012;PERSIST SECURITY INFO=True;USER ID=TERRY_1116;Min Pool Size=10;Max Pool Size=10;");

                var cmd = new OracleCommand(sql, conn);

                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                var obj = cmd.ExecuteNonQuery();

                conn.Close();
                conn.Dispose();

                var conn2 = new OracleConnection("DATA SOURCE=//192.168.0.11/YUNSH;PASSWORD=jooge2012;PERSIST SECURITY INFO=True;USER ID=TERRY_1116;Min Pool Size=10;Max Pool Size=10;");

                var cmd2 = new OracleCommand(sql, conn2);

                if (conn2.State == ConnectionState.Closed)
                    conn2.Open();

                obj = cmd2.ExecuteNonQuery();

                conn2.Close();
                conn2.Dispose();


                trans.Complete();
            }

        }

        /// <summary>
        /// 测试在一个事务中2次都使用EL5.0 DAAB 获取Connection执行的情况
        /// 没有问题，连接必须及时关闭
        /// </summary>
        private void TransactionTest5()
        {
            var shop = new Shop();
            shop.ShId = Guid.NewGuid();

            using (var trans = new TransactionScope())
            {

                var sql = string.Format("insert into t1 values('{0}')", DateTime.Now);

                var connStr = "DATA SOURCE=//192.168.0.11/YUNSH;PASSWORD=jooge2012;PERSIST SECURITY INFO=True;USER ID=TERRY_1116;PROMOTABLE TRANSACTION=LOCAL;";

                var db = new OracleAcessDataBase(connStr);

                var conn = db.CreateConnection() as OracleConnection;
                var cmd = new OracleCommand(sql, conn);

                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                var obj = cmd.ExecuteNonQuery();

                conn.Close();
                conn.Dispose();


                var db2 = new OracleAcessDataBase(connStr);

                var conn2 = db2.CreateConnection() as OracleConnection;
                var cmd2 = new OracleCommand(sql, conn2);

                if (conn2.State == ConnectionState.Closed)
                    conn2.Open();

                var obj2 = cmd2.ExecuteNonQuery();

                conn2.Close();
                conn2.Dispose();


                trans.Complete();
            }

        }

        private void btnSpeedTest_Click(object sender, EventArgs e)
        {
            MixTransactionSpeedTest();
        }

        /// <summary>
        /// 混合事务
        /// </summary>
        private void MixTransactionSpeedTest()
        {
            StringBuilder sb = new StringBuilder();

            var code = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var connStr = "DATA SOURCE=//192.168.0.11/YUNSH;PASSWORD=jooge2012;PERSIST SECURITY INFO=True;USER ID=TERRY_1116;PROMOTABLE TRANSACTION=LOCAL;";

            var watch = new Stopwatch();

            watch.Start();

            //混合事务
            //用ef 和 EntLib 插入店铺表n次

            var times = 600;

            using (var trans = new TransactionScope())
            {

                using (var scope = DbScope.Create())
                {
                    for (var i = 0; i < times; i++)
                    {
                        var identityCode = code + i;

                        var shop = new Shop();
                        shop.ShId = Guid.NewGuid();

                        var context = scope.DbContexts.Get<FreeDbContext>();

                        shop.ShCode = txtCode.Text;
                        shop.ShName = txtName.Text;
                        shop.ShDomainId = Guid.Empty;

                        shop.ShIdentityCode = identityCode;

                        DbScope.Parse(context, shop);

                        identityCode = code + i + "_2";

                        string sql =
                            "insert into \"SHOP\"(\"SHID\", \"SHDOMAINID\", \"SHIDENTITYCODE\", \"SHCODE\", \"SHNAME\", \"SHDEFAULTWAREHOUSE\", \"SHWEBSHOPPLATFORMTYPE\", \"SHSHORTCUTCODE\", \"SHOUTERCODE\", \"SHWEBSHOPTYPE\", \"SHDESC\", \"SHTYPE\", \"SHLEVEL\", \"SHISWEBSHOP\", \"SHWEBSHOPID\", \"SHWEBSHOPURL\", \"SHWEBSHOPUSERIDENTITY\", \"SHWEBSHOPAPPKEY\", \"SHWEBSHOPAPPSECRET\", \"SHWEBSHOPSESSIONKEY\", \"SHWEBSHOPSESSIONTAG\", \"SHENABLED\", \"SHOWNERNICK\", \"SHOWNERNAME\", \"SHCOUNTRY\", \"SHPROVINCE\", \"SHCITY\", \"SHDISTRICT\", \"SHADDRESS\", \"SHPOSTCODE\", \"SHPHONE\", \"SHFAX\", \"SHAREA\", \"SHTEXT1\", \"SHTEXT2\", \"SHTEXT3\", \"SHTEXT4\", \"SHTEXT5\", \"SHTEXT6\", \"SHTEXT7\", \"SHTEXT8\", \"SHTEXT9\", \"SHTEXT10\", \"SHLONGTEXT1\", \"SHLONGTEXT2\", \"SHLONGTEXT3\", \"SHCHECKBOX1\", \"SHCHECKBOX2\", \"SHCHECKBOX3\", \"SHCHECKBOX4\", \"SHCHECKBOX5\", \"SHDATE1\", \"SHDATE2\", \"SHDATE3\", \"SHINT1\", \"SHINT2\", \"SHINT3\", \"SHDECIMAL1\", \"SHDECIMAL2\", \"SHDECIMAL3\", \"SHCREATEUSER\", \"SHCREATETIME\", \"SHLASTUPDATEUSER\", \"SHLASTUPDATETIME\", \"SHINVEXPSETTING\", \"SHWEBSHOPOTHERPARAM\", \"SHRELATECUSTOMER\") values (:p0, :p1, :p2, :p3, :p4, null, null, null, null, null, null, null, null, :p5, null, null, null, null, null, null, null, :p6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, :p7, :p8, :p9, :p10, :p11, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null)";

                        List<OracleParameter> list = new List<OracleParameter>
                {
                    new OracleParameter("p0", OracleDbType.Raw, Guid.NewGuid().ToByteArray(), ParameterDirection.Input),
                    new OracleParameter("p1", OracleDbType.Raw, Guid.Empty.ToByteArray(), ParameterDirection.Input),
                    new OracleParameter("p2", OracleDbType.NVarchar2, identityCode, ParameterDirection.Input),
                    new OracleParameter("p3", OracleDbType.NVarchar2,txtName.Text, ParameterDirection.Input),
                    new OracleParameter("p4", OracleDbType.NVarchar2,txtCode.Text, ParameterDirection.Input),
                    new OracleParameter("p5", OracleDbType.Int16,0, ParameterDirection.Input),
                    new OracleParameter("p6", OracleDbType.Int16,0, ParameterDirection.Input),
                    new OracleParameter("p7", OracleDbType.Int16,0, ParameterDirection.Input),
                    new OracleParameter("p8", OracleDbType.Int16,0, ParameterDirection.Input),
                    new OracleParameter("p9", OracleDbType.Int16,0, ParameterDirection.Input),
                    new OracleParameter("p10", OracleDbType.Int16,0, ParameterDirection.Input),
                    new OracleParameter("p11", OracleDbType.Int16,0, ParameterDirection.Input),
                };

                        OracleHelper.ExecuteNonQuery(connStr, sql, list.ToArray());

                    }

                    scope.SaveChanges();
                }

                trans.Complete();
            }

            watch.Stop();

            sb.AppendFormat("EF + EntLib 混合事务插入 {0} 次：{1} 毫秒 \r\n", times, watch.ElapsedMilliseconds);

            MessageBox.Show(sb.ToString());
        }

        /// <summary>
        /// 有事务
        /// </summary>
        private void HasTransactionSpeedTest()
        {
            StringBuilder sb = new StringBuilder();

            var code = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var connStr = "DATA SOURCE=//192.168.0.11/YUNSH;PASSWORD=jooge2012;PERSIST SECURITY INFO=True;USER ID=TERRY_1116;PROMOTABLE TRANSACTION=LOCAL;";

            var watch = new Stopwatch();

            watch.Start();

            //有事务
            //先用ef插入店铺表n次

            var times = 1200;

            using (var trans = new TransactionScope())
            {

                using (var scope = DbScope.Create())
                {
                    for (var i = 0; i < times; i++)
                    {
                        var identityCode = code + i;

                        var shop = new Shop();
                        shop.ShId = Guid.NewGuid();

                        var context = scope.DbContexts.Get<FreeDbContext>();

                        shop.ShCode = txtCode.Text;
                        shop.ShName = txtName.Text;
                        shop.ShDomainId = Guid.Empty;

                        shop.ShIdentityCode = identityCode;

                        //context.Shop.Add(shop);

                        DbScope.Parse(context, shop);
                    }

                    scope.SaveChanges();
                }

                trans.Complete();
            }

            watch.Stop();

            sb.AppendFormat("EF有事务插入 {0} 次：{1} 毫秒 \r\n", times, watch.ElapsedMilliseconds);

            watch.Restart();


            code = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            //再用EntLib插入

            using (var trans = new TransactionScope())
            {

                for (var i = 0; i < times; i++)
                {
                    var identityCode = code + i;

                    string sql =
                        "insert into \"SHOP\"(\"SHID\", \"SHDOMAINID\", \"SHIDENTITYCODE\", \"SHCODE\", \"SHNAME\", \"SHDEFAULTWAREHOUSE\", \"SHWEBSHOPPLATFORMTYPE\", \"SHSHORTCUTCODE\", \"SHOUTERCODE\", \"SHWEBSHOPTYPE\", \"SHDESC\", \"SHTYPE\", \"SHLEVEL\", \"SHISWEBSHOP\", \"SHWEBSHOPID\", \"SHWEBSHOPURL\", \"SHWEBSHOPUSERIDENTITY\", \"SHWEBSHOPAPPKEY\", \"SHWEBSHOPAPPSECRET\", \"SHWEBSHOPSESSIONKEY\", \"SHWEBSHOPSESSIONTAG\", \"SHENABLED\", \"SHOWNERNICK\", \"SHOWNERNAME\", \"SHCOUNTRY\", \"SHPROVINCE\", \"SHCITY\", \"SHDISTRICT\", \"SHADDRESS\", \"SHPOSTCODE\", \"SHPHONE\", \"SHFAX\", \"SHAREA\", \"SHTEXT1\", \"SHTEXT2\", \"SHTEXT3\", \"SHTEXT4\", \"SHTEXT5\", \"SHTEXT6\", \"SHTEXT7\", \"SHTEXT8\", \"SHTEXT9\", \"SHTEXT10\", \"SHLONGTEXT1\", \"SHLONGTEXT2\", \"SHLONGTEXT3\", \"SHCHECKBOX1\", \"SHCHECKBOX2\", \"SHCHECKBOX3\", \"SHCHECKBOX4\", \"SHCHECKBOX5\", \"SHDATE1\", \"SHDATE2\", \"SHDATE3\", \"SHINT1\", \"SHINT2\", \"SHINT3\", \"SHDECIMAL1\", \"SHDECIMAL2\", \"SHDECIMAL3\", \"SHCREATEUSER\", \"SHCREATETIME\", \"SHLASTUPDATEUSER\", \"SHLASTUPDATETIME\", \"SHINVEXPSETTING\", \"SHWEBSHOPOTHERPARAM\", \"SHRELATECUSTOMER\") values (:p0, :p1, :p2, :p3, :p4, null, null, null, null, null, null, null, null, :p5, null, null, null, null, null, null, null, :p6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, :p7, :p8, :p9, :p10, :p11, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null)";

                    List<OracleParameter> list = new List<OracleParameter>
                {
                    new OracleParameter("p0", OracleDbType.Raw, Guid.NewGuid().ToByteArray(), ParameterDirection.Input),
                    new OracleParameter("p1", OracleDbType.Raw, Guid.Empty.ToByteArray(), ParameterDirection.Input),
                    new OracleParameter("p2", OracleDbType.NVarchar2, identityCode, ParameterDirection.Input),
                    new OracleParameter("p3", OracleDbType.NVarchar2,txtName.Text, ParameterDirection.Input),
                    new OracleParameter("p4", OracleDbType.NVarchar2,txtCode.Text, ParameterDirection.Input),
                    new OracleParameter("p5", OracleDbType.Int16,0, ParameterDirection.Input),
                    new OracleParameter("p6", OracleDbType.Int16,0, ParameterDirection.Input),
                    new OracleParameter("p7", OracleDbType.Int16,0, ParameterDirection.Input),
                    new OracleParameter("p8", OracleDbType.Int16,0, ParameterDirection.Input),
                    new OracleParameter("p9", OracleDbType.Int16,0, ParameterDirection.Input),
                    new OracleParameter("p10", OracleDbType.Int16,0, ParameterDirection.Input),
                    new OracleParameter("p11", OracleDbType.Int16,0, ParameterDirection.Input),
                };

                    OracleHelper.ExecuteNonQuery(connStr, sql, list.ToArray());
                }
                trans.Complete();
            }


            watch.Stop();

            sb.AppendFormat("EntLib有事务插入 {0} 次：{1} 毫秒 \r\n", times, watch.ElapsedMilliseconds);


            MessageBox.Show(sb.ToString());
        }


        /// <summary>
        /// 无事务
        /// </summary>
        private void NoTransactionSpeedTest()
        {
            StringBuilder sb = new StringBuilder();

            var code = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var connStr = "DATA SOURCE=//192.168.0.11/YUNSH;PASSWORD=jooge2012;PERSIST SECURITY INFO=True;USER ID=TERRY_1116;PROMOTABLE TRANSACTION=LOCAL;";

            var watch = new Stopwatch();

            watch.Start();

            //没有事务
            //先用ef插入店铺表n次

            var times = 1200;

            using (var scope = DbScope.Create())
            {
                for (var i = 0; i < times; i++)
                {
                    var identityCode = code + i;

                    var shop = new Shop();
                    shop.ShId = Guid.NewGuid();

                    var context = scope.DbContexts.Get<FreeDbContext>();

                    shop.ShCode = txtCode.Text;
                    shop.ShName = txtName.Text;
                    shop.ShDomainId = Guid.Empty;

                    shop.ShIdentityCode = identityCode;

                    //context.Shop.Add(shop);

                    DbScope.Parse(context, shop);
                }

                scope.SaveChanges();
            }

            watch.Stop();

            sb.AppendFormat("EF无事务插入 {0} 次：{1} 毫秒 \r\n", times, watch.ElapsedMilliseconds);

            watch.Restart();


            code = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            //再用EntLib插入

            for (var i = 0; i < times; i++)
            {
                var identityCode = code + i;

                string sql =
                    "insert into \"SHOP\"(\"SHID\", \"SHDOMAINID\", \"SHIDENTITYCODE\", \"SHCODE\", \"SHNAME\", \"SHDEFAULTWAREHOUSE\", \"SHWEBSHOPPLATFORMTYPE\", \"SHSHORTCUTCODE\", \"SHOUTERCODE\", \"SHWEBSHOPTYPE\", \"SHDESC\", \"SHTYPE\", \"SHLEVEL\", \"SHISWEBSHOP\", \"SHWEBSHOPID\", \"SHWEBSHOPURL\", \"SHWEBSHOPUSERIDENTITY\", \"SHWEBSHOPAPPKEY\", \"SHWEBSHOPAPPSECRET\", \"SHWEBSHOPSESSIONKEY\", \"SHWEBSHOPSESSIONTAG\", \"SHENABLED\", \"SHOWNERNICK\", \"SHOWNERNAME\", \"SHCOUNTRY\", \"SHPROVINCE\", \"SHCITY\", \"SHDISTRICT\", \"SHADDRESS\", \"SHPOSTCODE\", \"SHPHONE\", \"SHFAX\", \"SHAREA\", \"SHTEXT1\", \"SHTEXT2\", \"SHTEXT3\", \"SHTEXT4\", \"SHTEXT5\", \"SHTEXT6\", \"SHTEXT7\", \"SHTEXT8\", \"SHTEXT9\", \"SHTEXT10\", \"SHLONGTEXT1\", \"SHLONGTEXT2\", \"SHLONGTEXT3\", \"SHCHECKBOX1\", \"SHCHECKBOX2\", \"SHCHECKBOX3\", \"SHCHECKBOX4\", \"SHCHECKBOX5\", \"SHDATE1\", \"SHDATE2\", \"SHDATE3\", \"SHINT1\", \"SHINT2\", \"SHINT3\", \"SHDECIMAL1\", \"SHDECIMAL2\", \"SHDECIMAL3\", \"SHCREATEUSER\", \"SHCREATETIME\", \"SHLASTUPDATEUSER\", \"SHLASTUPDATETIME\", \"SHINVEXPSETTING\", \"SHWEBSHOPOTHERPARAM\", \"SHRELATECUSTOMER\") values (:p0, :p1, :p2, :p3, :p4, null, null, null, null, null, null, null, null, :p5, null, null, null, null, null, null, null, :p6, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, :p7, :p8, :p9, :p10, :p11, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null)";

                List<OracleParameter> list = new List<OracleParameter>
                {
                    new OracleParameter("p0", OracleDbType.Raw, Guid.NewGuid().ToByteArray(), ParameterDirection.Input),
                    new OracleParameter("p1", OracleDbType.Raw, Guid.Empty.ToByteArray(), ParameterDirection.Input),
                    new OracleParameter("p2", OracleDbType.NVarchar2, identityCode, ParameterDirection.Input),
                    new OracleParameter("p3", OracleDbType.NVarchar2,txtName.Text, ParameterDirection.Input),
                    new OracleParameter("p4", OracleDbType.NVarchar2,txtCode.Text, ParameterDirection.Input),
                    new OracleParameter("p5", OracleDbType.Int16,0, ParameterDirection.Input),
                    new OracleParameter("p6", OracleDbType.Int16,0, ParameterDirection.Input),
                    new OracleParameter("p7", OracleDbType.Int16,0, ParameterDirection.Input),
                    new OracleParameter("p8", OracleDbType.Int16,0, ParameterDirection.Input),
                    new OracleParameter("p9", OracleDbType.Int16,0, ParameterDirection.Input),
                    new OracleParameter("p10", OracleDbType.Int16,0, ParameterDirection.Input),
                    new OracleParameter("p11", OracleDbType.Int16,0, ParameterDirection.Input),
                };

                OracleHelper.ExecuteNonQuery(connStr, sql, list.ToArray());
            }


            watch.Stop();

            sb.AppendFormat("EntLib无事务插入 {0} 次：{1} 毫秒 \r\n", times, watch.ElapsedMilliseconds);


            MessageBox.Show(sb.ToString());
        }

    }
}
