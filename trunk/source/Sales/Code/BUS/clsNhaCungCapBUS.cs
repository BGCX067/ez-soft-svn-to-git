using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsNhaCungCapBUS
    {
        /// <summary>
        /// Đối tượng nhà cung cấp DAO
        /// </summary>
        private clsNhaCungCapDAO NhaCungCapDAO = new clsNhaCungCapDAO();

        public DataTable LayBang()
        {
            return NhaCungCapDAO.LayBang();
        }

        public string LayMaNhaCungCapMoi()
        {
            return NhaCungCapDAO.LayMaNhaCungCapMoi();
        }

        /// <summary>
        /// Thêm thông nhà cung cấp
        /// </summary>
        /// <param name="LoaiMatHang">
        /// MaLoaiMatHang   nvarchar(10)
        /// TenLoaiMatHang  nvarchar(255)
        /// DienGiai   ntext
        /// NgayTao  smalldatetime
        /// TrangThai int
        /// </param>
        public int Them(clsNhaCungCapDTO NhaCungCap)
        {
            return NhaCungCapDAO.Them(NhaCungCap);
        }

        /// <summary>
        /// Sửa thông tin nhà cung cấp
        /// </summary>
        /// <param name="LoaiMatHang">
        /// MaLoaiMatHang  nvarchar(10)
        /// TenLoaiMatHang  nvarchar(255)
        /// DienGiai  ntext
        /// </param>
        public int Sua(clsNhaCungCapDTO NhaCungCap)
        {
            return NhaCungCapDAO.Sua(NhaCungCap);
        }

        /// <summary>
        /// Xóa thông tin nhà cung cấp
        /// </summary>
        /// <param name="MaLoaiMatHang">Mã loại mặt hàng</param>
        public int Xoa(string MaNhaCungCap)
        {
            return NhaCungCapDAO.Xoa(MaNhaCungCap);
        }

        public DataTable ReportDSNhaCungCap(string MaNhaCungCap, string TenNhaCungCap)
        {
            return NhaCungCapDAO.ReportDSNhaCungCap(MaNhaCungCap, TenNhaCungCap);
        }
    }
}
