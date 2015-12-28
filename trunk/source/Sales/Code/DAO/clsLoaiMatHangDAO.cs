using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsLoaiMatHangDAO
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
            string sql = "sp_GetBangLoaiMatHang";
            DataTable table = sqlServer.readData(sql, "LOAI_MAT_HANG",true);
            return table;
        }

        /// <summary>
        /// Lấy Mã loại mặt hàng kế tiếp để phục vụ cho chức năng Insert
        /// </summary>
        public string LayMaLoaiMatHangMoi()
        {
            string sql = "sp_GetNewMaLoaiMatHang";
            return sqlServer.readData(sql, "@MaLoaiMatHangMoi");
        }
        /// <summary>
        /// Thêm thông tin loại mặt hàng
        /// </summary>
        /// <param name="LoaiMatHang">
        /// MaLoaiMatHang   nvarchar(10)
        /// TenLoaiMatHang  nvarchar(255)
        /// DienGiai   ntext
        /// NgayTao  smalldatetime
        /// TrangThai int
        /// </param>
        public int Them(clsLoaiMatHangDTO LoaiMatHang)
        {
            int i = -1;
            string sql = "sp_InsertLoaiMatHang";
            string[] ParameterColection = new string[3];
            Object[] valueofParameter = new Object[3];
            ParameterColection[0] = "@MaLoaiMatHang";
            valueofParameter[0] = LoaiMatHang.MaLoaiMatHang;
            ParameterColection[1] = "@TenLoaiMatHang";
            valueofParameter[1] = LoaiMatHang.TenLoaiMatHang;
            ParameterColection[2] = "@DienGiai";
            valueofParameter[2] = LoaiMatHang.DienGiai;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Sửa thông tin loại mặt hàng
        /// </summary>
        /// <param name="LoaiMatHang">
        /// MaLoaiMatHang  nvarchar(10)
        /// TenLoaiMatHang  nvarchar(255)
        /// DienGiai  ntext
        /// </param>
        public int Sua(clsLoaiMatHangDTO LoaiMatHang)
        {
            int i = -1;
            string sql = "sp_UpdateLoaiMatHang";
            string[] ParameterColection = new string[3];
            Object[] valueofParameter = new Object[3];
            ParameterColection[0] = "@MaLoaiMatHang";
            valueofParameter[0] = LoaiMatHang.MaLoaiMatHang;
            ParameterColection[1] = "@TenLoaiMatHang";
            valueofParameter[1] = LoaiMatHang.TenLoaiMatHang;
            ParameterColection[2] = "@DienGiai";
            valueofParameter[2] = LoaiMatHang.DienGiai;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Xóa loại mặt hàng
        /// </summary>
        /// <param name="MaLoaiMatHang">Mã mặt hàng</param>
        public int Xoa(string MaLoaiMatHang)
        {
            int i = -1;
            string sql = "sp_DeleteLoaiMatHang";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaLoaiMatHang";
            valueofParameter[0] = MaLoaiMatHang;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        public DataTable ReportDSLoaiMatHang(string MaLoaiNhomHang, string TenLoaiNhomHang)
        {
            string sql = "sp_ReportDSLoaiMatHang";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@MaLoaiMatHang";
            valueofParameter[0] = MaLoaiNhomHang;
            ParameterColection[1] = "@TenLoaiMatHang";
            valueofParameter[1] = TenLoaiNhomHang;

            DataTable table = sqlServer.readData(sql, "sp_ReportDSLoaiMatHang;1", ParameterColection, valueofParameter);

            return table;
        }
    }
}
