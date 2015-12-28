using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    class clsNuocSanXuatDAO
    {
        #region Attribute
        /// <summary>
        /// Kết nối SQL Server
        /// </summary>
        private CSQLServer sqlServer = new CSQLServer();
        #endregion

        /// <summary>
        /// Lấy danh sách Loại mặt hàng
        /// </summary>
        public DataTable LayBang()
        {
            string sql = "sp_GetBangNuocSanXuat";
            DataTable table = sqlServer.readData(sql);
            return table;
        }

        /// <summary>
        /// Lấy Mã loại mặt hàng kế tiếp để phục vụ cho chức năng Insert
        /// </summary>
        public string LayMaNuocSanXuat()
        {
            string sql = "sp_GetNewMaNuocSanXuat";
            return sqlServer.readData(sql, "@MaNuocSanXuat");
        }
        /// <summary>
        /// Thêm thông tin loại mặt hàng
        /// </summary>
        /// <param name="NuocSanXuat">
        /// MaNuocSanXuat   nvarchar(10)
        /// TenNuocSanXuat  nvarchar(255)
        /// DienGiai   ntext
        /// NgayTao  smalldatetime
        /// TrangThai int
        /// </param>
        public int Them(clsNuocSanXuatDTO NuocSanXuat)
        {
            int i = -1;
            string sql = "sp_InsertNuocSanXuat";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@MaNuocSanXuat";
            valueofParameter[0] = NuocSanXuat.MaNuocSanXuat;
            ParameterColection[1] = "@TenNuocSanXuat";
            valueofParameter[1] = NuocSanXuat.TenNuocSanXuat;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Sửa thông tin loại mặt hàng
        /// </summary>
        /// <param name="NuocSanXuat">
        /// MaNuocSanXuat  nvarchar(10)
        /// TenNuocSanXuat  nvarchar(255)
        /// DienGiai  ntext
        /// </param>
        public int Sua(clsNuocSanXuatDTO NuocSanXuat)
        {
            int i = -1;
            string sql = "sp_UpdateNuocSanXuat";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@MaNuocSanXuat";
            valueofParameter[0] = NuocSanXuat.MaNuocSanXuat;
            ParameterColection[1] = "@TenNuocSanXuat";
            valueofParameter[1] = NuocSanXuat.TenNuocSanXuat;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Xóa loại mặt hàng
        /// </summary>
        /// <param name="MaNuocSanXuat">Mã mặt hàng</param>
        public int Xoa(string MaNuocSanXuat)
        {
            int i = -1;
            string sql = "sp_DeleteNuocSanXuat";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaNuocSanXuat";
            valueofParameter[0] = MaNuocSanXuat;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }
    }
}
