using System;
using System.Collections.Generic;
using System.Text;

namespace Sales
{
    public class clsPhieuChiDTO
    {
        public string MaPhieuChi;
        public DateTime NgayChi;
        public string NguoiNhan;
        public double SoTien;
        public string LyDo;
        public int TrangThai;
        public string NguoiChi;

        public clsPhieuChiDTO(string _MaPhieuChi, DateTime _NgayChi, string _NguoiNhan, double _SoTien, string _LyDo, int _TrangThai, string _NguoiChi)
        {
            MaPhieuChi = _MaPhieuChi;
            NgayChi = _NgayChi;
            NguoiNhan = _NguoiNhan;
            SoTien = _SoTien;
            LyDo = _LyDo;
            TrangThai = _TrangThai;
            NguoiChi = _NguoiChi;
        }
        public clsPhieuChiDTO()
        {
            MaPhieuChi = "";
            NgayChi = new DateTime();
            NguoiNhan = "";
            SoTien = 0;
            LyDo = "";
            TrangThai = 1;
            NguoiChi = "";
        }
    }
}
