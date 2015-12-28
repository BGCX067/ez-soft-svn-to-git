using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsCongTyDAO
    {

        /// <summary>
        /// Kết nối SQL Server
        /// </summary>
        public CSQLServer sqlServer = new CSQLServer();

        /// <summary>
        /// Lấy DS khách hàng
        /// </summary>
        public clsCongTyDTO LayThongTin()
        {
            clsCongTyDTO CongTy = new clsCongTyDTO();
            string sql = "sp_GetInfoCongTy";
            DataTable table = sqlServer.readData(sql);
            if (table.Rows.Count == 1)
            {
                CongTy.MaCongTy =int.Parse( table.Rows[0]["MaCongTy"].ToString());
                CongTy.TenCongTy = table.Rows[0]["TenCongTy"].ToString();
                CongTy.DiaChi = table.Rows[0]["DiaChi"].ToString();
                CongTy.DienThoai = table.Rows[0]["DienThoai"].ToString();
                CongTy.MaSoThue = table.Rows[0]["MaSoThue"].ToString();
                CongTy.Fax = table.Rows[0]["Fax"].ToString();
                CongTy.Email = table.Rows[0]["Email"].ToString();
                CongTy.Website = table.Rows[0]["Website"].ToString();
                CongTy.Logo = table.Rows[0]["Logo"].ToString();
            }
            return CongTy;
        }

     
        /// <summary>
        /// Cập nhật khách hàng
        /// </summary>
        public int Sua(clsCongTyDTO CongTy)
        {
            int i = -1;
            string sql = "sp_UpdateCongTy";
            string[] ParameterColection = new string[9];
            Object[] valueofParameter = new Object[9];
            ParameterColection[0] = "@MaCongTy";
            valueofParameter[0] = CongTy.MaCongTy;
            ParameterColection[1] = "@TenCongTy";
            valueofParameter[1] = CongTy.TenCongTy;
            ParameterColection[2] = "@DiaChi";
            valueofParameter[2] = CongTy.DiaChi;
            ParameterColection[3] = "@DienThoai";
            valueofParameter[3] = CongTy.DienThoai;
            ParameterColection[4] = "@MaSoThue";
            valueofParameter[4] = CongTy.MaSoThue;
            ParameterColection[5] = "@Fax";
            valueofParameter[5] = CongTy.Fax;
            ParameterColection[6] = "@Email";
            valueofParameter[6] = CongTy.Email;
            ParameterColection[7] = "@Website";
            valueofParameter[7] = CongTy.Website;
            ParameterColection[8] = "@Logo";
            valueofParameter[8] = CongTy.Logo;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        public DataTable ReportCongTy()
        {
            string sql = "sp_ReportCongTy";
            string[] ParameterColection = new string[3];
            Object[] valueofParameter = new Object[3];
            
            DataTable table = sqlServer.readData(sql, "sp_ReportCongTy;1", ParameterColection, valueofParameter);
            return table;
        }

        /// <summary>
        /// Lấy danh sách thông tin chi tiết chi thu của công ty từ ngày đến ngày
        /// </summary>
        /// <param name="TuNgay">Từ ngày</param>
        /// <param name="DenNgay">Đến ngày</param>
        /// <returns></returns>
        /// 
        public DataTable LayBangChiTietThuChi(DateTime TuNgay, DateTime DenNgay, string LyDo, string TenDoiTuong)
        {
            string sql = "sp_GetBangChiTietChiThu";
            string[] ParameterColection = new string[4];
            Object[] valueofParameter = new Object[4];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            ParameterColection[2] = "@LyDo";
            valueofParameter[2] = LyDo;
            ParameterColection[3] = "@TenDoiTuong";
            valueofParameter[3] = TenDoiTuong;
            return sqlServer.readData(sql, ParameterColection, valueofParameter);
        }

        /// <summary>
        /// Lấy số tiền tồn đầu kỳ
        /// </summary>
        public Double LayTienTonDauKy()
        {
            string sql = "sp_GetInfoTienTonDauKy";
            return Double.Parse(sqlServer.readData(sql, "@TienTonDauKy"));
        }

        /// <summary>
        /// Cập nhật số tiền tồn đầu kỳ
        /// </summary>
        public Double CapNhatTienTonDauKy(Double TienTonDauKy)
        {
            int i = -1;
            string sql = "sp_UpdateTienTonDauKy";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@TienTonDauKy";
            valueofParameter[0] = TienTonDauKy;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }


        /// <summary>
        /// Lấy danh sách các lý do
        /// </summary>
        public DataTable LayBangLyDo()
        {
            string sql = "sp_GetBangLyDo";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@LoaiLyDo";
            valueofParameter[0] = "TatCa";
            return sqlServer.readData(sql, ParameterColection, valueofParameter);
        }

        public DataTable ReportQuyTienMat(DateTime TuNgay, DateTime DenNgay, string LyDo, string TenDoiTuong)
        {
            string sql = "sp_ReportQuyTienMat";
            string[] ParameterColection = new string[4];
            Object[] valueofParameter = new Object[4];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            ParameterColection[2] = "@LyDo";
            valueofParameter[2] = LyDo;
            ParameterColection[3] = "@TenDoiTuong";
            valueofParameter[3] = TenDoiTuong;
            DataTable table = sqlServer.readData(sql, "sp_ReportQuyTienMat;1", ParameterColection, valueofParameter);
            return table;
        }

        public int SaoLuuDuLieu(string DuongDan, string MatKhau)
        {
            int i = -1;
            string sql = "sp_BackupDatabase";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@DuongDan";
            valueofParameter[0] = DuongDan;
            ParameterColection[1] = "@MatKhau";
            valueofParameter[1] = MatKhau;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

    }
}
