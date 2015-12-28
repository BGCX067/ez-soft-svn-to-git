using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsChiTietPhieuNhapBUS
    {
        /// <summary>
        /// Chi tiết phiếu nhập
        /// </summary>
        private clsChiTietPhieuNhapDAO ChiTietPhieuNhapDAO = new clsChiTietPhieuNhapDAO();

        /// <summary>
        /// Lấy danh sách chi tiết phiếu nhập
        /// </summary>
        /// <param name="MaPhieuNhap">Mã phiếu nhập</param>
        public DataTable LayBang(string MaPhieuNhap)
        {
            return ChiTietPhieuNhapDAO.LayBang(MaPhieuNhap);
        }
        /// <summary>
        /// Thêm thông tin chi tiết phiếu nhập 
        /// </summary>
        /// <param name="ChiTietPhieuNhap">
        /// MaPhieuNhap  nvarchar(10)
        /// MaMatHang  nvarchar(10)
        /// SoLuong  int
        /// DonGia  float
        /// ChietKhau  float
        /// ThanhTien  float
        /// ThueVAT  float
        /// </param>
        public int Them(clsChiTietPhieuNhapDTO ChiTietPhieuNhap)
        {
            return ChiTietPhieuNhapDAO.Them(ChiTietPhieuNhap);
        }
         /// <summary>
        /// Sửa thông tin chi tiếtphiếu nhập hàng theo mã mặt hàng và mã sản phẩm
        /// Chú ý: không cho sửa sản phẩm
        /// </summary>
        /// <param name="ChiTietPhieuNhap">
        /// MaPhieuNhap  nvarchar(10)
        /// MaMatHang  nvarchar(10)
        /// SoLuong  int
        /// DonGia  float
        /// ChietKhau  float
        /// ThanhTien  float
        /// ThueVAT  float
        /// </param>
        public int Sua(clsChiTietPhieuNhapDTO ChiTietPhieuNhap)
        {
            return ChiTietPhieuNhapDAO.Sua(ChiTietPhieuNhap);
        }
        /// <summary>
        /// Sửa thông tin chi tiếtphiếu nhập hàng theo mã mặt hàng và mã sản phẩm
        /// Chú ý: cho phép sửa tất cả các trường từ MaPhieuXuat
        /// </summary>
        /// <param name="ChiTietPhieuNhap">
        /// MaPhieuNhap  nvarchar(10)
        /// MaMatHang  nvarchar(10)
        /// SoLuong  int
        /// DonGia  float
        /// ChietKhau  float
        /// ThanhTien  float
        /// ThueVAT  float
        /// </param>
        public int Sua(clsChiTietPhieuNhapDTO ChiTietPhieuNhap, string MaMatHangMoi)
        {
            return ChiTietPhieuNhapDAO.Sua(ChiTietPhieuNhap, MaMatHangMoi);
        }
          /// <summary>
        /// Xóa thông tin phiếu nhập
        /// </summary>
        /// <param name="MaChiTietPhieuNhap">MaChiTietPhieuNhap   string</param>
        public int Xoa(string MaPhieuNhap, string MaMatHang)
        {
            return ChiTietPhieuNhapDAO.Xoa(MaPhieuNhap, MaMatHang);
        }
          /// <summary>
        /// Xóa tất cả các chi tiết phiếu nhập theo mã phiếu nhập
        /// </summary>
        /// <param name="MaphieuNhap">Mã phiếu nhập</param>
        public int XoaTatCa(string MaPhieuNhap)
        {
            return ChiTietPhieuNhapDAO.XoaTatCa(MaPhieuNhap);
        }
               
    }

    
}
