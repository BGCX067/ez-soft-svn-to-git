using System;
using System.Collections.Generic;
using System.Text;

namespace Sales
{
    public class clsPhieuChiHangDTO : clsPhieuChiDTO
    {
        public string NhaCungCap;
        public List<clsChiTietPhieuChiDTO> DS_ChiTietPhieuChi;

        //public clsPhieuChiHangDTO(string _MaPhieuThu, DateTime _NgayThu, string _NguoiNop, double _SoTien, string _LyDo, string _NhaCungCap, int _TrangThai)
        //{
        //    DS_ChiTietPhieuChi = new List<clsChiTietPhieuChiDTO>();
        //}

        public clsPhieuChiHangDTO()
            : base()
        {
            DS_ChiTietPhieuChi = new List<clsChiTietPhieuChiDTO>();
        }
        public clsPhieuChiHangDTO(string _MaPhieuChi, DateTime _NgayChi, string _NguoiNhan, double _SoTien, string _LyDo, int _TrangThai, string _NguoiChi)
        {
            MaPhieuChi = _MaPhieuChi;
            NgayChi = _NgayChi;
            NguoiNhan = _NguoiNhan;
            SoTien = _SoTien;
            LyDo = _LyDo;
            TrangThai = _TrangThai;
            NguoiChi = _NguoiChi;
            DS_ChiTietPhieuChi = new List<clsChiTietPhieuChiDTO>();
        }
    }
}
