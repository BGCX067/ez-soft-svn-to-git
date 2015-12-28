using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Sales
{
    class clsQuyenHanDAO
    {
        #region Attribute
        /// <summary>
        /// Kết nối SQL Server
        /// </summary>
        private CSQLServer sqlServer = new CSQLServer();
        private clsPhanQuyenChucNangDAO PhanQuyenChucNangDAO = new clsPhanQuyenChucNangDAO();
        #endregion

        /// <summary>
        /// Lấy danh sách quyền hạn
        /// </summary>
        public DataTable LayBang()
        {
            string sql = "sp_GetBangQuyenHan";
            DataTable table = sqlServer.readData(sql);
            return table;
        }

        /// <summary>
        /// Lấy thông tin quyền hạn
        /// </summary>
        /// <param name="QuyenHan">
        /// MaQuyenHan  nvarchar(10)
        /// NgayNhap  smalldatetime
        /// MaNhaCungCap  nvarchar(50)
        /// TongTien  float
        /// ConNo  float
        /// TragThai  int
        /// </param>
        public clsQuyenHanDTO LayThongTin(int MaQuyenHan)
        {
            string sql = "sp_GetInfoQuyenHan";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaQuyenHan";
            valueofParameter[0] = MaQuyenHan;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return ChuyenDoi(table);
        }

        private clsQuyenHanDTO ChuyenDoi(DataTable table)
        {
            clsQuyenHanDTO QuyenHan = new clsQuyenHanDTO();
            if (table.Rows.Count == 1)
            {
                DataRow Dong = table.Rows[0];
                QuyenHan.MaQuyenHan = int.Parse(Dong["MaQuyenHan"].ToString());
                QuyenHan.TenQuyenHan = Dong["TenQuyenHan"].ToString();
                QuyenHan.TrangThai = int.Parse(Dong["TrangThai"].ToString());
                QuyenHan.DS_PhanQuyenChucNang = PhanQuyenChucNangDAO.LayDanhSach(QuyenHan.MaQuyenHan);
            }
            else
            {
                return null;
            }
            return QuyenHan;

        }


       /// <summary>
       /// Thêm quyền hạn
       /// </summary>
       /// <param name="QuyenHan"></param>
       /// <returns></returns>
        public int Them(clsQuyenHanDTO QuyenHan)
        {
            int i = -1;
            string sql = "sp_InsertQuyenHan";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@TenQuyenHan";
            valueofParameter[0] = QuyenHan.TenQuyenHan;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            if (i != -1)
            {
                QuyenHan.MaQuyenHan = i;
                for (int k = 0; k < QuyenHan.DS_PhanQuyenChucNang.Count; k++)
                {
                    QuyenHan.DS_PhanQuyenChucNang[k].MaQuyenHan = QuyenHan.MaQuyenHan;
                    i = PhanQuyenChucNangDAO.Them(QuyenHan.DS_PhanQuyenChucNang[k]);
                }
            }
            return i;
        }

        /// <summary>
        /// Sửa thông tin loại mặt hàng
        /// </summary>
        /// <param name="QuyenHan">
        /// MaQuyenHan  nvarchar(10)
        /// TenQuyenHan  nvarchar(255)
        /// DienGiai  ntext
        /// </param>
        public int Sua(clsQuyenHanDTO QuyenHan)
        {
            int i = -1;
            string sql = "sp_UpdateQuyenHan";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@MaQuyenHan";
            valueofParameter[0] = QuyenHan.MaQuyenHan;
            ParameterColection[1] = "@TenQuyenHan";
            valueofParameter[1] = QuyenHan.TenQuyenHan;
            //xoa tat cac phan quyen chuc nang roi them cac phan quyen chuc năng moi
            i = PhanQuyenChucNangDAO.XoaTatCa(QuyenHan.MaQuyenHan);
            if (i != -1)
            {
                //Cacp nhat ten quyen han
                i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
                if (i != -1)
                {
                    //Them cac chuc nang chu quyen han
                    for (int k = 0; k < QuyenHan.DS_PhanQuyenChucNang.Count; k++)
                    {
                        i = PhanQuyenChucNangDAO.Them(QuyenHan.DS_PhanQuyenChucNang[k]);
                    }
                }
            }
            return i;
        }

        /// <summary>
        /// Xóa loại mặt hàng
        /// </summary>
        /// <param name="MaQuyenHan">Mã mặt hàng</param>
        public int Xoa(string MaQuyenHan)
        {
            int i = -1;
            string sql = "sp_DeleteQuyenHan";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaQuyenHan";
            valueofParameter[0] = MaQuyenHan;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }
    }

    public class clsPhanQuyenChucNangDAO
    {
        #region Attribute
        /// <summary>
        /// Kết nối SQL Server
        /// </summary>
        private CSQLServer sqlServer = new CSQLServer();
        #endregion

        /// <summary>
        /// Lấy danh sách chi tiết  Chức năng theo quyền hạn
        /// </summary>
        /// <param name="MaQuyenHan">Mã phiếu thu</param>
        public DataTable LayBang(int MaQuyenHan)
        {
            string sql = "sp_GetBangPhanQuyenChucNang";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaQuyenHan";
            valueofParameter[0] = MaQuyenHan;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return table;
        }

        /// <summary>
        /// Lấy danh sách chi tiết Chức năng theo quyền hạn
        /// </summary>
        /// <param name="MaQuyenHan">Mã quyền hạn</param>
        public List<clsPhanQuyenChucNangDTO> LayDanhSach(int MaQuyenHan)
        {
            string sql = "sp_GetBangPhanQuyenChucNang";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaQuyenHan";
            valueofParameter[0] = MaQuyenHan;
            DataTable table = sqlServer.readData(sql, ParameterColection, valueofParameter);
            return ChuyenDoi(table, MaQuyenHan);
        }

        /// <summary>
        /// chuyển từ một dòng sang đối tượng phân quyền chức năng
        /// </summary>
        private List<clsPhanQuyenChucNangDTO> ChuyenDoi(DataTable table, int MaQuyenHan)
        {
            List<clsPhanQuyenChucNangDTO> DanhSach = new List<clsPhanQuyenChucNangDTO>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                clsPhanQuyenChucNangDTO PQ_CN = new clsPhanQuyenChucNangDTO();
                PQ_CN.MaQuyenHan = MaQuyenHan;
                PQ_CN.ChucNang.MaChucNang = int.Parse(table.Rows[i]["MaChucNang"].ToString());
                PQ_CN.ChucNang.TenChucNang = table.Rows[i]["TenChucNang"].ToString();
                DanhSach.Add(PQ_CN);
            }
            return DanhSach;
        }

        /// <summary>
        /// Thêm một phân quyền chức năng
        /// </summary>
        /// <param name="PhanQuyenChucNang">
        /// MaQuyenHan  nvarchar(10)
        /// MaPhieuXuat  nvarchar(10)
        /// SoTien  int
        /// </param>
        public int Them(clsPhanQuyenChucNangDTO PhanQuyenChucNang)
        {
            int i = -1;
            string sql = "sp_InsertPhanQuyenChucNang";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@MaQuyenHan";
            valueofParameter[0] = PhanQuyenChucNang.MaQuyenHan;
            ParameterColection[1] = "@MaChucNang";
            valueofParameter[1] = PhanQuyenChucNang.ChucNang.MaChucNang;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Xóa thông tin phân quyền chức năng
        /// </summary>
        /// <param name="PhanQuyenChucNang">
        /// MaQuyenHan  nvarchar(10)
        /// MaPhieuXuat  nvarchar(10)
        /// </param>
        public int Xoa(clsPhanQuyenChucNangDTO PhanQuyenChucNang)
        {
            int i = -1;
            string sql = "sp_DeletePhanQuyenChucNang";
            string[] ParameterColection = new string[2];
            Object[] valueofParameter = new Object[2];
            ParameterColection[0] = "@MaQuyenHan";
            valueofParameter[0] = PhanQuyenChucNang.MaQuyenHan;
            ParameterColection[1] = "@MaChucNang";
            valueofParameter[1] = PhanQuyenChucNang.ChucNang.MaChucNang;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }

        /// <summary>
        /// Xóa tất cả các quyền hạn theo mã quyền hạn
        /// </summary>
        /// <param name="PhanQuyenChucNang">
        /// MaQuyenHan  nvarchar(10)
        /// MaPhieuXuat  nvarchar(10)
        /// </param>
        public int XoaTatCa(int MaQuyenHan)
        {
            int i = -1;
            string sql = "sp_DeleteAllPhanQuyenChucNang";
            string[] ParameterColection = new string[1];
            Object[] valueofParameter = new Object[1];
            ParameterColection[0] = "@MaQuyenHan";
            valueofParameter[0] = MaQuyenHan;
            i = sqlServer.writeData(sql, ParameterColection, valueofParameter);
            return i;
        }
    }

}
