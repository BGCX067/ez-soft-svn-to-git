using System;
using System.Collections.Generic;
using System.Text;

namespace Sales
{
    public  class clsPhieuXuatDTO
    {
        public string MaPhieuXuat;
        public DateTime NgayXuat;
        public clsNhanVienDTO NhanVien;
        public clsKhachHangDTO KhachHang;
        public double TongTien;
        public double DaTra;
        public int TrangThai;
        public System.Collections.Generic.List<clsChiTietPhieuXuatDTO> DS_ChiTietPhieuXuat;

        public clsPhieuXuatDTO(string _MaPhieuXuat, DateTime _NgayXuat, clsNhanVienDTO _NhanVien, clsKhachHangDTO _KhachHang, double _TongTien, double _DaTra, int _TrangThai)
        {
            MaPhieuXuat = _MaPhieuXuat;
            NgayXuat = _NgayXuat;
            NhanVien = _NhanVien;
            KhachHang = _KhachHang;
            TongTien = _TongTien;
            DaTra = _DaTra;
            TrangThai = _TrangThai;
            DS_ChiTietPhieuXuat = new List<clsChiTietPhieuXuatDTO>();
        }

        public clsPhieuXuatDTO()
        {
            MaPhieuXuat = "";
            NgayXuat = new DateTime();
            NhanVien = new clsNhanVienDTO();
            KhachHang = new clsKhachHangDTO();
            TongTien = 0;
            DaTra = 0;
            TrangThai = 1;
            DS_ChiTietPhieuXuat = new List<clsChiTietPhieuXuatDTO>();
        }

        
    }
}
