using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsPhieuThuBanHangDAO : clsPhieuThuDAO
    {
        #region Attribute
        private clsChiTietPhieuThuDAO ChiTietPhieuThuDAO = new clsChiTietPhieuThuDAO();
        #endregion

        /// <summary>
        /// Lấy thông tin phiếu thu bán hàng
        /// </summary>
        /// <param name="PhieuThu">
        /// MaPhieuThu   nvarchar(10)
        /// NgayThu   smalldatetime
        /// NguoiNop   nvarchar(255)
        /// SoTien   float
        /// LyDo  nvarchar(255)
        /// KhachHang  nvarchar(255)
        /// TrangThai  int
        /// </param>
        public clsPhieuThuBanHangDTO LayThongTin(string MaPhieuThu)
        {
            string sql = "sp_GetInfoPhieuThuBanHang";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuThu";
            valueofParameter[0] = MaPhieuThu;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            clsPhieuThuBanHangDTO PhieuThu = ChuyenDoi(table);
            return PhieuThu;
        }

        /// <summary>
        /// chuyển từ một dòng sang đối tượng PhieuThuBanHangDTO
        /// </summary>
        /// 

        private clsPhieuThuBanHangDTO ChuyenDoi(DataTable table)
        {
            clsPhieuThuBanHangDTO PhieuThu = new clsPhieuThuBanHangDTO(); 
            if (table.Rows.Count == 1)
            {
                DataRow Dong = table.Rows[0];
                PhieuThu.MaPhieuThu = Dong["MaPhieuThu"].ToString();
                PhieuThu.NgayThu = DateTime.Parse(Dong["NgayThu"].ToString());
                PhieuThu.NguoiNop = Dong["NguoiNop"].ToString();
                PhieuThu.SoTien = Double.Parse(Dong["SoTien"].ToString());
                PhieuThu.LyDo = Dong["LyDo"].ToString();
                PhieuThu.KhachHang = Dong["KhachHang"].ToString();
                PhieuThu.TrangThai = int.Parse(Dong["TrangThai"].ToString());
                PhieuThu.NguoiThu = Dong["MaNhanVien"].ToString();
                //Đọc danh sách chi tiết phiếu thu bán hàng
                PhieuThu.DS_ChiTietPhieuThu = ChiTietPhieuThuDAO.LayDanhSach(PhieuThu.MaPhieuThu);
                return PhieuThu;
            }
            return null;

        }
        /// <summary>
        /// Thêm thông tin phiếu thu
        /// </summary>
        /// <param name="PhieuThu">
        /// MaPhieuThu   nvarchar(10)
        /// NgayThu   smalldatetime
        /// NguoiNop   nvarchar(255)
        /// SoTien   float
        /// LyDo  nvarchar(255)
        /// KhachHang  nvarchar(255)
        /// TrangThai  int
        /// </param>
        public int Them(clsPhieuThuBanHangDTO PhieuThu)
        {
            int i = -1;
            string sql = "sp_InsertPhieuThuBanHang";
            string[] ParameterColection = new string[8];
            Object[] valueofParameter = new Object[8];
            ParameterColection[0] = "@MaPhieuThu";
            valueofParameter[0] = PhieuThu.MaPhieuThu;
            ParameterColection[1] = "@NgayThu";
            valueofParameter[1] = PhieuThu.NgayThu;
            ParameterColection[2] = "@NguoiNop";
            valueofParameter[2] = PhieuThu.NguoiNop;
            ParameterColection[3] = "@SoTien";
            valueofParameter[3] = PhieuThu.SoTien;
            ParameterColection[4] = "@LyDo";
            valueofParameter[4] = PhieuThu.LyDo;
            ParameterColection[5] = "@KhachHang";
            valueofParameter[5] = PhieuThu.KhachHang;
            ParameterColection[6] = "@LoaiPhieuThu";
            valueofParameter[6] = "Thu bán hàng";
            ParameterColection[7] = "@MaNhanVien";
            valueofParameter[7] = PhieuThu.NguoiThu;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            if (i != -1)
            {
                for (int k = 0; k < PhieuThu.DS_ChiTietPhieuThu.Count; k++)
                {
                    i = ChiTietPhieuThuDAO.Them(PhieuThu.DS_ChiTietPhieuThu[k]);
                }
            }
            return i;
        }

        /// <summary>
        /// Sửa thông tin phiếu thu bán hàng
        /// </summary>
        /// <param name="PhieuThu">
        /// MaPhieuThu   nvarchar(10)
        /// NgayThu   smalldatetime
        /// NguoiNop   nvarchar(255)
        /// SoTien   float
        /// LyDo  nvarchar(255)
        /// KhachHang  nvarchar(255)
        /// TrangThai  int
        /// </param>
        public int Sua(clsPhieuThuBanHangDTO PhieuThu)
        {
            int i = -1;
            string sql = "sp_UpdatePhieuThuBanHang";
            string[] ParameterColection = new string[5];
            Object[] valueofParameter = new Object[5];
            ParameterColection[0] = "@MaPhieuThu";
            valueofParameter[0] = PhieuThu.MaPhieuThu;
            ParameterColection[1] = "@NgayThu";
            valueofParameter[1] = PhieuThu.NgayThu;
            ParameterColection[2] = "@NguoiNop";
            valueofParameter[2] = PhieuThu.NguoiNop;
            ParameterColection[3] = "@SoTien";
            valueofParameter[3] = PhieuThu.SoTien;
            ParameterColection[4] = "@LyDo";
            valueofParameter[4] = PhieuThu.LyDo;

            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            if (i != -1)
            {
                for (int k = 0; k < PhieuThu.DS_ChiTietPhieuThu.Count; k++)
                {
                    i = ChiTietPhieuThuDAO.Them(PhieuThu.DS_ChiTietPhieuThu[k]);
                }
            }
            return i;
        }

        /// <summary>
        /// Xóa thông tin phiếu thu bán hàng
        /// </summary>
        /// <param name="MaPhieuThu">Mã phiếu thu</param>
        public int Xoa(string MaPhieuThu)
        {
            int i = -1;
            string sql = "sp_DeletePhieuThuBanHang";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuThu";
            valueofParameter[0] = MaPhieuThu;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Hủy thông tin phiếu thu
        /// </summary>
        /// <param name="MaPhieuChi">Mã phiếu thu</param>
        public int Huy(string MaPhieuThu)
        {
            int i = -1;
            string sql = "sp_RemovePhieuThuBanHang";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuThu";
            valueofParameter[0] = MaPhieuThu;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Tìm kiếm thông tin phiếu thu bán hàng
        /// </summary>
        /// <param name="TuNgay">Ngày bắt đầu</param>
        /// <param name="DenNgay">Ngày kết thúc</param>
        public DataTable TimKiem(DateTime TuNgay, DateTime DenNgay)
        {
            string sql = "sp_SearchPhieuThuBanHang";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return table;
        }

        /// <summary>
        /// Tìm kiếm thông tin phiếu thu bán hàng
        /// </summary>
        /// <param name="TuNgay">Ngày bắt đầu</param>
        /// <param name="DenNgay">Ngày kết thúc</param>
        public DataTable TimKiem(DateTime TuNgay, DateTime DenNgay, clsKhachHangDTO KhachHang)
        {
            string sql = "sp_SearchPhieuThuBanHangTheoKH";
            string[] ParameterColection = new string[3];
            Object[] valueofParameter = new Object[3];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            ParameterColection[2] = "@MaKhachHang";
            valueofParameter[2] = KhachHang.MaKhachHang;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return table;
        }

        public DataTable ReportPhieuThu(string MaPhieuThu)
        {
            string sql = "sp_ReportPhieuThu";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuThu";
            valueofParameter[0] = MaPhieuThu;
            DataTable table = sqlServer.readData(sql, "sp_ReportPhieuThu;1", ParameterColection, valueofParameter);

            return table;
        }

        public DataTable ReportDSPhieuThu(DateTime TuNgay, DateTime DenNgay, string MaPhieuThu, string MaKhachHang)
        {
            string sql = "sp_ReportDSPhieuThu";
            string[] ParameterColection = new string[4];
            Object[] valueofParameter = new Object[4];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            ParameterColection[2] = "@MaPhieuThu";
            valueofParameter[2] = MaPhieuThu;
            ParameterColection[3] = "@MaKhachHang";
            valueofParameter[3] = MaKhachHang;
            DataTable table = sqlServer.readData(sql, "sp_ReportDSPhieuThu;1", ParameterColection, valueofParameter);

            return table;
        }
    }
}