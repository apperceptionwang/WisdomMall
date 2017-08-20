namespace WisdomMall
{
    partial class ProductInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        public void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("所有商品");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductInfo));
            this.tv_ProductInfo = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tp_List = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtn_add = new System.Windows.Forms.ToolStripButton();
            this.tsbtn_Edit = new System.Windows.Forms.ToolStripButton();
            this.tsbtn_Delete = new System.Windows.Forms.ToolStripButton();
            this.tsbtn_Change = new System.Windows.Forms.ToolStripButton();
            this.tsbtn_Set = new System.Windows.Forms.ToolStripButton();
            this.listView1 = new System.Windows.Forms.ListView();
            this.tp_Info = new System.Windows.Forms.TabPage();
            this.sM_DBDataSet = new WisdomMall.SM_DBDataSet();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsbtn_addNode = new System.Windows.Forms.ToolStripButton();
            this.tsbtn_deleteNode = new System.Windows.Forms.ToolStripButton();
            this.tabControl1.SuspendLayout();
            this.tp_List.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sM_DBDataSet)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tv_ProductInfo
            // 
            this.tv_ProductInfo.Location = new System.Drawing.Point(-9, 41);
            this.tv_ProductInfo.Name = "tv_ProductInfo";
            treeNode1.Name = "root";
            treeNode1.Text = "所有商品";
            this.tv_ProductInfo.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.tv_ProductInfo.Size = new System.Drawing.Size(261, 542);
            this.tv_ProductInfo.TabIndex = 0;
            this.tv_ProductInfo.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_ProductInfo_AfterSelect);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tp_List);
            this.tabControl1.Controls.Add(this.tp_Info);
            this.tabControl1.Location = new System.Drawing.Point(258, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(827, 571);
            this.tabControl1.TabIndex = 3;
            // 
            // tp_List
            // 
            this.tp_List.Controls.Add(this.toolStrip1);
            this.tp_List.Controls.Add(this.listView1);
            this.tp_List.Location = new System.Drawing.Point(4, 22);
            this.tp_List.Name = "tp_List";
            this.tp_List.Padding = new System.Windows.Forms.Padding(3);
            this.tp_List.Size = new System.Drawing.Size(819, 545);
            this.tp_List.TabIndex = 0;
            this.tp_List.Text = "商品列表";
            this.tp_List.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtn_add,
            this.tsbtn_Edit,
            this.tsbtn_Delete,
            this.tsbtn_Change,
            this.tsbtn_Set});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(813, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtn_add
            // 
            this.tsbtn_add.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_add.Image")));
            this.tsbtn_add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_add.Name = "tsbtn_add";
            this.tsbtn_add.Size = new System.Drawing.Size(76, 22);
            this.tsbtn_add.Text = "新增商品";
            this.tsbtn_add.Click += new System.EventHandler(this.tsbtn_add_Click);
            // 
            // tsbtn_Edit
            // 
            this.tsbtn_Edit.Enabled = false;
            this.tsbtn_Edit.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_Edit.Image")));
            this.tsbtn_Edit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_Edit.Name = "tsbtn_Edit";
            this.tsbtn_Edit.Size = new System.Drawing.Size(76, 22);
            this.tsbtn_Edit.Text = "编辑商品";
            this.tsbtn_Edit.Click += new System.EventHandler(this.tsbtn_Edit_Click);
            // 
            // tsbtn_Delete
            // 
            this.tsbtn_Delete.Enabled = false;
            this.tsbtn_Delete.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_Delete.Image")));
            this.tsbtn_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_Delete.Name = "tsbtn_Delete";
            this.tsbtn_Delete.Size = new System.Drawing.Size(76, 22);
            this.tsbtn_Delete.Text = "删除商品";
            // 
            // tsbtn_Change
            // 
            this.tsbtn_Change.Enabled = false;
            this.tsbtn_Change.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_Change.Image")));
            this.tsbtn_Change.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_Change.Name = "tsbtn_Change";
            this.tsbtn_Change.Size = new System.Drawing.Size(76, 22);
            this.tsbtn_Change.Text = "改变标签";
            // 
            // tsbtn_Set
            // 
            this.tsbtn_Set.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_Set.Image")));
            this.tsbtn_Set.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_Set.Name = "tsbtn_Set";
            this.tsbtn_Set.Size = new System.Drawing.Size(76, 22);
            this.tsbtn_Set.Text = "设置条码";
            // 
            // listView1
            // 
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(-4, 59);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(815, 490);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // tp_Info
            // 
            this.tp_Info.Location = new System.Drawing.Point(4, 22);
            this.tp_Info.Name = "tp_Info";
            this.tp_Info.Padding = new System.Windows.Forms.Padding(3);
            this.tp_Info.Size = new System.Drawing.Size(819, 545);
            this.tp_Info.TabIndex = 1;
            this.tp_Info.Text = "款型信息";
            this.tp_Info.UseVisualStyleBackColor = true;
            // 
            // sM_DBDataSet
            // 
            this.sM_DBDataSet.DataSetName = "SM_DBDataSet";
            this.sM_DBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtn_addNode,
            this.tsbtn_deleteNode});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1085, 25);
            this.toolStrip2.TabIndex = 4;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsbtn_addNode
            // 
            this.tsbtn_addNode.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_addNode.Image")));
            this.tsbtn_addNode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_addNode.Name = "tsbtn_addNode";
            this.tsbtn_addNode.Size = new System.Drawing.Size(52, 22);
            this.tsbtn_addNode.Text = "新增";
            // 
            // tsbtn_deleteNode
            // 
            this.tsbtn_deleteNode.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_deleteNode.Image")));
            this.tsbtn_deleteNode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_deleteNode.Name = "tsbtn_deleteNode";
            this.tsbtn_deleteNode.Size = new System.Drawing.Size(52, 22);
            this.tsbtn_deleteNode.Text = "删除";
            // 
            // ProductInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1085, 579);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.tv_ProductInfo);
            this.Name = "ProductInfo";
            this.Text = "商品信息";
            this.Load += new System.EventHandler(this.ProductInfo_Load);
            this.tabControl1.ResumeLayout(false);
            this.tp_List.ResumeLayout(false);
            this.tp_List.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sM_DBDataSet)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tv_ProductInfo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tp_List;
        public System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TabPage tp_Info;
        private SM_DBDataSet sM_DBDataSet;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtn_add;
        private System.Windows.Forms.ToolStripButton tsbtn_Edit;
        private System.Windows.Forms.ToolStripButton tsbtn_Delete;
        private System.Windows.Forms.ToolStripButton tsbtn_Change;
        private System.Windows.Forms.ToolStripButton tsbtn_Set;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsbtn_addNode;
        private System.Windows.Forms.ToolStripButton tsbtn_deleteNode;
    }
}