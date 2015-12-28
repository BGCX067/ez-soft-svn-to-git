using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsLoaiMatHangBUS
    {

        /// <summary>
        /// Đối tượng Loại mặt hàng DAO
        /// </summary>
        private clsLoaiMatHangDAO LoaiMatHangDAO =new clsLoaiMatHangDAO();

        public DataTable LayBang()
        {
            return LoaiMatHangDAO.LayBang();
        }
        /// <summary>
        /// Lấy mã loại mặt hàng mới
        /// </summary>
        public string LayMaLoaiMatHangMoi()
        {
            return LoaiMatHangDAO.LayMaLoaiMatHangMoi();
        }
        /// <summary>
        /// Thêm thông tin loại mặt hàng
        /// </summary>
        /// <param name="LoaiMatHang">
        /// MaLoaiMatHang   nvarchar(10)
        /// TenLoaiMatHang  nvarchar(255)
        /// DienGiai   ntext
        /// NgayTao  smalldatetime
        /// TrangThai int
        /// </param>
        public int Them(clsLoaiMatHangDTO LoaiMatHang)
        {
            return LoaiMatHangDAO.Them(LoaiMatHang);
        }

        /// <summary>
        /// Sửa thông tin  loại mặt hàng
        /// </summary>
        /// <param name="LoaiMatHang">
        /// MaLoaiMatHang  nvarchar(10)
        /// TenLoaiMatHang  nvarchar(255)
        /// DienGiai  ntext
        /// </param>
        public int Sua(clsLoaiMatHangDTO LoaiMatHang)
        {
            return LoaiMatHangDAO.Sua(LoaiMatHang);
        }

        /// <summary>
        /// Xóa thông tin loại mặt hàng
        /// </summary>
        /// <param name="MaLoaiMatHang">Mã loại mặt hàng</param>
        public int Xoa(string MaLoaiMatHang)
        {
            return LoaiMatHangDAO.Xoa(MaLoaiMatHang);
        }

        public DataTable ReportDSLoaiMatHang(string MaLoaiNhomHang, string TenLoaiNhomHang)
        {
            return LoaiMatHangDAO.ReportDSLoaiMatHang(MaLoaiNhomHang, TenLoaiNhomHang);
        }
    }
}
