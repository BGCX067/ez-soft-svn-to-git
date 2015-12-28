using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsCongTyBUS
    {
        /// <summary>
        /// Đối tượng công ty
        /// </summary>
        private clsCongTyDAO CongTyDAO = new clsCongTyDAO();

        public clsCongTyDTO LayThongTin()
        {
            return CongTyDAO.LayThongTin();
        }

        
        public int Sua(clsCongTyDTO CongTy)
        {
            return CongTyDAO.Sua(CongTy);
        }

        public DataTable ReportCongTy()
        {
            return CongTyDAO.ReportCongTy();
        }

        /// <summary>
        /// Lấy danh sách thông tin chi tiết chi thu của công ty từ ngày đến ngày
        /// </summary>
        /// <param name="TuNgay">Từ ngày</param>
        /// <param name="DenNgay">Đến ngày</param>
        /// <returns></returns>
        public DataTable LayBangChiTietThuChi(DateTime TuNgay, DateTime DenNgay, string LyDo, string TenDoiTuong)
        {
            return CongTyDAO.LayBangChiTietThuChi(TuNgay, DenNgay, LyDo, TenDoiTuong);
        }

        /// <summary>
        /// Lấy số tiền tồn đầu kỳ
        /// </summary>
        public Double LayTienTonDauKy()
        {
            return CongTyDAO.LayTienTonDauKy();
        }

        /// <summary>
        /// Cập nhật số tiền tồn đầu kỳ
        /// </summary>
        public Double CapNhatTienTonDauKy(Double TienTonDauKy)
        {
            return CongTyDAO.CapNhatTienTonDauKy(TienTonDauKy);
        }

        public DataTable LayBangLyDo()
        {
            return CongTyDAO.LayBangLyDo();
        }

        public DataTable ReportQuyTienMat(DateTime TuNgay, DateTime DenNgay, string LyDo, string TenDoiTuong)
        {
            return CongTyDAO.ReportQuyTienMat(TuNgay, DenNgay, LyDo, TenDoiTuong);
        }

        public int SaoLuuDuLieu(string DuongDan, string MatKhau)
        {
            return CongTyDAO.SaoLuuDuLieu(DuongDan, MatKhau);
        }
    }
}
