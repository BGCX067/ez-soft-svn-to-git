using System;
using System.Collections.Generic;
using System.Text;

namespace Sales
{
    public class clsChiTietPhieuNhapDTO
    {
        public string MaPhieuNhap;
        public clsMatHangDTO MatHang;
        public double DonGia;
        public int SoLuong;
        public double ThanhTien;
        public double ChietKhau;
        public double ThueVAT;
        public int SoLuongTon;

        /// <param name="_MaPhieuNhap">Mã phiếu nhập</param>
        /// <param name="_MatHang">mặt hàng</param>
        public clsChiTietPhieuNhapDTO(string _MaPhieuNhap, clsMatHangDTO _MatHang, int _SoLuong, double _DonGia, double _ThanhTien, double _ChietKhau, double _ThueVAT, int _SoLuongTon)
        {
            MaPhieuNhap =_MaPhieuNhap;
             MatHang = _MatHang;
            DonGia = _DonGia;
            SoLuong = _SoLuong;
            ThanhTien = _ThanhTien;
            ChietKhau = _ChietKhau;
            ThueVAT = _ThueVAT;
            SoLuongTon = _SoLuongTon;
        }

        /// <param name="_MaPhieuNhap">Mã phiếu nhập</param>
        /// <param name="_MatHang">mặt hàng</param>
        public clsChiTietPhieuNhapDTO()
        {
             MaPhieuNhap="";
            MatHang=new clsMatHangDTO();
            DonGia=0;
            SoLuong=0;
            ThanhTien=0;
            ChietKhau=0;
            ThueVAT=0;
            SoLuongTon =0;
        }

    }

    
}
