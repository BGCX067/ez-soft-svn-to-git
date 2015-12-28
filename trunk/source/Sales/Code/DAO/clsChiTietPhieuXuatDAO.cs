using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsChiTietPhieuXuatDAO
    {
        #region Attribute
        /// <summary>
        /// Kết nối SQL Server
        /// </summary>
        private CSQLServer sqlServer = new CSQLServer();
        #endregion

        /// <summary>
        /// Lấy danh sách chi tiết Phiếu xuất hàng vào kho theo mã phiếu xuất
        /// </summary>
        /// <param name="MaPhieuNhap">Mã phiếu nhập</param>
        public DataTable LayBang(string MaPhieuXuat)
        {
            string sql = "sp_GetBangChiTietPhieuXuat";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuXuat";
            valueofParameter[0] = MaPhieuXuat;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return table;
        }

        /// <summary>
        /// Lấy danh sách chi tiết  Phiếu xuất bán lẻ theo mã phiếu xuất
        /// </summary>
        /// <param name="MaPhieuNhap">Mã phiếu xuất</param>
        public List<clsChiTietPhieuXuatDTO> LayDanhSach(string MaPhieuXuat)
        {
            string sql = "sp_GetBangChiTietPhieuXuat";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuXuat";
            valueofParameter[0] = MaPhieuXuat;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return ChuyenDoi(table);
        }

        /// <summary>
        /// Lấy danh sách chi tiết  Phiếu xuất bán lẻ theo mã phiếu xuất
        /// </summary>
        /// <param name="MaPhieuNhap">Mã phiếu xuất</param>
        public List<clsChiTietPhieuXuatDTO> LayDanhSachTheoPhieuNhap(string MaPhieuXuat)
        {
            string sql = "sp_GetBangChiTietPhieuXuatTheoPhieuNhap";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuXuat";
            valueofParameter[0] = MaPhieuXuat;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return ChuyenDoiTheoPhieuNhap(table);
        }

        /// <summary>
        /// chuyển từ một dòng sang đối tượng PhieuXuatDTO
        /// </summary>
        private List<clsChiTietPhieuXuatDTO> ChuyenDoi(DataTable table)
        {
            List<clsChiTietPhieuXuatDTO> DanhSach = new List<clsChiTietPhieuXuatDTO>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                clsChiTietPhieuXuatDTO CT_PhieuXuat = new clsChiTietPhieuXuatDTO();
                CT_PhieuXuat.MaPhieuXuat = table.Rows[i]["MaPhieuXuat"].ToString();
                CT_PhieuXuat.MatHang.MaMatHang = table.Rows[i]["MaMatHang"].ToString();
                //CT_PhieuXuat.MaPhieuNhap = table.Rows[i]["MaPhieuNhap"].ToString();
                CT_PhieuXuat.MatHang.TenMatHang = table.Rows[i]["TenMatHang"].ToString();
                CT_PhieuXuat.MatHang.DonViTinh = table.Rows[i]["DonViTinh"].ToString();
                CT_PhieuXuat.SoLuong = int.Parse(table.Rows[i]["SoLuong"].ToString());
                CT_PhieuXuat.DonGia = Double.Parse(table.Rows[i]["DonGia"].ToString());
                CT_PhieuXuat.ChietKhau = Double.Parse(table.Rows[i]["ChietKhau"].ToString());
                CT_PhieuXuat.ThanhTien = Double.Parse(table.Rows[i]["ThanhTien"].ToString());
                CT_PhieuXuat.ThueVAT = Double.Parse(table.Rows[i]["ThueVAT"].ToString());
                DanhSach.Add(CT_PhieuXuat);
            }
            return DanhSach;
        }

        /// <summary>
        /// chuyển từ một dòng sang đối tượng PhieuXuatDTO
        /// </summary>
        private List<clsChiTietPhieuXuatDTO> ChuyenDoiTheoPhieuNhap(DataTable table)
        {
            List<clsChiTietPhieuXuatDTO> DanhSach = new List<clsChiTietPhieuXuatDTO>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                clsChiTietPhieuXuatDTO CT_PhieuXuat = new clsChiTietPhieuXuatDTO();
                CT_PhieuXuat.MaPhieuXuat = table.Rows[i]["MaPhieuXuat"].ToString();
                CT_PhieuXuat.MatHang.MaMatHang = table.Rows[i]["MaMatHang"].ToString();
                CT_PhieuXuat.MaPhieuNhap = table.Rows[i]["MaPhieuNhap"].ToString();
                CT_PhieuXuat.MatHang.TenMatHang = table.Rows[i]["TenMatHang"].ToString();
                CT_PhieuXuat.MatHang.DonViTinh = table.Rows[i]["DonViTinh"].ToString();
                CT_PhieuXuat.SoLuong = int.Parse(table.Rows[i]["SoLuong"].ToString());
                CT_PhieuXuat.DonGia = Double.Parse(table.Rows[i]["DonGia"].ToString());
                CT_PhieuXuat.ChietKhau = Double.Parse(table.Rows[i]["ChietKhau"].ToString());
                CT_PhieuXuat.ThanhTien = Double.Parse(table.Rows[i]["ThanhTien"].ToString());
                CT_PhieuXuat.ThueVAT = Double.Parse(table.Rows[i]["ThueVAT"].ToString());
                DanhSach.Add(CT_PhieuXuat);
            }
            return DanhSach;
        }

        /// <summary>
        /// Thêm thông tin chi tiết phiếu xuất 
        /// </summary>
        /// <param name="ChiTietPhieuXuất">
        /// MaPhieuXuat  nvarchar(10)
        /// MaMatHang  nvarchar(10)
        /// SoLuong  int
        /// DonGia  float
        /// ChietKhau  float
        /// ThanhTien  float
        /// ThueVAT  float
        /// </param>
        public int Them(clsChiTietPhieuXuatDTO ChiTietPhieuXuat)
        {
            int i = -1;
            string sql = "sp_InsertChiTietPhieuXuat";
            string[] ParameterColection = new string[8];
            Object[] valueofParameter = new Object[8];
            ParameterColection[0] = "@MaPhieuXuat";
            valueofParameter[0] = ChiTietPhieuXuat.MaPhieuXuat;
            ParameterColection[1] = "@MaMatHang";
            valueofParameter[1] = ChiTietPhieuXuat.MatHang.MaMatHang;
            ParameterColection[2] = "@MaPhieuNhap";
            valueofParameter[2] = ChiTietPhieuXuat.MaPhieuNhap;
            ParameterColection[3] = "@SoLuong";
            valueofParameter[3] = ChiTietPhieuXuat.SoLuong;
            ParameterColection[4] = "@DonGia";
            valueofParameter[4] = ChiTietPhieuXuat.DonGia;
            ParameterColection[5] = "@ChietKhau";
            valueofParameter[5] = ChiTietPhieuXuat.ChietKhau;
            ParameterColection[6] = "@ThanhTien";
            valueofParameter[6] = ChiTietPhieuXuat.ThanhTien;
            ParameterColection[7] = "@ThueVAT";
            valueofParameter[7] = ChiTietPhieuXuat.ThueVAT;
            
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Sửa thông tin chi tiếtphiếu xuất hàng theo mã mặt hàng và mã sản phẩm
        /// Chú ý: không cho sửa sản phẩm
        /// </summary>
        /// <param name="ChiTietPhieuXuat">
        /// MaPhieuXuat  nvarchar(10)
        /// MaMatHang  nvarchar(10)
        /// SoLuong  int
        /// DonGia  float
        /// ChietKhau  float
        /// ThanhTien  float
        /// ThueVAT  float
        /// </param>
        public int Sua(clsChiTietPhieuXuatDTO ChiTietPhieuXuat)
        {
            int i = -1;
            string sql = "sp_UpdateChiTietPhieuXuat";
            string[] ParameterColection = new string[8];
            Object[] valueofParameter = new Object[8];
            ParameterColection[0] = "@MaPhieuXuat";
            valueofParameter[0] = ChiTietPhieuXuat.MaPhieuXuat;
            ParameterColection[1] = "@MaMatHang";
            valueofParameter[1] = ChiTietPhieuXuat.MatHang.MaMatHang;
            ParameterColection[2] = "@MaPhieuNhap";
            valueofParameter[2] = ChiTietPhieuXuat.MaPhieuNhap;
            ParameterColection[3] = "@SoLuong";
            valueofParameter[3] = ChiTietPhieuXuat.SoLuong;
            ParameterColection[4] = "@DonGia";
            valueofParameter[4] = ChiTietPhieuXuat.DonGia;
            ParameterColection[5] = "@ChietKhau";
            valueofParameter[5] = ChiTietPhieuXuat.ChietKhau;
            ParameterColection[6] = "@ThanhTien";
            valueofParameter[6] = ChiTietPhieuXuat.ThanhTien;
            ParameterColection[7] = "@ThueVAT";
            valueofParameter[7] = ChiTietPhieuXuat.ThueVAT;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Sửa thông tin chi tiếtphiếu xuất hàng theo mã mặt hàng và mã sản phẩm
        /// Chú ý: cho phép sửa tất cả các trường từ MaPhieuXuat
        /// </summary>
        /// <param name="ChiTietPhieuXuat">
        /// MaPhieuXuat  nvarchar(10)
        /// MaMatHang  nvarchar(10)
        /// SoLuong  int
        /// DonGia  float
        /// ChietKhau  float
        /// ThanhTien  float
        /// ThueVAT  float
        /// </param>
        public int Sua(clsChiTietPhieuXuatDTO ChiTietPhieuXuat, string MaMatHangMoi, string MaPhieuNhapMoi)
        {
            int i = -1;
            string sql = "sp_UpdateChiTietPhieuXuat";
            string[] ParameterColection = new string[10];
            Object[] valueofParameter = new Object[10];
            ParameterColection[0] = "@MaPhieuXuat";
            valueofParameter[0] = ChiTietPhieuXuat.MaPhieuXuat;
            ParameterColection[1] = "@MaMatHang";
            valueofParameter[1] = ChiTietPhieuXuat.MatHang.MaMatHang;
            ParameterColection[2] = "@MaPhieuNhap";
            valueofParameter[2] = ChiTietPhieuXuat.MaPhieuNhap;
            ParameterColection[3] = "@SoLuong";
            valueofParameter[3] = ChiTietPhieuXuat.SoLuong;
            ParameterColection[4] = "@DonGia";
            valueofParameter[4] = ChiTietPhieuXuat.DonGia;
            ParameterColection[5] = "@ChietKhau";
            valueofParameter[5] = ChiTietPhieuXuat.ChietKhau;
            ParameterColection[6] = "@ThanhTien";
            valueofParameter[6] = ChiTietPhieuXuat.ThanhTien;
            ParameterColection[7] = "@ThueVAT";
            valueofParameter[7] = ChiTietPhieuXuat.ThueVAT;
            ParameterColection[8] = "@MaMatHangMoi";
            valueofParameter[8] = MaMatHangMoi;
            ParameterColection[9] = "@MaPhieuNhapMoi";
            valueofParameter[9] = MaPhieuNhapMoi;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Xóa thông tin phiếu xuất
        /// </summary>
        /// <param name="MaChiTietPhieuXuat">MaChiTietPhieuXuat   string</param>
        public int Xoa(string MaPhieuXuat, string MaMatHang, string MaPhieuNhap)
        {
            int i = -1;
            string sql = "sp_DeleteChiTietPhieuXuat";
            string[] ParameterColection = new string[3];
            Object[] valueofParameter = new Object[3];
            ParameterColection[0] = "@MaPhieuXuat";
            valueofParameter[0] = MaPhieuXuat;
            ParameterColection[1] = "@MaMatHang";
            valueofParameter[1] = MaMatHang;
            ParameterColection[2] = "@MaPhieuNhap";
            valueofParameter[2] = MaPhieuNhap;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Xóa tất cả các chi tiết phiếu xuất theo mã phiếu xuất
        /// </summary>
        /// <param name="MaPhieuXuat">Mã phiếu xuất</param>
        public int XoaTatCa(string MaPhieuXuat)
        {
            int i = -1;
            string sql = "sp_DeleteAllChiTietPhieuXuat";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuXuat";
            valueofParameter[0] = MaPhieuXuat;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        } 

    }
}
