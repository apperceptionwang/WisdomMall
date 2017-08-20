namespace WisdomMall
{
    partial class ShelfInfo
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
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShelfInfo));
            this.tv_Shelf = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tp_List = new System.Windows.Forms.TabPage();
            this.tp_Info = new System.Windows.Forms.TabPage();
            this.sM_DBDataSet = new WisdomMall.SM_DBDataSet();
            this.listView1 = new System.Windows.Forms.ListView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtn_addShelf = new System.Windows.Forms.ToolStripButton();
            this.tsbtn_addWarehouse = new System.Windows.Forms.ToolStripButton();
            this.tsbtn_Delete = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsbtn_Back = new System.Windows.Forms.ToolStripButton();
            this.tabControl1.SuspendLayout();
            this.tp_List.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sM_DBDataSet)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tv_Shelf
            // 
            this.tv_Shelf.Location = new System.Drawing.Point(0, 46);
            this.tv_Shelf.Name = "tv_Shelf";
            this.tv_Shelf.Size = new System.Drawing.Size(324, 513);
            this.tv_Shelf.TabIndex = 4;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tp_List);
            this.tabControl1.Controls.Add(this.tp_Info);
            this.tabControl1.Location = new System.Drawing.Point(330, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(682, 531);
            this.tabControl1.TabIndex = 8;
            // 
            // tp_List
            // 
            this.tp_List.Controls.Add(this.toolStrip2);
            this.tp_List.Controls.Add(this.listView1);
            this.tp_List.Location = new System.Drawing.Point(4, 22);
            this.tp_List.Name = "tp_List";
            this.tp_List.Padding = new System.Windows.Forms.Padding(3);
            this.tp_List.Size = new System.Drawing.Size(674, 505);
            this.tp_List.TabIndex = 1;
            this.tp_List.Text = "商品列表";
            this.tp_List.UseVisualStyleBackColor = true;
            // 
            // tp_Info
            // 
            this.tp_Info.Location = new System.Drawing.Point(4, 22);
            this.tp_Info.Name = "tp_Info";
            this.tp_Info.Size = new System.Drawing.Size(674, 521);
            this.tp_Info.TabIndex = 2;
            this.tp_Info.Text = "货架信息";
            this.tp_Info.UseVisualStyleBackColor = true;
            // 
            // sM_DBDataSet
            // 
            this.sM_DBDataSet.DataSetName = "SM_DBDataSet";
            this.sM_DBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // listView1
            // 
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(6, 31);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(659, 484);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtn_addShelf,
            this.tsbtn_addWarehouse,
            this.tsbtn_Delete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1026, 25);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtn_addShelf
            // 
            this.tsbtn_addShelf.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_addShelf.Image")));
            this.tsbtn_addShelf.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_addShelf.Name = "tsbtn_addShelf";
            this.tsbtn_addShelf.Size = new System.Drawing.Size(76, 22);
            this.tsbtn_addShelf.Text = "新增货架";
            // 
            // tsbtn_addWarehouse
            // 
            this.tsbtn_addWarehouse.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_addWarehouse.Image")));
            this.tsbtn_addWarehouse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_addWarehouse.Name = "tsbtn_addWarehouse";
            this.tsbtn_addWarehouse.Size = new System.Drawing.Size(76, 22);
            this.tsbtn_addWarehouse.Text = "新增货仓";
            // 
            // tsbtn_Delete
            // 
            this.tsbtn_Delete.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_Delete.Image")));
            this.tsbtn_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_Delete.Name = "tsbtn_Delete";
            this.tsbtn_Delete.Size = new System.Drawing.Size(52, 22);
            this.tsbtn_Delete.Text = "删除";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtn_Back});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(668, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsbtn_Back
            // 
            this.tsbtn_Back.Image = ((System.Drawing.Image)(resources.GetObject("tsbtn_Back.Image")));
            this.tsbtn_Back.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtn_Back.Name = "tsbtn_Back";
            this.tsbtn_Back.Size = new System.Drawing.Size(76, 22);
            this.tsbtn_Back.Text = "返回仓库";
            this.tsbtn_Back.Visible = false;
            // 
            // ShelfInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 562);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.tv_Shelf);
            this.Name = "ShelfInfo";
            this.Text = "货架信息";
            this.Load += new System.EventHandler(this.ShelfInfo_Load);
            this.tabControl1.ResumeLayout(false);
            this.tp_List.ResumeLayout(false);
            this.tp_List.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sM_DBDataSet)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tv_Shelf;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tp_List;
        private System.Windows.Forms.TabPage tp_Info;
        private SM_DBDataSet sM_DBDataSet;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtn_addShelf;
        private System.Windows.Forms.ToolStripButton tsbtn_addWarehouse;
        private System.Windows.Forms.ToolStripButton tsbtn_Delete;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsbtn_Back;
    }
}