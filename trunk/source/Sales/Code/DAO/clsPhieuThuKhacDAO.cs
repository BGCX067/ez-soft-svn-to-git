using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsPhieuThuKhacDAO : clsPhieuThuDAO
    {
        /// <summary>
        /// Lấy thông tin phiếu thu khác
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
        public clsPhieuThuKhacDTO LayThongTin(string MaPhieuThu)
        {
            string sql = "sp_GetInfoPhieuThuKhac";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuThu";
            valueofParameter[0] = MaPhieuThu;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            clsPhieuThuKhacDTO PhieuThu = ChuyenDoi(table);
            return PhieuThu;
        }

        /// <summary>
        /// chuyển từ một dòng sang đối tượng PhieuThuKhacDTO
        /// </summary>
        /// 

        private clsPhieuThuKhacDTO ChuyenDoi(DataTable table)
        {
            clsPhieuThuKhacDTO PhieuThu = new clsPhieuThuKhacDTO(); 
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
                PhieuThu.DiaChi = Dong["DiaChi"].ToString();
                return PhieuThu;
                
            }
            return null;

        }
        /// <summary>
        /// Thêm thông tin phiếu thu khác
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
        public int Them(clsPhieuThuKhacDTO PhieuThu)
        {
            int i = -1;
            string sql = "sp_InsertPhieuThuKhac";
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
            //ParameterColection[5] = "@KhachHang";
            //valueofParameter[5] = PhieuThu.KhachHang;
            ParameterColection[5] = "@LoaiPhieuThu";
            valueofParameter[5] = "Thu khác";
            ParameterColection[6] = "@MaNhanVien";
            valueofParameter[6] = PhieuThu.NguoiThu;
            ParameterColection[7] = "@DiaChi";
            valueofParameter[7] = PhieuThu.DiaChi;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Sửa thông tin phiếu thu khác
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
        public int Sua(clsPhieuThuKhacDTO PhieuThu)
        {
            int i = -1;
            string sql = "sp_UpdatePhieuThuKhac";
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
            return i;
        }

        /// <summary>
        /// Xóa thông tin phiếu thu bán hàng
        /// </summary>
        /// <param name="MaPhieuChi">Mã phiếu chi</param>
        public int Xoa(string MaPhieuThu)
        {
            int i = -1;
            string sql = "sp_DeletePhieuThuKhac";
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
            string sql = "sp_SearchPhieuThuKhac";
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
            string sql = "sp_SearchPhieuThuKhacTheoKH";
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

        public DataTable GetThuNhapKhac(DateTime TuNgay, DateTime DenNgay)
        {
            string sql = "sp_GetThuNhapKhac";
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
        /// Lấy danh sách các lý do
        /// </summary>
        public DataTable LayBangLyDo()
        {
            string sql = "sp_GetBangLyDo";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@LoaiLyDo";
            valueofParameter[0] = "Thu tiền";
            return  sqlServer.readData(sql, ParameterColection, valueofParameter);
        }

        public DataTable ReportPhieuThuKhac(string MaPhieuThu)
        {
            string sql = "sp_ReportPhieuThuKhac";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuThu";
            valueofParameter[0] = MaPhieuThu;
            DataTable table = sqlServer.readData(sql, "sp_ReportPhieuThuKhac;1", ParameterColection, valueofParameter);

            return table;
        }
    }
}
