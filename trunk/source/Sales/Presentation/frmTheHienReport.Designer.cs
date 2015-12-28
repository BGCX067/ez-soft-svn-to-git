namespace Sales
{
    partial class frmTheHienReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTheHienReport));
            this.crystalReportViewer_TheHien = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crystalReportViewer_TheHien
            // 
            this.crystalReportViewer_TheHien.ActiveViewIndex = -1;
            this.crystalReportViewer_TheHien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer_TheHien.DisplayGroupTree = false;
            this.crystalReportViewer_TheHien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer_TheHien.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewer_TheHien.Name = "crystalReportViewer_TheHien";
            this.crystalReportViewer_TheHien.SelectionFormula = "";
            this.crystalReportViewer_TheHien.Size = new System.Drawing.Size(792, 483);
            this.crystalReportViewer_TheHien.TabIndex = 0;
            this.crystalReportViewer_TheHien.ViewTimeSelectionFormula = "";
            // 
            // frmTheHienReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 483);
            this.Controls.Add(this.crystalReportViewer_TheHien);
            this.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmTheHienReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hien Thi Du Lieu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmTheHienReport_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer_TheHien;

    }
}