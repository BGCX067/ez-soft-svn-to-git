using System;
using System.Collections.Generic;
using System.Text;

namespace Sales
{
    public class clsPhieuThuBanHangDTO : clsPhieuThuDTO
    {
        public string KhachHang;
        public List<clsChiTietPhieuThuDTO> DS_ChiTietPhieuThu;

        public clsPhieuThuBanHangDTO()
            :base()
        {
            DS_ChiTietPhieuThu = new List<clsChiTietPhieuThuDTO>();
        }

        public clsPhieuThuBanHangDTO(string _MaPhieuThu, DateTime _NgayThu, string _NguoiNop, double _SoTien, string _LyDo, string _KhachHang, int _TrangThai)
        {
            MaPhieuThu = _MaPhieuThu;
            NgayThu = _NgayThu;
            NguoiNop = _NguoiNop;
            SoTien = _SoTien;
            LyDo = _LyDo;
            DS_ChiTietPhieuThu = new List<clsChiTietPhieuThuDTO>();
        }

    }

    
}
