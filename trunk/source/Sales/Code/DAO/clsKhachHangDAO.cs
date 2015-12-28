using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsKhachHangDAO
    {

        /// <summary>
        /// Kết nối SQL Server
        /// </summary>
        public CSQLServer sqlServer = new CSQLServer();

        /// <summary>
        /// Lấy DS khách hàng
        /// </summary>
        public DataTable LayBang()
        {
            string sql = "sp_GetBangKhachHang";
            DataTable table = sqlServer.readData(sql);
            return table;
        }

        /// <summary>
        /// Lấy Mã Khách hàng kế tiếp để phục vụ cho chức năng Insert
        /// </summary>
        public string LayMaKhachHangMoi()
        {
            string sql = "sp_GetNewMaKhachHang";
            return sqlServer.readData(sql, "@MaKhachHang");
        }

        /// <summary>
        /// Cập nhật khách hàng
        /// </summary>
        public int Sua(clsKhachHangDTO KhachHang)
        {
            int i = -1;
            string sql = "sp_UpdateKhachHang";
            string[] ParameterColection = new string[10];
            Object[] valueofParameter = new Object[10];
            ParameterColection[0] = "@MaKhachHang";
            valueofParameter[0] = KhachHang.MaKhachHang;
            ParameterColection[1] = "@TenKhachHang";
            valueofParameter[1] = KhachHang.TenKhachHang;
            ParameterColection[2] = "@DienThoai";
            valueofParameter[2] = KhachHang.DienThoai;
            ParameterColection[3] = "@DiaChi";
            valueofParameter[3] = KhachHang.DiaChi;
            ParameterColection[4] = "@Fax";
            valueofParameter[4] = KhachHang.Fax;
            ParameterColection[5] = "@MaSoThue";
            valueofParameter[5] = KhachHang.MaSoThue;
            ParameterColection[6] = "@BaoGia";
            valueofParameter[6] = KhachHang.BaoGia;
            ParameterColection[7] = "@ChietKhau";
            valueofParameter[7] = KhachHang.ChietKhau;
            ParameterColection[8] = "@NoDauKy";
            valueofParameter[8] = KhachHang.NoDauKy;
            ParameterColection[9] = "@TenNguoiLienHe";
            valueofParameter[9] = KhachHang.TenNguoiLienHe;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Thêm khách hàng
        /// </summary>
        public int Them(clsKhachHangDTO KhachHang)
        {
            int i = -1;
            string sql = "sp_InsertKhachHang";
            string[] ParameterColection = new string[10];
            Object[] valueofParameter = new Object[10];
            ParameterColection[0] = "@MaKhachHang";
            valueofParameter[0] = KhachHang.MaKhachHang;
            ParameterColection[1] = "@TenKhachHang";
            valueofParameter[1] = KhachHang.TenKhachHang;
            ParameterColection[2] = "@DienThoai";
            valueofParameter[2] = KhachHang.DienThoai;
            ParameterColection[3] = "@DiaChi";
            valueofParameter[3] = KhachHang.DiaChi;
            ParameterColection[4] = "@Fax";
            valueofParameter[4] = KhachHang.Fax;
            ParameterColection[5] = "@MaSoThue";
            valueofParameter[5] = KhachHang.MaSoThue;
            ParameterColection[6] = "@BaoGia";
            valueofParameter[6] = KhachHang.BaoGia;
            ParameterColection[7] = "@ChietKhau";
            valueofParameter[7] = KhachHang.ChietKhau;
            ParameterColection[8] = "@NoDauKy";
            valueofParameter[8] = KhachHang.NoDauKy;
            ParameterColection[9] = "@TenNguoiLienHe";
            valueofParameter[9] = KhachHang.TenNguoiLienHe;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Xóa khách hàng
        /// </summary>
        public int Xoa(string MaKhachHang)
        {
            int i = -1;
            string sql = "sp_DeleteKhachHang";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaKhachHang";
            valueofParameter[0] = MaKhachHang;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        public DataTable ReportDSKhachHang(string MaKhachHang, string TenKhachHang)
        {
            string sql = "sp_ReportDSKhachHang";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@MaKhachHang";
            valueofParameter[0] = MaKhachHang;
            ParameterColection[1] = "@TenKhachHang";
            valueofParameter[1] = TenKhachHang;

            DataTable table = sqlServer.readData(sql, "sp_ReportDSKhachHang;1", ParameterColection, valueofParameter);

            return table;
        }
    }
}
