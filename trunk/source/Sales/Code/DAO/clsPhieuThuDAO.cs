using System;
using System.Collections.Generic;
using System.Text;

namespace Sales
{
    public class clsPhieuThuDAO
    {
        #region Attribute
        /// <summary>
        /// Kết nối SQL Server
        /// </summary>
        protected CSQLServer sqlServer = new CSQLServer();
        #endregion

        /// <summary>
        /// Lấy Mã phiếu thu kế tiếp để phục vụ cho chức năng Insert
        /// </summary>
        public string LayMaPhieuThuMoi()
        {
            string sql = "sp_GetNewMaPhieuThu";
            return sqlServer.readData(sql, "@MaPhieuThuMoi");
        }
    }
}
