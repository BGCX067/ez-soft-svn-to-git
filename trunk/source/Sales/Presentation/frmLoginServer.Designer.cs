namespace Sales
{
    partial class frmLoginServer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoginServer));
            this.txtPas = new System.Windows.Forms.TextBox();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.txtLoginname = new System.Windows.Forms.TextBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnDongY = new System.Windows.Forms.Button();
            this.picIconLock = new System.Windows.Forms.PictureBox();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIconLock)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTitle
            // 
            this.pnlTitle.Size = new System.Drawing.Size(439, 24);
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(256, 16);
            this.lblTitle.Text = "CẤU HÌNH KẾT NỐI CƠ SỞ DỮ LIỆU";
            // 
            // txtPas
            // 
            this.txtPas.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPas.Location = new System.Drawing.Point(216, 129);
            this.txtPas.Name = "txtPas";
            this.txtPas.PasswordChar = '*';
            this.txtPas.Size = new System.Drawing.Size(193, 21);
            this.txtPas.TabIndex = 4;
            // 
            // txtDatabase
            // 
            this.txtDatabase.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDatabase.Location = new System.Drawing.Point(216, 67);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(193, 21);
            this.txtDatabase.TabIndex = 2;
            // 
            // txtLoginname
            // 
            this.txtLoginname.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoginname.Location = new System.Drawing.Point(216, 97);
            this.txtLoginname.Name = "txtLoginname";
            this.txtLoginname.Size = new System.Drawing.Size(193, 21);
            this.txtLoginname.TabIndex = 3;
            // 
            // txtServer
            // 
            this.txtServer.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServer.Location = new System.Drawing.Point(216, 38);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(193, 21);
            this.txtServer.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(155, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 15);
            this.label4.TabIndex = 25;
            this.label4.Text = "CSDL";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(146, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 15);
            this.label3.TabIndex = 20;
            this.label3.Text = "Mật Khẩu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(116, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 15);
            this.label2.TabIndex = 26;
            this.label2.Text = "Tên Đăng Nhập";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(128, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 24;
            this.label1.Text = "Tên Máy Chủ";
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(356, 162);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(75, 28);
            this.btnThoat.TabIndex = 6;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnDongY
            // 
            this.btnDongY.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDongY.Location = new System.Drawing.Point(281, 162);
            this.btnDongY.Name = "btnDongY";
            this.btnDongY.Size = new System.Drawing.Size(69, 28);
            this.btnDongY.TabIndex = 5;
            this.btnDongY.Text = "Lưu";
            this.btnDongY.UseVisualStyleBackColor = true;
            this.btnDongY.Click += new System.EventHandler(this.btnDongY_Click);
            // 
            // picIconLock
            // 
            this.picIconLock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.picIconLock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picIconLock.Image = ((System.Drawing.Image)(resources.GetObject("picIconLock.Image")));
            this.picIconLock.Location = new System.Drawing.Point(6, 29);
            this.picIconLock.Name = "picIconLock";
            this.picIconLock.Size = new System.Drawing.Size(110, 128);
            this.picIconLock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picIconLock.TabIndex = 27;
            this.picIconLock.TabStop = false;
            // 
            // frmLoginServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 197);
            this.Controls.Add(this.picIconLock);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnDongY);
            this.Controls.Add(this.txtPas);
            this.Controls.Add(this.txtDatabase);
            this.Controls.Add(this.txtLoginname);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLoginServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmLoginServer_Load);
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtServer, 0);
            this.Controls.SetChildIndex(this.txtLoginname, 0);
            this.Controls.SetChildIndex(this.txtDatabase, 0);
            this.Controls.SetChildIndex(this.txtPas, 0);
            this.Controls.SetChildIndex(this.btnDongY, 0);
            this.Controls.SetChildIndex(this.btnThoat, 0);
            this.Controls.SetChildIndex(this.picIconLock, 0);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIconLock)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPas;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.TextBox txtLoginname;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnDongY;
        private System.Windows.Forms.PictureBox picIconLock;
    }
}