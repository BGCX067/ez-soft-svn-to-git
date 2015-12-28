using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsPhieuNhapDAO
    {
        #region Attribute
        /// <summary>
        /// Kết nối SQL Server
        /// </summary>
        private CSQLServer sqlServer = new CSQLServer();
        private clsChiTietPhieuNhapDAO ChihTietPhieuNhapDAO = new clsChiTietPhieuNhapDAO();
        #endregion

        /// <summary>
        /// Lấy Mã phiếu nhập kế tiếp để phục vụ cho chức năng Insert
        /// </summary>
        public string LayMaPhieuNhapMoi()
        {
            string sql = "sp_GetNewMaPhieuNhap";
            return sqlServer.readData(sql, "@MaPhieuNhapMoi");
        }

        /// <summary>
        /// Lấy danh sách thông tin chi tiết  Phiếu nhập hàng vào kho theo từ ngày đến ngày
        /// </summary>
        /// <param name="TuNgay">Từ ngày</param>
        /// <param name="DenNgay">Đến ngày</param>
        /// <returns></returns>
        public DataTable LayBang(DateTime TuNgay, DateTime DenNgay)
        {
            string sql = "sp_GetBangThongTinChiTietCacPhieuNhap";
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
        /// Lấy thông tin phiếu nhập
        /// </summary>
        /// <param name="PhieuNhap">
        /// MaPhieuNhap  nvarchar(10)
        /// NgayNhap  smalldatetime
        /// MaNhaCungCap  nvarchar(50)
        /// TongTien  float
        /// ConNo  float
        /// TragThai  int
        /// </param>
        public clsPhieuNhapDTO LayThongTin(string MaPhieuNhap)
        {
            string sql = "sp_GetInfoPhieuNhap";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuNhap";
            valueofParameter[0] = MaPhieuNhap;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return ChuyenDoi(table);
        }

        /// <summary>
        /// chuyển từ một dòng sang đối tượng PhieuNhapDTO
        /// </summary>
        private clsPhieuNhapDTO ChuyenDoi(DataTable table)
        {
            clsPhieuNhapDTO PhieuNhap = new clsPhieuNhapDTO();
            if (table.Rows.Count == 1)
            {
                DataRow Dong = table.Rows[0];
                PhieuNhap.MaPhieuNhap = Dong["MaPhieuNhap"].ToString();
                PhieuNhap.NgayNhap = DateTime.Parse(Dong["NgayNhap"].ToString());
                PhieuNhap.NhaCungCap.MaNhaCungCap = Dong["MaNhaCungCap"].ToString();
                PhieuNhap.NhaCungCap.TenNhaCungCap = Dong["TenNhaCungCap"].ToString();
                PhieuNhap.TongTien = Double.Parse(Dong["TongTien"].ToString());
                PhieuNhap.ConNo = Double.Parse(Dong["ConNo"].ToString());
                PhieuNhap.TrangThai = int.Parse(Dong["TrangThai"].ToString());
                PhieuNhap.NguoiNhap = Dong["MaNhanVien"].ToString();
                PhieuNhap.DS_ChiTietPhieuNhap = ChihTietPhieuNhapDAO.LayDanhSach(PhieuNhap.MaPhieuNhap);
            }
            else
            {
                return null;
            }
            return PhieuNhap;

        }

        /// <summary>
        /// Thêm thông tin phiếu nhập
        /// </summary>
        /// <param name="PhieuNhap">
        /// MaPhieuNhap  nvarchar(10)
        /// NgayNhap  smalldatetime
        /// MaNhaCungCap  nvarchar(50)
        /// TongTien  float
        /// ConNo  float
        /// TragThai  int
        /// </param>
        public int Them(clsPhieuNhapDTO PhieuNhap)
        {
            int i = -1;
            string sql = "sp_InsertPhieuNhap";
            string[] ParameterColection = new string[7];
            Object[] valueofParameter = new Object[7];
            ParameterColection[0] = "@MaPhieuNhap";
            valueofParameter[0] = PhieuNhap.MaPhieuNhap;
            ParameterColection[1] = "@NgayNhap";
            valueofParameter[1] = PhieuNhap.NgayNhap;
            ParameterColection[2] = "@MaNhaCungCap";
            valueofParameter[2] = PhieuNhap.NhaCungCap.MaNhaCungCap;
            ParameterColection[3] = "@TongTien";
            valueofParameter[3] = PhieuNhap.TongTien;
            ParameterColection[4] = "@ConNo";
            valueofParameter[4] = PhieuNhap.ConNo;
            ParameterColection[5] = "@TrangThai";
            valueofParameter[5] = PhieuNhap.TrangThai;
            ParameterColection[6] = "@MaNhanVien";
            valueofParameter[6] = PhieuNhap.NguoiNhap;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            if (i != -1)
            {
                for (int k = 0; k < PhieuNhap.DS_ChiTietPhieuNhap.Count; k++)
                {
                    i = ChihTietPhieuNhapDAO.Them(PhieuNhap.DS_ChiTietPhieuNhap[k]);
                }
            }
            return i;
        }

        /// <summary>
        /// Sửa thông tin phiếu nhập hàng
        /// </summary>
        /// <param name="PhieuNhap">
        /// MaPhieuNhap  nvarchar(10)
        /// NgayNhap  smalldatetime
        /// MaNhaCungCap  nvarchar(50)
        /// TongTien  float
        /// ConNo  float
        /// TragThai  int
        /// </param>
        public int Sua(clsPhieuNhapDTO PhieuNhap)
        {
            int i = -1;
            string sql = "sp_UpdatePhieuNhap";
            string[] ParameterColection = new string[5];
            Object[] valueofParameter = new Object[5];
            ParameterColection[0] = "@MaPhieuNhap";
            valueofParameter[0] = PhieuNhap.MaPhieuNhap;
            ParameterColection[1] = "@NgayNhap";
            valueofParameter[1] = PhieuNhap.NgayNhap;
            ParameterColection[2] = "@MaNhaCungCap";
            valueofParameter[2] = PhieuNhap.NhaCungCap.MaNhaCungCap.ToString();
            ParameterColection[3] = "@TongTien";
            valueofParameter[3] = PhieuNhap.TongTien;
            ParameterColection[4] = "@ConNo";
            valueofParameter[4] = PhieuNhap.ConNo;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            //Thêm mới từng chi tiết phiếu nhập
            for (int k = 0; k < PhieuNhap.DS_ChiTietPhieuNhap.Count; k++)
            {
                i = ChihTietPhieuNhapDAO.Them(PhieuNhap.DS_ChiTietPhieuNhap[k]);
            }
            return i;
        }

        /// <summary>
        /// Sửa thông tin phiếu nhập hàng
        /// </summary>
        /// <param name="PhieuNhap">
        /// MaPhieuNhap  nvarchar(10)
        /// NgayNhap  smalldatetime
        /// MaNhaCungCap  nvarchar(50)
        /// TongTien  float
        /// ConNo  float
        /// TragThai  int
        /// </param>
        public int CapNhatTienConNo(string MaPhieuNhap, double TienConNo )
        {
            int i = -1;
            string sql = "sp_UpdateTienConNoTrongPhieuNhap";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@MaPhieuNhap";
            valueofParameter[0] = MaPhieuNhap;
            ParameterColection[1] = "@ConNo";
            valueofParameter[1] = TienConNo;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Xóa thông tin phiếu nhập
        /// </summary>
        /// <param name="MaPhieuNhap">MaPhieuNhap   string</param>
        public int Xoa(string MaPhieuNhap)
        {
            int i = -1;
            string sql = "sp_DeletePhieuNhap";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuNhap";
            valueofParameter[0] = MaPhieuNhap;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Hủy thông tin phiếu nhập
        /// </summary>
        /// <param name="MaPhieuNhap">MaPhieuNhap   string</param>
        public int Huy(string MaPhieuNhap)
        {
            int i = -1;
            string sql = "sp_RemovePhieuNhap";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuNhap";
            valueofParameter[0] = MaPhieuNhap;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Tìm kiếm thông tin phiếu nhập từ ngày đến ngày với theo nhà cung cấp
        /// </summary>
        /// <param name="TieuChi">Tiêu chí tìm kiếm</param>
        public DataTable TimKiem(DateTime TuNgay, DateTime DenNgay, clsNhaCungCapDTO NhaCungCap)
        {
            string sql = "sp_SearchPhieuNhapTheoNCC";
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

        public DataTable LayBangConNo(clsNhaCungCapDTO NhaCungCap)
        {
            string sql = "sp_GetTablePhieuNhapConNoTheoNCC";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaNhaCungCap";
            valueofParameter[0] = NhaCungCap.MaNhaCungCap;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return table;
        }
        //dung cho khach hang co no dau ky
        //public DataTable LayBangConNoVaNoDauKy(clsNhaCungCapDTO NhaCungCap)
        //{
        //    string sql = "sp_GetTablePhieuNhapConNoVaNoDauKyTheoNCC";
        //    string[] ParameterColection = new string[1];
        //    Object[] valueofParameter = new Object[1];
        //    ParameterColection[0] = "@MaNhaCungCap";
        //    valueofParameter[0] = NhaCungCap.MaNhaCungCap;
        //    DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
        //    return table;
        //}
        /// <summary>
        /// Tìm kiếm thông tin phiếu nhập từ ngày đến ngày với tất cả các nhà cung cấp
        /// </summary>
        /// <param name="TieuChi">Tiêu chí tìm kiếm</param>
        public DataTable TimKiem(DateTime TuNgay, DateTime DenNgay)
        {
            string sql = "sp_SearchPhieuNhap";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return table;
        }

        public DataTable CongNoNhaCungCap(DateTime TuNgay, DateTime DenNgay)
        {
            string sql = "sp_CongNoNhaCungCap";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return table;
        }

        public DataTable CongNoNhaCungCapTheoNCC(DateTime TuNgay, DateTime DenNgay, clsNhaCungCapDTO NhaCungCap)
        {
            string sql = "sp_CongNoNhaCungCapTheoNCC";
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

        public List<clsChiTietPhieuNhapDTO> LayDanhSachChiTietPhieuNhapTheoMatHang(string MaMatHang)
        {
            return ChihTietPhieuNhapDAO.LayDanhSachTheoMatHang(MaMatHang);
        }

        public DataTable ReportPhieuNhapHang(string MaPhieuNhap)
        {
            string sql = "sp_ReportPhieuNhapHang";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuNhap";
            valueofParameter[0] = MaPhieuNhap;
            DataTable table = sqlServer.readData(sql, "sp_ReportPhieuNhapHang;1", ParameterColection, valueofParameter);
            return table;
        }

        public DataTable ReportCT_PhieuNhapHang(string MaPhieuNhap)
        {
            string sql = "sp_ReportCT_PhieuNhapHang";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuNhap";
            valueofParameter[0] = MaPhieuNhap;
            DataTable table = sqlServer.readData(sql, "sp_ReportCT_PhieuNhapHang;1", ParameterColection, valueofParameter);
            return table;
        }

        public DataTable ReportDonHangDaMua(DateTime TuNgay, DateTime DenNgay, string MaNhaCungCap)
        {
            string sql = "sp_ReportDonHangDaMua";
            string[] ParameterColection = new string[3];
            Object[] valueofParameter = new Object[3];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            ParameterColection[2] = "@MaNhaCungCap";
            valueofParameter[2] = MaNhaCungCap;

            DataTable table = sqlServer.readData(sql, "sp_ReportDonHangDaMua;1", ParameterColection, valueofParameter);
            return table;
        }

        public DataTable ReportCongNoNhaCungCap(DateTime DenNgay, string MaNhaCungCap)
        {
            string sql = "sp_ReportCongNoNhaCungCap";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            
            ParameterColection[0] = "@DenNgay";
            valueofParameter[0] = DenNgay;
            ParameterColection[1] = "@MaNhaCungCap";
            valueofParameter[1] = MaNhaCungCap;

            DataTable table = sqlServer.readData(sql, "sp_ReportCongNoNhaCungCap;1", ParameterColection, valueofParameter);
            return table;
        }

        public DataTable ReportChiTietHangNhap(DateTime TuNgay, DateTime DenNgay, string MaMatHang, string MaNhaCungCap)
        {
            string sql = "sp_ReportChiTietHangNhap";
            string[] ParameterColection = new string[4];
            Object[] valueofParameter = new Object[4];

            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            ParameterColection[2] = "@MaMatHang";
            valueofParameter[2] = MaMatHang;
            ParameterColection[3] = "@MaNhaCungCap";
            valueofParameter[3] = MaNhaCungCap;

            DataTable table = sqlServer.readData(sql, "sp_ReportChiTietHangNhap;1", ParameterColection, valueofParameter);
            return table;
        }

    }
}
