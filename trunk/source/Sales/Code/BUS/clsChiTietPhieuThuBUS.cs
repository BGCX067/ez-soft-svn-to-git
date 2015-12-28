using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsChiTietPhieuThuBUS
    {
        #region Attribute
        private clsChiTietPhieuThuDAO ChiTietPhieuThuDAO = new clsChiTietPhieuThuDAO();
        #endregion
        /// <summary>
        /// Lấy danh sách chi tiết  Phiếu xuất theo mã phiếu thu
        /// </summary>
        /// <param name="MaPhieuThu">Mã phiếu thu</param>
        public DataTable LayBang(string MaPhieuThu)
        {
            return ChiTietPhieuThuDAO.LayBang(MaPhieuThu);
        }

        /// <summary>
        /// Thêm thông tin chi tiết phiếu thu
        /// </summary>
        /// <param name="ChiTietPhieuThu">
        /// MaPhieuThu  nvarchar(10)
        /// MaPhieuXuat  nvarchar(10)
        /// SoTien  int
        /// </param>
        public int Them(clsChiTietPhieuThuDTO ChiTietPhieuThu)
        {
            return ChiTietPhieuThuDAO.Them(ChiTietPhieuThu);
        }

        /// <summary>
        /// Sửa thông tin chi tiết phiếu thu [chi được phép sửa số tiền]
        /// </summary>
        /// <param name="ChiTietPhieuThu">
        /// MaPhieuThu  nvarchar(10)
        /// MaPhieuXuat  nvarchar(10)
        /// SoTien  int
        /// </param>
        public int Sua(clsChiTietPhieuThuDTO ChiTietPhieuThu)
        {
            return ChiTietPhieuThuDAO.Sua(ChiTietPhieuThu);
        }

        /// <summary>
        /// Xóa thông tin chi tiết phiếu chi
        /// </summary>
        /// <param name="ChiTietPhieuThu">
        /// MaPhieuThu  nvarchar(10)
        /// MaPhieuXuat  nvarchar(10)
        /// </param>
        public int Xoa(clsChiTietPhieuThuDTO ChiTietPhieuThu)
        {
            return ChiTietPhieuThuDAO.Xoa(ChiTietPhieuThu);
        }

        /// <summary>
        /// Xóa thông tin chi tiết phiếu thu
        /// </summary>
        /// <param name="ChiTietPhieuChi">
        /// MaPhieuThu  nvarchar(10)
        /// MaPhieuXuat  nvarchar(10)
        /// </param>
        public int XoaTatCa(string MaPhieuThu)
        {
            return ChiTietPhieuThuDAO.XoaTatCa(MaPhieuThu);
        }
    }
}
