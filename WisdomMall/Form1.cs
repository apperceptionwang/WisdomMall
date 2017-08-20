using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
namespace WisdomMall
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        #region 商品登记窗口跳转
        private void btn_Register_Click(object sender, EventArgs e)
        {
            new AddRegister().Show();
        }
        #endregion


        #region 数据库测试代码
        private void button1_Click(object sender, EventArgs e)
        {
            SqlDbHelper helper = new SqlDbHelper();
            SqlConnection conn=helper.GetConnection();
            string sql = "select * from  Type";
            DataSet ds = SqlDbHelper.ExecuteSelectSql(sql);
            //btn_Register.Text = ds.Tables[0].Rows[4][1].ToString();
            if (conn != null)
            {
                MessageBox.Show(String.Format("成功!"));
            }
            
            

        }
        #endregion


        #region 商品信息窗口跳转
        private void ProConfig_Click(object sender, EventArgs e)
        {
            new ProductInfo().Show();
        }
        #endregion


        #region 货架信息窗口跳转
        private void ShelfConfig_Click(object sender, EventArgs e)
        {
            new ShelfInfo().Show();
        }
        #endregion


        private void InStockConfig_Click(object sender, EventArgs e)
        {
            new InStock().Show();
        }

    }
}
