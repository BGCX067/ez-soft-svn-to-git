using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsChiTietPhieuChiBUS
    {
        #region Attribute
        /// <summary>
        /// Kết nối tầng DAo
        /// </summary>
        private clsChiTietPhieuChiDAO ChiTietPhieuChiDAO = new clsChiTietPhieuChiDAO();
        #endregion

        /// <summary>
        /// Lấy danh sách chi tiết  Phiếu nhập hàng vào kho theo mã phiếu chi
        /// </summary>
        /// <param name="MaPhieuChi">Mã phiếu chi</param>
        public DataTable LayBang(string MaPhieuChi)
        {
            return ChiTietPhieuChiDAO.LayBang(MaPhieuChi);
        }

        /// <summary>
        /// Thêm thông tin chi tiết phiếu chi 
        /// </summary>
        /// <param name="ChiTietPhieuChi">
        /// MaPhieuChi  nvarchar(10)
        /// MaPhieuNhap  nvarchar(10)
        /// SoTien  int
        /// </param>
        public int Them(clsChiTietPhieuChiDTO ChiTietPhieuChi)
        {
            return ChiTietPhieuChiDAO.Them(ChiTietPhieuChi);
        }

        /// <summary>
        /// Sửa thông tin chi tiết phiếu chi [chi được phép sửa số tiền]
        /// </summary>
        /// <param name="ChiTietPhieuChi">
        /// MaPhieuChi  nvarchar(10)
        /// MaPhieuNhap  nvarchar(10)
        /// SoTien  int
        /// </param>
        public int Sua(clsChiTietPhieuChiDTO ChiTietPhieuChi)
        {
            return ChiTietPhieuChiDAO.Sua(ChiTietPhieuChi);
        }

        /// <summary>
        /// Xóa thông tin chi tiết phiếu chi [chi được phép sửa số tiền]
        /// </summary>
        /// <param name="ChiTietPhieuChi">
        /// MaPhieuChi  nvarchar(10)
        /// MaPhieuNhap  nvarchar(10)
        /// </param>
        public int Xoa(clsChiTietPhieuChiDTO ChiTietPhieuChi)
        {
            return ChiTietPhieuChiDAO.Xoa(ChiTietPhieuChi);
        }

        /// <summary>
        /// Xóa tất cả  thông tin chi tiết phiếu chi [chi được phép sửa số tiền]
        /// </summary>
        /// <param name="ChiTietPhieuChi">
        /// MaPhieuChi  nvarchar(10)
        /// MaPhieuNhap  nvarchar(10)
        /// </param>
        public int XoaTatCa(string MaPhieuChi)
        {
            return ChiTietPhieuChiDAO.XoaTatCa(MaPhieuChi);
        }

    }

    
}
