using System;
using System.Collections.Generic;
using System.Text;

namespace Sales
{
    public class clsCongTyDTO
    {
        public int MaCongTy;
        public string TenCongTy;
        public string DienThoai;
        public string DiaChi;
        public string Fax;
        public string MaSoThue;
        public string Email;
        public string Website;
        public string Logo;
 
        public clsCongTyDTO()
        {
             MaCongTy = 1 ;
             TenCongTy="";
             DienThoai="";
             DiaChi="";
             Fax="";
             MaSoThue="";
             Email = "";
             Website = "";
             Logo = "";
        }
    }
}
