using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    class clsDonViTinhBUS
    {
        /// <summary>
        /// Đối tượng Loại mặt hàng DAO
        /// </summary>
        private clsDonViTinhDAO DonViTinhDAO = new clsDonViTinhDAO();

        public DataTable LayBang()
        {
            return DonViTinhDAO.LayBang();
        }
        /// <summary>
        /// Lấy mã loại mặt hàng mới
        /// </summary>
        public string LayMaDonViTinhMoi()
        {
            return DonViTinhDAO.LayMaDonViTinh();
        }
        /// <summary>
        /// Thêm thông tin loại mặt hàng
        /// </summary>
        /// <param name="DonViTinh">
        /// MaDonViTinh   nvarchar(10)
        /// TenDonViTinh  nvarchar(255)
        /// DienGiai   ntext
        /// NgayTao  smalldatetime
        /// TrangThai int
        /// </param>
        public int Them(clsDonViTinhDTO DonViTinh)
        {
            return DonViTinhDAO.Them(DonViTinh);
        }

        /// <summary>
        /// Sửa thông tin  loại mặt hàng
        /// </summary>
        /// <param name="DonViTinh">
        /// MaDonViTinh  nvarchar(10)
        /// TenDonViTinh  nvarchar(255)
        /// DienGiai  ntext
        /// </param>
        public int Sua(clsDonViTinhDTO DonViTinh)
        {
            return DonViTinhDAO.Sua(DonViTinh);
        }

        /// <summary>
        /// Xóa thông tin loại mặt hàng
        /// </summary>
        /// <param name="MaDonViTinh">Mã loại mặt hàng</param>
        public int Xoa(string MaDonViTinh)
        {
            return DonViTinhDAO.Xoa(MaDonViTinh);
        }
    }
}
