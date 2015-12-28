using System;
using System.Collections.Generic;
using System.Text;

namespace Sales
{
    public class clsPhieuThuDTO
    {
        public string MaPhieuThu;
        public DateTime NgayThu;
        public string NguoiNop;
        public double SoTien;
        public string LyDo;
        public int TrangThai;
        public string NguoiThu;

        public clsPhieuThuDTO()
        {
            MaPhieuThu = "";
            NgayThu = new DateTime();
            NguoiNop = "";
            SoTien = 0;
            LyDo = "";
            NguoiThu = "";
            TrangThai = 1;
        }

        public clsPhieuThuDTO(string _MaPhieuThu, DateTime _NgayThu, string _NguoiNop, double _SoTien, string _LyDo, int _TrangThai, string _NguoiThu)
        {
            MaPhieuThu = _MaPhieuThu;
            NgayThu = _NgayThu;
            NguoiNop = _NguoiNop;
            SoTien = _SoTien;
            LyDo = _LyDo;
            TrangThai = _TrangThai;
            NguoiThu = _NguoiThu;
        }
    }
}
