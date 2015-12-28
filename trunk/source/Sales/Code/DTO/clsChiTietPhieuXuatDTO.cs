using System;
using System.Collections.Generic;
using System.Text;

namespace Sales
{
    public class clsChiTietPhieuXuatDTO
    {
        public string MaPhieuXuat;
        public clsMatHangDTO MatHang;
        public string MaPhieuNhap;
        public int SoLuong;
        public double DonGia;
        public double ThanhTien;
        public double ChietKhau;
        public double ThueVAT;

        /// <param name="_MaPhieuNhap">Mã phiếu nhập</param>
        /// <param name="_MatHang">mặt hàng</param>
        public clsChiTietPhieuXuatDTO(string _MaPhieuXuat, clsMatHangDTO _MatHang,string _MaPhieuNhap, int _SoLuong, double _DonGia, double _ThanhTien, double _ChietKhau, double _ThueVAT)
        {
            MaPhieuXuat=_MaPhieuXuat; 
            MatHang=_MatHang;
            SoLuong=_SoLuong;
            DonGia=_DonGia;
            ThanhTien=_ThanhTien;
            ChietKhau=_ChietKhau;
            ThueVAT = _ThueVAT;
            MaPhieuNhap = _MaPhieuNhap;
        }

        public clsChiTietPhieuXuatDTO()
        {

            MaPhieuXuat = "";
            MatHang = new clsMatHangDTO();
            MaPhieuNhap = "";
            SoLuong = 0;
            DonGia = 0;
            ThanhTien = 0;
            ChietKhau = 0;
            ThueVAT = 0;
        }

    }
}
