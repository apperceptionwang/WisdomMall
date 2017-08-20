namespace WisdomMall
{
    partial class InStock
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
            this.gb_ProInfo = new System.Windows.Forms.GroupBox();
            this.btn_OpenShelf = new System.Windows.Forms.Button();
            this.tbx_cargo = new System.Windows.Forms.TextBox();
            this.tbx_time = new System.Windows.Forms.TextBox();
            this.tbx_style = new System.Windows.Forms.TextBox();
            this.lbl_time = new System.Windows.Forms.Label();
            this.lbl_cargo = new System.Windows.Forms.Label();
            this.lbl_style = new System.Windows.Forms.Label();
            this.gb_Label = new System.Windows.Forms.GroupBox();
            this.lbl_label = new System.Windows.Forms.Label();
            this.btn_In = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.gb_ProInfo.SuspendLayout();
            this.gb_Label.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_ProInfo
            // 
            this.gb_ProInfo.Controls.Add(this.btn_OpenShelf);
            this.gb_ProInfo.Controls.Add(this.tbx_cargo);
            this.gb_ProInfo.Controls.Add(this.tbx_time);
            this.gb_ProInfo.Controls.Add(this.tbx_style);
            this.gb_ProInfo.Controls.Add(this.lbl_time);
            this.gb_ProInfo.Controls.Add(this.lbl_cargo);
            this.gb_ProInfo.Controls.Add(this.lbl_style);
            this.gb_ProInfo.Location = new System.Drawing.Point(39, 51);
            this.gb_ProInfo.Name = "gb_ProInfo";
            this.gb_ProInfo.Size = new System.Drawing.Size(283, 226);
            this.gb_ProInfo.TabIndex = 0;
            this.gb_ProInfo.TabStop = false;
            this.gb_ProInfo.Text = "商品信息";
            // 
            // btn_OpenShelf
            // 
            this.btn_OpenShelf.Location = new System.Drawing.Point(256, 147);
            this.btn_OpenShelf.Name = "btn_OpenShelf";
            this.btn_OpenShelf.Size = new System.Drawing.Size(21, 23);
            this.btn_OpenShelf.TabIndex = 5;
            this.btn_OpenShelf.UseVisualStyleBackColor = true;
            this.btn_OpenShelf.Click += new System.EventHandler(this.btn_OpenShelf_Click);
            // 
            // tbx_cargo
            // 
            this.tbx_cargo.Location = new System.Drawing.Point(135, 149);
            this.tbx_cargo.Name = "tbx_cargo";
            this.tbx_cargo.Size = new System.Drawing.Size(100, 21);
            this.tbx_cargo.TabIndex = 4;
            // 
            // tbx_time
            // 
            this.tbx_time.Location = new System.Drawing.Point(135, 89);
            this.tbx_time.Name = "tbx_time";
            this.tbx_time.Size = new System.Drawing.Size(100, 21);
            this.tbx_time.TabIndex = 3;
            // 
            // tbx_style
            // 
            this.tbx_style.Location = new System.Drawing.Point(135, 34);
            this.tbx_style.Name = "tbx_style";
            this.tbx_style.ReadOnly = true;
            this.tbx_style.Size = new System.Drawing.Size(100, 21);
            this.tbx_style.TabIndex = 2;
            // 
            // lbl_time
            // 
            this.lbl_time.AutoSize = true;
            this.lbl_time.Location = new System.Drawing.Point(24, 89);
            this.lbl_time.Name = "lbl_time";
            this.lbl_time.Size = new System.Drawing.Size(47, 12);
            this.lbl_time.TabIndex = 1;
            this.lbl_time.Text = "保质期:";
            // 
            // lbl_cargo
            // 
            this.lbl_cargo.AutoSize = true;
            this.lbl_cargo.Location = new System.Drawing.Point(36, 149);
            this.lbl_cargo.Name = "lbl_cargo";
            this.lbl_cargo.Size = new System.Drawing.Size(35, 12);
            this.lbl_cargo.TabIndex = 1;
            this.lbl_cargo.Text = "货仓:";
            // 
            // lbl_style
            // 
            this.lbl_style.AutoSize = true;
            this.lbl_style.Location = new System.Drawing.Point(36, 37);
            this.lbl_style.Name = "lbl_style";
            this.lbl_style.Size = new System.Drawing.Size(35, 12);
            this.lbl_style.TabIndex = 0;
            this.lbl_style.Text = "款型:";
            // 
            // gb_Label
            // 
            this.gb_Label.Controls.Add(this.lbl_label);
            this.gb_Label.Location = new System.Drawing.Point(361, 60);
            this.gb_Label.Name = "gb_Label";
            this.gb_Label.Size = new System.Drawing.Size(213, 217);
            this.gb_Label.TabIndex = 1;
            this.gb_Label.TabStop = false;
            this.gb_Label.Text = "标签号";
            // 
            // lbl_label
            // 
            this.lbl_label.AutoSize = true;
            this.lbl_label.Location = new System.Drawing.Point(82, 83);
            this.lbl_label.Name = "lbl_label";
            this.lbl_label.Size = new System.Drawing.Size(0, 12);
            this.lbl_label.TabIndex = 0;
            // 
            // btn_In
            // 
            this.btn_In.Location = new System.Drawing.Point(361, 297);
            this.btn_In.Name = "btn_In";
            this.btn_In.Size = new System.Drawing.Size(75, 23);
            this.btn_In.TabIndex = 2;
            this.btn_In.Text = "开始入库";
            this.btn_In.UseVisualStyleBackColor = true;
            this.btn_In.Click += new System.EventHandler(this.btn_In_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(483, 297);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 3;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // InStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 341);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_In);
            this.Controls.Add(this.gb_Label);
            this.Controls.Add(this.gb_ProInfo);
            this.Name = "InStock";
            this.Text = "商品入库";
            this.Load += new System.EventHandler(this.InStock_Load);
            this.gb_ProInfo.ResumeLayout(false);
            this.gb_ProInfo.PerformLayout();
            this.gb_Label.ResumeLayout(false);
            this.gb_Label.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_ProInfo;
        private System.Windows.Forms.GroupBox gb_Label;
        private System.Windows.Forms.Button btn_In;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.TextBox tbx_time;
        private System.Windows.Forms.TextBox tbx_style;
        private System.Windows.Forms.Label lbl_time;
        private System.Windows.Forms.Label lbl_cargo;
        private System.Windows.Forms.Label lbl_style;
        private System.Windows.Forms.Label lbl_label;
        private System.Windows.Forms.Button btn_OpenShelf;
        public System.Windows.Forms.TextBox tbx_cargo;
    }
}