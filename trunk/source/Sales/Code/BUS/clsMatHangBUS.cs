using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsMatHangBUS
    {
        /// <summary>
        /// Đối tượng mặt hàng DAO
        /// </summary>
        private clsMatHangDAO MatHangDAO = new clsMatHangDAO();

        /// <summary>
        /// Lấy danh sách mặt hàng
        /// </summary>
        public DataTable LayBang()
        {
            return MatHangDAO.LayBang();
        }

        /// <summary>
        /// Lấy danh sách mặt hàng
        /// </summary>
        public DataTable LayBangHangBan()
        {
            return MatHangDAO.LayBangHangBan();
        }

        /// <summary>
        /// Lấy danh sách mặt hàng
        /// </summary>
        public DataTable LayBang(string MaLoaiMatHang)
        {
            return MatHangDAO.LayBang(MaLoaiMatHang);
        }

        /// <summary>
        /// Lấy mã mặt hàng mới phục vụ cho chức năng  insert Mặt hàng
        /// </summary>
        public string LayMaMatHangMoi()
        {
           return MatHangDAO.LayMaMatHangMoi();
        }

        public DataTable ReportBaoGia(string MaNhomHang, string MaMatHang, string TenMatHang)
        {
            return MatHangDAO.ReportBaoGia(MaNhomHang, MaMatHang, TenMatHang);
        }

        public DataTable ReportMatHangMua(string MaNhomHang, string MaMatHang, string TenMatHang)
        {
            return MatHangDAO.ReportMatHangMua(MaNhomHang, MaMatHang, TenMatHang);
        }

        /// <summary>
        /// Thêm thông tin mặt hàng
        /// </summary>
        /// <param name="LoaiMatHang">
        /// MaLoaiMatHang   nvarchar(10)
        /// TenLoaiMatHang  nvarchar(255)
        /// DienGiai   ntext
        /// NgayTao  smalldatetime
        /// TrangThai int
        /// </param>
        public int Them(clsMatHangDTO MatHang)
        {
            return MatHangDAO.Them(MatHang);
        }

        /// <summary>
        /// Sửa thông tin  mặt hàng
        /// </summary>
        /// <param name="LoaiMatHang">
        /// MaLoaiMatHang  nvarchar(10)
        /// TenLoaiMatHang  nvarchar(255)
        /// DienGiai  ntext
        /// </param>
        public int Sua(clsMatHangDTO MatHang)
        {
            return MatHangDAO.Sua(MatHang);
        }

        /// <summary>
        /// Xóa thông tin mặt hàng.
        /// </summary>
        /// <param name="MaLoaiMatHang">Mã loại mặt hàng</param>
        public int Xoa(string MaNhanVien)
        {
            return MatHangDAO.Xoa(MaNhanVien);
        }
        /// <summary>
        /// Thiet lap dinh muc
        /// </summary>
        /// <param name="MaMatHang"></param>
        /// <param name="LuongMin"></param>
        /// <param name="LuongMax"></param>
        /// <returns></returns>
        public int ThietLapDinhMuc(string MaMatHang, int LuongMin, int LuongMax)
        {
            return MatHangDAO.ThietLapDinhMuc(MaMatHang, LuongMin, LuongMax);
        }

        public int ThietLapDinhMucDanhSachMatHang(List< clsMatHangDTO> DS_MatHang)
        {
            int kq = 1;
            for (int i = 0; i < DS_MatHang.Count; i++)
            {
                kq = MatHangDAO.ThietLapDinhMuc(DS_MatHang[i].MaMatHang, DS_MatHang[i].LuongMin, DS_MatHang[i].LuongMax);
                if (kq == -1)
                {
                    return -1;
                }
            }
            return 1;
        }

     
        /// <summary>
        /// Lấy danh sách mặt hàng tồn kho từ ngày, đến ngày theo nhóm hàng
        /// </summary>
        public DataTable ThongKeHangTonKho(DateTime TuNgay, DateTime DenNgay, string MaLoaiHang)
        {
            return MatHangDAO.ThongKeHangTonKho(TuNgay, DenNgay, MaLoaiHang);
        }

        /// <summary>
        /// Lấy thông tin report Thống kê hàng tồn kho
        /// </summary>
        /// <param name="MaPhieuXuat"></param>
        /// <returns></returns>
        public DataTable ReportThongKeHangTonKho(DateTime TuNgay, DateTime DenNgay, string MaLoaiHang,string MaMatHang,string TenMatHang)
        {
            return MatHangDAO.ReportThongKeHangTonKho(TuNgay, DenNgay, MaLoaiHang, MaMatHang, TenMatHang);
        }

    }
}
