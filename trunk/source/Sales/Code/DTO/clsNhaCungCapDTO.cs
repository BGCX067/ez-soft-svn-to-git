using System;
using System.Collections.Generic;
using System.Text;

namespace Sales
{
    public class clsNhaCungCapDTO
    {
        public string MaNhaCungCap;
        public String TenNhaCungCap;
        public string DienThoai;
        public string DiaChi;
        public string Fax;
        public string MaSoThue;
        public double NoDauKy;
        public string TenNguoiLienHe;
        public int TrangThai;
        public DateTime NgayTao;

        public clsNhaCungCapDTO(string _MaNhaCungCap, string _TenNhaCungCap, string _DiaChi, string _DienThoai, string _Fax, string _MaSoThue, string _TenNguoiLienHe, double _NoDauKy, int _TrangThai, DateTime _NgayTao)
        {
            MaNhaCungCap = _MaNhaCungCap;
            TenNhaCungCap = _TenNhaCungCap;
            DienThoai = _DienThoai;
            DiaChi = _DiaChi;
            Fax = _Fax;
            MaSoThue = _MaSoThue;
            NoDauKy = _NoDauKy;
            TenNguoiLienHe = _TenNguoiLienHe;
            TrangThai = _TrangThai;
            NgayTao = _NgayTao;
        }

        public clsNhaCungCapDTO()
        {
            MaNhaCungCap="";
            TenNhaCungCap="";
            DienThoai="";
            DiaChi="";
            Fax="";
            MaSoThue="";
            NoDauKy=0;
            TenNguoiLienHe="";
            TrangThai=1;
            NgayTao=new DateTime();
        }
    }
}
