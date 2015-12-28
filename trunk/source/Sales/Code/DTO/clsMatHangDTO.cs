using System;
using System.Collections.Generic;
using System.Text;

namespace Sales
{
    public class clsMatHangDTO
    {
        public string MaMatHang;
        public string TenMatHang;
        public string DienGiai;
        public Double DonGia;
        public string DonViTinh;
        public double GiaBanLe;
        public double GiaBanSi;
        public Double GiaMua;
        public clsLoaiMatHangDTO LoaiMatHang;
        public int LuongMin;
        public int LuongMax;
        public string MaVach;
        public double PT_GiaBanSi;
        public double PT_GiaBanLe;
        public double SoLuongTon;
        public double ThueVAT;
        public string XuatXu;
        public DateTime NgayTao;
        public int TrangThai;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_MaMatHang"></param>
        /// <param name="_TenMatHang"></param>
        /// <param name="_LoaiMatHang"></param>
        /// <param name="_DonViTinh"></param>
        /// <param name="_DonGia"></param>
        /// <param name="_GiaMua"></param>
        /// <param name="_GiaBanSi"></param>
        /// <param name="_GiaBanLe"></param>
        /// <param name="_PT_GiaBanSi"></param>
        /// <param name="_PT_GiaBanLe"></param>
        /// <param name="_LuongMin"></param>
        /// <param name="_LuongMax"></param>
        /// <param name="_SoLuongTon"></param>
        /// <param name="_ThueVAT"></param>
        /// <param name="_XuatXu"></param>
        /// <param name="_DienGia"></param>
        /// <param name="_MaVach"></param>
        /// <param name="_TrangThai"></param>
        public clsMatHangDTO(string _MaMatHang, string _TenMatHang, clsLoaiMatHangDTO _LoaiMatHang, string _DonViTinh, double _DonGia, double _GiaMua, double _GiaBanSi, double _GiaBanLe, double _PT_GiaBanSi, double _PT_GiaBanLe, int _LuongMin, int _LuongMax, int _SoLuongTon, double _ThueVAT, string _XuatXu, string _DienGia, string _MaVach, int _TrangThai)
        {
            MaMatHang = _MaMatHang;
            TenMatHang = _TenMatHang;
            DienGiai = _DienGia;
            DonGia = _DonGia;
            DonViTinh = _DonViTinh;
            GiaBanLe = _GiaBanLe;
            GiaBanSi = _GiaBanSi;
            GiaMua = _GiaMua;
            LoaiMatHang = _LoaiMatHang;
            LuongMin = _LuongMin;
            LuongMax = _LuongMax;
            MaVach =_MaVach;
            PT_GiaBanSi = _PT_GiaBanSi;
            PT_GiaBanLe = _PT_GiaBanLe;
            SoLuongTon = _SoLuongTon;
            ThueVAT = _ThueVAT;
            XuatXu = _XuatXu;
            NgayTao = new DateTime();
            TrangThai = 1;
        }
        
        public clsMatHangDTO()
        {
            MaMatHang="";
            TenMatHang="";
            DienGiai="";
            DonGia=0;
            DonViTinh="";
            GiaBanLe=0;
            GiaBanSi=0;
            GiaMua=0;
            LoaiMatHang=new clsLoaiMatHangDTO();
            LuongMin=0;
            LuongMax=0;
            MaVach="";
            PT_GiaBanSi=0;
            PT_GiaBanLe=0;
            SoLuongTon=0;
            ThueVAT=0;
            XuatXu="";
            NgayTao=new DateTime();
            TrangThai=1;
        }

        
    }
}
