using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    class clsNuocSanXuatBUS
    {
        /// <summary>
        /// Đối tượng Loại mặt hàng DAO
        /// </summary>
        private clsNuocSanXuatDAO NuocSanXuatDAO = new clsNuocSanXuatDAO();

        public DataTable LayBang()
        {
            return NuocSanXuatDAO.LayBang();
        }
        /// <summary>
        /// Lấy mã loại mặt hàng mới
        /// </summary>
        public string LayMaNuocSanXuatMoi()
        {
            return NuocSanXuatDAO.LayMaNuocSanXuat();
        }
        /// <summary>
        /// Thêm thông tin loại mặt hàng
        /// </summary>
        /// <param name="NuocSanXuat">
        /// MaNuocSanXuat   nvarchar(10)
        /// TenNuocSanXuat  nvarchar(255)
        /// DienGiai   ntext
        /// NgayTao  smalldatetime
        /// TrangThai int
        /// </param>
        public int Them(clsNuocSanXuatDTO NuocSanXuat)
        {
            return NuocSanXuatDAO.Them(NuocSanXuat);
        }

        /// <summary>
        /// Sửa thông tin  loại mặt hàng
        /// </summary>
        /// <param name="NuocSanXuat">
        /// MaNuocSanXuat  nvarchar(10)
        /// TenNuocSanXuat  nvarchar(255)
        /// DienGiai  ntext
        /// </param>
        public int Sua(clsNuocSanXuatDTO NuocSanXuat)
        {
            return NuocSanXuatDAO.Sua(NuocSanXuat);
        }

        /// <summary>
        /// Xóa thông tin loại mặt hàng
        /// </summary>
        /// <param name="MaNuocSanXuat">Mã loại mặt hàng</param>
        public int Xoa(string MaNuocSanXuat)
        {
            return NuocSanXuatDAO.Xoa(MaNuocSanXuat);
        }
    }
}
