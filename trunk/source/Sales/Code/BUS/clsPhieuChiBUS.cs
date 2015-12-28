using System;
using System.Collections.Generic;
using System.Text;
using System.Data;


namespace Sales
{
    public class clsPhieuChiBUS
    {
        private clsPhieuChiDAO PhieuChiDAO = new clsPhieuChiDAO();
        /// <summary>
        /// Lấy mã phiếu chi mới
        /// </summary>
        /// <returns></returns>
        public string LayMaPhieuChiMoi()
        {
           return PhieuChiDAO.LayMaPhieuChiMoi();
        }

         /// <summary>
        /// Tìm kiếm thông tin tất cả các phiếu chi cho phiếu nhập và chi khác
        /// </summary>
        /// <param name="TuNgay">Ngày bắt đầu</param>
        /// <param name="DenNgay">Ngày kết thúc</param>
        public DataTable TiemKiemChung(DateTime TuNgay, DateTime DenNgay)
        {
            return PhieuChiDAO.TiemKiemChung(TuNgay, DenNgay);
        }

        /// <summary>
        /// Tìm kiếm thông tin tất cả các phiếu chi cho phiếu nhập và chi khác
        /// </summary>
        /// <param name="TuNgay">Ngày bắt đầu</param>
        /// <param name="DenNgay">Ngày kết thúc</param>
        /// ///<param name="NhaCungCap">Nhà cung cấp</param>
        public DataTable TiemKiemChung(DateTime TuNgay, DateTime DenNgay,clsNhaCungCapDTO NhaCungCap)
        {
            return PhieuChiDAO.TiemKiemChung(TuNgay, DenNgay, NhaCungCap);
        }
    }
}
