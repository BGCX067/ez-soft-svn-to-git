using System;
using System.Collections.Generic;
using System.Text;

namespace Sales
{
    public class clsLoaiMatHangDTO
    {
        public string MaLoaiMatHang;
        public string TenLoaiMatHang;
        public string DienGiai;
        public DateTime NgayNhap;
        public int TrangThai;

        public clsLoaiMatHangDTO(string _MaLoaiMatHang, string _TenLoaiMatHang, string _DienGiai)
        {
            MaLoaiMatHang = _MaLoaiMatHang;
            TenLoaiMatHang = _TenLoaiMatHang;
            DienGiai = _DienGiai;
            TrangThai = 1;
            NgayNhap = new DateTime();
        }
        public clsLoaiMatHangDTO()
        {
            MaLoaiMatHang = "";
            TenLoaiMatHang = "";
            DienGiai = "";
            TrangThai = 1;
            NgayNhap = new DateTime();
        }
    }
}
