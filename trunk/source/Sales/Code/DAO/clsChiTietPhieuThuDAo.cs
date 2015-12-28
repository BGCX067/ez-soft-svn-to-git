using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsChiTietPhieuThuDAO
    {
        #region Attribute
        /// <summary>
        /// Kết nối SQL Server
        /// </summary>
        private CSQLServer sqlServer = new CSQLServer();
        #endregion

        /// <summary>
        /// Lấy danh sách chi tiết  Phiếu xuất theo mã phiếu thu
        /// </summary>
        /// <param name="MaPhieuThu">Mã phiếu thu</param>
        public DataTable LayBang(string MaPhieuThu)
        {
            string sql = "sp_GetBangChiTietPhieuThu";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuThu";
            valueofParameter[0] = MaPhieuThu;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return table;
        }

        /// <summary>
        /// Lấy danh sách chi tiết phiếu thu bán hàng theo mã phiếu thu
        /// </summary>
        /// <param name="MaPhieuThu">Mã phiếu chi</param>
        public List<clsChiTietPhieuThuDTO> LayDanhSach(string MaPhieuThu)
        {
            string sql = "sp_GetBangChiTietPhieuThu";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuThu";
            valueofParameter[0] = MaPhieuThu;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return ChuyenDoi(table, MaPhieuThu);
        }

        /// <summary>
        /// chuyển từ một dòng sang đối tượng PhieuThuDTO
        /// </summary>
        private List<clsChiTietPhieuThuDTO> ChuyenDoi(DataTable table, string MaPhieuThu)
        {
            List<clsChiTietPhieuThuDTO> DanhSach = new List<clsChiTietPhieuThuDTO>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                clsChiTietPhieuThuDTO CT_PhieuThu = new clsChiTietPhieuThuDTO();
                CT_PhieuThu.MaPhieuThu = MaPhieuThu;
                CT_PhieuThu.SoTien = Double.Parse(table.Rows[i]["SoTien"].ToString());
                CT_PhieuThu.PhieuXuat.MaPhieuXuat = table.Rows[i]["MaPhieuXuat"].ToString();
                CT_PhieuThu.PhieuXuat.NgayXuat = DateTime.Parse(table.Rows[i]["NgayXuat"].ToString());
                CT_PhieuThu.PhieuXuat.KhachHang.MaKhachHang = table.Rows[i]["MaKhachHang"].ToString();
                CT_PhieuThu.PhieuXuat.TongTien = Double.Parse(table.Rows[i]["TongTien"].ToString());
                CT_PhieuThu.PhieuXuat.DaTra = Double.Parse(table.Rows[i]["DaTra"].ToString());
                CT_PhieuThu.PhieuXuat.TrangThai = int.Parse(table.Rows[i]["TrangThai"].ToString());
                DanhSach.Add(CT_PhieuThu);
            }
            return DanhSach;
        }

        /// <summary>
        /// Thêm thông tin chi tiết phiếu thu
        /// </summary>
        /// <param name="ChiTietPhieuThu">
        /// MaPhieuThu  nvarchar(10)
        /// MaPhieuXuat  nvarchar(10)
        /// SoTien  int
        /// </param>
        public int Them(clsChiTietPhieuThuDTO ChiTietPhieuThu)
        {
            int i = -1;
            string sql = "sp_InsertChiTietPhieuThu";
            string[] ParameterColection = new string[3];
            Object[] valueofParameter = new Object[3];
            ParameterColection[0] = "@MaPhieuThu";
            valueofParameter[0] = ChiTietPhieuThu.MaPhieuThu;
            ParameterColection[1] = "@MaPhieuXuat";
            valueofParameter[1] = ChiTietPhieuThu.PhieuXuat.MaPhieuXuat;
            ParameterColection[2] = "@SoTien";
            valueofParameter[2] = ChiTietPhieuThu.SoTien;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Sửa thông tin chi tiết phiếu thu [chi được phép sửa số tiền]
        /// </summary>
        /// <param name="ChiTietPhieuThu">
        /// MaPhieuThu  nvarchar(10)
        /// MaPhieuXuat  nvarchar(10)
        /// SoTien  int
        /// </param>
        public int Sua(clsChiTietPhieuThuDTO ChiTietPhieuThu)
        {
            int i = -1;
            string sql = "sp_UpdateChiTietPhieuThu";
            string[] ParameterColection = new string[3];
            Object[] valueofParameter = new Object[3];
            ParameterColection[0] = "@MaPhieuThu";
            valueofParameter[0] = ChiTietPhieuThu.MaPhieuThu;
            ParameterColection[1] = "@MaPhieuXuat";
            valueofParameter[1] = ChiTietPhieuThu.PhieuXuat;
            ParameterColection[2] = "@SoTien";
            valueofParameter[2] = ChiTietPhieuThu.SoTien;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Xóa thông tin chi tiết phiếu chi
        /// </summary>
        /// <param name="ChiTietPhieuThu">
        /// MaPhieuThu  nvarchar(10)
        /// MaPhieuXuat  nvarchar(10)
        /// </param>
        public int Xoa(clsChiTietPhieuThuDTO ChiTietPhieuThu)
        {
            int i = -1;
            string sql = "sp_DeleteChiTietPhieuThu";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@MaPhieuThu";
            valueofParameter[0] = ChiTietPhieuThu.MaPhieuThu;
            ParameterColection[1] = "@MaPhieuXuat";
            valueofParameter[1] = ChiTietPhieuThu.PhieuXuat;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Xóa thông tin chi tiết phiếu thu
        /// </summary>
        /// <param name="ChiTietPhieuThu">
        /// MaPhieuThu  nvarchar(10)
        /// MaPhieuXuat  nvarchar(10)
        /// </param>
        public int XoaTatCa(string MaPhieuThu)
        {
            int i = -1;
            string sql = "sp_DeleteChiTietPhieuThu";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuThu";
            valueofParameter[0] = MaPhieuThu;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }
    }

}
