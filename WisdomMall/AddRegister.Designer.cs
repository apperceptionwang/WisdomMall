namespace WisdomMall
{
    partial class AddRegister
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmb_PortID = new System.Windows.Forms.ComboBox();
            this.txt_CardID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_OpenSerialPort = new System.Windows.Forms.Button();
            this.btn_ReadCardID = new System.Windows.Forms.Button();
            this.btnGoodsLogin = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmb_Type = new System.Windows.Forms.ComboBox();
            this.sM_DBDataSet = new WisdomMall.SM_DBDataSet();
            this.sMDBDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.gbx_label = new System.Windows.Forms.GroupBox();
            this.lbl_label = new System.Windows.Forms.Label();
            this.cmb_size = new System.Windows.Forms.ComboBox();
            this.cmb_color = new System.Windows.Forms.ComboBox();
            this.modelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.modelTableAdapter = new WisdomMall.SM_DBDataSetTableAdapters.ModelTableAdapter();
            this.goodsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.goodsTableAdapter = new WisdomMall.SM_DBDataSetTableAdapters.GoodsTableAdapter();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sM_DBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sMDBDataSetBindingSource)).BeginInit();
            this.gbx_label.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.modelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.goodsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmb_PortID);
            this.groupBox1.Controls.Add(this.txt_CardID);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btn_OpenSerialPort);
            this.groupBox1.Controls.Add(this.btn_ReadCardID);
            this.groupBox1.Controls.Add(this.btnGoodsLogin);
            this.groupBox1.Location = new System.Drawing.Point(473, 162);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(286, 369);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // cmb_PortID
            // 
            this.cmb_PortID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_PortID.FormattingEnabled = true;
            this.cmb_PortID.Location = new System.Drawing.Point(100, 27);
            this.cmb_PortID.Name = "cmb_PortID";
            this.cmb_PortID.Size = new System.Drawing.Size(139, 20);
            this.cmb_PortID.TabIndex = 12;
            // 
            // txt_CardID
            // 
            this.txt_CardID.Location = new System.Drawing.Point(100, 73);
            this.txt_CardID.Name = "txt_CardID";
            this.txt_CardID.ReadOnly = true;
            this.txt_CardID.Size = new System.Drawing.Size(139, 21);
            this.txt_CardID.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "卡    号：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "串 口 号：";
            // 
            // btn_OpenSerialPort
            // 
            this.btn_OpenSerialPort.Location = new System.Drawing.Point(9, 328);
            this.btn_OpenSerialPort.Name = "btn_OpenSerialPort";
            this.btn_OpenSerialPort.Size = new System.Drawing.Size(75, 23);
            this.btn_OpenSerialPort.TabIndex = 2;
            this.btn_OpenSerialPort.Text = "打开串口";
            this.btn_OpenSerialPort.UseVisualStyleBackColor = true;
            this.btn_OpenSerialPort.Click += new System.EventHandler(this.btn_OpenSerialPort_Click);
            // 
            // btn_ReadCardID
            // 
            this.btn_ReadCardID.Location = new System.Drawing.Point(101, 328);
            this.btn_ReadCardID.Name = "btn_ReadCardID";
            this.btn_ReadCardID.Size = new System.Drawing.Size(75, 23);
            this.btn_ReadCardID.TabIndex = 1;
            this.btn_ReadCardID.Text = "读取卡号";
            this.btn_ReadCardID.UseVisualStyleBackColor = true;
            this.btn_ReadCardID.Click += new System.EventHandler(this.btn_ReadCardID_Click);
            // 
            // btnGoodsLogin
            // 
            this.btnGoodsLogin.Location = new System.Drawing.Point(205, 328);
            this.btnGoodsLogin.Name = "btnGoodsLogin";
            this.btnGoodsLogin.Size = new System.Drawing.Size(75, 23);
            this.btnGoodsLogin.TabIndex = 0;
            this.btnGoodsLogin.Text = "商品注册";
            this.btnGoodsLogin.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "颜色：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 248);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "出厂日期:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "尺码:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmb_color);
            this.groupBox2.Controls.Add(this.cmb_size);
            this.groupBox2.Controls.Add(this.cmb_Type);
            this.groupBox2.Controls.Add(this.btn_Cancel);
            this.groupBox2.Controls.Add(this.btn_OK);
            this.groupBox2.Controls.Add(this.dateTimePicker1);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(56, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(325, 386);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "登记信息";
            // 
            // cmb_Type
            // 
            this.cmb_Type.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.sM_DBDataSet, "Type.ID", true));
            this.cmb_Type.DataSource = this.modelBindingSource;
            this.cmb_Type.DisplayMember = "Name";
            this.cmb_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Type.FormattingEnabled = true;
            this.cmb_Type.Location = new System.Drawing.Point(118, 44);
            this.cmb_Type.Name = "cmb_Type";
            this.cmb_Type.Size = new System.Drawing.Size(138, 20);
            this.cmb_Type.TabIndex = 23;
            this.cmb_Type.ValueMember = "Name";
            // 
            // sM_DBDataSet
            // 
            this.sM_DBDataSet.DataSetName = "SM_DBDataSet";
            this.sM_DBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // sMDBDataSetBindingSource
            // 
            this.sMDBDataSetBindingSource.DataSource = this.sM_DBDataSet;
            this.sMDBDataSetBindingSource.Position = 0;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(154, 309);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 21;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(6, 309);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 20;
            this.btn_OK.Text = "确认";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(120, 250);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(140, 21);
            this.dateTimePicker1.TabIndex = 19;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 12);
            this.label9.TabIndex = 16;
            this.label9.Text = "款型:";
            // 
            // gbx_label
            // 
            this.gbx_label.Controls.Add(this.lbl_label);
            this.gbx_label.Location = new System.Drawing.Point(473, 12);
            this.gbx_label.Name = "gbx_label";
            this.gbx_label.Size = new System.Drawing.Size(245, 144);
            this.gbx_label.TabIndex = 3;
            this.gbx_label.TabStop = false;
            this.gbx_label.Text = "标签号";
            // 
            // lbl_label
            // 
            this.lbl_label.AutoSize = true;
            this.lbl_label.Location = new System.Drawing.Point(68, 62);
            this.lbl_label.Name = "lbl_label";
            this.lbl_label.Size = new System.Drawing.Size(0, 12);
            this.lbl_label.TabIndex = 0;
            // 
            // cmb_size
            // 
            this.cmb_size.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.sM_DBDataSet, "Type.ID", true));
            this.cmb_size.DataSource = this.goodsBindingSource;
            this.cmb_size.DisplayMember = "Size";
            this.cmb_size.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_size.FormattingEnabled = true;
            this.cmb_size.Location = new System.Drawing.Point(120, 169);
            this.cmb_size.Name = "cmb_size";
            this.cmb_size.Size = new System.Drawing.Size(138, 20);
            this.cmb_size.TabIndex = 24;
            this.cmb_size.ValueMember = "Size";
            // 
            // cmb_color
            // 
            this.cmb_color.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.sM_DBDataSet, "Type.ID", true));
            this.cmb_color.DataSource = this.goodsBindingSource;
            this.cmb_color.DisplayMember = "Color";
            this.cmb_color.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_color.FormattingEnabled = true;
            this.cmb_color.Location = new System.Drawing.Point(120, 102);
            this.cmb_color.Name = "cmb_color";
            this.cmb_color.Size = new System.Drawing.Size(138, 20);
            this.cmb_color.TabIndex = 25;
            this.cmb_color.ValueMember = "Color";
            // 
            // modelBindingSource
            // 
            this.modelBindingSource.DataMember = "Model";
            this.modelBindingSource.DataSource = this.sMDBDataSetBindingSource;
            // 
            // modelTableAdapter
            // 
            this.modelTableAdapter.ClearBeforeFill = true;
            // 
            // goodsBindingSource
            // 
            this.goodsBindingSource.DataMember = "Goods";
            this.goodsBindingSource.DataSource = this.sMDBDataSetBindingSource;
            // 
            // goodsTableAdapter
            // 
            this.goodsTableAdapter.ClearBeforeFill = true;
            // 
            // AddRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 567);
            this.Controls.Add(this.gbx_label);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "AddRegister";
            this.Text = "添加商品";
            this.Load += new System.EventHandler(this.AddRegister_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sM_DBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sMDBDataSetBindingSource)).EndInit();
            this.gbx_label.ResumeLayout(false);
            this.gbx_label.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.modelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.goodsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmb_PortID;
        private System.Windows.Forms.TextBox txt_CardID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_OpenSerialPort;
        private System.Windows.Forms.Button btn_ReadCardID;
        private System.Windows.Forms.Button btnGoodsLogin;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.BindingSource sMDBDataSetBindingSource;
        private SM_DBDataSet sM_DBDataSet;
        private System.Windows.Forms.ComboBox cmb_Type;
        private System.Windows.Forms.GroupBox gbx_label;
        private System.Windows.Forms.Label lbl_label;
        private System.Windows.Forms.ComboBox cmb_color;
        private System.Windows.Forms.ComboBox cmb_size;
        private System.Windows.Forms.BindingSource modelBindingSource;
        private SM_DBDataSetTableAdapters.ModelTableAdapter modelTableAdapter;
        private System.Windows.Forms.BindingSource goodsBindingSource;
        private SM_DBDataSetTableAdapters.GoodsTableAdapter goodsTableAdapter;

    }
}