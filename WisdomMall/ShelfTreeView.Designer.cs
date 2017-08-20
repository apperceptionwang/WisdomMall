namespace WisdomMall
{
    partial class ShelfTreeView
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
            this.tv_Shelf = new System.Windows.Forms.TreeView();
            this.btn_SelectCargo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tv_Shelf
            // 
            this.tv_Shelf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_Shelf.Location = new System.Drawing.Point(0, 0);
            this.tv_Shelf.Name = "tv_Shelf";
            this.tv_Shelf.Size = new System.Drawing.Size(736, 542);
            this.tv_Shelf.TabIndex = 5;
            // 
            // btn_SelectCargo
            // 
            this.btn_SelectCargo.Location = new System.Drawing.Point(522, 253);
            this.btn_SelectCargo.Name = "btn_SelectCargo";
            this.btn_SelectCargo.Size = new System.Drawing.Size(75, 23);
            this.btn_SelectCargo.TabIndex = 6;
            this.btn_SelectCargo.Text = "选定该仓库";
            this.btn_SelectCargo.UseVisualStyleBackColor = true;
            this.btn_SelectCargo.Click += new System.EventHandler(this.btn_SelectCargo_Click);
            // 
            // ShelfTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 542);
            this.Controls.Add(this.btn_SelectCargo);
            this.Controls.Add(this.tv_Shelf);
            this.Name = "ShelfTreeView";
            this.Text = "ShelfTreeView";
            this.Load += new System.EventHandler(this.ShelfTreeView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tv_Shelf;
        private System.Windows.Forms.Button btn_SelectCargo;
    }
}