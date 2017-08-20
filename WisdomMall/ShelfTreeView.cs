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
    public partial class ShelfTreeView : Form
    {
        string strInfo;
        public string str_Info
        { 
            get
            {
                return this.strInfo;
            }
            //set
            //{
            //    str_Info = strInfo;
            //}
        }
        //重写控件show方法
        //public string show(string str)
        //{
        //    this.Show();
        //    return "str_Info";
        //}

        public ShelfTreeView()
        {
            InitializeComponent();
        }

        private void ShelfTreeView_Load(object sender, EventArgs e)
        {
            LoadShelf(this.tv_Shelf);
        }

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
                    tv.Nodes[i].Nodes.Add(ds_cargo.Tables[0].Rows[j][0].ToString(), ds_cargo.Tables[0].Rows[j][3].ToString());

                }

            }


        }

        private void btn_SelectCargo_Click(object sender, EventArgs e)
        {
            strInfo=tv_Shelf.SelectedNode.Text;

        }

    }
}
