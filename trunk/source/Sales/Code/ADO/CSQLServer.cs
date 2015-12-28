using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace Sales
{

    /// <summary>
    /// Summary description for CSQLServer
    /// </summary>
    public class CSQLServer : ADO_Net
    {
        #region Attribute
        static public string ConnectionStr =""; //ConfigurationSettings.AppSettings["SalesConnectiongstring"];
        public SqlConnection Connection;
        #endregion

        #region Method
        // mo ket noi
        public void OpenConnection()
        {
            if (Connection == null || Connection.State == ConnectionState.Closed)
            {
                Connection = new SqlConnection();
                Connection.ConnectionString = ConnectionStr;
                Connection.Open();
            }
        }
        //Dong ket noi
        public void CloseConnection()
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Close();
            }
            Connection.Dispose();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="ParameterConlection"></param>
        /// <param name="valueofParameter"></param>
        /// <returns></returns>
        public SqlCommand AddParameters(SqlCommand command, string[] ParameterConlection, Object[] valueofParameter)
        {
            for (int i = 0; i < ParameterConlection.Length; i++)
            {
                if (ParameterConlection[i] != null)
                {
                    command.Parameters.AddWithValue(ParameterConlection[i], valueofParameter[i]);
                }
            }
            return command;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public override DataTable readData(string sql)
        {
            DataTable datatable = CreateTable();
            try
            {
                OpenConnection();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.Connection = Connection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(datatable);
                sqlCommand.Dispose();
                sqlDataAdapter.Dispose();
                CloseConnection();
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                CloseConnection();
            }
            return datatable;
        }

        public override DataTable readData(string sql, string NameTable, Boolean Bool)
        {
            DataTable datatable = CreateTable(NameTable);
            try
            {
                OpenConnection();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.Connection = Connection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(datatable);
                sqlCommand.Dispose();
                sqlDataAdapter.Dispose();
                CloseConnection();
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                CloseConnection();
            }
            return datatable;
        }

        public override DataTable readData(string Sql, string[] ParameterConlection, Object[] valueofParameter)
        {
            DataTable datatable = CreateTable();
            try
            {
                OpenConnection();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = Sql;
                sqlCommand.Connection = Connection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = AddParameters(sqlCommand, ParameterConlection, valueofParameter);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(datatable);
                sqlCommand.Dispose();
                sqlDataAdapter.Dispose();
                CloseConnection();
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                CloseConnection();
            }
            return datatable;
        }

        public override DataTable readData(string Sql, string NameTable, string[] ParameterConlection, Object[] valueofParameter)
        {
            DataTable datatable = CreateTable(NameTable);
            try
            {
                OpenConnection();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = Sql;
                sqlCommand.Connection = Connection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = AddParameters(sqlCommand, ParameterConlection, valueofParameter);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(datatable);
                sqlCommand.Dispose();
                sqlDataAdapter.Dispose();
                CloseConnection();
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                CloseConnection();
            }
            return datatable;
        }

        private DataTable CreateTable()
        {
            DataTable datatable = new DataTable();
            DataColumn AutoNumberColumn = new DataColumn();
            AutoNumberColumn.ColumnName = "STT";
            AutoNumberColumn.DataType = System.Type.GetType("System.Int32");
            AutoNumberColumn.ReadOnly = true;
            AutoNumberColumn.AutoIncrement = true;
            AutoNumberColumn.AutoIncrementSeed = 1;
            AutoNumberColumn.AutoIncrementStep = 1;
            datatable.Columns.Add(AutoNumberColumn);
            return datatable;
        }
        private DataTable CreateTable(string TableName)
        {
            DataTable datatable = new DataTable(TableName);
            DataColumn AutoNumberColumn = new DataColumn();
            AutoNumberColumn.ColumnName = "STT";
            AutoNumberColumn.DataType = System.Type.GetType("System.Int32");
            AutoNumberColumn.ReadOnly = true;
            AutoNumberColumn.AutoIncrement = true;
            AutoNumberColumn.AutoIncrementSeed = 1;
            AutoNumberColumn.AutoIncrementStep = 1;
            datatable.Columns.Add(AutoNumberColumn);
            return datatable;
        }
        public override List<string> readData(string Sql, string[] ParameterConlection, Object[] valueofParameter, string[] ParameterOutputs)
        {
            List<string> results = new List<string>();
            try
            {
                OpenConnection();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = Sql;
                sqlCommand.Connection = Connection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = AddParameters(sqlCommand, ParameterConlection, valueofParameter);
                for (int i = 0; i < ParameterOutputs.Length; i++)
                {
                    sqlCommand.Parameters.Add(ParameterOutputs[i], SqlDbType.NVarChar, 10).Direction = ParameterDirection.Output;
                }
                sqlCommand.ExecuteNonQuery();
                for (int i = 0; i < ParameterOutputs.Length; i++)
                {
                    results.Add((string)sqlCommand.Parameters[ParameterOutputs[i]].Value);
                }
                sqlCommand.Dispose();
                CloseConnection();
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                CloseConnection();
            }
            return results;
        }

        public override string readData(string Sql, string[] ParameterConlection, Object[] valueofParameter, string ParameterOutput)
        {
            string result = null;
            try
            {
                OpenConnection();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = Sql;
                sqlCommand.Connection = Connection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = AddParameters(sqlCommand, ParameterConlection, valueofParameter);
                sqlCommand.Parameters.Add(ParameterOutput, SqlDbType.NVarChar, 10).Direction = ParameterDirection.Output;
                sqlCommand.ExecuteNonQuery();
                result = (string)sqlCommand.Parameters[ParameterOutput].Value;
                sqlCommand.Dispose();
                CloseConnection();
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                CloseConnection();
            }
            return result;
        }

        public override List<string> readData(string sql, string[] ParameterOutputs)
        {
            List<string> results = new List<string>();
            try
            {
                OpenConnection();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.Connection = Connection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < ParameterOutputs.Length; i++)
                {
                    sqlCommand.Parameters.Add(ParameterOutputs[i], SqlDbType.NVarChar, 10).Direction = ParameterDirection.Output;
                }
                sqlCommand.ExecuteNonQuery();
                for (int i = 0; i < ParameterOutputs.Length; i++)
                {
                    results.Add((string)sqlCommand.Parameters[ParameterOutputs[i]].Value);
                }
                sqlCommand.Dispose();
                CloseConnection();
            }
            catch (Exception ex)
            {
                CloseConnection();
            }

            return results;
        }

        public override string readData(string sql, string ParameterOutput)
        {
            string result = null;
            try
            {
                OpenConnection();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.Connection = Connection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(ParameterOutput, SqlDbType.NVarChar, 20).Direction = ParameterDirection.Output;
                sqlCommand.ExecuteNonQuery();
                result = (string)sqlCommand.Parameters[ParameterOutput].Value;
                sqlCommand.Dispose();
                CloseConnection();
            }
            catch (Exception ex)
            {
                CloseConnection();
            }

            return result;
        }

        public override int writeData(string sql, string[] ParameterConlection, Object[] valueofParameter)
        {
            int rows = -1;
            try
            {
                OpenConnection();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.Connection = Connection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand = AddParameters(sqlCommand, ParameterConlection, valueofParameter);
                SqlParameter p = sqlCommand.Parameters.Add("@kq", SqlDbType.Int);
                p.Direction = ParameterDirection.ReturnValue;
                sqlCommand.ExecuteNonQuery();
                rows = (int)sqlCommand.Parameters["@kq"].Value;
                sqlCommand.Dispose();
                CloseConnection();
            }
            catch (Exception ex)
            {
                rows = -1;
                CloseConnection();
            }

            return rows;
        }

        public override int writeData(string sql)
        {
            int rows = -1;
            try
            {
                OpenConnection();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = sql;
                sqlCommand.Connection = Connection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                rows = sqlCommand.ExecuteNonQuery();
                sqlCommand.Dispose();
                CloseConnection();
            }
            catch (Exception ex)
            {
                rows = -1;
                CloseConnection();
            }
            return rows;
        }

        #endregion

        #region Contructor
        public CSQLServer()
        {
            ConnectionStr = clsConnection.GetConnectionString();
        }
        public CSQLServer(string ConnectionString)
        {
            ConnectionStr = ConnectionString;
        }
        #endregion
    }
}