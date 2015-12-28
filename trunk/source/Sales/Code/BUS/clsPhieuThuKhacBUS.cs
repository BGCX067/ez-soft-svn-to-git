using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsPhieuThuKhacBUS : clsPhieuThuBUS
    {
        #region
        private clsPhieuThuKhacDAO PhieuThuKhacDAO = new clsPhieuThuKhacDAO();
        #endregion

        /// <summary>
        /// Lấy thông tin phiếu thu khác
        /// </summary>
        /// <param name="PhieuThu">
        /// MaPhieuThu   nvarchar(10)
        /// NgayThu   smalldatetime
        /// NguoiNop   nvarchar(255)
        /// SoTien   float
        /// LyDo  nvarchar(255)
        /// KhachHang  nvarchar(255)
        /// TrangThai  int
        /// </param>
        public clsPhieuThuKhacDTO LayThongTin(string MaPhieuThu)
        {
            return PhieuThuKhacDAO.LayThongTin(MaPhieuThu);
        }

        /// <summary>
        /// Thêm thông tin phiếu thu
        /// </summary>
        /// <param name="PhieuThu">
        /// MaPhieuThu   nvarchar(10)
        /// NgayThu   smalldatetime
        /// NguoiNop   nvarchar(255)
        /// SoTien   float
        /// LyDo  nvarchar(255)
        /// KhachHang  nvarchar(255)
        /// TrangThai  int
        /// </param>
        public int Them(clsPhieuThuKhacDTO PhieuThu)
        {
            return PhieuThuKhacDAO.Them(PhieuThu);
        }

        /// <summary>
        /// Sửa thông tin phiếu thu
        /// </summary>
        /// <param name="PhieuThu">
        /// MaPhieuThu   nvarchar(10)
        /// NgayThu   smalldatetime
        /// NguoiNop   nvarchar(255)
        /// SoTien   float
        /// LyDo  nvarchar(255)
        /// KhachHang  nvarchar(255)
        /// TrangThai  int
        /// </param>
        public int Sua(clsPhieuThuKhacDTO PhieuThu)
        {
            return PhieuThuKhacDAO.Sua(PhieuThu);
        }

        /// <summary>
        /// Xóa thông tin phiếu thu
        /// </summary>
        /// <param name="MaPhieuThu">Mã phiếu thu</param>
        public int Xoa(string MaPhieuThu)
        {
            return PhieuThuKhacDAO.Xoa(MaPhieuThu);
        }

        /// <summary>
        /// Tìm kiếm thông tin phiếu thu
        /// </summary>
        /// <param name="TuNgay">Ngày bắt đầu</param>
        /// <param name="DenNgay">Ngày kết thúc</param>
        public DataTable TimKiem(DateTime TuNgay, DateTime DenNgay)
        {
            return PhieuThuKhacDAO.TimKiem(TuNgay, DenNgay);
        }

        /// <summary>
        /// Tìm kiếm thông tin phiếu thu
        /// </summary>
        /// <param name="TuNgay">Ngày bắt đầu</param>
        /// <param name="DenNgay">Ngày kết thúc</param>
        public DataTable TimKiem(DateTime TuNgay, DateTime DenNgay, clsKhachHangDTO KhachHang)
        {
            return PhieuThuKhacDAO.TimKiem(TuNgay, DenNgay, KhachHang);
        }

        public DataTable GetThuNhapKhac(DateTime TuNgay, DateTime DenNgay)
        {
            return PhieuThuKhacDAO.GetThuNhapKhac(TuNgay, DenNgay);
        }

        /// <summary>
        /// Lấy danh sách các lý do
        /// </summary>
        public DataTable LayBangLyDo()
        {
            return PhieuThuKhacDAO.LayBangLyDo();
        }

        public DataTable ReportPhieuThuKhac(string MaPhieuThu)
        {
            return PhieuThuKhacDAO.ReportPhieuThuKhac(MaPhieuThu);
        }
    }
}
