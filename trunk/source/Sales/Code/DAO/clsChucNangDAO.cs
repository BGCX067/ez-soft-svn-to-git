using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    class clsChucNangDAO
    {
        #region Attribute
        /// <summary>
        /// Kết nối SQL Server
        /// </summary>
        private CSQLServer sqlServer = new CSQLServer();
        #endregion

        /// <summary>
        /// Lấy danh sách chức năng
        /// </summary>
        public DataTable LayBang()
        {
            string sql = "sp_GetBangChucNang";
            DataTable table = sqlServer.readData(sql);
            return table;
        }

       /// Thêm chức năng
       /// </summary>
       /// <param name="ChucNang"></param>
       /// <returns></returns>
        public int Them(clsChucNangDTO ChucNang)
        {
            int i = -1;
            string sql = "sp_InsertChucNang";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@TenChucNang";
            valueofParameter[0] = ChucNang.TenChucNang;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Sửa thông tin chức năng
        /// </summary>
        /// <param name="ChucNang">
        /// MaChucNang  nvarchar(10)
        /// TenChucNang  nvarchar(255)
        /// </param>
        public int Sua(clsChucNangDTO ChucNang)
        {
            int i = -1;
            string sql = "sp_UpdateChucNang";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@MaChucNang";
            valueofParameter[0] = ChucNang.MaChucNang;
            ParameterColection[1] = "@TenChucNang";
            valueofParameter[1] = ChucNang.TenChucNang;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Xóa chức năng
        /// </summary>
        /// <param name="MaChucNang">Mã chức năng</param>
        public int Xoa(string MaChucNang)
        {
            int i = -1;
            string sql = "sp_DeleteChucNang";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaChucNang";
            valueofParameter[0] = MaChucNang;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }
    }
}
