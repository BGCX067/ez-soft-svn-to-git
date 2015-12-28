using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    class clsDonViTinhDAO
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
            string sql = "sp_GetBangDonViTinh";
            DataTable table = sqlServer.readData(sql);
            return table;
        }

        /// <summary>
        /// Lấy Mã loại mặt hàng kế tiếp để phục vụ cho chức năng Insert
        /// </summary>
        public string LayMaDonViTinh()
        {
            string sql = "sp_GetNewMaDonViTinh";
            return sqlServer.readData(sql, "@MaDonViTinh");
        }
        /// <summary>
        /// Thêm thông tin loại mặt hàng
        /// </summary>
        /// <param name="DonViTinh">
        /// MaDonViTinh   nvarchar(10)
        /// TenDonViTinh  nvarchar(255)
        /// DienGiai   ntext
        /// NgayTao  smalldatetime
        /// TrangThai int
        /// </param>
        public int Them(clsDonViTinhDTO DonViTinh)
        {
            int i = -1;
            string sql = "sp_InsertDonViTinh";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@MaDonViTinh";
            valueofParameter[0] = DonViTinh.MaDonViTinh;
            ParameterColection[1] = "@TenDonViTinh";
            valueofParameter[1] = DonViTinh.TenDonViTinh;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Sửa thông tin loại mặt hàng
        /// </summary>
        /// <param name="DonViTinh">
        /// MaDonViTinh  nvarchar(10)
        /// TenDonViTinh  nvarchar(255)
        /// DienGiai  ntext
        /// </param>
        public int Sua(clsDonViTinhDTO DonViTinh)
        {
            int i = -1;
            string sql = "sp_UpdateDonViTinh";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@MaDonViTinh";
            valueofParameter[0] = DonViTinh.MaDonViTinh;
            ParameterColection[1] = "@TenDonViTinh";
            valueofParameter[1] = DonViTinh.TenDonViTinh;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Xóa loại mặt hàng
        /// </summary>
        /// <param name="MaDonViTinh">Mã mặt hàng</param>
        public int Xoa(string MaDonViTinh)
        {
            int i = -1;
            string sql = "sp_DeleteDonViTinh";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaDonViTinh";
            valueofParameter[0] = MaDonViTinh;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }
    }
}
