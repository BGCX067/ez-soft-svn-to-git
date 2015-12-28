using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    class clsQuyenHanBUS
    {
        /// <summary>
        /// Đối tượng quyền hạn DAO
        /// </summary>
        private clsQuyenHanDAO QuyenHanDAO = new clsQuyenHanDAO();

        public DataTable LayBang()
        {
            return QuyenHanDAO.LayBang();
        }
        public clsQuyenHanDTO LayThongTin(int MaQuyenHan)
        {
            return QuyenHanDAO.LayThongTin(MaQuyenHan);
        }
        /// <summary>
        /// Thêm thông tin quyền hạn
        /// </summary>
        /// <param name="QuyenHan">
        /// MaQuyenHan   nvarchar(10)
        /// TenQuyenHan  nvarchar(255)
        /// TrangThai int
        /// </param>
        public int Them(clsQuyenHanDTO QuyenHan)
        {
            return QuyenHanDAO.Them(QuyenHan);
        }

        /// <summary>
        /// Sửa thông tin  loại mặt hàng
        /// </summary>
        /// <param name="QuyenHan">
        /// MaQuyenHan  nvarchar(10)
        /// TenQuyenHan  nvarchar(255)
        /// </param>
        public int Sua(clsQuyenHanDTO QuyenHan)
        {
            return QuyenHanDAO.Sua(QuyenHan);
        }

        /// <summary>
        /// Xóa thông tin loại mặt hàng
        /// </summary>
        /// <param name="MaQuyenHan">Mã loại mặt hàng</param>
        public int Xoa(string MaQuyenHan)
        {
            return QuyenHanDAO.Xoa(MaQuyenHan);
        }
    }
}
