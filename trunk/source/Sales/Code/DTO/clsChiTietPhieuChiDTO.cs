using System;
using System.Collections.Generic;
using System.Text;

namespace Sales
{
    public class clsChiTietPhieuChiDTO
    {
        public string MaPhieuChi;
        public clsPhieuNhapDTO PhieuNhap;
        public double SoTien;

        public clsChiTietPhieuChiDTO(string _MaPhieuChi, clsPhieuNhapDTO _PhieuNhap, double _SoTien)
        {
            MaPhieuChi = _MaPhieuChi;
            PhieuNhap = _PhieuNhap;
            SoTien = _SoTien;
        }
        public clsChiTietPhieuChiDTO()
        {
            MaPhieuChi = "";
            PhieuNhap = new clsPhieuNhapDTO();
            SoTien = 0;
        }
        
    }

    
}
