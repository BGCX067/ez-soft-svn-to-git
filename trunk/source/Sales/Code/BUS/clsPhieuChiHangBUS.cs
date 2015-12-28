using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    class clsPhieuChiHangBUS:clsPhieuChiBUS
    {
        #region
        private clsPhieuChiHangDAO PhieuChiHangDAO = new clsPhieuChiHangDAO();
        #endregion
        /// <summary>
        /// Lấy thông tin phiếu chi hàng
        /// </summary>
        /// <param name="PhieuChi">
        /// MaPhieuChi   nvarchar(10)
        /// NgayChi   smalldatetime
        /// NguoiNhan   nvarchar(255)
        /// SoTien   float
        /// LyDo  nvarchar(255)
        /// NhaCungCap  nvarchar(255)
        /// TrangThai  int
        /// </param>
        public clsPhieuChiHangDTO LayThongTin(string MaPhieuChi)
        {
            return PhieuChiHangDAO.LayThongTin(MaPhieuChi);
        }

        /// <summary>
        /// Thêm thông tin phiếu chi
        /// </summary>
        /// <param name="PhieuChi">
        /// MaPhieuChi   nvarchar(10)
        /// NgayChi   smalldatetime
        /// NguoiNhan   nvarchar(255)
        /// SoTien   float
        /// LyDo  nvarchar(255)
        /// NhaCungCap  nvarchar(255)
        /// TrangThai  int
        /// </param>
        public int Them(clsPhieuChiHangDTO PhieuChi)
        {
            return PhieuChiHangDAO.Them(PhieuChi);
        }

        /// <summary>
        /// Sửa thông tin phiếu chi
        /// </summary>
        /// <param name="PhieuChi">
        /// MaPhieuChi   nvarchar(10)
        /// NgayChi   smalldatetime
        /// NguoiNhan   nvarchar(255)
        /// SoTien   float
        /// LyDo  nvarchar(255)
        /// NhaCungCap  nvarchar(255)
        /// TrangThai  int
        /// </param>
        public int Sua(clsPhieuChiHangDTO PhieuChi)
        {
            return PhieuChiHangDAO.Sua(PhieuChi);
        }

         /// <summary>
        /// Xóa thông tin phiếu chi
        /// </summary>
        /// <param name="MaPhieuChi">Mã phiếu chi</param>
        public int Xoa(string MaPhieuChi)
        {
           return PhieuChiHangDAO.Xoa(MaPhieuChi);
        }

        /// <summary>
        /// Hủy thông tin phiếu chi
        /// </summary>
        /// <param name="MaPhieuChi">Mã phiếu chi</param>
        public int Huy(string MaPhieuChi)
        {
            return PhieuChiHangDAO.Huy(MaPhieuChi);
        }

          /// <summary>
        /// Tìm kiếm thông tin phiếu chi
        /// </summary>
        /// <param name="TuNgay">Ngày bắt đầu</param>
        /// <param name="DenNgay">Ngày kết thúc</param>
        public DataTable TimKiem(DateTime TuNgay, DateTime DenNgay)
        {
            return PhieuChiHangDAO.TimKiem(TuNgay, DenNgay);
        }

         /// <summary>
        /// Tìm kiếm thông tin phiếu chi
        /// </summary>
        /// <param name="TuNgay">Ngày bắt đầu</param>
        /// <param name="DenNgay">Ngày kết thúc</param>
        public DataTable TimKiem(DateTime TuNgay, DateTime DenNgay, clsNhaCungCapDTO NhaCungCap)
        {
            return PhieuChiHangDAO.TimKiem(TuNgay, DenNgay, NhaCungCap);
        }

        public DataTable ReportPhieuChiHang(string MaPhieuChi)
        {
            return PhieuChiHangDAO.ReportPhieuChiHang(MaPhieuChi);
        }

        public DataTable ReportDSPhieuChi(DateTime TuNgay, DateTime DenNgay, string MaPhieuChi, string MaNhaCungCap)
        {
            return PhieuChiHangDAO.ReportDSPhieuChi(TuNgay,DenNgay,MaPhieuChi,MaNhaCungCap);
        }
    }
}
