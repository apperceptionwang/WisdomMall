using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ISO15693;

namespace WisdomMall
{
    public partial class InStock : Form
    {
        Reader reader = new Reader();
        static bool a = true;
        public InStock()
        {
            InitializeComponent();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btn_In_Click(object sender, EventArgs e)
        {
            string sql = "insert into CargoGoods values ((select ID from Cargo where ShelfID = (select ID from Shelf where Name='智能销售货架01') and Name='货仓1'),'E0AACE74A9548CD3')";
            sql = string.Format(sql,tbx_cargo.Text ,lbl_label.Text);
            if (lbl_label.Text == "")
            {
                MessageBox.Show(String.Format("标签号为空!"));
                return;
            }
            int n = SqlDbHelper.ExecuteInsertSql(sql);
            if (n > 0)
            {
                MessageBox.Show(String.Format("插入成功!"));
            }
            else
            {
                MessageBox.Show(String.Format("插入失败!"));
            }
        }

        private void btn_OpenShelf_Click(object sender, EventArgs e)
        {
            ShelfTreeView stv=new ShelfTreeView();
            stv.Show();
            this.tbx_cargo.Text = stv.str_Info;
        }

        private void InStock_Load(object sender, EventArgs e)
        {
            string PortID= "COM102";
            byte result = reader.OpenSerialPort(PortID);
            if (ReadCardID())
            {
                lbl_label.Visible = true;
            }
            string sql = "select * from Model where ID=(select ModelID from Goods where ID='" + lbl_label.Text + "')";
            DataSet ds=SqlDbHelper.ExecuteSelectSql(sql);
            tbx_time.Text = ds.Tables[0].Rows[0][7].ToString();
            tbx_style.Text=ds.Tables[0].Rows[0][2].ToString();
        }


        #region 读取卡号
        public bool ReadCardID()
        {
            try
            {
                int tagCount = 0;
                string[] tagNumber = null;
                byte result = reader.Inventory(ModulateMethod.FSK, InventoryModel.Multiple, ref tagCount, ref tagNumber);
                //判断是否读取到卡号
                if (result == 0x00)
                {
                    lbl_label.Text = tagNumber[0];
                    a = false;
                    return true;
                }
                else
                {
                    //判断是否有对话框弹出
                    a = false;
                    MessageBox.Show("寻卡失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        #endregion
    }
}
