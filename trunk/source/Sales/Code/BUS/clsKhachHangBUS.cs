using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsKhachHangBUS
    {
        /// <summary>
        /// Đối tượng Loại mặt hàng DAO
        /// </summary>
        private clsKhachHangDAO KhachHangDAO = new clsKhachHangDAO();

        public DataTable LayBang()
        {
            return KhachHangDAO.LayBang();
        }

        public string LayMaKhachHangMoi()
        {
            return KhachHangDAO.LayMaKhachHangMoi();
         }
        /// <summary>
        /// Thêm thông tin khách hàng
        /// </summary>
        /// <param name="LoaiMatHang">
        /// MaLoaiMatHang   nvarchar(10)
        /// TenLoaiMatHang  nvarchar(255)
        /// DienGiai   ntext
        /// NgayTao  smalldatetime
        /// TrangThai int
        /// </param>
        public int Them(clsKhachHangDTO KhachHang)
        {
            return KhachHangDAO.Them(KhachHang);
        }

        /// <summary>
        /// Sửa thông tin khách hàng
        /// </summary>
        /// <param name="LoaiMatHang">
        /// MaLoaiMatHang  nvarchar(10)
        /// TenLoaiMatHang  nvarchar(255)
        /// DienGiai  ntext
        /// </param>
        public int Sua(clsKhachHangDTO KhachHang)
        {
            return KhachHangDAO.Sua(KhachHang);
        }

        /// <summary>
        /// Xóa thông tin khách hàng
        /// </summary>
        /// <param name="MaLoaiMatHang">Mã loại mặt hàng</param>
        public int Xoa(string MaKhachHang)
        {
            return KhachHangDAO.Xoa(MaKhachHang);
        }

        public DataTable ReportDSKhachHang(string MaKhachHang, string TenKhachHang)
        {
            return KhachHangDAO.ReportDSKhachHang(MaKhachHang, TenKhachHang);
        }
    }
}
