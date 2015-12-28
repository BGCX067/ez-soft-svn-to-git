namespace Sales
{
    partial class frmInSanPham
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnHaiGia = new System.Windows.Forms.Button();
            this.btnGiaLe = new System.Windows.Forms.Button();
            this.btnGiaSi = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(415, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bạn muốn in \"Bảng Báo Giá\" : Giá Sỉ hay Giá Lẻ hoặc Cả 2 Giá";
            // 
            // btnHaiGia
            // 
            this.btnHaiGia.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHaiGia.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHaiGia.Location = new System.Drawing.Point(291, 62);
            this.btnHaiGia.Margin = new System.Windows.Forms.Padding(2);
            this.btnHaiGia.Name = "btnHaiGia";
            this.btnHaiGia.Size = new System.Drawing.Size(83, 29);
            this.btnHaiGia.TabIndex = 27;
            this.btnHaiGia.Text = "Cả Hai Giá";
            this.btnHaiGia.UseVisualStyleBackColor = true;
            this.btnHaiGia.Visible = false;
            // 
            // btnGiaLe
            // 
            this.btnGiaLe.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGiaLe.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGiaLe.Location = new System.Drawing.Point(185, 62);
            this.btnGiaLe.Margin = new System.Windows.Forms.Padding(2);
            this.btnGiaLe.Name = "btnGiaLe";
            this.btnGiaLe.Size = new System.Drawing.Size(69, 29);
            this.btnGiaLe.TabIndex = 26;
            this.btnGiaLe.Text = "Giá Lẻ";
            this.btnGiaLe.UseVisualStyleBackColor = true;
            this.btnGiaLe.Visible = false;
            // 
            // btnGiaSi
            // 
            this.btnGiaSi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGiaSi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGiaSi.Location = new System.Drawing.Point(75, 62);
            this.btnGiaSi.Margin = new System.Windows.Forms.Padding(2);
            this.btnGiaSi.Name = "btnGiaSi";
            this.btnGiaSi.Size = new System.Drawing.Size(69, 29);
            this.btnGiaSi.TabIndex = 25;
            this.btnGiaSi.Text = "Giá Sỉ";
            this.btnGiaSi.UseVisualStyleBackColor = true;
            this.btnGiaSi.Visible = false;
            this.btnGiaSi.Click += new System.EventHandler(this.btnGiaSi_Click);
            // 
            // frmInSanPham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 123);
            this.Controls.Add(this.btnHaiGia);
            this.Controls.Add(this.btnGiaLe);
            this.Controls.Add(this.btnGiaSi);
            this.Controls.Add(this.label1);
            this.Name = "frmInSanPham";
            this.Text = "In San Pham";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnHaiGia;
        private System.Windows.Forms.Button btnGiaLe;
        private System.Windows.Forms.Button btnGiaSi;
    }
}