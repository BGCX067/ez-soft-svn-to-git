using System;
using System.Collections.Generic;
using System.Text;

namespace Sales
{
    public class clsPhieuNhapDTO
    {
        public string MaPhieuNhap;
        public DateTime NgayNhap;
        public clsNhaCungCapDTO NhaCungCap;
        public double TongTien;
        public double ConNo;
        public int TrangThai;
        public List<clsChiTietPhieuNhapDTO> DS_ChiTietPhieuNhap;
        public string NguoiNhap;

        public clsPhieuNhapDTO(string _MaPhieuNhap, DateTime _NgayNhap, clsNhaCungCapDTO _NhaCungCap, double _TongTien, double _CongNo, int _TrangThai, string _NguoiNhap)
        {
            MaPhieuNhap=_MaPhieuNhap;
            NgayNhap=_NgayNhap;
            NhaCungCap=_NhaCungCap;
            TongTien=_TongTien;
            ConNo=_CongNo;
            TrangThai = _TrangThai;
            NguoiNhap = _NguoiNhap;
            DS_ChiTietPhieuNhap = new List<clsChiTietPhieuNhapDTO>();
        }

        public clsPhieuNhapDTO()
        {
            MaPhieuNhap = "";
            NgayNhap = new DateTime();
            NhaCungCap = new clsNhaCungCapDTO();
            TongTien = 0;
            ConNo = 0;
            TrangThai =1;
            NguoiNhap = "";
            DS_ChiTietPhieuNhap = new List<clsChiTietPhieuNhapDTO>();
        }
    }

   
}
