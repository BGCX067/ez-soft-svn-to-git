using System;
using System.Collections.Generic;
using System.Text;
using System.Data;


namespace Sales
{
    public class clsPhieuChiDAO
    {
        #region Attribute
        /// <summary>
        /// Kết nối SQL Server
        /// </summary>
        protected CSQLServer sqlServer = new CSQLServer();
        #endregion

        /// <summary>
        /// Tìm kiếm thông tin tất cả các phiếu chi cho phiếu nhập và chi khác
        /// </summary>
        /// <param name="TuNgay">Ngày bắt đầu</param>
        /// <param name="DenNgay">Ngày kết thúc</param>
        public DataTable TiemKiemChung(DateTime TuNgay, DateTime DenNgay)
        {
            string sql = "sp_SearchPhieuChi";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            return  sqlServer.readData(sql, ParameterColection, valueofParameter);
        }

        /// <summary>
        /// Tìm kiếm thông tin tất cả các phiếu chi cho phiếu nhập và chi khác
        /// </summary>
        /// <param name="TuNgay">Ngày bắt đầu</param>
        /// <param name="DenNgay">Ngày kết thúc</param>
        ///<param name="NhaCungCap">Nhà cung cấp</param>
        public DataTable TiemKiemChung(DateTime TuNgay, DateTime DenNgay, clsNhaCungCapDTO NhaCungCap)
        {
            string sql = "sp_SearchPhieuChiTheoNCC";
            string[] ParameterColection = new string[3];
            Object[] valueofParameter = new Object[3];
            ParameterColection[0] = "@TuNgay";
            valueofParameter[0] = TuNgay;
            ParameterColection[1] = "@DenNgay";
            valueofParameter[1] = DenNgay;
            ParameterColection[2] = "@MaNhaCungCap";
            valueofParameter[2] = NhaCungCap.MaNhaCungCap;
            return sqlServer.readData(sql, ParameterColection, valueofParameter);
        }

        /// <summary>
        /// Lấy Mã phiếu nhập kế tiếp để phục vụ cho chức năng Insert
        /// </summary>
        public string LayMaPhieuChiMoi()
        {
            string sql = "sp_GetNewMaPhieuChi";
            return sqlServer.readData(sql, "@MaPhieuChiMoi");
        }

       
    }
}
