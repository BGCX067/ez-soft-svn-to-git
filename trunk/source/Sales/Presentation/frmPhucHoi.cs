using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;


namespace Sales
{
    public partial class frmPhucHoi : Form
    {
       

        public frmPhucHoi()
        {
            InitializeComponent();
        }

        #region Phím tắt
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //Thoát 
            if (keyData == Keys.Escape)
            {
                DongCuaSo();
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void DongCuaSo()
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DongCuaSo();
        }

        private void btnDuongDan_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    txtDuongDan.Text = openFileDialog1.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //private void btnPhucHoi_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // Create a DataTable where we enumerate the available servers
        //        //DataTable dtServers = SmoApplication.EnumAvailableSqlServers(true);
        //         Server srvSql;
        //        // If there are any servers at all
        //        //if (dtServers.Rows.Count > 0)
        //        //{
        //            //string TenMayChu = dtServers.Rows[0]["Name"].ToString();
        //            // Create a new connection to the selected server name
        //            ServerConnection srvConn = new ServerConnection(clsConnection.ServerName);
        //            // Log in using SQL authentication instead of Windows authentication
        //            srvConn.LoginSecure = false;
        //            // Give the login username
        //            srvConn.Login = clsConnection.LoginName;
        //            // Give the login password
        //            srvConn.Password = clsConnection.LoginPassword;
        //            // Create a new SQL Server object using the connection we created
        //            srvSql = new Server(srvConn);
        //            //Boolean Co = false;
        //            //// Loop through the databases list
        //            //foreach (Database dbServer in srvSql.Databases)
        //            //{
        //            //    if (dbServer.Name == "BD_Sales")
        //            //    {
        //            //        Co = true;
        //            //        break;
        //            //    }
        //            //}
        //            //if (Co == true)
        //            //{
        //                // If there was a SQL connection created
        //                if (srvSql != null)
        //                {
        //                    // If the user has chosen a path where to save the backup file
        //                    if (txtDuongDan.Text != "")
        //                    {
        //                        // Create a new database restore operation
        //                        Restore rstDatabase = new Restore();
        //                        // Set the restore type to a database restore
        //                        rstDatabase.Action = RestoreActionType.Database;
        //                        // Set the database that we want to perform the restore on
        //                        rstDatabase.Database = "BD_Sales";

        //                        // Set the backup device from which we want to restore, to a file
        //                        BackupDeviceItem bkpDevice = new BackupDeviceItem(txtDuongDan.Text, DeviceType.File);
        //                        // Add the backup device to the restore type
        //                        rstDatabase.Devices.Add(bkpDevice);
        //                        // If the database already exists, replace it
        //                        rstDatabase.ReplaceDatabase = true;
        //                        // Perform the restore
        //                        rstDatabase.SqlRestore(srvSql);
        //                        MessageBox.Show("Phục hồi dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show("Xin vui lòng chọn tập tin đã sao lưu muốn khôi phục dữ liệu!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                    }
        //                }
        //                else
        //                {
        //                    MessageBox.Show("Kết nối Cơ sở dữ liệu không thành công!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                }
        //            //}
        //            //else
        //            //{
        //            //    MessageBox.Show("Kết nối Cơ sở dữ liệu không thành công!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //            //}
        //        }
        //   // }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi kết nối", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void btnPhucHoi_Click(object sender, EventArgs e)
        {
            if (txtDuongDan.Text != "")
            {
                if (RestoreDatabase(clsConnection.Databasename, txtDuongDan.Text, clsConnection.ServerName, clsConnection.LoginName, clsConnection.LoginPassword, Application.StartupPath + @"\\Database\\", Application.StartupPath + @"\\Database\\") == true)
                {
                    MessageBox.Show("Phục hồi dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Phục hồi dữ liệu thất bại. Nguyên nhân do lỗi kết nối cơ sở dữ liệu!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Xin vui lòng chọn tập tin đã sao lưu muốn khôi phục dữ liệu!", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        public Boolean RestoreDatabase(String databaseName, String filePath, String serverName, String userName, String password, String dataFilePath, String logFilePath)
        {
           Boolean Co = true;
            Restore sqlRestore = new Restore();
           //sqlRestore.NoRecovery = true;
           sqlRestore.SetPassword("BMBez2010");
           //sqlRestore.SetMediaPassword("123456");
            BackupDeviceItem deviceItem = new BackupDeviceItem(filePath, DeviceType.File);
            sqlRestore.Devices.Add(deviceItem);
            sqlRestore.NoRecovery = false;
            sqlRestore.ReplaceDatabase = true;
            sqlRestore.Database = databaseName;

            ServerConnection connection;
            // for Windows Authentication
            if (userName == "")
            {
                SqlConnection sqlCon = new SqlConnection(@"Data Source=" + serverName + @"; Integrated Security=True;");
                connection = new ServerConnection(sqlCon);
            }
            // for Server Authentication
            else
                connection = new ServerConnection(serverName, userName, password);

            Server sqlServer = new Server(connection);

            Database db = sqlServer.Databases[databaseName];

            sqlServer.KillAllProcesses(databaseName);

            sqlRestore.Action = RestoreActionType.Database;
            String dataFileLocation = dataFilePath + databaseName + ".mdf";
            String logFileLocation = logFilePath + databaseName + "_Log.ldf";
            db = sqlServer.Databases[databaseName];
            RelocateFile rf = new RelocateFile(databaseName, dataFileLocation);

            sqlRestore.RelocateFiles.Add(new RelocateFile(databaseName, dataFileLocation));
            sqlRestore.RelocateFiles.Add(new RelocateFile(databaseName + "_log", logFileLocation));
            sqlRestore.ReplaceDatabase = true;
            sqlRestore.Complete += new ServerMessageEventHandler(sqlRestore_Complete);
            sqlRestore.PercentCompleteNotification = 10;
            sqlRestore.PercentComplete += new PercentCompleteEventHandler(sqlRestore_PercentComplete);

            try
            {
                sqlRestore.SqlRestore(sqlServer);
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.InnerException.ToString());
                Co = false;
            }
            //db = sqlServer.Databases[databaseName];
            //db.SetOnline();
            sqlServer.Databases[databaseName].SetOnline();
            sqlServer.Refresh();
            return Co;
        }

        public event EventHandler<PercentCompleteEventArgs> PercentComplete;

        void sqlRestore_PercentComplete(object sender, PercentCompleteEventArgs e)
        {
            if (PercentComplete != null)
                PercentComplete(sender, e);
        }

        public event EventHandler<ServerMessageEventArgs> Complete;

        void sqlRestore_Complete(object sender, ServerMessageEventArgs e)
        {
            if (Complete != null)
                Complete(sender, e);
        }
    }
}