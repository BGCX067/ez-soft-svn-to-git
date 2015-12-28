using System;
using System.Collections.Generic;
using System.Text;

namespace Sales
{
    public class clsPhieuXuatBanLeDTO : clsPhieuXuatDTO 
    {
        public string KhachHang;
        public clsPhieuXuatBanLeDTO(string _MaPhieuXuat, DateTime _NgayXuat, clsNhanVienDTO _NhanVien, double _TongTien, double _DaTra, string _KhachHang, int _TrangThai)
        {
            MaPhieuXuat = _MaPhieuXuat;
            NgayXuat = _NgayXuat;
            NhanVien = _NhanVien;
            TongTien = _TongTien;
            DaTra = _DaTra;
            KhachHang = _KhachHang;
            TrangThai = _TrangThai;
            DS_ChiTietPhieuXuat = new List<clsChiTietPhieuXuatDTO>();
        }

        public clsPhieuXuatBanLeDTO()
        {
            MaPhieuXuat = "";
            NgayXuat = new DateTime();
            NhanVien = new clsNhanVienDTO();
            TongTien = 0;
            DaTra = 0;
            KhachHang = "";
            TrangThai = 1;
            DS_ChiTietPhieuXuat = new List<clsChiTietPhieuXuatDTO>();
        }
    }

   

    
}
