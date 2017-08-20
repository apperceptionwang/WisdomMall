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
    public partial class ProductInfo : Form
    {
        static List<TreeNode> nodeList = new List<TreeNode>();
        List<string> li = new List<string>();
        public ProductInfo()
        {
            InitializeComponent();
        }


        #region listview配置
        private void ProInfo_Load(ListView lv,string sql)
        {
            
            DataSet ds_goodsOther = SqlDbHelper.ExecuteSelectSql(sql);
            for (int i = 0; i < ds_goodsOther.Tables[0].Columns.Count; i++)
            {
                lv.Columns.Add(ds_goodsOther.Tables[0].Columns[i].ColumnName.ToString(), 120, HorizontalAlignment.Left);
            }
            List<ListViewItem> lvi = new List<ListViewItem>();
            for (int j = 0; j < ds_goodsOther.Tables[0].Rows.Count; j++)
            {
                lvi.Add(new ListViewItem(ds_goodsOther.Tables[0].Rows[j][0].ToString()));//添加首项
                for (int k = 1; k < ds_goodsOther.Tables[0].Columns.Count; k++)
                {
                    lvi[j].SubItems.Add(ds_goodsOther.Tables[0].Rows[j][k].ToString());
                }
                lv.Items.Add(lvi[j]);
                

            }
        }
        #endregion


        #region 加载界面信息
        private void ProductInfo_Load(object sender, EventArgs e)
        {
            string sql_goods="select * from  Goods";
            ProInfo_Load(this.listView1,sql_goods);
            string sql = "select * from  Type";
            string sql_Model = "select * from Model";
            DataSet ds = SqlDbHelper.ExecuteSelectSql(sql);
            DataSet ds_Model = SqlDbHelper.ExecuteSelectSql(sql_Model);
            TreeNode root = tv_ProductInfo.GetNodeAt(0, 0);//根节点
            for (int j = 0, i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (int.Parse(ds.Tables[0].Rows[i][2].ToString()) == 0)
                {

                    root.Nodes.Add(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString());
                    j++;
                    continue;
                }
                int root_Next_C = root.Nodes.Count;
                for (int k = 0; k < root_Next_C; k++)//此时root_Next_C=2 
                {
                    if (ds.Tables[0].Rows[i][2].ToString().Equals(ds.Tables[0].Rows[k][0].ToString()))
                    {
                        root.Nodes[k].Nodes.Add(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString());

                    }

                }


            }
                for (int k = 2; k < ds.Tables[0].Rows.Count; k++)
                {
                    string sql_Goods = "select * from  Model,Type where Type.ID=" + int.Parse(ds.Tables[0].Rows[k][0].ToString()) + " and Model.TypeID =" + int.Parse(ds.Tables[0].Rows[k][0].ToString());
                    DataSet ds_goods = SqlDbHelper.ExecuteSelectSql(sql_Goods);
                    for (int count = 0,j =0; count < ds_goods.Tables[0].Rows.Count; count++)
                    {

                            root.Nodes[j].Nodes[k-2].Nodes.Add(ds_goods.Tables[0].Rows[count][0].ToString(), ds_goods.Tables[0].Rows[count][2].ToString());

                        
                        
                        
                        //switch(j)
                        //{
                        //    case 0:
                        //        root.Nodes[0].Nodes[k - 2].Nodes.Add(ds_goods.Tables[0].Rows[count][0].ToString(), ds_goods.Tables[0].Rows[count][2].ToString());
                        //        break;
                        //    case 1:
                        //        root.Nodes[1].Nodes[k - 2].Nodes.Add(ds_goods.Tables[0].Rows[count][0].ToString(), ds_goods.Tables[0].Rows[count][2].ToString());
                        //        break;
                        //}
                            
                    }
                }

            
            


            
        }
        #endregion

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
           
        }

        private void tsbtn_add_Click(object sender, EventArgs e)
        {
            new AddRegister().Show();
        }

        private void tsbtn_Edit_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < this.listView1.Columns.Count; i++)
            {
                li.Add(this.listView1.FocusedItem.SubItems[i].Text);
                MessageBox.Show(li[i]);
            }
            new EditProduct(ref li).Show();
        }

        public void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                tsbtn_Edit.Enabled = true;
                tsbtn_Delete.Enabled = true;
                tsbtn_Change.Enabled = true;
            }
        }

        private void tv_ProductInfo_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string sql=null;
            string sql_other = null;
            DataSet ds = null;
            int n = tv_ProductInfo.SelectedNode.Level;
            switch(n)
            {
                case 0:
                    listView1.Clear();
                    sql = "select * from Goods";
                    ProInfo_Load(this.listView1, sql);
                    break;
                case 1:
                    listView1.Clear();
                    sql = "select * from Goods where ModelID in (select ID from Model where TypeID in (select ID from Type where ParentID =(select ID from Type where Name='" + tv_ProductInfo.SelectedNode.Text + "')))";
                    ProInfo_Load(this.listView1, sql);
                    break;
                case 2:
                    listView1.Clear();
                    sql= "select ID from Model where (select ID from Type where Name='" + tv_ProductInfo.SelectedNode.Text+ "')=TypeID";
                    ds = SqlDbHelper.ExecuteSelectSql(sql);
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        sql_other = "select * from Goods where ModelID=" + int.Parse(ds.Tables[0].Rows[i][0].ToString());
                        ProInfo_Load(this.listView1, sql_other);
                    }
                    
                    break;     
                case 3:
                    listView1.Clear();
                    sql = "select * from Goods where ModelID=(select ID from Model where Name='" + tv_ProductInfo.SelectedNode.Text + "')";
                    ProInfo_Load(this.listView1, sql);
                    break;
                    
            }
            
        }

        //#region 迭代
        //private void FetchNode(TreeNode node)
        //{
        //    nodeList.Add(node);
        //    for (int i = 0; i < node.Nodes.Count; i++)
        //    {
        //        FetchNode(node.Nodes[i]);
        //    }
            
        //}
        //#endregion

        //#region 遍历
        //private void ergodic()
        //{
        //    for (int i = 0; i < this.tv_ProductInfo.Nodes.Count; i++)
        //    {
        //        FetchNode(this.tv_ProductInfo.Nodes[i]);
        //        button1.Text += nodeList[i].Text;
        //    }
        //}
        //#endregion



    }
}
