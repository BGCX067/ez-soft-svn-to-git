using System;
using System.Collections.Generic;
using System.Text;

namespace Sales
{
   public class clsUser
    {
        #region Thuoc tinh

        public string TenNguoiDung;
        public string MatKhau;
       public static string MaNhanVien = "";

        #endregion

        #region Khoi Tao

        public clsUser(string _TenNguoiDung, string _MatKhau)
        {
            TenNguoiDung = _TenNguoiDung;
            MatKhau = _MatKhau;
        }

        public clsUser()
        {
            TenNguoiDung = "";
            MatKhau = "";
        }

        #endregion

        #region Phuong thuc

        public Boolean DangNhap(Boolean GhiNhoNguoiDung)
        {
            Boolean KQ = new clsNhanVienBUS().KiemTraNguoiDung(TenNguoiDung, MatKhau);
            if (KQ ==true)
            {
                if (GhiNhoNguoiDung == true)
                {
                    GhiThongTinCanNho();
                }
                else
                {
                    XoaThongTinGhiNho();
                }
            }
            return KQ;
        }
        //──────────────────────────────────────────────────────────────────────────────────────────

        public  void DocThongTinGhiNho()
        {
            TenNguoiDung = clsSupport.ReadRegistry("cms_UserSales" );
            MatKhau = clsSupport.ReadRegistry("cms_PassSales" );
        }//

        //──────────────────────────────────────────────────────────────────────────────────────────
        public void GhiThongTinCanNho()
        {
            clsSupport.WriteRegistry("cms_UserSales" , TenNguoiDung);
            clsSupport.WriteRegistry("cms_PassSales" , MatKhau);
        }
        //──────────────────────────────────────────────────────────────────────────────────────────

        public void XoaThongTinGhiNho()
        {
            clsSupport.DeleteRegistry("cms_UserSales");
            clsSupport.DeleteRegistry("cms_PassSales");
        }//

        /// <summary>
        /// lấy thông tin người dùng khi đăng nhập thành công
        /// </summary>
        /// <returns></returns>
        public clsNhanVienDTO LayThongTinNguoiDung()
        {
            clsNhanVienDTO NguoiDung = new clsNhanVienDTO();
            //if (DangNhap() == true)
            //{
                NguoiDung = new clsNhanVienBUS().LayThongTinTheoTenNguoiDung(TenNguoiDung);
                NguoiDung.QuyenHan = new clsQuyenHanBUS().LayThongTin(NguoiDung.QuyenHan.MaQuyenHan);
            //}
            //else
            //{
            //   NguoiDung=null;
            //}
            MaNhanVien = NguoiDung.MaNhanVien.Trim();
            return NguoiDung;
        }

        #endregion


    }
}
