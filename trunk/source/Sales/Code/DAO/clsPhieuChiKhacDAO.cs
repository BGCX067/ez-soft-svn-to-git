using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsPhieuChiKhacDAO : clsPhieuChiDAO
    {

        /// <summary>
        /// Lấy thông tin phiếu chi hàng
        /// </summary>
        /// <param name="PhieuChi">
        /// MaPhieuChi   nvarchar(10)
        /// NgayChi   smalldatetime
        /// NguoiNhan   nvarchar(255)
        /// SoTien   float
        /// LyDo  nvarchar(255)
        /// NhaCungCap  nvarchar(255)
        /// TrangThai  int
        /// </param>
        public clsPhieuChiKhacDTO LayThongTin(string MaPhieuChi)
        {
            string sql = "sp_GetInfoPhieuChiKhac";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuChi";
            valueofParameter[0] = MaPhieuChi;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            clsPhieuChiKhacDTO PhieuChi = ChuyenDoi(table);
            return PhieuChi;
        }

        /// <summary>
        /// chuyển từ một dòng sang đối tượng PhieuNhapDTO
        /// </summary>
        private clsPhieuChiKhacDTO ChuyenDoi(DataTable table)
        {
            clsPhieuChiKhacDTO PhieuChi = new clsPhieuChiKhacDTO();
            if (table.Rows.Count == 1)
            {
                DataRow Dong = table.Rows[0];
                PhieuChi.MaPhieuChi = Dong["MaPhieuChi"].ToString();
                PhieuChi.NgayChi = DateTime.Parse(Dong["NgayChi"].ToString());
                PhieuChi.NguoiNhan = Dong["NguoiNhan"].ToString();
                PhieuChi.SoTien = Double.Parse(Dong["SoTien"].ToString());
                PhieuChi.LyDo = Dong["LyDo"].ToString();
                PhieuChi.TrangThai = int.Parse(Dong["TrangThai"].ToString());
                PhieuChi.NguoiChi = Dong["MaNhanVien"].ToString();
                PhieuChi.DiaChi = Dong["DiaChi"].ToString();
                return PhieuChi;
                //Chưa đọc chi tiết phiếu chi hàng
            }
            return null;

        }
        /// <summary>
        /// Thêm thông tin phiếu chi
        /// </summary>
        /// <param name="PhieuChi">
        /// MaPhieuChi   nvarchar(10)
        /// NgayChi   smalldatetime
        /// NguoiNhan   nvarchar(255)
        /// SoTien   float
        /// LyDo  nvarchar(255)
        /// NhaCungCap  nvarchar(255)
        /// TrangThai  int
        /// </param>
        public int Them(clsPhieuChiKhacDTO PhieuChi)
        {
            int i = -1;
            string sql = "sp_InsertPhieuChiKhac";
            string[] ParameterColection = new string[8];
            Object[] valueofParameter = new Object[8];
            ParameterColection[0] = "@MaPhieuChi";
            valueofParameter[0] = PhieuChi.MaPhieuChi;
            ParameterColection[1] = "@NgayChi";
            valueofParameter[1] = PhieuChi.NgayChi;
            ParameterColection[2] = "@NguoiNhan";
            valueofParameter[2] = PhieuChi.NguoiNhan;
            ParameterColection[3] = "@SoTien";
            valueofParameter[3] = PhieuChi.SoTien;
            ParameterColection[4] = "@LyDo";
            valueofParameter[4] = PhieuChi.LyDo;
            ParameterColection[5] = "@LoaiPhieuChi";
            valueofParameter[5] = "Chi khác";
            ParameterColection[6] = "@MaNhanVien";
            valueofParameter[6] = PhieuChi.NguoiChi;
            ParameterColection[7] = "@DiaChi";
            valueofParameter[7] = PhieuChi.DiaChi;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Sửa thông tin phiếu chi
        /// </summary>
        /// <param name="PhieuChi">
        /// MaPhieuChi   nvarchar(10)
        /// NgayChi   smalldatetime
        /// NguoiNhan   nvarchar(255)
        /// SoTien   float
        /// LyDo  nvarchar(255)
        /// NhaCungCap  nvarchar(255)
        /// TrangThai  int
        /// </param>
        public int Sua(clsPhieuChiKhacDTO PhieuChi)
        {
            int i = -1;
            string sql = "sp_UpdatePhieuChiKhac";
            string[] ParameterColection = new string[5];
            Object[] valueofParameter = new Object[5];
            ParameterColection[0] = "@MaPhieuChi";
            valueofParameter[0] = PhieuChi.MaPhieuChi;
            ParameterColection[1] = "@NgayChi";
            valueofParameter[1] = PhieuChi.NgayChi;
            ParameterColection[2] = "@NguoiNhan";
            valueofParameter[2] = PhieuChi.NguoiNhan;
            ParameterColection[3] = "@SoTien";
            valueofParameter[3] = PhieuChi.SoTien;
            ParameterColection[4] = "@LyDo";
            valueofParameter[4] = PhieuChi.LyDo;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Xóa thông tin phiếu chi
        /// </summary>
        /// <param name="MaPhieuChi">Mã phiếu chi</param>
        public int Xoa(string MaPhieuChi)
        {
            int i = -1;
            string sql = "sp_DeletePhieuChiKhac";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuChi";
            valueofParameter[0] = MaPhieuChi;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Tìm kiếm thông tin phiếu chi
        /// </summary>
        /// <param name="TuNgay">Ngày bắt đầu</param>
        /// <param name="DenNgay">Ngày kết thúc</param>
        public DataTable TimKiem(DateTime TuNgay, DateTime DenNgay)
        {
            string sql = "sp_SearchPhieuChiKhac";
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
        /// Tìm kiếm thông tin phiếu chi
        /// </summary>
        /// <param name="TuNgay">Ngày bắt đầu</param>
        /// <param name="DenNgay">Ngày kết thúc</param>
        public DataTable TimKiem(DateTime TuNgay, DateTime DenNgay, clsNhaCungCapDTO NhaCungCap)
        {
            string sql = "sp_SearchPhieuChiKhacTheoNCC";
            string[] ParameterColection = new string[3];
            Object[] valueofParameter = new Object[3];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            ParameterColection[2] = "@NhaCungCap";
            valueofParameter[2] = NhaCungCap.TenNhaCungCap;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return table;
        }

        public DataTable GetChiPhiKhac(DateTime TuNgay, DateTime DenNgay)
        {
            string sql = "sp_GetChiPhiKhac";
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
            valueofParameter[0] = "Trả tiền";
            return sqlServer.readData(sql, ParameterColection, valueofParameter);
        }

        public DataTable ReportPhieuChiKhac(string MaPhieuChi)
        {
            string sql = "sp_ReportPhieuChiKhac";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuChi";
            valueofParameter[0] = MaPhieuChi;
            DataTable table = sqlServer.readData(sql, "sp_ReportPhieuChiKhac;1", ParameterColection, valueofParameter);
            return table;
        }
    }
}
