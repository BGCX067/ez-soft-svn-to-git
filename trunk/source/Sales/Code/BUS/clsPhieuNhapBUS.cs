using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    public class clsPhieuNhapBUS
    {
        /// <summary>
        /// 
        /// </summary>
        private clsPhieuNhapDAO PhieuNhapDAO = new clsPhieuNhapDAO();

        /// <summary>
        /// Lấy Mã phiếu nhập kế tiếp để phục vụ cho chức năng Insert
        /// </summary>
        public string LayMaPhieuNhapMoi()
        {
            return PhieuNhapDAO.LayMaPhieuNhapMoi();
        }

        /// <summary>
        /// Lấy danh sách thông tin chi tiết  Phiếu nhập hàng vào kho theo từ ngày đến ngày
        /// </summary>
        /// <param name="TuNgay">Từ ngày</param>
        /// <param name="DenNgay">Đến ngày</param>
        /// <returns></returns>
        public DataTable LayBang(DateTime TuNgay, DateTime DenNgay)
        {
            return PhieuNhapDAO.LayBang(TuNgay, DenNgay);
        }

        /// <summary>
        /// Lấy danh sách phiếu nhập hàng
        /// </summary>
        //public DataTable LayBang()
        //{
        //    throw new System.NotImplementedException();
        //}
        /// <summary>
        /// Thông thông tin phiếu nhập hàng
        /// </summary>
        /// <param name="PhieuNhap">
        /// MaPhieuNhap  nvarchar(10)
        /// NgayNhap  smalldatetime
        /// MaNhaCungCap  nvarchar(50)
        /// TongTien  float
        /// ConNo  float
        /// TragThai  int
        /// </param>
        public clsPhieuNhapDTO LayThongTin(string MaPhieuNhap)
        {
            return PhieuNhapDAO.LayThongTin(MaPhieuNhap);
        }

        /// <summary>
        /// Thêm thông tin phiếu nhập hàng
        /// </summary>
        /// <param name="PhieuNhap">
        /// MaPhieuNhap  nvarchar(10)
        /// NgayNhap  smalldatetime
        /// MaNhaCungCap  nvarchar(50)
        /// TongTien  float
        /// ConNo  float
        /// TragThai  int
        /// </param>
        public int Them(clsPhieuNhapDTO PhieuNhap)
        {
            return PhieuNhapDAO.Them(PhieuNhap);
        }

        /// <summary>
        /// Sửa thông tin phiếu nhập
        /// </summary>
        /// <param name="PhieuNhap">
        /// MaPhieuNhap  nvarchar(10)
        /// NgayNhap  smalldatetime
        /// MaNhaCungCap  nvarchar(50)
        /// TongTien  float
        /// ConNo  float
        /// TragThai  int
        /// </param>
        public int Sua(clsPhieuNhapDTO PhieuNhap)
        {
            return PhieuNhapDAO.Sua(PhieuNhap);
        }

        public int CapNhatTienConNo(string MaPhieuNhap, double TienConNo)
        {
            return PhieuNhapDAO.CapNhatTienConNo(MaPhieuNhap, TienConNo);
        }

        /// <summary>
        /// Xóa thông tin phiếu nhập
        /// </summary>
        /// <param name="MaPhieuNhap">Mã phiếu nhập</param>
        public int Xoa(string MaPhieuNhap)
        {
            return PhieuNhapDAO.Xoa(MaPhieuNhap);
        }

        public int Huy(string MaPhieuNhap)
        {
            return PhieuNhapDAO.Huy(MaPhieuNhap);
        }

        public Boolean KiemTraHuyPhieuNhap(string MaPhieuNhap)
        {
            clsPhieuNhapDTO PhieuNhap= PhieuNhapDAO.LayThongTin(MaPhieuNhap);
            Boolean Co = true;
            for (int i = 0; i < PhieuNhap.DS_ChiTietPhieuNhap.Count; i++ )
            {
                //So luong tra hang lon hon so luong ton hay noi cach khac la mat hang nay da ban roi nen ko cho phep huy phieu nhap
                if (PhieuNhap.DS_ChiTietPhieuNhap[i].SoLuongTon < PhieuNhap.DS_ChiTietPhieuNhap[i].SoLuong)
                {
                    Co = false;
                    break;
                }
            }
            return Co;
        }

        /// <summary>
        /// Tìm kiếm thông tin Phiếu nhập
        /// </summary>
        /// <param name="TuNgay">Ngày bắt đầu</param>
        /// <param name="DenNgay">Ngày kết thúc</param>
        public DataTable TimKiem(DateTime TuNgay, DateTime DenNgay)
        {
            return PhieuNhapDAO.TimKiem(TuNgay, DenNgay);
        }
        /// <summary>
        /// Tìm kiếm thông tin Phiếu nhập
        /// </summary>
        /// <param name="TuNgay">Ngày bắt đầu</param>
        /// <param name="DenNgay">Ngày kết thúc</param>
        public DataTable TimKiem(DateTime TuNgay, DateTime DenNgay, clsNhaCungCapDTO NhaCungCap)
        {
            return PhieuNhapDAO.TimKiem(TuNgay, DenNgay, NhaCungCap);
        }

        public DataTable CongNoNhaCungCap(DateTime TuNgay, DateTime DenNgay)
        {
            return PhieuNhapDAO.CongNoNhaCungCap(TuNgay, DenNgay);
        }

        public DataTable CongNoNhaCungCapTheoNCC(DateTime TuNgay, DateTime DenNgay, clsNhaCungCapDTO NhaCungCap)
        {
            return PhieuNhapDAO.CongNoNhaCungCapTheoNCC(TuNgay, DenNgay, NhaCungCap);
        }

        public DataTable LayBangConNo(clsNhaCungCapDTO NhaCungCap)
        {
            return PhieuNhapDAO.LayBangConNo(NhaCungCap);
        }

        //public DataTable LayBangConNoVaNoDauKy(clsNhaCungCapDTO NhaCungCap)
        //{
        //    return PhieuNhapDAO.LayBangConNoVaNoDauKy(NhaCungCap);
        //}

        public List<clsChiTietPhieuNhapDTO> LayDanhSachChiTietPhieuNhapTheoMatHang(string MaMatHang)
        {
            return PhieuNhapDAO.LayDanhSachChiTietPhieuNhapTheoMatHang(MaMatHang);
        }


        public List<clsChiTietPhieuNhapDTO> ChonMatHangNhapVoiGiaCao(string MaMatHang, int SoLuong)
        {
            List<clsChiTietPhieuNhapDTO> CacCTPNDuocChon=new List<clsChiTietPhieuNhapDTO>();
            List<clsChiTietPhieuNhapDTO> CacCTPN= LayDanhSachChiTietPhieuNhapTheoMatHang( MaMatHang);
            if (CacCTPN.Count == 0)
            {
                return CacCTPNDuocChon;
            }
            for(int i=0;i<CacCTPN.Count;i++)
            {
                if (CacCTPN[i].SoLuongTon >= SoLuong)
                {
                    CacCTPN[i].SoLuongTon = SoLuong;//So luong ton o day chinh la so luong ban ra cua phieu nhap do
                    CacCTPNDuocChon.Add(CacCTPN[i]);
                    break;
                }
                else
                {
                    CacCTPNDuocChon.Add(CacCTPN[i]);
                    SoLuong -= CacCTPN[i].SoLuongTon;
                }
            }
            return CacCTPNDuocChon;
        }

        public List<clsChiTietPhieuNhapDTO> SapSepTheoGiaBanGiamDan(List<clsChiTietPhieuNhapDTO> danhsach)
        {
            for (int i = 0; i < danhsach.Count-1; i++)
            {
                for (int j =i+1; j < danhsach.Count; j++)
                {
                    if (danhsach[i].DonGia > danhsach[j].DonGia)
                    {
                        clsChiTietPhieuNhapDTO Tam = danhsach[i];
                        danhsach[i] = danhsach[j];
                        danhsach[j] = Tam;
                    }
                }
            }
            return danhsach;
        }

        public List<clsChiTietPhieuNhapDTO> ChonMatHangNhapVoiGiaCao(int SoLuong, List<clsChiTietPhieuNhapDTO> danhsach)
        {
            List<clsChiTietPhieuNhapDTO> CacCTPNDuocChon = new List<clsChiTietPhieuNhapDTO>();
            danhsach=SapSepTheoGiaBanGiamDan(danhsach);
            if (danhsach.Count == 0)
            {
                return null;
            }
            for (int i = 0; i < danhsach.Count; i++)
            {
                if (danhsach[i].SoLuongTon >= SoLuong)
                {
                    danhsach[i].SoLuongTon = SoLuong;//So luong ton o day chinh la so luong ban ra cua phieu nhap do
                    CacCTPNDuocChon.Add(danhsach[i]);
                    break;
                }
                else
                {
                    CacCTPNDuocChon.Add(danhsach[i]);
                    SoLuong -= danhsach[i].SoLuongTon;
                }
            }
            return CacCTPNDuocChon;
        }

        public DataTable ReportPhieuNhapHang(string MaPhieuNhap)
        {
            return PhieuNhapDAO.ReportPhieuNhapHang(MaPhieuNhap);
        }

        public DataTable ReportCT_PhieuNhapHang(string MaPhieuNhap)
        {
            return PhieuNhapDAO.ReportCT_PhieuNhapHang(MaPhieuNhap);
        }


        public DataTable ReportDonHangDaMua(DateTime TuNgay, DateTime DenNgay, string MaNhaCungCap)
        {
            return PhieuNhapDAO.ReportDonHangDaMua(TuNgay, DenNgay, MaNhaCungCap);
        }

        public DataTable ReportCongNoNhaCungCap(DateTime DenNgay, string MaNhaCungCap)
        {
            return PhieuNhapDAO.ReportCongNoNhaCungCap(DenNgay, MaNhaCungCap);
        }

        public DataTable ReportChiTietHangNhap(DateTime TuNgay, DateTime DenNgay, string MaMatHang, string MaNhaCungCap)
        {
            return PhieuNhapDAO.ReportChiTietHangNhap(TuNgay,DenNgay,MaMatHang,MaNhaCungCap);
        }
    }
}
