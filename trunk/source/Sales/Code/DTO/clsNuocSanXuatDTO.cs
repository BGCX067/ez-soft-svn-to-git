using System;
using System.Collections.Generic;
using System.Text;

namespace Sales
{
    class clsNuocSanXuatDTO
    {
        public string MaNuocSanXuat;
        public string TenNuocSanXuat;
        public int TrangThai;

        public clsNuocSanXuatDTO(string _MaNuocSanXuat, string _TenNuocSanXuat)
        {
            MaNuocSanXuat = _MaNuocSanXuat;
            TenNuocSanXuat = _TenNuocSanXuat;
            TrangThai = 1;
        }
    }
}
