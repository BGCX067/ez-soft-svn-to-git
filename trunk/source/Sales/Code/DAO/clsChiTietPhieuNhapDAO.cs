using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsChiTietPhieuNhapDAO
    {
        #region Attribute
        /// <summary>
        /// Kết nối SQL Server
        /// </summary>
        private CSQLServer sqlServer = new CSQLServer();
        clsMatHangDAO MatHangDAO = new clsMatHangDAO();
        #endregion

        /// <summary>
        /// Lấy danh sách chi tiết  Phiếu nhập hàng vào kho theo mã phiếu nhập
        /// </summary>
        /// <param name="MaPhieuNhap">Mã phiếu nhập</param>
        public DataTable LayBang(string MaPhieuNhap)
        {
            string sql = "sp_GetBangChiTietPhieuNhap";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuNhap";
            valueofParameter[0] = MaPhieuNhap;
            DataTable table = sqlServer.readData(sql,ParameterColection,valueofParameter);
            return table;
        }

        /// <summary>
        /// Lấy danh sách chi tiết  Phiếu nhập hàng vào kho theo mã mặt hàng 
        /// </summary>
        /// <param name="MaPhieuNhap">Mã phiếu nhập</param>
        public List<clsChiTietPhieuNhapDTO> LayDanhSachTheoMatHang(string MaMatHang)
        {
            string sql = "sp_GetBangChiTietPhieuNhapTheoMatHang";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaMatHang";
            valueofParameter[0] = MaMatHang;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return ChuyenDoi(table);
        }

        /// <summary>
        /// Lấy danh sách chi tiết  Phiếu nhập hàng vào kho theo mã phiếu nhập
        /// </summary>
        /// <param name="MaPhieuNhap">Mã phiếu nhập</param>
        public List<clsChiTietPhieuNhapDTO> LayDanhSach(string MaPhieuNhap)
        {
            string sql = "sp_GetBangChiTietPhieuNhap";
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
        private  List<clsChiTietPhieuNhapDTO> ChuyenDoi(DataTable table)
        {
            List<clsChiTietPhieuNhapDTO> DanhSach = new List<clsChiTietPhieuNhapDTO>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                clsChiTietPhieuNhapDTO CT_PhieuNhap = new clsChiTietPhieuNhapDTO();
                CT_PhieuNhap.MaPhieuNhap = table.Rows[i]["MaPhieuNhap"].ToString();
                CT_PhieuNhap.MatHang.MaMatHang = table.Rows[i]["MaMatHang"].ToString();
                CT_PhieuNhap.MatHang.TenMatHang = table.Rows[i]["TenMatHang"].ToString();
                CT_PhieuNhap.MatHang.DonViTinh = table.Rows[i]["DonViTinh"].ToString();
                CT_PhieuNhap.SoLuongTon = int.Parse(table.Rows[i]["SoLuongTon"].ToString());
                CT_PhieuNhap.SoLuong = int.Parse(table.Rows[i]["SoLuong"].ToString());
                CT_PhieuNhap.DonGia = Double.Parse(table.Rows[i]["DonGia"].ToString());
                CT_PhieuNhap.ChietKhau = Double.Parse(table.Rows[i]["ChietKhau"].ToString());
                CT_PhieuNhap.ThanhTien = Double.Parse(table.Rows[i]["ThanhTien"].ToString());
                CT_PhieuNhap.ThueVAT = Double.Parse(table.Rows[i]["ThueVAT"].ToString());
                DanhSach.Add(CT_PhieuNhap);
            }
            return DanhSach;
        }

        /// <summary>
        /// Thêm thông tin chi tiết phiếu nhập 
        /// </summary>
        /// <param name="ChiTietPhieuNhap">
        /// MaPhieuNhap  nvarchar(10)
        /// MaMatHang  nvarchar(10)
        /// SoLuong  int
        /// DonGia  float
        /// ChietKhau  float
        /// ThanhTien  float
        /// ThueVAT  float
        /// </param>
        public int Them(clsChiTietPhieuNhapDTO ChiTietPhieuNhap)
        {
            int i = -1;
            string sql = "sp_InsertChiTietPhieuNhap";
            string[] ParameterColection = new string[7];
            Object[] valueofParameter = new Object[7];
            ParameterColection[0] = "@MaPhieuNhap";
            valueofParameter[0] = ChiTietPhieuNhap.MaPhieuNhap;
            ParameterColection[1] = "@MaMatHang";
            valueofParameter[1] = ChiTietPhieuNhap.MatHang.MaMatHang;
            ParameterColection[2] = "@SoLuong";
            valueofParameter[2] = ChiTietPhieuNhap.SoLuong;
            ParameterColection[3] = "@DonGia";
            valueofParameter[3] = ChiTietPhieuNhap.DonGia;
            ParameterColection[4] = "@ChietKhau";
            valueofParameter[4] = ChiTietPhieuNhap.ChietKhau;
            ParameterColection[5] = "@ThanhTien";
            valueofParameter[5] = ChiTietPhieuNhap.ThanhTien;
            ParameterColection[6] = "@ThueVAT";
            valueofParameter[6] = ChiTietPhieuNhap.ThueVAT;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Sửa thông tin chi tiếtphiếu nhập hàng theo mã mặt hàng và mã sản phẩm
        /// Chú ý: không cho sửa sản phẩm
        /// </summary>
        /// <param name="ChiTietPhieuNhap">
        /// MaPhieuNhap  nvarchar(10)
        /// MaMatHang  nvarchar(10)
        /// SoLuong  int
        /// DonGia  float
        /// ChietKhau  float
        /// ThanhTien  float
        /// ThueVAT  float
        /// </param>
        public int Sua(clsChiTietPhieuNhapDTO ChiTietPhieuNhap)
        {
            int i = -1;
            string sql = "sp_UpdateChiTietPhieuNhap";
            string[] ParameterColection = new string[7];
            Object[] valueofParameter = new Object[7];
            ParameterColection[0] = "@MaPhieuNhap";
            valueofParameter[0] = ChiTietPhieuNhap.MaPhieuNhap;
            ParameterColection[1] = "@MaMatHang";
            valueofParameter[1] = ChiTietPhieuNhap.MatHang.MaMatHang;
            ParameterColection[2] = "@SoLuong";
            valueofParameter[2] = ChiTietPhieuNhap.SoLuong;
            ParameterColection[3] = "@DonGia";
            valueofParameter[3] = ChiTietPhieuNhap.DonGia;
            ParameterColection[4] = "@ChietKhau";
            valueofParameter[4] = ChiTietPhieuNhap.ChietKhau;
            ParameterColection[5] = "@ThanhTien";
            valueofParameter[5] = ChiTietPhieuNhap.ThanhTien;
            ParameterColection[6] = "@ThueVAT";
            valueofParameter[6] = ChiTietPhieuNhap.ThueVAT;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Sửa thông tin chi tiếtphiếu nhập hàng theo mã mặt hàng và mã sản phẩm
        /// Chú ý: cho phép sửa tất cả các trường từ MaPhieuXuat
        /// </summary>
        /// <param name="ChiTietPhieuNhap">
        /// MaPhieuNhap  nvarchar(10)
        /// MaMatHang  nvarchar(10)
        /// SoLuong  int
        /// DonGia  float
        /// ChietKhau  float
        /// ThanhTien  float
        /// ThueVAT  float
        /// </param>
        public int Sua(clsChiTietPhieuNhapDTO ChiTietPhieuNhap, string MaMatHangMoi)
        {
            int i = -1;
            string sql = "sp_UpdateChiTietPhieuNhap";
            string[] ParameterColection = new string[8];
            Object[] valueofParameter = new Object[8];
            ParameterColection[0] = "@MaPhieuNhap";
            valueofParameter[0] = ChiTietPhieuNhap.MaPhieuNhap;
            ParameterColection[1] = "@MaMatHang";
            valueofParameter[1] = ChiTietPhieuNhap.MatHang.MaMatHang;
            ParameterColection[2] = "@SoLuong";
            valueofParameter[2] = ChiTietPhieuNhap.SoLuong;
            ParameterColection[3] = "@DonGia";
            valueofParameter[3] = ChiTietPhieuNhap.DonGia;
            ParameterColection[4] = "@ChietKhau";
            valueofParameter[4] = ChiTietPhieuNhap.ChietKhau;
            ParameterColection[5] = "@ThanhTien";
            valueofParameter[5] = ChiTietPhieuNhap.ThanhTien;
            ParameterColection[6] = "@ThueVAT";
            valueofParameter[6] = ChiTietPhieuNhap.ThueVAT;
            ParameterColection[7] = "@MaMatHangMoi";
            valueofParameter[7] = MaMatHangMoi;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Xóa thông tin phiếu nhập
        /// </summary>
        /// <param name="MaChiTietPhieuNhap">MaChiTietPhieuNhap   string</param>
        public int Xoa(string MaPhieuNhap, string MaMatHang)
        {
            int i = -1;
            string sql = "sp_DeleteChiTietPhieuNhap";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@MaPhieuNhap";
            valueofParameter[0] = MaPhieuNhap;
            ParameterColection[1] = "@MaMatHang";
            valueofParameter[1] = MaMatHang;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// phục hồi số lượng các mặt hàng theo mã phiếu nhập
        /// </summary>
        /// <param name="MaphieuNhap">Mã phiếu nhập</param>
        public int PhucHoiSoLuongCacMatHang(string MaPhieuNhap)
        {
            List<clsChiTietPhieuNhapDTO> DSCTPN = LayDanhSach(MaPhieuNhap);
            for (int i = 0; i < DSCTPN.Count; i++)
            {
                if (PhucHoiSoLuongMatHang(DSCTPN[i]) == -1)
                {
                    return -1;
                }
            }
            return 1;
        }

        public int PhucHoiSoLuongMatHang(clsChiTietPhieuNhapDTO CTPN)
        {
            //Dấu - chỉ là phục hồi lại số lượng mặt hàng trước khi xóa phiếu nhập
            return MatHangDAO.CapNhatSoLuongTon(CTPN.MatHang.MaMatHang, - CTPN.SoLuong);
        } 

        /// <summary>
        /// Xóa tất cả các chi tiết phiếu nhập theo mã phiếu nhập
        /// </summary>
        /// <param name="MaphieuNhap">Mã phiếu nhập</param>
        public int XoaTatCa(string MaPhieuNhap)
        {
            int i = -1;
            string sql = "sp_DeleteAllChiTietPhieuNhap";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuNhap";
            valueofParameter[0] = MaPhieuNhap;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        } 

       
    }

    
}
