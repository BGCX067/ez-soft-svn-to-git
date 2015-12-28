using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace Sales
{
    public static class clsConnection
    {
        #region Attribute
        public static string sProductName = "";
        static public string ServerName = "";
        static public string Databasename = "";
        static public string LoginName = "";
        static public string LoginPassword = "";
        static public string ConnectionStr = "";
        #endregion

        public static string GetConnectionString()
        {
            ConnectionStr = "Data Source=" + ServerName + ";Initial Catalog=" + Databasename + ";User ID=" + LoginName + "; Password=" + LoginPassword + ";Connect Timeout=100000";
            return ConnectionStr;
        }// End of setConnectionString pro

        public static void CreateConnect()
        {
            ReadKeys();
            ConnectionStr = "Data Source=" + ServerName + ";Initial Catalog=" + Databasename + ";User ID=" + LoginName + "; Password=" + LoginPassword + ";Connect Timeout=100000";
        }// End of setConnectionString pro

        public static Boolean IsCheckConnect(string _ServerName, string _Databasename, string _LoginName, string _LoginPassword)
        {
            try
            {
                string ConnectionString = "Data Source=" + _ServerName + ";Initial Catalog=" + _Databasename + ";User ID=" + _LoginName + "; Password=" + _LoginPassword + ";Connect Timeout=100";
                CSQLServer sqlServer = new CSQLServer(ConnectionString);
                string sql = "sp_GetInfoCongTy";
                DataTable table = sqlServer.readData(sql);
                if (table != null && table.Rows.Count>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //──────────────────────────────────────────────────────────────────────────────────────────


        public static void ReadKeys()
        {
            ServerName = clsSupport.ReadRegistry("cms_datasource" + sProductName);
            Databasename = clsSupport.ReadRegistry("cms_database" + sProductName);
            LoginName = clsSupport.ReadRegistry("cms_username" + sProductName);
            LoginPassword = clsSupport.ReadRegistry("cms_LoginPassword" + sProductName);
        }// End of setConnectionString pro

        //──────────────────────────────────────────────────────────────────────────────────────────
        public static void WriteKeys()
        {
            clsSupport.WriteRegistry("cms_datasource" + sProductName, ServerName);
            clsSupport.WriteRegistry("cms_database" + sProductName, Databasename);
            clsSupport.WriteRegistry("cms_username" + sProductName, LoginName);
            clsSupport.WriteRegistry("cms_LoginPassword" + sProductName, LoginPassword);
        }
    }
}
