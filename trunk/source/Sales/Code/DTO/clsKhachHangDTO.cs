using System;
using System.Collections.Generic;
using System.Text;

namespace Sales
{
    public class clsKhachHangDTO
    {
        public string MaKhachHang;
        public string TenKhachHang;
        public string DienThoai;
        public string DiaChi;
        public string Fax;
        public string MaSoThue;
        public string BaoGia;
        public double ChietKhau;
        public double NoDauKy;
        public string TenNguoiLienHe;
        public DateTime NgayTao;
        public int TrangThai;

        public clsKhachHangDTO(string _MaKhachHang, string _TenKhachHang, string _DienThoai, string _DiaChi, string _Fax, string _MaSoThue, string _BaoGia, double _ChietKhau, double _NoDauKy, string _TenNguoiLienHe, int _TrangThai)
        {
            MaKhachHang = _MaKhachHang;
            TenKhachHang = _TenKhachHang;
            DienThoai = _DienThoai;
            DiaChi = _DiaChi;
            Fax = _Fax;
            MaSoThue = _MaSoThue;
            BaoGia = _BaoGia;
            ChietKhau = _ChietKhau;
            NoDauKy = _NoDauKy;
            TenNguoiLienHe = _TenNguoiLienHe;
            NgayTao = new DateTime();
            TrangThai = 1;
        }
        public clsKhachHangDTO()
        {
             MaKhachHang = "" ;
             TenKhachHang="";
             DienThoai="";
             DiaChi="";
             Fax="";
             MaSoThue="";
             BaoGia="";
             ChietKhau=0;
             NoDauKy=0;
             TenNguoiLienHe="";
             NgayTao=new DateTime();
             TrangThai=1;
        }
    }
}
