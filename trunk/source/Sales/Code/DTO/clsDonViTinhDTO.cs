using System;
using System.Collections.Generic;
using System.Text;

namespace Sales
{
    class clsDonViTinhDTO
    {
        public string MaDonViTinh;
        public string TenDonViTinh;
        public int TrangThai;

        public clsDonViTinhDTO(string _MaDonViTinh, string _TenDonViTinh)
        {
            MaDonViTinh = _MaDonViTinh;
            TenDonViTinh = _TenDonViTinh;
            TrangThai = 1;
        }
    }
}
