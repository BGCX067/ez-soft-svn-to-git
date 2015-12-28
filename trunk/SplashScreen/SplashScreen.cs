using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using Microsoft.Win32;

namespace SplashScreen
{
    public class SplashScreen : System.Windows.Forms.Form
    {
        // Threading
        static SplashScreen ms_frmSplash = null;
        static Thread ms_oThread = null;

        // Fade in and out.
        private double m_dblOpacityIncrement = .05;
        private double m_dblOpacityDecrement = .08;
        private const int TIMER_INTERVAL = 50;

        // Status and progress bar
        static string ms_sStatus;
        private double m_dblCompletionFraction = 0;
        private Rectangle m_rProgress;

        // Progress smoothing
        private double m_dblLastCompletionFraction = 0.0;
        private double m_dblPBIncrementPerTimerInterval = .015;

        // Self-calibration support
        private bool m_bFirstLaunch = false;
        private DateTime m_dtStart;
        private bool m_bDTSet = false;
        private int m_iIndex = 1;
        private int m_iActualTicks = 0;
        private ArrayList m_alPreviousCompletionFraction;
        private ArrayList m_alActualTimes = new ArrayList();
        private const string REG_KEY_INITIALIZATION = "Initialization";
        private const string REGVALUE_PB_MILISECOND_INCREMENT = "Increment";
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblStatus;
        private System.ComponentModel.IContainer components;
        private const string REGVALUE_PB_PERCENTS = "Percents";

        #region " Windows Form Designer generated code "

        public SplashScreen()
        {
            InitializeComponent();
            this.Opacity = .00;
            timer1.Interval = TIMER_INTERVAL;
            timer1.Start();
            //this.ClientSize = this.BackgroundImage.Size;
        }

        //Form overrides dispose to clean up the component list.
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        //Required by the Windows Form Designer
        //private System.ComponentModel.IContainer components;

