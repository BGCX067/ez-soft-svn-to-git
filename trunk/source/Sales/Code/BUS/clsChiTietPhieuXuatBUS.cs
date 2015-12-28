using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsChiTietPhieuXuatBUS
    {
        #region Attribute
        private clsChiTietPhieuXuatDAO ChiTietPhieuXuatDAO = new clsChiTietPhieuXuatDAO();
        #endregion

        /// <summary>
        /// Lấy danh sách chi tiết Phiếu xuất hàng vào kho theo mã phiếu xuất
        /// </summary>
        /// <param name="MaPhieuNhap">Mã phiếu nhập</param>
        public DataTable LayBang(string MaPhieuXuat)
        {
            return ChiTietPhieuXuatDAO.LayBang(MaPhieuXuat);
        }

        /// <summary>
        /// Thêm thông tin chi tiết phiếu xuất 
        /// </summary>
        /// <param name="ChiTietPhieuXuất">
        /// MaPhieuXuat  nvarchar(10)
        /// MaMatHang  nvarchar(10)
        /// SoLuong  int
        /// DonGia  float
        /// ChietKhau  float
        /// ThanhTien  float
        /// ThueVAT  float
        /// </param>
        public int Them(clsChiTietPhieuXuatDTO ChiTietPhieuXuat)
        {
            return ChiTietPhieuXuatDAO.Them(ChiTietPhieuXuat);
        }

        /// <summary>
        /// Sửa thông tin chi tiếtphiếu xuất hàng theo mã mặt hàng và mã sản phẩm
        /// Chú ý: không cho sửa sản phẩm
        /// </summary>
        /// <param name="ChiTietPhieuXuat">
        /// MaPhieuXuat  nvarchar(10)
        /// MaMatHang  nvarchar(10)
        /// SoLuong  int
        /// DonGia  float
        /// ChietKhau  float
        /// ThanhTien  float
        /// ThueVAT  float
        /// </param>
        public int Sua(clsChiTietPhieuXuatDTO ChiTietPhieuXuat)
        {
            return ChiTietPhieuXuatDAO.Sua(ChiTietPhieuXuat);
        }

        /// <summary>
        /// Sửa thông tin chi tiếtphiếu xuất hàng theo mã mặt hàng và mã sản phẩm
        /// Chú ý: cho phép sửa tất cả các trường từ MaPhieuXuat
        /// </summary>
        /// <param name="ChiTietPhieuXuat">
        /// MaPhieuXuat  nvarchar(10)
        /// MaMatHang  nvarchar(10)
        /// SoLuong  int
        /// DonGia  float
        /// ChietKhau  float
        /// ThanhTien  float
        /// ThueVAT  float
        /// </param>
        public int Sua(clsChiTietPhieuXuatDTO ChiTietPhieuXuat, string MaMatHangMoi, string MaPhieuNhapMoi)
        {
            return ChiTietPhieuXuatDAO.Sua(ChiTietPhieuXuat, MaMatHangMoi, MaPhieuNhapMoi);
        }

        /// <summary>
        /// Xóa thông tin phiếu xuất
        /// </summary>
        /// <param name="MaChiTietPhieuXuat">MaChiTietPhieuXuat   string</param>
        public int Xoa(string MaPhieuXuat, string MaMatHang, string MaPhieuNhap)
        {
            return ChiTietPhieuXuatDAO.Xoa(MaPhieuXuat, MaMatHang, MaPhieuNhap);
        }

        /// <summary>
        /// Xóa tất cả các chi tiết phiếu xuất theo mã phiếu xuất
        /// </summary>
        /// <param name="MaPhieuXuat">Mã phiếu xuất</param>
        public int XoaTatCa(string MaPhieuXuat)
        {
            return ChiTietPhieuXuatDAO.XoaTatCa(MaPhieuXuat);
        } 
    }
}
