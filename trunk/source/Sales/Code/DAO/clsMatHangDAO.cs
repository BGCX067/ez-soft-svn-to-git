using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsMatHangDAO
    {
        #region Attribute
        /// <summary>
        /// Kết nối SQL Server
        /// </summary>
        private CSQLServer sqlServer = new CSQLServer();
        #endregion

        #region Method
        /// <summary>
        /// Lấy danh sách thông tin mặt hàng
        /// </summary>
        public DataTable LayBang()
        {
            string sql = "sp_GetBangMatHang";
            return  sqlServer.readData(sql,"MAT_HANG",true);
        }

        /// <summary>
        /// Lấy danh sách thông tin mặt hàng
        /// </summary>
        public DataTable LayBangHangBan()
        {
            string sql = "sp_GetBangMatHangBan";
            return sqlServer.readData(sql, "MAT_HANG", true);
        }

        /// <summary>
        /// Lấy danh sách thông tin mặt hàng
        /// </summary>
        public DataTable LayBang(string MaLoaiMatHang)
        {
            string sql = "sp_GetBangMatHangTheoMaLoaiMatHang";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaLoaiMatHang";
            valueofParameter[0] = MaLoaiMatHang;
            return  sqlServer.readData(sql, "MAT_HANG", ParameterColection, valueofParameter);
        }


        /// <summary>
        /// Lấy danh sách mặt hàng tồn kho từ ngày, đến ngày theo nhóm hàng
        /// </summary>
        public DataTable ThongKeHangTonKho(DateTime TuNgay, DateTime DenNgay, string MaLoaiHang)
        {
            string sql = "sp_GetBangThongKeHangTonKho";
            string[] ParameterColection = new string[3];
            Object[] valueofParameter = new Object[3];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            ParameterColection[2] = "@MaLoaiMatHang";
            valueofParameter[2] = MaLoaiHang;
            return sqlServer.readData(sql, "MAT_HANG", ParameterColection, valueofParameter);
        }

        /// <summary>
        /// Lấy thông tin report Thống kê hàng tồn kho
        /// </summary>
        /// <param name="MaPhieuXuat"></param>
        /// <returns></returns>
        public DataTable ReportThongKeHangTonKho(DateTime TuNgay, DateTime DenNgay, string MaLoaiHang, string MaMatHang, string TenMatHang)
        {
            string sql = "sp_ReportThongKeHangTonKho";
            string[] ParameterColection = new string[5];
            Object[] valueofParameter = new Object[5];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            ParameterColection[2] = "@MaLoaiMatHang";
            valueofParameter[2] = MaLoaiHang;
            ParameterColection[3] = "@MaMatHang";
            valueofParameter[3] = MaMatHang;
            ParameterColection[4] = "@TenMatHang";
            valueofParameter[4] = TenMatHang;
            return sqlServer.readData(sql, "sp_ReportThongKeHangTonKho;1", ParameterColection, valueofParameter);
        }

        /// <summary>
        /// Lấy Mã mặt hàng kế tiếp để phục vụ cho chức năng Insert
        /// </summary>
        public string LayMaMatHangMoi()
        {
            string sql = "sp_GetNewMaMatHang";
            return sqlServer.readData(sql,"@MaMatHangMoi");
        }

        public DataTable ReportBaoGia(string MaNhomHang, string MaMatHang, string TenMatHang)
        {
            string sql = "sp_ReportBaoGia";
            string[] ParameterColection = new string[3];
            Object[] valueofParameter = new Object[3];
            ParameterColection[0] = "@MaNhomHang";
            valueofParameter[0] = MaNhomHang;
            ParameterColection[1] = "@MaMatHang";
            valueofParameter[1] = MaMatHang;
            ParameterColection[2] = "@TenMatHang";
            valueofParameter[2] = TenMatHang;
            DataTable table = sqlServer.readData(sql, "sp_ReportBaoGia;1", ParameterColection, valueofParameter);

            return table;
        }

        public DataTable ReportMatHangMua(string MaNhomHang, string MaMatHang, string TenMatHang)
        {
            string sql = "sp_ReportMatHangMua";
            string[] ParameterColection = new string[3];
            Object[] valueofParameter = new Object[3];
            ParameterColection[0] = "@MaNhomHang";
            valueofParameter[0] = MaNhomHang;
            ParameterColection[1] = "@MaMatHang";
            valueofParameter[1] = MaMatHang;
            ParameterColection[2] = "@TenMatHang";
            valueofParameter[2] = TenMatHang;
            DataTable table = sqlServer.readData(sql, "sp_ReportMatHangMua;1", ParameterColection, valueofParameter);

            return table;
        }

        /// <summary>
        /// Thêm thông tin mặt hàng
        /// </summary>
        /// <param name="MatHang">
        /// MaMatHang
        /// TenMatHang
        /// </param>
        public int Them(clsMatHangDTO MatHang)
        {
            int i = -1;
            string sql = "sp_InsertMatHang";
            string[] ParameterColection = new string[18];
            Object[] valueofParameter = new Object[18];
            ParameterColection[0] = "@MaMatHang";
            valueofParameter[0] = MatHang.MaMatHang;
            ParameterColection[1] = "@TenMatHang";
            valueofParameter[1] = MatHang.TenMatHang;
            ParameterColection[2] = "@MaLoaiMatHang";
            valueofParameter[2] = MatHang.LoaiMatHang.MaLoaiMatHang;
            ParameterColection[3] = "@DonViTinh";
            valueofParameter[3] = MatHang.DonViTinh;
            ParameterColection[4] = "@DonGia";
            valueofParameter[4] = MatHang.DonGia;
            ParameterColection[5] = "@GiaMua";
            valueofParameter[5] = MatHang.GiaMua;
            ParameterColection[6] = "@GiaBanSi";
            valueofParameter[6] = MatHang.GiaBanSi;
            ParameterColection[7] = "@GiaBanLe";
            valueofParameter[7] = MatHang.GiaBanLe;
            ParameterColection[8] = "@PT_GiaBanSi";
            valueofParameter[8] = MatHang.PT_GiaBanSi;
            ParameterColection[9] = "@PT_GiaBanLe";
            valueofParameter[9] = MatHang.PT_GiaBanLe;
            ParameterColection[10] = "@LuongMin";
            valueofParameter[10] = MatHang.LuongMin;
            ParameterColection[11] = "@LuongMax";
            valueofParameter[11] = MatHang.LuongMax;
            ParameterColection[12] = "@SoLuongTon";
            valueofParameter[12] = MatHang.SoLuongTon;
            ParameterColection[13] = "@ThueVAT";
            valueofParameter[13] = MatHang.ThueVAT;
            ParameterColection[14] = "@XuatXu";
            valueofParameter[14] = MatHang.XuatXu;
            ParameterColection[15] = "@DienGiai";
            valueofParameter[15] = MatHang.DienGiai;
            ParameterColection[16] = "@MaVach";
            valueofParameter[16] = MatHang.MaVach;
            ParameterColection[17] = "@TrangThai";
            valueofParameter[17] = MatHang.TrangThai;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }
      

        /// <summary>
        /// Sửa thông tin mặt hàng
        /// </summary>
        /// <param name="MatHang">Mặt hàng</param>
        public int Sua(clsMatHangDTO MatHang)
        {
            int i = -1;
            string sql = "sp_UpdateMatHang";
            string[] ParameterColection = new string[18];
            Object[] valueofParameter = new Object[18];
            ParameterColection[0] = "@MaMatHang";
            valueofParameter[0] = MatHang.MaMatHang;
            ParameterColection[1] = "@TenMatHang";
            valueofParameter[1] = MatHang.TenMatHang;
            ParameterColection[2] = "@MaLoaiMatHang";
            valueofParameter[2] = MatHang.LoaiMatHang.MaLoaiMatHang;
            ParameterColection[3] = "@DonViTinh";
            valueofParameter[3] = MatHang.DonViTinh;
            ParameterColection[4] = "@DonGia";
            valueofParameter[4] = MatHang.DonGia;
            ParameterColection[5] = "@GiaMua";
            valueofParameter[5] = MatHang.GiaMua;
            ParameterColection[6] = "@GiaBanSi";
            valueofParameter[6] = MatHang.GiaBanSi;
            ParameterColection[7] = "@GiaBanLe";
            valueofParameter[7] = MatHang.GiaBanLe;
            ParameterColection[8] = "@PT_GiaBanSi";
            valueofParameter[8] = MatHang.PT_GiaBanSi;
            ParameterColection[9] = "@PT_GiaBanLe";
            valueofParameter[9] = MatHang.PT_GiaBanLe;
            ParameterColection[10] = "@LuongMin";
            valueofParameter[10] = MatHang.LuongMin;
            ParameterColection[11] = "@LuongMax";
            valueofParameter[11] = MatHang.LuongMax;
            ParameterColection[12] = "@SoLuongTon";
            valueofParameter[12] = MatHang.SoLuongTon;
            ParameterColection[13] = "@ThueVAT";
            valueofParameter[13] = MatHang.ThueVAT;
            ParameterColection[14] = "@XuatXu";
            valueofParameter[14] = MatHang.XuatXu;
            ParameterColection[15] = "@DienGiai";
            valueofParameter[15] = MatHang.DienGiai;
            ParameterColection[16] = "@MaVach";
            valueofParameter[16] = MatHang.MaVach;
            ParameterColection[17] = "@TrangThai";
            valueofParameter[17] = MatHang.TrangThai;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Sửa thông tin mặt hàng
        /// </summary>
        /// <param name="MatHang">Mặt hàng</param>
        public int CapNhatSoLuongTon(string MaMatHang, int SoLuong)
        {
            int i = -1;
            string sql = "sp_UpdateSoLuongTonMatHang";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@MaMatHang";
            valueofParameter[0] = MaMatHang;
            ParameterColection[1] = "@Soluong";
            valueofParameter[1] = SoLuong;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Thiet lap dinh muc
        /// </summary>
        /// <param name="MatHang">Mặt hàng</param>
        public int ThietLapDinhMuc(string MaMatHang, int LuongMin, int LuongMax)
        {
            int i = -1;
            string sql = "sp_UpdateThietLapDinhMucCuaMatHang";
            string[] ParameterColection = new string[3];
            Object[] valueofParameter = new Object[3];
            ParameterColection[0] = "@MaMatHang";
            valueofParameter[0] = MaMatHang;
            ParameterColection[1] = "@LuongMin";
            valueofParameter[1] = LuongMin;
            ParameterColection[2] = "@LuongMax";
            valueofParameter[2] = LuongMax;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Xóa thông tin mặt hàng
        /// </summary>
        /// <param name="MaMatHang">Mã mặt hàng</param>
        public int Xoa(string MaMatHang)
        {
            int i = -1;
            string sql = "sp_DeleteMatHang";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaMatHang";
            valueofParameter[0] = MaMatHang;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Tìm kiếm thông tin mặt hàng
        /// </summary>
        /// <param name="TieuChi">Tiêu chí tìm kiếm</param>
        public string TimKiem(string TieuChi)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
