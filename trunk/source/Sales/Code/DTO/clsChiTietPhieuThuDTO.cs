using System;
using System.Collections.Generic;
using System.Text;

namespace Sales
{
    public class clsChiTietPhieuThuDTO
    {
        public string MaPhieuThu;
        public clsPhieuXuatDTO PhieuXuat;
        public double SoTien;

        public clsChiTietPhieuThuDTO()
        {
            MaPhieuThu = "";
            PhieuXuat = new clsPhieuXuatDTO();
            SoTien = 0;

        }

        public clsChiTietPhieuThuDTO(string _MaPhieuThu, clsPhieuXuatDTO _PhieuXuat, double _SoTien)
        {
            MaPhieuThu = _MaPhieuThu;
            PhieuXuat = _PhieuXuat;
            SoTien = _SoTien;
        }

        
    }

   }
