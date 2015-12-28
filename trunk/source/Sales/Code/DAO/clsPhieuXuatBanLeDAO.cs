using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsPhieuXuatBanLeDAO : clsPhieuXuatDAO
    {
        /// <summary>
        /// Lấy thông tin phiếu xuất bán lẻ
        /// </summary>
        /// <param name="PhieuXuat">
        /// MaPhieuXuat                  nvarchar(10)
        /// NgayXuat                       smalldatetime
        /// MaNhanVienBanHang     nvarchar(10)
        /// TongTien                        float
        /// DaTra                             float
        /// KhachHang                     nvarchar(255)
        /// LoaiPhieuXuat                 nvarchar(100)
        /// TrangThai                       int
        /// </param>
        public clsPhieuXuatBanLeDTO LayThongTin(string MaPhieuXuat)
        {
            string sql = "sp_GetInfoPhieuXuatBanLe";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuXuat";
            valueofParameter[0] = MaPhieuXuat;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            clsPhieuXuatBanLeDTO PhieuXuat = ChuyenDoi(table);
            return PhieuXuat;
        }

        public clsPhieuXuatBanLeDTO LayThongTinTheoPhieuNhap(string MaPhieuXuat)
        {
            string sql = "sp_GetInfoPhieuXuatBanLe";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuXuat";
            valueofParameter[0] = MaPhieuXuat;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            clsPhieuXuatBanLeDTO PhieuXuat = ChuyenDoiTheoPhieuNhap(table);
            return PhieuXuat;
        }

        public DataTable ReportCTHoaDonBanLe(string MaPhieuXuat)
        {
            string sql = "sp_ReportCTHoaDonBanLe";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuXuat";
            valueofParameter[0] = MaPhieuXuat;
            DataTable table = sqlServer.readData(sql,"sp_ReportCTHoaDonBanLe;1",ParameterColection, valueofParameter);

            return table;
        }
        public DataTable ReportHoaDonBanLe(string MaPhieuXuat)
        {
            string sql = "sp_ReportHoaDonBanLe";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuXuat";
            valueofParameter[0] = MaPhieuXuat;
            DataTable table = sqlServer.readData(sql, "sp_ReportHoaDonBanLe;1", ParameterColection, valueofParameter);

            return table;
        }
        /// <summary>
        /// Tìm kiếm thông tin phiếu xuất từ ngày đến ngày với tất cả các khách hàng
        /// </summary>
        /// <param name="TuNgay">
        /// MaPhieuXuat                  nvarchar(10)
        /// NgayXuat                       smalldatetime
        /// MaNhanVienBanHang     nvarchar(10)
        /// TongTien                        float
        /// DaTra                             float
        /// KhachHang                     nvarchar(255)
        /// LoaiPhieuXuat                 nvarchar(100)
        /// TrangThai                       int
        /// </param>
        //public DataTable TimKiem(DateTime TuNgay, DateTime DenNgay)
        //{
        //    string sql = "sp_SearchPhieuXuatBanLe";
        //    string[] ParameterColection = new string[2];
        //    Object[] valueofParameter = new Object[2];
        //    ParameterColection[0] = "@TuNgay";
        //    valueofParameter[0] = TuNgay;
        //    ParameterColection[1] = "@DenNgay";
        //    valueofParameter[1] = DenNgay;
        //    DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
        //    return table;
        //}
        /// <summary>
        /// Tìm kiếm thông tin phiếu xuất từ ngày đến ngày với theo khách hàng
        /// </summary>
        /// <param name="TuNgay">
        /// MaPhieuXuat                  nvarchar(10)
        /// NgayXuat                       smalldatetime
        /// MaNhanVienBanHang     nvarchar(10)
        /// TongTien                        float
        /// DaTra                             float
        /// KhachHang                     nvarchar(255)
        /// LoaiPhieuXuat                 nvarchar(100)
        /// TrangThai                       int
        /// </param>
        //public DataTable TimKiem(DateTime TuNgay, DateTime DenNgay, clsKhachHangDTO KhachHang)
        //{
        //    string sql = "sp_SearchPhieuXuatBanLeTheoKH";
        //    string[] ParameterColection = new string[3];
        //    Object[] valueofParameter = new Object[3];
        //    ParameterColection[0] = "@TuNgay";
        //    valueofParameter[0] = TuNgay;
        //    ParameterColection[1] = "@DenNgay";
        //    valueofParameter[1] = DenNgay;
        //    ParameterColection[2] = "@MaKhachHang";
        //    valueofParameter[2] = KhachHang.MaKhachHang;
        //    DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
        //    return table;
        //}
        /// <summary>
        /// chuyển từ một dòng sang đối tượng PhieuXuatBanLeDTO
        /// </summary>
        private clsPhieuXuatBanLeDTO ChuyenDoi(DataTable table)
        {
            clsPhieuXuatBanLeDTO PhieuXuat = new clsPhieuXuatBanLeDTO();
            if (table.Rows.Count == 1)
            {
                DataRow Dong = table.Rows[0];
                PhieuXuat.MaPhieuXuat = Dong["MaPhieuXuat"].ToString();
                PhieuXuat.NgayXuat = DateTime.Parse(Dong["NgayXuat"].ToString());
                PhieuXuat.NhanVien.MaNhanVien = Dong["MaNhanVienBanHang"].ToString();
                //PhieuXuat.NhanVien.TenNhanVien = Dong["TenNhanVien"].ToString();
                PhieuXuat.TongTien = Double.Parse(Dong["TongTien"].ToString());
                PhieuXuat.DaTra = Double.Parse(Dong["DaTra"].ToString());
                PhieuXuat.KhachHang = Dong["KhachBanLe"].ToString();
                PhieuXuat.TrangThai = int.Parse(Dong["TrangThai"].ToString());
                //Đọc chi tiết Phiếu Xuất Bán Lẻ
                PhieuXuat.DS_ChiTietPhieuXuat = ChiTietPhieuXuatDAO.LayDanhSach(PhieuXuat.MaPhieuXuat);
                return PhieuXuat;

            }
            return null;
        }

        /// <summary>
        /// chuyển từ một dòng sang đối tượng PhieuXuatBanLeDTO
        /// </summary>
        private clsPhieuXuatBanLeDTO ChuyenDoiTheoPhieuNhap(DataTable table)
        {
            clsPhieuXuatBanLeDTO PhieuXuat = new clsPhieuXuatBanLeDTO();
            if (table.Rows.Count == 1)
            {
                DataRow Dong = table.Rows[0];
                PhieuXuat.MaPhieuXuat = Dong["MaPhieuXuat"].ToString();
                PhieuXuat.NgayXuat = DateTime.Parse(Dong["NgayXuat"].ToString());
                PhieuXuat.NhanVien.MaNhanVien = Dong["MaNhanVienBanHang"].ToString();
                //PhieuXuat.NhanVien.TenNhanVien = Dong["TenNhanVien"].ToString();
                PhieuXuat.TongTien = Double.Parse(Dong["TongTien"].ToString());
                PhieuXuat.DaTra = Double.Parse(Dong["DaTra"].ToString());
                PhieuXuat.KhachHang = Dong["KhachBanLe"].ToString();
                PhieuXuat.TrangThai = int.Parse(Dong["TrangThai"].ToString());
                //Đọc chi tiết Phiếu Xuất Bán Lẻ
                PhieuXuat.DS_ChiTietPhieuXuat = ChiTietPhieuXuatDAO.LayDanhSachTheoPhieuNhap(PhieuXuat.MaPhieuXuat);
                return PhieuXuat;

            }
            return null;
        }
        /// <summary>
        /// Thêm thông tin Phiếu Xuất Bán Lẻ
        /// </summary>
        /// <param name="PhieuXuat">
        /// MaPhieuXuat                  nvarchar(10)
        /// NgayXuat                       smalldatetime
        /// MaNhanVienBanHang     nvarchar(10)
        /// TongTien                        float
        /// DaTra                             float
        /// KhachHang                     nvarchar(255)
        /// LoaiPhieuXuat                 nvarchar(100)
        /// TrangThai                       int
        /// </param>
        public int Them(clsPhieuXuatBanLeDTO PhieuXuatBanLe)
        {
            int i = -1;
            string sql = "sp_InsertPhieuXuatBanLe";
            string[] ParameterColection = new string[7];
            Object[] valueofParameter = new Object[7];
            ParameterColection[0] = "@MaPhieuXuat";
            valueofParameter[0] = PhieuXuatBanLe.MaPhieuXuat;
            ParameterColection[1] = "@NgayXuat";
            valueofParameter[1] = PhieuXuatBanLe.NgayXuat;
            ParameterColection[2] = "@MaNhanVienBanHang";
            valueofParameter[2] = PhieuXuatBanLe.NhanVien.MaNhanVien.ToString();
            ParameterColection[3] = "@TongTien";
            valueofParameter[3] = PhieuXuatBanLe.TongTien;
            ParameterColection[4] = "@DaTra";
            valueofParameter[4] = PhieuXuatBanLe.DaTra;
            ParameterColection[5] = "@LoaiPhieuXuat";
            valueofParameter[5] = "Xuất bán lẻ";
            ParameterColection[6] = "@KhachBanLe";
            valueofParameter[6] = PhieuXuatBanLe.KhachHang;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            if (i != -1)
            {
                for (int k = 0; k < PhieuXuatBanLe.DS_ChiTietPhieuXuat.Count; k++)
                {
                    i = ChiTietPhieuXuatDAO.Them(PhieuXuatBanLe.DS_ChiTietPhieuXuat[k]);
                }
            }
            return i;
        }
        /// <summary>
        /// Sửa thông tin phiếu xuất bán lẻ
        /// </summary>
        /// <param name="PhieuXuat">
        /// MaPhieuXuat                  nvarchar(10)
        /// NgayXuat                       smalldatetime
        /// MaNhanVienBanHang     nvarchar(10)
        /// TongTien                        float
        /// DaTra                             float
        /// KhachHang                     nvarchar(255)
        /// LoaiPhieuXuat                 nvarchar(100)
        /// TrangThai                       int
        /// </param>
        public int Sua(clsPhieuXuatBanLeDTO PhieuXuatBanLe)
        {
            int i = -1;
            string sql = "sp_UpdatePhieuXuatBanLe";
            string[] ParameterColection = new string[6];
            Object[] valueofParameter = new Object[6];
            ParameterColection[0] = "@MaPhieuXuat";
            valueofParameter[0] = PhieuXuatBanLe.MaPhieuXuat;
            ParameterColection[1] = "@NgayXuat";
            valueofParameter[1] = PhieuXuatBanLe.NgayXuat;
            ParameterColection[2] = "@MaNhanVienBanHang";
            valueofParameter[2] = PhieuXuatBanLe.NhanVien.MaNhanVien.ToString();
            ParameterColection[3] = "@TongTien";
            valueofParameter[3] = PhieuXuatBanLe.TongTien;
            ParameterColection[4] = "@DaTra";
            valueofParameter[4] = PhieuXuatBanLe.DaTra;
            ParameterColection[5] = "@KhachBanLe";
            valueofParameter[5] = PhieuXuatBanLe.KhachHang;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            if (i != -1)
            {
                for (int k = 0; k < PhieuXuatBanLe.DS_ChiTietPhieuXuat.Count; k++)
                {
                    i = ChiTietPhieuXuatDAO.Them(PhieuXuatBanLe.DS_ChiTietPhieuXuat[k]);
                }
            }
            return i;
        }
        /// <summary>
        /// Xóa thông tin phiếu xuất bán lẻ
        /// </summary>
        /// <param name="MaPhieuXuat">MaPhieuXuat   string</param>
        public int Xoa(string MaPhieuXuat)
        {
            int i = -1;
            string sql = "sp_DeletePhieuXuatBanLe";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuXuat";
            valueofParameter[0] = MaPhieuXuat;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

    }    
}
