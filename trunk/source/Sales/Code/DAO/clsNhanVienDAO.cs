using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsNhanVienDAO
    {
        /// <summary>
        /// Kết nối SQL Server
        /// </summary>
        public CSQLServer sqlServer = new CSQLServer();

        /// <summary>
        /// Lấy Mã mặt hàng kế tiếp để phục vụ cho chức năng Insert
        /// </summary>
        public string LayMaNhanVienMoi()
        {
            string sql = "sp_GetNewMaNhanVien";
            return sqlServer.readData(sql, "@MaNhanVienMoi");
        }

        public clsNhanVienDTO LayThongTin(string MaNhanVien)
        {
            string sql = "sp_GetInfoNhanVien";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaNhanVien";
            valueofParameter[0] = MaNhanVien;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return ChuyenDoi(table);
        }

        public clsNhanVienDTO LayThongTinTheoTenNguoiDung(string TenNguoiDung)
        {
            string sql = "sp_GetInfoNhanVienTheoTenNguoiDung";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@TenNguoiDung";
            valueofParameter[0] = TenNguoiDung;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return ChuyenDoi(table);
        }

        private clsNhanVienDTO ChuyenDoi(DataTable table)
        {
            clsNhanVienDTO NhanVien = new clsNhanVienDTO();
            if (table.Rows.Count == 1)
            {
                DataRow Dong = table.Rows[0];
                NhanVien.MaNhanVien = Dong["MaNhanVien"].ToString();
                NhanVien.TenNhanVien = Dong["TenNhanVien"].ToString();
                NhanVien.DienThoai = Dong["DienThoai"].ToString();
                NhanVien.DiaChi = Dong["DiaChi"].ToString();
                NhanVien.GhiChu = Dong["GhiChu"].ToString();
                NhanVien.TenNguoiDung = Dong["TenNguoiDung"].ToString();
                NhanVien.MatKhau = Dong["MatKhau"].ToString();
                NhanVien.Email = Dong["Email"].ToString();
                NhanVien.QuyenHan.MaQuyenHan = int.Parse(Dong["QuyenHan"].ToString());
                NhanVien.QuyenHan.TenQuyenHan = Dong["TenQuyenHan"].ToString();
                NhanVien.NgayTao = DateTime.Parse(Dong["NgayTao"].ToString());
                NhanVien.TrangThai = int.Parse(Dong["TrangThai"].ToString());
                return NhanVien;
            }
            return null;

        }

        /// <summary>
        /// Lấy DS Nhân viên
        /// </summary>
        public DataTable LayBang()
        {
            string sql = "sp_GetBangNhanVien";
            DataTable table = sqlServer.readData(sql);
            return table;
        }

        /// <summary>
        /// Lấy DS Nhân viên
        /// </summary>
        public DataTable LayBangChoQuanTri()
        {
            string sql = "sp_GetBangNhanVienChoQuanTri";
            DataTable table = sqlServer.readData(sql);
            return table;
        }

        /// <summary>
        /// Cập nhật Nhân viên
        /// </summary>
        public int Sua(clsNhanVienDTO NhanVien)
        {
            int i = -1;
            string sql = "sp_UpdateNhanVien";
            string[] ParameterColection = new string[8];
            Object[] valueofParameter = new Object[8];
            ParameterColection[0] = "@MaNhanVien";
            valueofParameter[0] = NhanVien.MaNhanVien;
            ParameterColection[1] = "@TenNhanVien";
            valueofParameter[1] = NhanVien.TenNhanVien;
            ParameterColection[2] = "@DienThoai";
            valueofParameter[2] = NhanVien.DienThoai;
            ParameterColection[3] = "@DiaChi";
            valueofParameter[3] = NhanVien.DiaChi;
            ParameterColection[4] = "@GhiChu";
            valueofParameter[4] = NhanVien.GhiChu;
            ParameterColection[5] = "@QuyenHan";
            valueofParameter[5] = NhanVien.QuyenHan.MaQuyenHan;
            ParameterColection[6] = "@TenNguoiDung";
            valueofParameter[6] = NhanVien.TenNguoiDung;
            ParameterColection[7] = "@MatKhau";
            valueofParameter[7] = NhanVien.MatKhau;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Cập nhật Nhân viên
        /// </summary>
        public int DoiMatKhau(string TenNguoiDung, string MatKhau)
        {
            int i = -1;
            string sql = "sp_UpdateMatKhau";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@TenNguoiDung";
            valueofParameter[0] = TenNguoiDung;
            ParameterColection[1] = "@MatKhau";
            valueofParameter[1] = MatKhau;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Kiểm tra người dùng đăng nhập
        /// </summary>
        public Boolean KiemTraNguoiDung(string TenNguoiDung, string MatKhau)
        {
            string sql = "sp_DangNhap";//Chú ý chưa có store
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@TenNguoiDung";
            valueofParameter[0] = TenNguoiDung;
            ParameterColection[1] = "@MatKhau";
            valueofParameter[1] = MatKhau;
            string KetQua = sqlServer.readData(sql, ParameterColection, valueofParameter,"@KetQua");
            if (KetQua.Trim() == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Cập nhật quyền hạn sử dụng
        /// </summary>
        public int DoiQuyenHanSuDung(string MaNhanVien, int QuyenHan)
        {
            int i = -1;
            string sql = "sp_UpdateDoiQuyenHanSuDung";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@MaNhanVien";
            valueofParameter[0] = MaNhanVien;
            ParameterColection[1] = "@QuyenHan";
            valueofParameter[1] = QuyenHan;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Thêm Nhân viên
        /// </summary>
        public int Them(clsNhanVienDTO NhanVien)
        {
            int i = -1;
            string sql = "sp_InsertNhanVien";
            string[] ParameterColection = new string[8];
            Object[] valueofParameter = new Object[8];
            ParameterColection[0] = "@MaNhanVien";
            valueofParameter[0] = NhanVien.MaNhanVien;
            ParameterColection[1] = "@TenNhanVien";
            valueofParameter[1] = NhanVien.TenNhanVien;
            ParameterColection[2] = "@DienThoai";
            valueofParameter[2] = NhanVien.DienThoai;
            ParameterColection[3] = "@DiaChi";
            valueofParameter[3] = NhanVien.DiaChi;
            ParameterColection[4] = "@GhiChu";
            valueofParameter[4] = NhanVien.GhiChu;
            ParameterColection[5] = "@TenNguoiDung";
            valueofParameter[5] = NhanVien.TenNguoiDung;
            ParameterColection[6] = "@MatKhau";
            valueofParameter[6] = NhanVien.MatKhau;
            ParameterColection[7] = "@QuyenHan";
            valueofParameter[7] = NhanVien.QuyenHan.MaQuyenHan;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Xóa Nhân viên
        /// </summary>
        public int Xoa(string MaNhanVien)
        {
            int i = -1;
            string sql = "sp_DeleteNhanVien";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaNhanVien";
            valueofParameter[0] = MaNhanVien;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        public DataTable ReportDSNhanVien(string TenNhanVien, string TenNguoiDung)
        {
            string sql = "sp_ReportDSNhanVien";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@TenNhanVien";
            valueofParameter[0] = TenNhanVien;
            ParameterColection[1] = "@TenNguoiDung";
            valueofParameter[1] = TenNguoiDung;

            DataTable table = sqlServer.readData(sql, "sp_ReportDSNhanVien;1", ParameterColection, valueofParameter);

            return table;
        }
    }
}
