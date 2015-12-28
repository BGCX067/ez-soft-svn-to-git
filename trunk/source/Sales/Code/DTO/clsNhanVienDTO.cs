using System;
using System.Collections.Generic;
using System.Text;

namespace Sales
{
    public class clsNhanVienDTO
    {
        public string MaNhanVien;
        public string TenNhanVien;
        public string DienThoai;
        public string DiaChi;
        public string GhiChu;
        public string TenNguoiDung="";
        public string MatKhau="";
        public string Email="";
        public int TinhTrangNguoiDung = 1;
        public DateTime NgayTao;
        public int TrangThai;
        public clsQuyenHanDTO QuyenHan;

        public clsNhanVienDTO(string _MaNhanVien, string _TenNhanVien, string _DienThoai, string _DiaChi, string _GhiChu, clsQuyenHanDTO _QuyenHan, DateTime _NgayTao, int _TrangThai)
        {
            MaNhanVien = _MaNhanVien;
            TenNhanVien = _TenNhanVien;
            DienThoai = _DienThoai;
            DiaChi = _DiaChi;
            GhiChu = _GhiChu;
            NgayTao = _NgayTao;
            TrangThai = _TrangThai;
            QuyenHan = _QuyenHan;
            TenNguoiDung = "";
            MatKhau = "";
            Email = "";
        }

        public clsNhanVienDTO()
        {
            MaNhanVien = "";
            TenNhanVien = "";
            DienThoai = "";
            DiaChi = "";
            GhiChu = "";
            NgayTao = new DateTime();
            TenNguoiDung = "";
            MatKhau = "";
            Email = "";
            TinhTrangNguoiDung = 1;
            TrangThai = 1;
            QuyenHan = new clsQuyenHanDTO();
        }

    }
}