        //NOTE: The following procedure is required by the Windows Form Designer
        //It can be modified using the Windows Form Designer.  
        //Do not modify it using the code editor.
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        //public ProgressEx progressEx1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.PictureBox pictureBox11;
        private System.Windows.Forms.PictureBox pictureBox12;
        private System.Windows.Forms.PictureBox pictureBox13;
        private System.Windows.Forms.PictureBox pictureBox14;
        private System.Windows.Forms.PictureBox pictureBox15;
        private System.Windows.Forms.PictureBox pictureBox16;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lblThreadID;
        public System.Windows.Forms.Label lblLengthyTask;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.PictureBox pictureBox17;
        private System.Windows.Forms.PictureBox pictureBox18;
        private System.Windows.Forms.PictureBox pictureBox19;
        private System.Windows.Forms.PictureBox pictureBox20;
        private System.Windows.Forms.PictureBox pictureBox21;
        private System.Windows.Forms.PictureBox pictureBox22;
        private System.Windows.Forms.PictureBox pictureBox23;
        private System.Windows.Forms.PictureBox pictureBox24;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox picWaiting;
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.PictureBox pictureBox25;
        private System.Windows.Forms.Label lblTimeRemaining;
        private System.Windows.Forms.Label label1;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lblThreadID = new System.Windows.Forms.Label();
            this.lblLengthyTask = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTimeRemaining = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox17 = new System.Windows.Forms.PictureBox();
            this.pictureBox18 = new System.Windows.Forms.PictureBox();
            this.pictureBox19 = new System.Windows.Forms.PictureBox();
            this.pictureBox20 = new System.Windows.Forms.PictureBox();
            this.pictureBox21 = new System.Windows.Forms.PictureBox();
            this.pictureBox22 = new System.Windows.Forms.PictureBox();
            this.pictureBox23 = new System.Windows.Forms.PictureBox();
            this.pictureBox24 = new System.Windows.Forms.PictureBox();
            this.pictureBox25 = new System.Windows.Forms.PictureBox();
            this.picWaiting = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.pictureBox12 = new System.Windows.Forms.PictureBox();
            this.pictureBox13 = new System.Windows.Forms.PictureBox();
            this.pictureBox14 = new System.Windows.Forms.PictureBox();
            this.pictureBox15 = new System.Windows.Forms.PictureBox();
            this.pictureBox16 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox25)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWaiting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox16)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 7F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 7F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox3, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox5, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox6, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox7, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox8, 2, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 8);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.803468F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 92.19653F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(25, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(261, 102);
            this.label1.TabIndex = 6;
            this.label1.Text = "This window is being serviced by thread: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 7F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 7F));
            this.tableLayoutPanel3.Controls.Add(this.pictureBox9, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.pictureBox10, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.pictureBox11, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.pictureBox12, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.pictureBox13, 4, 1);
            this.tableLayoutPanel3.Controls.Add(this.pictureBox14, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.pictureBox15, 4, 2);
            this.tableLayoutPanel3.Controls.Add(this.pictureBox16, 2, 2);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.803468F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 92.19653F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 11F));
            this.tableLayoutPanel4.Controls.Add(this.lblThreadID, 1, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // lblThreadID
            // 
            this.lblThreadID.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblThreadID.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblThreadID.Location = new System.Drawing.Point(17, 0);
            this.lblThreadID.Name = "lblThreadID";
            this.lblThreadID.Size = new System.Drawing.Size(168, 100);
            this.lblThreadID.TabIndex = 5;
            this.lblThreadID.Text = "SCTV EPG is executing...";
            this.lblThreadID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLengthyTask
            // 
            this.lblLengthyTask.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLengthyTask.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLengthyTask.Location = new System.Drawing.Point(29, 55);
            this.lblLengthyTask.Name = "lblLengthyTask";
            this.lblLengthyTask.Size = new System.Drawing.Size(310, 50);
            this.lblLengthyTask.TabIndex = 1;
            this.lblLengthyTask.Text = "Executing to uploading...";
            this.lblLengthyTask.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel5.ColumnCount = 5;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 7F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 7F));
            this.tableLayoutPanel5.Controls.Add(this.pictureBox17, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.pictureBox18, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.pictureBox19, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.pictureBox20, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.pictureBox21, 4, 1);
            this.tableLayoutPanel5.Controls.Add(this.pictureBox22, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.pictureBox23, 4, 2);
            this.tableLayoutPanel5.Controls.Add(this.pictureBox24, 2, 2);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 2, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 3;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 7F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(530, 176);
            this.tableLayoutPanel5.TabIndex = 7;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.ColumnCount = 4;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 322F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel6.Controls.Add(this.pictureBox25, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.picWaiting, 1, 2);
            this.tableLayoutPanel6.Controls.Add(this.lblTimeRemaining, 2, 2);
            this.tableLayoutPanel6.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.pnlStatus, 1, 1);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(12, 38);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 5;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.82609F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.84058F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(475, 128);
            this.tableLayoutPanel6.TabIndex = 8;
            // 
            // lblTimeRemaining
            // 
            this.lblTimeRemaining.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTimeRemaining.BackColor = System.Drawing.Color.Transparent;
            this.lblTimeRemaining.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeRemaining.Location = new System.Drawing.Point(148, 87);
            this.lblTimeRemaining.Name = "lblTimeRemaining";
            this.lblTimeRemaining.Size = new System.Drawing.Size(316, 20);
            this.lblTimeRemaining.TabIndex = 27;
            this.lblTimeRemaining.Text = "  giây còn lại...";
            this.lblTimeRemaining.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTimeRemaining.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.SetColumnSpan(this.label2, 2);
            this.label2.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(189)))), ((int)(((byte)(245)))));
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(123, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(341, 46);
            this.label2.TabIndex = 5;
            this.label2.Text = "Phần mềm EZ Sales đang tải dữ liệu";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // pnlStatus
            // 
            this.pnlStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlStatus.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel6.SetColumnSpan(this.pnlStatus, 2);
            this.pnlStatus.Location = new System.Drawing.Point(123, 60);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(341, 24);
            this.pnlStatus.TabIndex = 25;
            this.pnlStatus.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
            this.pnlStatus.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlStatus_Paint);
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Location = new System.Drawing.Point(112, 116);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(279, 14);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox17
            // 
            this.pictureBox17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox17.BackgroundImage = global::SplashScreen.Properties.Resources.top_mid;
            this.pictureBox17.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox17.Location = new System.Drawing.Point(9, 0);
            this.pictureBox17.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox17.Name = "pictureBox17";
            this.pictureBox17.Size = new System.Drawing.Size(481, 35);
            this.pictureBox17.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox17.TabIndex = 3;
            this.pictureBox17.TabStop = false;
            // 
            // pictureBox18
            // 
            this.pictureBox18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox18.BackgroundImage = global::SplashScreen.Properties.Resources.top_left;
            this.tableLayoutPanel5.SetColumnSpan(this.pictureBox18, 2);
            this.pictureBox18.Location = new System.Drawing.Point(0, 0);
            this.pictureBox18.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox18.Name = "pictureBox18";
            this.pictureBox18.Size = new System.Drawing.Size(9, 35);
            this.pictureBox18.TabIndex = 1;
            this.pictureBox18.TabStop = false;
            // 
            // pictureBox19
            // 
            this.pictureBox19.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox19.BackgroundImage = global::SplashScreen.Properties.Resources.left;
            this.pictureBox19.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox19.Location = new System.Drawing.Point(0, 35);
            this.pictureBox19.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox19.Name = "pictureBox19";
            this.pictureBox19.Size = new System.Drawing.Size(7, 134);
            this.pictureBox19.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox19.TabIndex = 0;
            this.pictureBox19.TabStop = false;
            // 
            // pictureBox20
            // 
            this.pictureBox20.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox20.BackgroundImage = global::SplashScreen.Properties.Resources.top_right;
            this.tableLayoutPanel5.SetColumnSpan(this.pictureBox20, 2);
            this.pictureBox20.Location = new System.Drawing.Point(490, 0);
            this.pictureBox20.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox20.Name = "pictureBox20";
            this.pictureBox20.Size = new System.Drawing.Size(40, 35);
            this.pictureBox20.TabIndex = 2;
            this.pictureBox20.TabStop = false;
            // 
            // pictureBox21
            // 
            this.pictureBox21.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox21.BackgroundImage = global::SplashScreen.Properties.Resources.right;
            this.pictureBox21.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox21.Location = new System.Drawing.Point(523, 35);
            this.pictureBox21.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox21.Name = "pictureBox21";
            this.pictureBox21.Size = new System.Drawing.Size(7, 134);
            this.pictureBox21.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox21.TabIndex = 4;
            this.pictureBox21.TabStop = false;
            // 
            // pictureBox22
            // 
            this.pictureBox22.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox22.BackgroundImage = global::SplashScreen.Properties.Resources.bottom_left;
            this.pictureBox22.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tableLayoutPanel5.SetColumnSpan(this.pictureBox22, 2);
            this.pictureBox22.Location = new System.Drawing.Point(0, 169);
            this.pictureBox22.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox22.Name = "pictureBox22";
            this.pictureBox22.Size = new System.Drawing.Size(9, 7);
            this.pictureBox22.TabIndex = 5;
            this.pictureBox22.TabStop = false;
            // 
            // pictureBox23
            // 
            this.pictureBox23.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox23.BackgroundImage = global::SplashScreen.Properties.Resources.bottom_right;
            this.pictureBox23.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox23.Location = new System.Drawing.Point(523, 169);
            this.pictureBox23.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox23.Name = "pictureBox23";
            this.pictureBox23.Size = new System.Drawing.Size(7, 7);
            this.pictureBox23.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox23.TabIndex = 6;
            this.pictureBox23.TabStop = false;
            // 
            // pictureBox24
            // 
            this.pictureBox24.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox24.BackgroundImage = global::SplashScreen.Properties.Resources.bottom_mid;
            this.pictureBox24.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tableLayoutPanel5.SetColumnSpan(this.pictureBox24, 2);
            this.pictureBox24.Image = global::SplashScreen.Properties.Resources.bottom_mid;
            this.pictureBox24.Location = new System.Drawing.Point(9, 169);
            this.pictureBox24.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox24.Name = "pictureBox24";
            this.pictureBox24.Size = new System.Drawing.Size(514, 7);
            this.pictureBox24.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox24.TabIndex = 7;
            this.pictureBox24.TabStop = false;
            // 
            // pictureBox25
            // 
            this.pictureBox25.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox25.Image = global::SplashScreen.Properties.Resources.insurance_256x256;
            this.pictureBox25.Location = new System.Drawing.Point(3, 3);
            this.pictureBox25.Name = "pictureBox25";
            this.tableLayoutPanel6.SetRowSpan(this.pictureBox25, 5);
            this.pictureBox25.Size = new System.Drawing.Size(114, 122);
            this.pictureBox25.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox25.TabIndex = 26;
            this.pictureBox25.TabStop = false;
            // 
            // picWaiting
            // 
            this.picWaiting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.picWaiting.Enabled = false;
            this.picWaiting.Image = global::SplashScreen.Properties.Resources.spinner;
            this.picWaiting.Location = new System.Drawing.Point(123, 90);
            this.picWaiting.Name = "picWaiting";
            this.picWaiting.Size = new System.Drawing.Size(19, 14);
            this.picWaiting.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picWaiting.TabIndex = 24;
            this.picWaiting.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox4.BackgroundImage = global::SplashScreen.Properties.Resources.top_mid;
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox4.Location = new System.Drawing.Point(9, 0);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(151, 20);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 3;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackgroundImage = global::SplashScreen.Properties.Resources.top_left;
            this.tableLayoutPanel1.SetColumnSpan(this.pictureBox2, 2);
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(9, 20);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImage = global::SplashScreen.Properties.Resources.left;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(0, 20);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(7, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.BackgroundImage = global::SplashScreen.Properties.Resources.top_right;
            this.tableLayoutPanel1.SetColumnSpan(this.pictureBox3, 2);
            this.pictureBox3.Location = new System.Drawing.Point(160, 0);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(40, 20);
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox5.BackgroundImage = global::SplashScreen.Properties.Resources.right;
            this.pictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox5.Location = new System.Drawing.Point(193, 20);
            this.pictureBox5.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(7, 20);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 4;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox6.BackgroundImage = global::SplashScreen.Properties.Resources.bottom_left;
            this.pictureBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tableLayoutPanel1.SetColumnSpan(this.pictureBox6, 2);
            this.pictureBox6.Location = new System.Drawing.Point(0, 40);
            this.pictureBox6.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(9, 7);
            this.pictureBox6.TabIndex = 5;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox7
            // 
            this.pictureBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox7.BackgroundImage = global::SplashScreen.Properties.Resources.bottom_right;
            this.pictureBox7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox7.Location = new System.Drawing.Point(193, 40);
            this.pictureBox7.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(7, 60);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox7.TabIndex = 6;
            this.pictureBox7.TabStop = false;
            // 
            // pictureBox8
            // 
            this.pictureBox8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox8.BackgroundImage = global::SplashScreen.Properties.Resources.bottom_mid;
            this.pictureBox8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tableLayoutPanel1.SetColumnSpan(this.pictureBox8, 2);
            this.pictureBox8.Image = global::SplashScreen.Properties.Resources.bottom_mid;
            this.pictureBox8.Location = new System.Drawing.Point(9, 40);
            this.pictureBox8.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(184, 60);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox8.TabIndex = 7;
            this.pictureBox8.TabStop = false;
            // 
            // pictureBox9
            // 
            this.pictureBox9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox9.BackgroundImage = global::SplashScreen.Properties.Resources.top_mid;
            this.pictureBox9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox9.Location = new System.Drawing.Point(9, 0);
            this.pictureBox9.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(151, 20);
            this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox9.TabIndex = 3;
            this.pictureBox9.TabStop = false;
            // 
            // pictureBox10
            // 
            this.pictureBox10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox10.BackgroundImage = global::SplashScreen.Properties.Resources.top_left;
            this.tableLayoutPanel3.SetColumnSpan(this.pictureBox10, 2);
            this.pictureBox10.Location = new System.Drawing.Point(0, 0);
            this.pictureBox10.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(9, 20);
            this.pictureBox10.TabIndex = 1;
            this.pictureBox10.TabStop = false;
            // 
            // pictureBox11
            // 
            this.pictureBox11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox11.BackgroundImage = global::SplashScreen.Properties.Resources.left;
            this.pictureBox11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox11.Location = new System.Drawing.Point(0, 20);
            this.pictureBox11.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox11.Name = "pictureBox11";
            this.pictureBox11.Size = new System.Drawing.Size(7, 20);
            this.pictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox11.TabIndex = 0;
            this.pictureBox11.TabStop = false;
            // 
            // pictureBox12
            // 
            this.pictureBox12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox12.BackgroundImage = global::SplashScreen.Properties.Resources.top_right;
            this.tableLayoutPanel3.SetColumnSpan(this.pictureBox12, 2);
            this.pictureBox12.Location = new System.Drawing.Point(160, 0);
            this.pictureBox12.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox12.Name = "pictureBox12";
            this.pictureBox12.Size = new System.Drawing.Size(40, 20);
            this.pictureBox12.TabIndex = 2;
            this.pictureBox12.TabStop = false;
            // 
            // pictureBox13
            // 
            this.pictureBox13.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox13.BackgroundImage = global::SplashScreen.Properties.Resources.right;
            this.pictureBox13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox13.Location = new System.Drawing.Point(193, 20);
            this.pictureBox13.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox13.Name = "pictureBox13";
            this.pictureBox13.Size = new System.Drawing.Size(7, 20);
            this.pictureBox13.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox13.TabIndex = 4;
            this.pictureBox13.TabStop = false;
            // 
            // pictureBox14
            // 
            this.pictureBox14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox14.BackgroundImage = global::SplashScreen.Properties.Resources.bottom_left;
            this.pictureBox14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tableLayoutPanel3.SetColumnSpan(this.pictureBox14, 2);
            this.pictureBox14.Location = new System.Drawing.Point(0, 40);
            this.pictureBox14.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox14.Name = "pictureBox14";
            this.pictureBox14.Size = new System.Drawing.Size(9, 7);
            this.pictureBox14.TabIndex = 5;
            this.pictureBox14.TabStop = false;
            // 
            // pictureBox15
            // 
            this.pictureBox15.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox15.BackgroundImage = global::SplashScreen.Properties.Resources.bottom_right;
            this.pictureBox15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox15.Location = new System.Drawing.Point(193, 40);
            this.pictureBox15.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox15.Name = "pictureBox15";
            this.pictureBox15.Size = new System.Drawing.Size(7, 60);
            this.pictureBox15.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox15.TabIndex = 6;
            this.pictureBox15.TabStop = false;
            // 
            // pictureBox16
            // 
            this.pictureBox16.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox16.BackgroundImage = global::SplashScreen.Properties.Resources.bottom_mid;
            this.pictureBox16.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tableLayoutPanel3.SetColumnSpan(this.pictureBox16, 2);
            this.pictureBox16.Image = global::SplashScreen.Properties.Resources.bottom_mid;
            this.pictureBox16.Location = new System.Drawing.Point(9, 40);
            this.pictureBox16.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox16.Name = "pictureBox16";
            this.pictureBox16.Size = new System.Drawing.Size(184, 60);
            this.pictureBox16.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox16.TabIndex = 7;
            this.pictureBox16.TabStop = false;
            // 
            // SplashScreen
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(530, 176);
            this.Controls.Add(this.tableLayoutPanel5);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SplashScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scanning";
            this.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox25)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWaiting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox16)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        // ************* Static Methods *************** //

        // A static method to create the thread and 
        // launch the SplashScreen.
        static public void ShowSplashScreen()
        {
            // Make sure it's only launched once.
            if (ms_frmSplash != null)
                return;
            ms_oThread = new Thread(new ThreadStart(SplashScreen.ShowForm));
            ms_oThread.IsBackground = true;
            ms_oThread.ApartmentState = ApartmentState.STA;
            ms_oThread.Start();
        }

        // A property returning the splash screen instance
        static public SplashScreen SplashForm
        {
            get
            {
                return ms_frmSplash;
            }
        }

        // A private entry point for the thread.
        static private void ShowForm()
        {
            ms_frmSplash = new SplashScreen();
            Application.Run(ms_frmSplash);
        }

        // A static method to close the SplashScreen
        static public void CloseForm()
        {
            if (ms_frmSplash != null && ms_frmSplash.IsDisposed == false)
            {
                // Make it start going away.
                ms_frmSplash.m_dblOpacityIncrement = -ms_frmSplash.m_dblOpacityDecrement;
            }
            ms_oThread = null;	// we don't need these any more.
            ms_frmSplash = null;
        }

        // A static method to set the status and update the reference.
        static public void SetStatus(string newStatus)
        {
            SetStatus(newStatus, true);
        }

        // A static method to set the status and optionally update the reference.
        // This is useful if you are in a section of code that has a variable
        // set of status string updates.  In that case, don't set the reference.
        static public void SetStatus(string newStatus, bool setReference)
        {
            ms_sStatus = newStatus;
            if (ms_frmSplash == null)
                return;
            if (setReference)
                ms_frmSplash.SetReferenceInternal();
        }

        // Static method called from the initializing application to 
        // give the splash screen reference points.  Not needed if
        // you are using a lot of status strings.
        static public void SetReferencePoint()
        {
            if (ms_frmSplash == null)
                return;
            ms_frmSplash.SetReferenceInternal();

        }

        // ************ Private methods ************

        // Internal method for setting reference points.
        private void SetReferenceInternal()
        {
            if (m_bDTSet == false)
            {
                m_bDTSet = true;
                m_dtStart = DateTime.Now;
                ReadIncrements();
            }
            double dblMilliseconds = ElapsedMilliSeconds();
            m_alActualTimes.Add(dblMilliseconds);
            m_dblLastCompletionFraction = m_dblCompletionFraction;
            if (m_alPreviousCompletionFraction != null && m_iIndex < m_alPreviousCompletionFraction.Count)
                m_dblCompletionFraction = (double)m_alPreviousCompletionFraction[m_iIndex++];
            else
                m_dblCompletionFraction = (m_iIndex > 0) ? 1 : 0;
        }

        // Utility function to return elapsed Milliseconds since the 
        // SplashScreen was launched.
        private double ElapsedMilliSeconds()
        {
            TimeSpan ts = DateTime.Now - m_dtStart;
            return ts.TotalMilliseconds;
        }

        // Function to read the checkpoint intervals from the previous invocation of the
        // splashscreen from the registry.
        private void ReadIncrements()
        {
            string sPBIncrementPerTimerInterval = RegistryAccess.GetStringRegistryValue(REGVALUE_PB_MILISECOND_INCREMENT, "0.0015");
            double dblResult;

            if (Double.TryParse(sPBIncrementPerTimerInterval, System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, out dblResult) == true)
                m_dblPBIncrementPerTimerInterval = dblResult;
            else
                m_dblPBIncrementPerTimerInterval = .0015;

            string sPBPreviousPctComplete = RegistryAccess.GetStringRegistryValue(REGVALUE_PB_PERCENTS, "");

            if (sPBPreviousPctComplete != "")
            {
                string[] aTimes = sPBPreviousPctComplete.Split(null);
                m_alPreviousCompletionFraction = new ArrayList();

                for (int i = 0; i < aTimes.Length; i++)
                {
                    double dblVal;
                    if (Double.TryParse(aTimes[i], System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, out dblVal))
                        m_alPreviousCompletionFraction.Add(dblVal);
                    else
                        m_alPreviousCompletionFraction.Add(1.0);
                }
            }
            else
            {
                m_bFirstLaunch = true;
                lblTimeRemaining.Text = "";
            }
        }

        // Method to store the intervals (in percent complete) from the current invocation of
        // the splash screen to the registry.
        private void StoreIncrements()
        {
            string sPercent = "";
            double dblElapsedMilliseconds = ElapsedMilliSeconds();
            for (int i = 0; i < m_alActualTimes.Count; i++)
                sPercent += ((double)m_alActualTimes[i] / dblElapsedMilliseconds).ToString("0.####", System.Globalization.NumberFormatInfo.InvariantInfo) + " ";

            RegistryAccess.SetStringRegistryValue(REGVALUE_PB_PERCENTS, sPercent);

            m_dblPBIncrementPerTimerInterval = 1.0 / (double)m_iActualTicks;
            RegistryAccess.SetStringRegistryValue(REGVALUE_PB_MILISECOND_INCREMENT, m_dblPBIncrementPerTimerInterval.ToString("#.000000", System.Globalization.NumberFormatInfo.InvariantInfo));
        }

        //********* Event Handlers ************

        // Tick Event handler for the Timer control.  Handle fade in and fade out.  Also
        // handle the smoothed progress bar.
        private void timer1_Tick(object sender, System.EventArgs e)
        {
            lblStatus.Text = ms_sStatus;

            if (m_dblOpacityIncrement > 0)
            {
                m_iActualTicks++;
                if (this.Opacity < 1)
                    this.Opacity += m_dblOpacityIncrement;
            }
            else
            {
                if (this.Opacity > 0)
                    this.Opacity += m_dblOpacityIncrement;
                else
                {
                    StoreIncrements();
                    this.Close();
                    Debug.WriteLine("Called this.Close()");
                }
            }
            if (m_bFirstLaunch == false && m_dblLastCompletionFraction < m_dblCompletionFraction)
            {
                m_dblLastCompletionFraction += m_dblPBIncrementPerTimerInterval;
                int width = (int)Math.Floor(pnlStatus.ClientRectangle.Width * m_dblLastCompletionFraction);
                int height = pnlStatus.ClientRectangle.Height;
                int x = pnlStatus.ClientRectangle.X;
                int y = pnlStatus.ClientRectangle.Y;
                if (width > 0 && height > 0)
                {
                    m_rProgress = new Rectangle(x, y, width, height);
                    pnlStatus.Invalidate(m_rProgress);
                    int iSecondsLeft = 1 + (int)(TIMER_INTERVAL * ((1.0 - m_dblLastCompletionFraction) / m_dblPBIncrementPerTimerInterval)) / 1000;
                    if (iSecondsLeft == 1)
                        lblTimeRemaining.Text = string.Format("1 giây còn lại...");
                    else
                        lblTimeRemaining.Text = string.Format("{0} giây còn lại...", iSecondsLeft);

                }
            }
        }

        // Paint the portion of the panel invalidated during the tick event.
        private void pnlStatus_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (m_bFirstLaunch == false && e.ClipRectangle.Width > 0 && m_iActualTicks > 1)
            {
                LinearGradientBrush brBackground = new LinearGradientBrush(m_rProgress, Color.FromArgb(58, 96, 151), Color.FromArgb(181, 237, 254), LinearGradientMode.Horizontal);
                e.Graphics.FillRectangle(brBackground, m_rProgress);
            }
        }

        private void SplashScreen_DoubleClick(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }

    /// <summary>
    /// A class for managing registry access.
    /// </summary>
    public class RegistryAccess
    {
        private const string SOFTWARE_KEY = "Software";
        private const string COMPANY_NAME = "MyCompany";
        private const string APPLICATION_NAME = "MyApplication";

        // Method for retrieving a Registry Value.
        static public string GetStringRegistryValue(string key, string defaultValue)
        {
            RegistryKey rkCompany;
            RegistryKey rkApplication;

            rkCompany = Registry.CurrentUser.OpenSubKey(SOFTWARE_KEY, false).OpenSubKey(COMPANY_NAME, false);
            if (rkCompany != null)
            {
                rkApplication = rkCompany.OpenSubKey(APPLICATION_NAME, true);
                if (rkApplication != null)
                {
                    foreach (string sKey in rkApplication.GetValueNames())
                    {
                        if (sKey == key)
                        {
                            return (string)rkApplication.GetValue(sKey);
                        }
                    }
                }
            }
            return defaultValue;
        }

        // Method for storing a Registry Value.
        static public void SetStringRegistryValue(string key, string stringValue)
        {
            RegistryKey rkSoftware;
            RegistryKey rkCompany;
            RegistryKey rkApplication;

            rkSoftware = Registry.CurrentUser.OpenSubKey(SOFTWARE_KEY, true);
            rkCompany = rkSoftware.CreateSubKey(COMPANY_NAME);
            if (rkCompany != null)
            {
                rkApplication = rkCompany.CreateSubKey(APPLICATION_NAME);
                if (rkApplication != null)
                {
                    rkApplication.SetValue(key, stringValue);
                }
            }
        }
    }
}

