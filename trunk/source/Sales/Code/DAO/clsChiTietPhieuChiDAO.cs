using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsChiTietPhieuChiDAO
    {
        #region Attribute
        /// <summary>
        /// Kết nối SQL Server
        /// </summary>
        private CSQLServer sqlServer = new CSQLServer();
        #endregion

        /// <summary>
        /// Lấy danh sách chi tiết  phiếu chi hàng vào kho theo mã phiếu chi
        /// </summary>
        /// <param name="MaPhieuChi">Mã phiếu chi</param>
        public DataTable LayBang(string MaPhieuChi)
        {
            string sql = "sp_GetBangChiTietPhieuChi";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuChi";
            valueofParameter[0] = MaPhieuChi;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return table;
        }

        /// <summary>
        /// Lấy danh sách chi tiết  phiếu chi hàng vào kho theo mã phiếu chi
        /// </summary>
        /// <param name="MaPhieuChi">Mã phiếu chi</param>
        public List<clsChiTietPhieuChiDTO> LayDanhSach(string MaPhieuChi)
        {
            string sql = "sp_GetBangChiTietPhieuChi";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuChi";
            valueofParameter[0] = MaPhieuChi;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return ChuyenDoi(table, MaPhieuChi);
        }

        /// <summary>
        /// chuyển từ một dòng sang đối tượng PhieuChiDTO
        /// </summary>
        private List<clsChiTietPhieuChiDTO> ChuyenDoi(DataTable table, string MaPhieuChi)
        {
            List<clsChiTietPhieuChiDTO> DanhSach = new List<clsChiTietPhieuChiDTO>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                clsChiTietPhieuChiDTO CT_PhieuChi = new clsChiTietPhieuChiDTO();
                CT_PhieuChi.MaPhieuChi = MaPhieuChi;
                CT_PhieuChi.SoTien =Double.Parse(table.Rows[i]["SoTien"].ToString());
                CT_PhieuChi.PhieuNhap.MaPhieuNhap = table.Rows[i]["MaPhieuNhap"].ToString();
                CT_PhieuChi.PhieuNhap.NgayNhap = DateTime.Parse(table.Rows[i]["NgayNhap"].ToString());
                CT_PhieuChi.PhieuNhap.NhaCungCap.MaNhaCungCap = table.Rows[i]["MaNhaCungCap"].ToString();
                CT_PhieuChi.PhieuNhap.TongTien = Double.Parse(table.Rows[i]["TongTien"].ToString());
                CT_PhieuChi.PhieuNhap.ConNo = Double.Parse(table.Rows[i]["ConNo"].ToString());
                CT_PhieuChi.PhieuNhap.TrangThai = int.Parse(table.Rows[i]["TrangThai"].ToString());
                DanhSach.Add(CT_PhieuChi);
            }
            return DanhSach;
        }

        /// <summary>
        /// Thêm thông tin chi tiết phiếu chi 
        /// </summary>
        /// <param name="ChiTietPhieuChi">
        /// MaPhieuChi  nvarchar(10)
        /// MaPhieuChi  nvarchar(10)
        /// SoTien  int
        /// </param>
        public int Them(clsChiTietPhieuChiDTO ChiTietPhieuChi)
        {
            int i = -1;
            string sql = "sp_InsertChiTietPhieuChi";
            string[] ParameterColection = new string[3];
            Object[] valueofParameter = new Object[3];
            ParameterColection[0] = "@MaPhieuChi";
            valueofParameter[0] = ChiTietPhieuChi.MaPhieuChi;
            ParameterColection[1] = "@MaPhieuNhap";
            valueofParameter[1] = ChiTietPhieuChi.PhieuNhap.MaPhieuNhap;
            ParameterColection[2] = "@SoTien";
            valueofParameter[2] = ChiTietPhieuChi.SoTien;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Sửa thông tin chi tiết phiếu chi [chi được phép sửa số tiền]
        /// </summary>
        /// <param name="ChiTietPhieuChi">
        /// MaPhieuChi  nvarchar(10)
        /// MaPhieuChi  nvarchar(10)
        /// SoTien  int
        /// </param>
        public int Sua(clsChiTietPhieuChiDTO ChiTietPhieuChi)
        {
            int i = -1;
            string sql = "sp_UpdateChiTietPhieuChi";
            string[] ParameterColection = new string[3];
            Object[] valueofParameter = new Object[3];
            ParameterColection[0] = "@MaPhieuChi";
            valueofParameter[0] = ChiTietPhieuChi.MaPhieuChi;
            ParameterColection[1] = "@MaPhieuNhap";
            valueofParameter[1] = ChiTietPhieuChi.PhieuNhap.MaPhieuNhap;
            ParameterColection[2] = "@SoTien";
            valueofParameter[2] = ChiTietPhieuChi.SoTien;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Xóa thông tin chi tiết phiếu chi [chi được phép sửa số tiền]
        /// </summary>
        /// <param name="ChiTietPhieuChi">
        /// MaPhieuChi  nvarchar(10)
        /// MaPhieuChi  nvarchar(10)
        /// </param>
        public int Xoa(clsChiTietPhieuChiDTO ChiTietPhieuChi)
        {
            int i = -1;
            string sql = "sp_DeleteChiTietPhieuChi";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@MaPhieuChi";
            valueofParameter[0] = ChiTietPhieuChi.MaPhieuChi;
            ParameterColection[1] = "@MaPhieuNhap";
            valueofParameter[1] = ChiTietPhieuChi.PhieuNhap.MaPhieuNhap;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Xóa thông tin chi tiết phiếu chi [chi được phép sửa số tiền]
        /// </summary>
        /// <param name="ChiTietPhieuChi">
        /// MaPhieuChi  nvarchar(10)
        /// MaPhieuChi  nvarchar(10)
        /// </param>
        public int XoaTatCa(string MaPhieuChi)
        {
            int i = -1;
            string sql = "sp_DeleteChiTietPhieuChi";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaPhieuChi";
            valueofParameter[0] = MaPhieuChi;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }
    }
}
