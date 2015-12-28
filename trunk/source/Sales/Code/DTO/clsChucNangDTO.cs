using System;
using System.Collections.Generic;
using System.Text;

namespace Sales
{
    public class clsChucNangDTO
    {
        public int MaChucNang;
        public string TenChucNang;
        public int TrangThai;

        public clsChucNangDTO(int _MaChucNang, string _TenChucNang)
        {
            MaChucNang = _MaChucNang;
            TenChucNang = _TenChucNang;
            TrangThai = 1;
        }
        public clsChucNangDTO()
        {
            MaChucNang = 0;
            TenChucNang = "";
            TrangThai = 1;
        }
    }
}
