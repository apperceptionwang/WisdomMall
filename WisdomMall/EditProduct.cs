using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WisdomMall
{
    public partial class EditProduct : Form
    {
        public List<string> li_ProInfo;
        public EditProduct(ref List<string> li_ProInfo)
        {
            if (li_ProInfo != null)
            {
                MessageBox.Show(li_ProInfo.Count.ToString());
            }
            //for (int i = 0; i < li_ProInfo.Count;i++ )
            //{
            //    this.li_ProInfo.Add(li_ProInfo[i]);
            //}
            InitializeComponent();
            
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {

            MessageBox.Show(li_ProInfo.Count.ToString());
            this.Close();
        }

        private void btn_Modify_Click(object sender, EventArgs e)
        {
            string sql = "update Goods set Color='{0}',Size={1} where ID='{2}'";
            sql = string.Format(sql, cmb_color.Text, cmb_size.Text, lbl_label.Text);
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

        private void EditProduct_Load(object sender, EventArgs e)
        {
            ProductInfo PInfo = new ProductInfo();
            // TODO: 这行代码将数据加载到表“sM_DBDataSet.Model”中。您可以根据需要移动或删除它。
            this.modelTableAdapter.Fill(this.sM_DBDataSet.Model);
            // TODO: 这行代码将数据加载到表“sM_DBDataSet.Goods”中。您可以根据需要移动或删除它。
            this.goodsTableAdapter.Fill(this.sM_DBDataSet.Goods);
            lbl_label.Text = li_ProInfo[0];
            cmb_color.Text = li_ProInfo[2];
            cmb_size.Text = li_ProInfo[3];
            dateTimePicker1.Text = li_ProInfo[4];
            cmb_Type.Text=li_ProInfo[1];
        }
    }
}
