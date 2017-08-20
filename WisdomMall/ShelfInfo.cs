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
    public partial class ShelfInfo : Form
    {
        public ShelfInfo()
        {
            InitializeComponent();
        }

        private void ShelfInfo_Load(object sender, EventArgs e)
        {
            SetListView();
            LoadShelf(tv_Shelf);

        }


        #region 加载货仓信息
        private void LoadShelf(TreeView tv)
        {
            string sql = "select * from  Shelf";
            
            DataSet ds = SqlDbHelper.ExecuteSelectSql(sql);
            //TreeNode root = tv.GetNodeAt(0, 0);//根节点
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tv.Nodes.Add(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString());
                string sql_ShelfToCargo = "select * from  Cargo where " + int.Parse(ds.Tables[0].Rows[i][0].ToString()) + " =Cargo.ShelfID";
                DataSet ds_cargo = SqlDbHelper.ExecuteSelectSql(sql_ShelfToCargo);
                for (int j = 0; j < ds_cargo.Tables[0].Rows.Count; j++)
                {
                    tv.Nodes[i].Nodes.Add(ds_cargo.Tables[0].Rows[j][0].ToString(),ds_cargo.Tables[0].Rows[j][3].ToString());

                }

            }
            

        }
        #endregion

        #region ListView控件的设置

        /// <summary>
        /// ListView控件的设置
        /// </summary>
        private void SetListView()
        {

            string sql_goods = "select * from  Goods";
            DataSet ds_goods = SqlDbHelper.ExecuteSelectSql(sql_goods);
            for (int i = 0; i < ds_goods.Tables[0].Columns.Count;i++ )
            {
                listView1.Columns.Add(ds_goods.Tables[0].Columns[i].ColumnName.ToString(), 120, HorizontalAlignment.Left);
            }
            List <ListViewItem>lvi=new List<ListViewItem>();
            for (int j = 0; j < ds_goods.Tables[0].Rows.Count; j++)
            {
                lvi.Add(new ListViewItem(ds_goods.Tables[0].Rows[j][0].ToString()));//添加首项
                for (int k = 1; k < ds_goods.Tables[0].Columns.Count;k++)
                {
                    lvi[j].SubItems.Add(ds_goods.Tables[0].Rows[j][k].ToString());
                }
                this.listView1.Items.Add(lvi[j]);
                
            }
            
        }

        #endregion

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                tsbtn_Back.Visible = true;
            }
        }

    }
}
