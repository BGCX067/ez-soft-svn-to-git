using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsNhaCungCapDAO
    {
        /// <summary>
        /// Kết nối SQL Server
        /// </summary>
        public CSQLServer sqlServer = new CSQLServer();

        /// <summary>
        /// Lấy DS nhà cung cấp
        /// </summary>
        public DataTable LayBang()
        {
            string sql = "sp_GetBangNhaCungCap";
            DataTable table = sqlServer.readData(sql);
            return table;
        }
        /// <summary>
        /// Lấy Mã mặt hàng kế tiếp để phục vụ cho chức năng Insert
        /// </summary>
        public string LayMaNhaCungCapMoi()
        {
            string sql = "sp_GetNewMaNhaCungCap";
            return sqlServer.readData(sql, "@MaNhaCungCapMoi");
        }

        /// <summary>
        /// Cập nhật nhà cung cấp
        /// </summary>
        public int Sua(clsNhaCungCapDTO NhaCungCap)
        {
            int i = -1;
            string sql = "sp_UpdateNhaCungCap";
            string[] ParameterColection = new string[8];
            Object[] valueofParameter = new Object[8];
            ParameterColection[0] = "@MaNhaCungCap";
            valueofParameter[0] = NhaCungCap.MaNhaCungCap;
            ParameterColection[1] = "@TenNhaCungCap";
            valueofParameter[1] = NhaCungCap.TenNhaCungCap;
            ParameterColection[2] = "@DienThoai";
            valueofParameter[2] = NhaCungCap.DienThoai;
            ParameterColection[3] = "@DiaChi";
            valueofParameter[3] = NhaCungCap.DiaChi;
            ParameterColection[4] = "@Fax";
            valueofParameter[4] = NhaCungCap.Fax;
            ParameterColection[5] = "@MaSoThue";
            valueofParameter[5] = NhaCungCap.MaSoThue;
            ParameterColection[6] = "@NoDauKy";
            valueofParameter[6] = NhaCungCap.NoDauKy;
            ParameterColection[7] = "@TenNguoiLienHe";
            valueofParameter[7] = NhaCungCap.TenNguoiLienHe;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Thêm nhà cung cấp
        /// </summary>
        public int Them(clsNhaCungCapDTO NhaCungCap)
        {
            int i = -1;
            string sql = "sp_InsertNhaCungCap";
            string[] ParameterColection = new string[8];
            Object[] valueofParameter = new Object[8];
            ParameterColection[0] = "@MaNhaCungCap";
            valueofParameter[0] = NhaCungCap.MaNhaCungCap;
            ParameterColection[1] = "@TenNhaCungCap";
            valueofParameter[1] = NhaCungCap.TenNhaCungCap;
            ParameterColection[2] = "@DienThoai";
            valueofParameter[2] = NhaCungCap.DienThoai;
            ParameterColection[3] = "@DiaChi";
            valueofParameter[3] = NhaCungCap.DiaChi;
            ParameterColection[4] = "@Fax";
            valueofParameter[4] = NhaCungCap.Fax;
            ParameterColection[5] = "@MaSoThue";
            valueofParameter[5] = NhaCungCap.MaSoThue;
            ParameterColection[6] = "@NoDauKy";
            valueofParameter[6] = NhaCungCap.NoDauKy;
            ParameterColection[7] = "@TenNguoiLienHe";
            valueofParameter[7] = NhaCungCap.TenNguoiLienHe;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Xóa nhà cung cấp
        /// </summary>
        public int Xoa(string MaNhaCungCap)
        {
            int i = -1;
            string sql = "sp_DeleteNhaCungCap";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaNhaCungCap";
            valueofParameter[0] = MaNhaCungCap;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        public DataTable ReportDSNhaCungCap(string MaNhaCungCap, string TenNhaCungCap)
        {
            string sql = "sp_ReportDSNhaCungCap";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@MaNhaCungCap";
            valueofParameter[0] = MaNhaCungCap;
            ParameterColection[1] = "@TenNhaCungCap";
            valueofParameter[1] = TenNhaCungCap;

            DataTable table = sqlServer.readData(sql, "sp_ReportDSNhaCungCap;1", ParameterColection, valueofParameter);

            return table;
        }
    }
}
