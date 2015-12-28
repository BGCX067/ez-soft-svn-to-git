using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    class clsPhieuChiHangDAO:clsPhieuChiDAO
    {

        #region Attribute
        private clsChiTietPhieuChiDAO ChihTietPhieuChiDAO = new clsChiTietPhieuChiDAO();
        #endregion
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
        public clsPhieuChiHangDTO LayThongTin(string MaPhieuChi)
        {
            string sql = "sp_GetInfoPhieuChiHang";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuChi";
            valueofParameter[0] = MaPhieuChi;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            clsPhieuChiHangDTO PhieuChi = ChuyenDoi(table);
            return PhieuChi;
        }

        /// <summary>
        /// chuyển từ một dòng sang đối tượng PhieuNhapDTO
        /// </summary>
        private clsPhieuChiHangDTO ChuyenDoi(DataTable table)
        {
            clsPhieuChiHangDTO PhieuChi = new clsPhieuChiHangDTO();
            if (table.Rows.Count == 1)
            {
                DataRow Dong = table.Rows[0];
                PhieuChi.MaPhieuChi = Dong["MaPhieuChi"].ToString();
                PhieuChi.NgayChi = DateTime.Parse(Dong["NgayChi"].ToString());
                PhieuChi.NguoiNhan = Dong["NguoiNhan"].ToString();
                PhieuChi.SoTien = Double.Parse(Dong["SoTien"].ToString());
                PhieuChi.LyDo = Dong["LyDo"].ToString();
                PhieuChi.NhaCungCap =Dong["NhaCungCap"].ToString();
                PhieuChi.TrangThai = int.Parse(Dong["TrangThai"].ToString());
                PhieuChi.NguoiChi = Dong["MaNhanVien"].ToString();
                PhieuChi.DS_ChiTietPhieuChi = ChihTietPhieuChiDAO.LayDanhSach(PhieuChi.MaPhieuChi);
                return PhieuChi;
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
        public int Them(clsPhieuChiHangDTO PhieuChi)
        {
            int i = -1;
            string sql = "sp_InsertPhieuChi";
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
            ParameterColection[5] = "@NhaCungCap";
            valueofParameter[5] = PhieuChi.NhaCungCap;
            ParameterColection[6] = "@LoaiPhieuChi";
            valueofParameter[6] = "Chi hàng";
            ParameterColection[7] = "@MaNhanVien";
            valueofParameter[7] = PhieuChi.NguoiChi;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            if (i != -1)
            {
                for (int k = 0; k < PhieuChi.DS_ChiTietPhieuChi.Count; k++)
                {
                    i = ChihTietPhieuChiDAO.Them(PhieuChi.DS_ChiTietPhieuChi[k]);
                }
            }
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
        public int Sua(clsPhieuChiHangDTO PhieuChi)
        {
            int i = -1;
            string sql = "sp_UpdatePhieuChiHang";
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
            //ParameterColection[5] = "@NhaCungCap";
            //valueofParameter[5] = PhieuChi.NhaCungCap;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            if (i != -1)
            {
                for (int k = 0; k < PhieuChi.DS_ChiTietPhieuChi.Count; k++)
                {
                    i = ChihTietPhieuChiDAO.Them(PhieuChi.DS_ChiTietPhieuChi[k]);
                }
            }
            return i;
        }

        /// <summary>
        /// Xóa thông tin phiếu chi
        /// </summary>
        /// <param name="MaPhieuChi">Mã phiếu chi</param>
        public int Xoa(string MaPhieuChi)
        {
            int i = -1;
            string sql = "sp_DeletePhieuChiHang";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuChi";
            valueofParameter[0] = MaPhieuChi;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Hủy thông tin phiếu chi
        /// </summary>
        /// <param name="MaPhieuChi">Mã phiếu chi</param>
        public int Huy(string MaPhieuChi)
        {
            int i = -1;
            string sql = "sp_RemovePhieuChiHang";
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
            string sql = "sp_SearchPhieuChiHang";
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
            string sql = "sp_SearchPhieuChiHangTheoNCC";
            string[] ParameterColection = new string[3];
            Object[] valueofParameter = new Object[3];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            ParameterColection[2] = "@MaNhaCungCap";
            valueofParameter[2] = NhaCungCap.MaNhaCungCap;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return table;
        }

        public DataTable ReportPhieuChiHang(string MaPhieuChi)
        {
            string sql = "sp_ReportPhieuChiHang";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuChi";
            valueofParameter[0] = MaPhieuChi;
            DataTable table = sqlServer.readData(sql, "sp_ReportPhieuChiHang;1", ParameterColection, valueofParameter);
            return table;
        }

        public DataTable ReportDSPhieuChi(DateTime TuNgay, DateTime DenNgay, string MaPhieuChi, string MaNhaCungCap)
        {
            string sql = "sp_ReportDSPhieuChi";
            string[] ParameterColection = new string[4];
            Object[] valueofParameter = new Object[4];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            ParameterColection[2] = "@MaPhieuChi";
            valueofParameter[2] = MaPhieuChi;
            ParameterColection[3] = "@MaNhaCungCap";
            valueofParameter[3] = MaNhaCungCap;
            DataTable table = sqlServer.readData(sql, "sp_ReportDSPhieuChi;1", ParameterColection, valueofParameter);
            return table;
        }
    }
}
