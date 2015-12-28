using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    class clsChucNangBUS
    {
        /// <summary>
        /// Đối tượng chức năng DAO
        /// </summary>
        private clsChucNangDAO ChucNangDAO = new clsChucNangDAO();

        public DataTable LayBang()
        {
            return ChucNangDAO.LayBang();
        }
      
        /// <summary>
        /// Thêm thông tin chức năng
        /// </summary>
        /// <param name="ChucNang">
        /// MaChucNang   nvarchar(10)
        /// TenChucNang  nvarchar(255)
        /// TrangThai int
        /// </param>
        public int Them(clsChucNangDTO ChucNang)
        {
            return ChucNangDAO.Them(ChucNang);
        }

        /// <summary>
        /// Sửa thông tin  loại mặt hàng
        /// </summary>
        /// <param name="ChucNang">
        /// MaChucNang  nvarchar(10)
        /// TenChucNang  nvarchar(255)
        /// </param>
        public int Sua(clsChucNangDTO ChucNang)
        {
            return ChucNangDAO.Sua(ChucNang);
        }

        /// <summary>
        /// Xóa thông tin loại mặt hàng
        /// </summary>
        /// <param name="MaChucNang">Mã loại mặt hàng</param>
        public int Xoa(string MaChucNang)
        {
            return ChucNangDAO.Xoa(MaChucNang);
        }
    }
}
