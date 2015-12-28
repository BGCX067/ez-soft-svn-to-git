using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsPhieuXuatBanSiDAO : clsPhieuXuatDAO
    {
        /// <summary>
        /// Lấy thông tin phiếu xuất bán sỉ
        /// </summary>
        /// <param name="PhieuThu">
        /// MaPhieuXuat                  nvarchar(10)
        /// NgayXuat                       smalldatetime
        /// MaNhanVienBanHang     nvarchar(10)
        /// TongTien                        float
        /// DaTra                             float
        /// KhachHang                     nvarchar(255)
        /// LoaiPhieuXuat                 nvarchar(100)
        /// TrangThai                       int
        /// </param>
        public clsPhieuXuatBanSiDTO LayThongTin(string MaPhieuXuat)
        {
            string sql = "sp_GetInfoPhieuXuatBanSi";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuXuat";
            valueofParameter[0] = MaPhieuXuat;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            clsPhieuXuatBanSiDTO PhieuXuat = ChuyenDoi(table);
            return PhieuXuat;
        }

        /// <summary>
        /// Lấy thông tin phiếu xuất bán sỉ
        /// </summary>
        /// <param name="PhieuThu">
        /// MaPhieuXuat                  nvarchar(10)
        /// NgayXuat                       smalldatetime
        /// MaNhanVienBanHang     nvarchar(10)
        /// TongTien                        float
        /// DaTra                             float
        /// KhachHang                     nvarchar(255)
        /// LoaiPhieuXuat                 nvarchar(100)
        /// TrangThai                       int
        /// </param>
        public clsPhieuXuatBanSiDTO LayThongTinTheoPhieuNhap(string MaPhieuXuat)
        {
            string sql = "sp_GetInfoPhieuXuatBanSi";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuXuat";
            valueofParameter[0] = MaPhieuXuat;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            clsPhieuXuatBanSiDTO PhieuXuat = ChuyenDoiTheoPhieuNhap(table);
            return PhieuXuat;
        }

        /// <summary>
        /// chuyển từ một dòng sang đối tượng PhieuXuatBanSiDTO
        /// </summary>
        private clsPhieuXuatBanSiDTO ChuyenDoi(DataTable table)
        {
            clsPhieuXuatBanSiDTO PhieuXuat = new clsPhieuXuatBanSiDTO();
            if (table.Rows.Count == 1)
            {
                DataRow Dong = table.Rows[0];
                PhieuXuat.MaPhieuXuat = Dong["MaPhieuXuat"].ToString();
                PhieuXuat.NgayXuat = DateTime.Parse(Dong["NgayXuat"].ToString());
                PhieuXuat.NhanVien.MaNhanVien = Dong["MaNhanVienBanHang"].ToString();
                PhieuXuat.TongTien = Double.Parse(Dong["TongTien"].ToString());
                PhieuXuat.DaTra = Double.Parse(Dong["DaTra"].ToString());
                //PhieuXuat.KhachHang = Dong["KhachBanLe"].ToString();
                PhieuXuat.KhachHang.MaKhachHang = Dong["MaKhachHang"].ToString();
                PhieuXuat.KhachHang.TenKhachHang = Dong["TenKhachhang"].ToString();
                PhieuXuat.TrangThai = int.Parse(Dong["TrangThai"].ToString());
                //Đọc chi tiết Phiếu Xuất Bán Sỉ
                PhieuXuat.DS_ChiTietPhieuXuat = ChiTietPhieuXuatDAO.LayDanhSach(PhieuXuat.MaPhieuXuat);
                return PhieuXuat;

            }
            return null;
        }

        /// <summary>
        /// chuyển từ một dòng sang đối tượng PhieuXuatBanSiDTO
        /// </summary>
        private clsPhieuXuatBanSiDTO ChuyenDoiTheoPhieuNhap(DataTable table)
        {
            clsPhieuXuatBanSiDTO PhieuXuat = new clsPhieuXuatBanSiDTO();
            if (table.Rows.Count == 1)
            {
                DataRow Dong = table.Rows[0];
                PhieuXuat.MaPhieuXuat = Dong["MaPhieuXuat"].ToString();
                PhieuXuat.NgayXuat = DateTime.Parse(Dong["NgayXuat"].ToString());
                PhieuXuat.NhanVien.MaNhanVien = Dong["MaNhanVienBanHang"].ToString();
                PhieuXuat.TongTien = Double.Parse(Dong["TongTien"].ToString());
                PhieuXuat.DaTra = Double.Parse(Dong["DaTra"].ToString());
                //PhieuXuat.KhachHang = Dong["KhachBanLe"].ToString();
                PhieuXuat.KhachHang.MaKhachHang = Dong["MaKhachHang"].ToString();
                PhieuXuat.KhachHang.TenKhachHang = Dong["TenKhachhang"].ToString();
                PhieuXuat.TrangThai = int.Parse(Dong["TrangThai"].ToString());
                //Đọc chi tiết Phiếu Xuất Bán Sỉ
                PhieuXuat.DS_ChiTietPhieuXuat = ChiTietPhieuXuatDAO.LayDanhSachTheoPhieuNhap(PhieuXuat.MaPhieuXuat);
                return PhieuXuat;

            }
            return null;
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
        public override DataTable TimKiem(DateTime TuNgay, DateTime DenNgay)
        {
            string sql = "sp_SearchPhieuXuatBanSi";
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
        public override DataTable TimKiem(DateTime TuNgay, DateTime DenNgay, clsKhachHangDTO KhachHang)
        {
            string sql = "sp_SearchPhieuXuatBanSiTheoKH";
            string[] ParameterColection = new string[3];
            Object[] valueofParameter = new Object[3];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            ParameterColection[2] = "@KhachHang";
            valueofParameter[2] = KhachHang.MaKhachHang;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return table;
        }

        public  DataTable CongNoKhachHang(DateTime TuNgay, DateTime DenNgay)
        {
            string sql = "sp_CongNoKhachHang";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return table;
        }

        public  DataTable CongNoKhachHangTheoKH(DateTime TuNgay, DateTime DenNgay, clsKhachHangDTO KhachHang)
        {
            string sql = "sp_CongNoKhachHangTheoKH";
            string[] ParameterColection = new string[3];
            Object[] valueofParameter = new Object[3];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            ParameterColection[2] = "@KhachHang";
            valueofParameter[2] = KhachHang.MaKhachHang;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return table;
        }

        public DataTable ReportHoaDonBanSi(string MaPhieuXuat)
        {
            string sql = "sp_ReportHoaDonBanSi";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuXuat";
            valueofParameter[0] = MaPhieuXuat;
            DataTable table = sqlServer.readData(sql, "sp_ReportHoaDonBanSi;1", ParameterColection, valueofParameter);

            return table;
        }

        public DataTable ReportCTHoaDonBanSi(string MaPhieuXuat)
        {
            string sql = "sp_ReportCTHoaDonBanSi";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuXuat";
            valueofParameter[0] = MaPhieuXuat;
            DataTable table = sqlServer.readData(sql, "sp_ReportCTHoaDonBanSi;1", ParameterColection, valueofParameter);

            return table;
        }

        public DataTable ReportCongNoKhachHang(DateTime DenNgay, string KhachHang)
        {
            string sql = "sp_ReportCongNoKhachHang";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
              ParameterColection[0] = "@DenNgay";
            valueofParameter[0] = DenNgay;
            ParameterColection[1] = "@KhachHang";
            valueofParameter[1] = KhachHang;
            DataTable table = sqlServer.readData(sql, "sp_ReportCongNoKhachHang;1", ParameterColection, valueofParameter);
            return table;
        }

      /// <summary>
        /// Thêm thông tin Phiếu Xuất Bán Sỉ
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
        public int Them(clsPhieuXuatBanSiDTO PhieuXuatBanSi)
        {
            int i = -1;
            string sql = "sp_InsertPhieuXuatBanSi";
            string[] ParameterColection = new string[7];
            Object[] valueofParameter = new Object[7];
            ParameterColection[0] = "@MaPhieuXuat";
            valueofParameter[0] = PhieuXuatBanSi.MaPhieuXuat;
            ParameterColection[1] = "@NgayXuat";
            valueofParameter[1] = PhieuXuatBanSi.NgayXuat;
            ParameterColection[2] = "@MaNhanVienBanHang";
            valueofParameter[2] = PhieuXuatBanSi.NhanVien.MaNhanVien.ToString();
            ParameterColection[3] = "@TongTien";
            valueofParameter[3] = PhieuXuatBanSi.TongTien;
            ParameterColection[4] = "@DaTra";
            valueofParameter[4] = PhieuXuatBanSi.DaTra;
            ParameterColection[5] = "@MaKhachHang";
            valueofParameter[5] = PhieuXuatBanSi.KhachHang.MaKhachHang;
            ParameterColection[6] = "@LoaiPhieuXuat";
            valueofParameter[6] = "Xuất bán sỉ";
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            if (i != -1)
            {
                for (int k = 0; k < PhieuXuatBanSi.DS_ChiTietPhieuXuat.Count; k++)
                {
                    i = ChiTietPhieuXuatDAO.Them(PhieuXuatBanSi.DS_ChiTietPhieuXuat[k]);
                }
            }
            return i;
        }
        /// <summary>
        /// Sửa thông tin phiếu xuất bán Sỉ
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
        public int Sua(clsPhieuXuatBanSiDTO PhieuXuatBanSi)
        {
            int i = -1;
            string sql = "sp_UpdatePhieuXuatBanSi";
            string[] ParameterColection = new string[6];
            Object[] valueofParameter = new Object[6];
            ParameterColection[0] = "@MaPhieuXuat";
            valueofParameter[0] = PhieuXuatBanSi.MaPhieuXuat;
            ParameterColection[1] = "@NgayXuat";
            valueofParameter[1] = PhieuXuatBanSi.NgayXuat;
            ParameterColection[2] = "@MaNhanVienBanHang";
            valueofParameter[2] = PhieuXuatBanSi.NhanVien.MaNhanVien.ToString();
            ParameterColection[3] = "@TongTien";
            valueofParameter[3] = PhieuXuatBanSi.TongTien;
            ParameterColection[4] = "@DaTra";
            valueofParameter[4] = PhieuXuatBanSi.DaTra;
            ParameterColection[5] = "@KhachHang";
            valueofParameter[5] = PhieuXuatBanSi.KhachHang.MaKhachHang;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            //Thêm mới từng chi tiết phiếu xuất
            if (i != -1)
            {
                for (int k = 0; k < PhieuXuatBanSi.DS_ChiTietPhieuXuat.Count; k++)
                {
                    if (i != -1)
                    {
                        i = ChiTietPhieuXuatDAO.Them(PhieuXuatBanSi.DS_ChiTietPhieuXuat[k]);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return i;
        }
        /// <summary>
        /// Xóa thông tin phiếu xuất bán sỉ
        /// </summary>
        /// <param name="MaPhieuXuat">MaPhieuXuat   string</param>
        public int Xoa(string MaPhieuXuat)
        {
            int i = -1;
            string sql = "sp_DeletePhieuXuatBanSi";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuXuat";
            valueofParameter[0] = MaPhieuXuat;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

    }
}
