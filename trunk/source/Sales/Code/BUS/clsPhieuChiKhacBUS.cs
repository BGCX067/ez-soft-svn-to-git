using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsPhieuChiKhacBUS : clsPhieuChiBUS
    {
        #region
        private clsPhieuChiKhacDAO PhieuChiKhacDAO = new clsPhieuChiKhacDAO();
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
        public clsPhieuChiKhacDTO LayThongTin(string MaPhieuChi)
        {
            return PhieuChiKhacDAO.LayThongTin(MaPhieuChi);
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
        public int Them(clsPhieuChiKhacDTO PhieuChi)
        {
            return PhieuChiKhacDAO.Them(PhieuChi);
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
        public int Sua(clsPhieuChiKhacDTO PhieuChi)
        {
            return PhieuChiKhacDAO.Sua(PhieuChi);
        }

         /// <summary>
        /// Xóa thông tin phiếu chi
        /// </summary>
        /// <param name="MaPhieuChi">Mã phiếu chi</param>
        public int Xoa(string MaPhieuChi)
        {
            return PhieuChiKhacDAO.Xoa(MaPhieuChi);
        }

         /// <summary>
        /// Tìm kiếm thông tin phiếu chi
        /// </summary>
        /// <param name="TuNgay">Ngày bắt đầu</param>
        /// <param name="DenNgay">Ngày kết thúc</param>
        public DataTable TimKiem(DateTime TuNgay, DateTime DenNgay)
        {
            return PhieuChiKhacDAO.TimKiem(TuNgay, DenNgay);
        }

       /// <summary>
        /// Tìm kiếm thông tin phiếu chi
        /// </summary>
        /// <param name="TuNgay">Ngày bắt đầu</param>
        /// <param name="DenNgay">Ngày kết thúc</param>
        public DataTable TimKiem(DateTime TuNgay, DateTime DenNgay, clsNhaCungCapDTO NhaCungCap)
        {
            return PhieuChiKhacDAO.TimKiem(TuNgay, DenNgay, NhaCungCap);
        }

        public DataTable GetChiPhiKhac(DateTime TuNgay, DateTime DenNgay)
        {
            return PhieuChiKhacDAO.GetChiPhiKhac(TuNgay,DenNgay);
        }

        /// <summary>
        /// Lấy danh sách các lý do
        /// </summary>
        public DataTable LayBangLyDo()
        {
            return PhieuChiKhacDAO.LayBangLyDo();
        }

        public DataTable ReportPhieuChiKhac(string MaPhieuChi)
        {
            return PhieuChiKhacDAO.ReportPhieuChiKhac(MaPhieuChi);
        }
    }
}
