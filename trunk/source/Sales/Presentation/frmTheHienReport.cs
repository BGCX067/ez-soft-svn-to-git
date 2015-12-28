using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sales
{
    public partial class frmTheHienReport : Form
    {
        public CrystalDecisions.Windows.Forms.CrystalReportViewer CrystalReportViewer_TheHien
        {
            get { return crystalReportViewer_TheHien; }
            set
            {
                crystalReportViewer_TheHien = value;
            }
        }

        public frmTheHienReport()
        {
            InitializeComponent();
        }

        private void frmTheHienReport_Load(object sender, EventArgs e)
        {

        }

    }
}