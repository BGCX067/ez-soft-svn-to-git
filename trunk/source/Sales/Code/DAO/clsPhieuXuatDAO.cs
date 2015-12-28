using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsPhieuXuatDAO
    {
        #region Attribute
        /// <summary>
        /// Kết nối SQL Server
        /// </summary>
        protected CSQLServer sqlServer = new CSQLServer();
        protected clsChiTietPhieuXuatDAO ChiTietPhieuXuatDAO = new clsChiTietPhieuXuatDAO();
        #endregion

        /// <summary>
        /// Lấy Mã phiếu xuất kế tiếp để phục vụ cho chức năng Insert
        /// </summary>
        public string LayMaPhieuXuatMoi()
        {
            string sql = "sp_GetNewMaPhieuXuat";
            return sqlServer.readData(sql, "@MaPhieuXuatMoi");
        }

        /// <summary>
        /// Lấy danh sách thông tin chi tiết  Phiếu xuất hàng vào kho theo từ ngày đến ngày
        /// </summary>
        /// <param name="TuNgay">Từ ngày</param>
        /// <param name="DenNgay">Đến ngày</param>
        /// <returns></returns>
        public DataTable LayBang(DateTime TuNgay, DateTime DenNgay)
        {
            string sql = "sp_GetBangThongTinChiTietCacPhieuXuat";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return table;
        }

        // Lấy thông tin phiếu xuất còn thu theo nhà cung cấp
        public DataTable LayBangConThu(clsKhachHangDTO KhachHang)
        {
            string sql = "sp_GetTablePhieuXuatConThuTheoKH";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaKhachHang";
            valueofParameter[0] = KhachHang.MaKhachHang;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return table;
        }

        /// <summary>
        /// Hủy thông tin phiếu xuất
        /// </summary>
        /// <param name="MaPhieuXuat">MaPhieuXuat   string</param>
        public int Huy(string MaPhieuXuat)
        {
            int i = -1;
            string sql = "sp_RemovePhieuXuat";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuXuat";
            valueofParameter[0] = MaPhieuXuat;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Tìm kiếm thông tin phiếu xuất từ ngày đến ngày với tất cả các khách hàng
        /// </summary>
        /// <param name="TuNgay">
        /// MaPhieuXuat                  nvarchar(10)
        /// NgayXuat                       smalldatetime
        /// MaNhanVienBanHang     nvarchar(10)
        /// TongTien                        float
        /// DaTra                             float
        /// KhachHang                     nvarchar(255)
        /// LoaiPhieuXuat                 nvarchar(100)
        /// TrangThai                       int
        /// </param>
        public virtual DataTable TimKiem(DateTime TuNgay, DateTime DenNgay)
        {
            string sql = "sp_SearchPhieuXuat";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return table;
        }

        /// <summary>
        /// Tìm kiếm thông tin phiếu xuất từ ngày đến ngày với theo khách hàng
        /// </summary>
        /// <param name="TuNgay">
        /// MaPhieuXuat                  nvarchar(10)
        /// NgayXuat                       smalldatetime
        /// MaNhanVienBanHang     nvarchar(10)
        /// TongTien                        float
        /// DaTra                             float
        /// KhachHang                     nvarchar(255)
        /// LoaiPhieuXuat                 nvarchar(100)
        /// TrangThai                       int
        /// </param>
        public virtual DataTable TimKiem(DateTime TuNgay, DateTime DenNgay, clsKhachHangDTO KhachHang)
        {
            string sql = "sp_SearchPhieuXuatTheoKH";
            string[] ParameterColection = new string[3];
            Object[] valueofParameter = new Object[3];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            ParameterColection[2] = "@KhachHang";
            valueofParameter[2] = KhachHang.MaKhachHang;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return table;
        }

        public DataTable TimKiemNV(DateTime TuNgay, DateTime DenNgay)
        {
            string sql = "sp_SearchPhieuXuatTatCaNV";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return table;
        }

        public DataTable TimKiemNV(DateTime TuNgay, DateTime DenNgay, clsNhanVienDTO NhanVien)
        {
            string sql = "sp_SearchPhieuXuatTheoNV";
            string[] ParameterColection = new string[3];
            Object[] valueofParameter = new Object[3];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            ParameterColection[2] = "@NhanVien";
            valueofParameter[2] = NhanVien.MaNhanVien;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return table;
        }

        public DataTable TimKiemNV_CT(DateTime TuNgay, DateTime DenNgay)
        {
            string sql = "sp_SearchCacPhieuXuatTatCaNV";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return table;
        }

        public DataTable TimKiemNV_CT(DateTime TuNgay, DateTime DenNgay, clsNhanVienDTO NhanVien)
        {
            string sql = "sp_SearchCacPhieuXuatTheoNV";
            string[] ParameterColection = new string[3];
            Object[] valueofParameter = new Object[3];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            ParameterColection[2] = "@NhanVien";
            valueofParameter[2] = NhanVien.MaNhanVien;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return table;
        }

        public DataTable GetDoanhThu(DateTime TuNgay, DateTime DenNgay)
        {
            string sql = "sp_GetDoanhThu";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return table;
        }

        public DataTable GetGiaVon(DateTime TuNgay, DateTime DenNgay)
        {
            string sql = "sp_GetGiaVon";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return table;
        }

        public DataTable GetSPBanChay_DoanhSo(DateTime TuNgay, DateTime DenNgay)
        {
            string sql = "sp_GetSPBanChay_DoanhSo";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            DataTable table = sqlServer.readData(sql, "sp_GetSPBanChay_DoanhSo;1", ParameterColection, valueofParameter);
            return table;
        }

        public DataTable GetSPBanChay_SoLuong(DateTime TuNgay, DateTime DenNgay)
        {
            string sql = "sp_GetSPBanChay_SoLuong";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            DataTable table = sqlServer.readData(sql, "sp_GetSPBanChay_SoLuong;1", ParameterColection, valueofParameter);
            return table;
        }

        public DataTable ReportPhieuXuatTheoTK(DateTime TuNgay, DateTime DenNgay, string MaKhachHang, string MaNhanVien)
        {
            string sql = "sp_ReportPhieuXuatTheoTK";
            string[] ParameterColection = new string[4];
            Object[] valueofParameter = new Object[4];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            ParameterColection[2] = "@MaKhachHang";
            valueofParameter[2] = MaKhachHang;
            ParameterColection[3] = "@MaNhanVien";
            valueofParameter[3] = MaNhanVien;
            DataTable table = sqlServer.readData(sql, "sp_ReportPhieuXuatTheoTK;1", ParameterColection, valueofParameter);
            return table;
        }

        public DataTable ReportPhieuXuatTheoNV(DateTime TuNgay, DateTime DenNgay, string MaNhanVien)
        {
            string sql = "sp_ReportPhieuXuatTheoNV";
            string[] ParameterColection = new string[3];
            Object[] valueofParameter = new Object[3];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            ParameterColection[2] = "@MaNhanVien";
            valueofParameter[2] = MaNhanVien;
            DataTable table = sqlServer.readData(sql, "sp_ReportPhieuXuatTheoNV;1", ParameterColection, valueofParameter);
            return table;
        }

        public DataTable ReportCT_PhieuXuatTheoNV(DateTime TuNgay, DateTime DenNgay, string MaNhanVien)
        {
            string sql = "sp_ReportCT_PhieuXuatTheoNV";
            string[] ParameterColection = new string[3];
            Object[] valueofParameter = new Object[3];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            ParameterColection[2] = "@MaNhanVien";
            valueofParameter[2] = MaNhanVien;
            DataTable table = sqlServer.readData(sql, "sp_ReportCT_PhieuXuatTheoNV;1", ParameterColection, valueofParameter);
            return table;
        }

        public DataTable ReportChiTietHangXuat(DateTime TuNgay, DateTime DenNgay, string MaMatHang, string MaKhachHang)
        {
            string sql = "sp_ReportChiTietHangXuat";
            string[] ParameterColection = new string[4];
            Object[] valueofParameter = new Object[4];

            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            ParameterColection[2] = "@MaMatHang";
            valueofParameter[2] = MaMatHang;
            ParameterColection[3] = "@MaKhachHang";
            valueofParameter[3] = MaKhachHang;

            DataTable table = sqlServer.readData(sql, "sp_ReportChiTietHangXuat;1", ParameterColection, valueofParameter);
            return table;
        }
    }
}
