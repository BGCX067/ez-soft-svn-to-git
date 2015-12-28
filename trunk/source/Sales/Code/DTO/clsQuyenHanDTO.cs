using System;
using System.Collections.Generic;
using System.Text;

namespace Sales
{
    public class clsQuyenHanDTO
    {
        public int MaQuyenHan;
        public string TenQuyenHan;
        public int TrangThai;
        public List<clsPhanQuyenChucNangDTO> DS_PhanQuyenChucNang;

        public clsQuyenHanDTO(int _MaQuyenHan, string _TenQuyenHan, List<clsPhanQuyenChucNangDTO> _DS_PhanQuyenChucNang)
        {
            MaQuyenHan = _MaQuyenHan;
            TenQuyenHan = _TenQuyenHan;
            TrangThai = 1;
            DS_PhanQuyenChucNang = _DS_PhanQuyenChucNang;
        }
        public clsQuyenHanDTO()
        {
            MaQuyenHan = 0;
            TenQuyenHan = "";
            TrangThai = 1;
            DS_PhanQuyenChucNang = new List<clsPhanQuyenChucNangDTO>();
        }
    }

    public class clsPhanQuyenChucNangDTO
    {
        public int MaQuyenHan;
        public clsChucNangDTO ChucNang;

        public clsPhanQuyenChucNangDTO()
        {
            MaQuyenHan = 0;
            ChucNang = new clsChucNangDTO();

        }

        public clsPhanQuyenChucNangDTO(int _MaQuyenHan, clsChucNangDTO _ChucNang)
        {
            MaQuyenHan = _MaQuyenHan;
            ChucNang = _ChucNang;
        }


    }
}
