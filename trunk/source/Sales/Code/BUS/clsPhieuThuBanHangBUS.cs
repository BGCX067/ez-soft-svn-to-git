using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsPhieuThuBanHangBUS : clsPhieuThuBUS
    {
        #region
        private clsPhieuThuBanHangDAO PhieuThuBanHangDAO = new clsPhieuThuBanHangDAO();
        #endregion

        /// <summary>
        /// Lấy thông tin phiếu thu bán hàng
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
        public clsPhieuThuBanHangDTO LayThongTin(string MaPhieuThu)
        {
            return PhieuThuBanHangDAO.LayThongTin(MaPhieuThu);
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
        public int Them(clsPhieuThuBanHangDTO PhieuThu)
        {
            return PhieuThuBanHangDAO.Them(PhieuThu);
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
        public int Sua(clsPhieuThuBanHangDTO PhieuThu)
        {
            return PhieuThuBanHangDAO.Sua(PhieuThu);
        }

        /// <summary>
        /// Xóa thông tin phiếu thu
        /// </summary>
        /// <param name="MaPhieuThu">Mã phiếu thu</param>
        public int Xoa(string MaPhieuThu)
        {
            return PhieuThuBanHangDAO.Xoa(MaPhieuThu);
        }

        /// <summary>
        /// Hủy thông tin phiếu thu
        /// </summary>
        /// <param name="MaPhieuChi">Mã phiếu thu</param>
        public int Huy(string MaPhieuThu)
        {
            return PhieuThuBanHangDAO.Huy(MaPhieuThu);
        }

        /// <summary>
        /// Tìm kiếm thông tin phiếu thu
        /// </summary>
        /// <param name="TuNgay">Ngày bắt đầu</param>
        /// <param name="DenNgay">Ngày kết thúc</param>
        public DataTable TimKiem(DateTime TuNgay, DateTime DenNgay)
        {
            return PhieuThuBanHangDAO.TimKiem(TuNgay, DenNgay);
        }

        /// <summary>
        /// Tìm kiếm thông tin phiếu thu
        /// </summary>
        /// <param name="TuNgay">Ngày bắt đầu</param>
        /// <param name="DenNgay">Ngày kết thúc</param>
        public DataTable TimKiem(DateTime TuNgay, DateTime DenNgay, clsKhachHangDTO KhachHang)
        {
            return PhieuThuBanHangDAO.TimKiem(TuNgay, DenNgay, KhachHang);
        }

        public DataTable ReportPhieuThu(string MaPhieuThu)
        {
            return PhieuThuBanHangDAO.ReportPhieuThu(MaPhieuThu);

        }

        public DataTable ReportDSPhieuThu(DateTime TuNgay, DateTime DenNgay, string MaPhieuThu, string MaKhachHang)
        {
            return PhieuThuBanHangDAO.ReportDSPhieuThu(TuNgay, DenNgay, MaPhieuThu, MaKhachHang);
        }
    }

}
