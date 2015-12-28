using System;
using System.Collections.Generic;
using System.Text;

namespace Sales
{
    public class clsPhieuThuBUS
    {
        #region Attribute
        private clsPhieuThuDAO PhieuThuDAO = new clsPhieuThuDAO();
        #endregion
        
        /// <summary>
        /// Lấy mã phiếu thu mới
        /// </summary>
        /// <returns></returns>
        public string LayMaPhieuThuMoi()
        {
            return PhieuThuDAO.LayMaPhieuThuMoi();
        }
    }
}
