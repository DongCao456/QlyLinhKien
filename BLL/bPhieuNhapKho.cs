using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BLL
{
    public class bPhieuNhapKho
    {
        DataQuanLyLinhKienDataContext data;
        public bPhieuNhapKho()
        {
            data = new DataQuanLyLinhKienDataContext();
        }
        public List<ePhieuNhapKho> layDanhSachPhieuNhapKho()
        {
            var kh = data.PhieuNhapKhos;
            List<ePhieuNhapKho> ls = new List<ePhieuNhapKho>();
            foreach (var item in kh)
            {
                ls.Add(new ePhieuNhapKho()
                {
                    MaPhieuNhapKho = item.maPhieuNhapKho,
                    MaNhaCungCap = item.maNhaCungCap,
                    MaNhanVienThuKho = item.maNhanVienThuKho,
                    MaNhanVienKeToanKho = item.maNhanVienKeToanKho,
                    NgayLap = item.ngayLap,
                    TongTien = item.tongTien,
                    TrangThai = item.trangThai
                });
            }
            return ls;
        }
        public ePhieuNhapKho thongTinPhieuNhapKho(string ma)
        {
            PhieuNhapKho item = data.PhieuNhapKhos.Single(n => n.maPhieuNhapKho == ma);
            return new ePhieuNhapKho()
            {
                MaPhieuNhapKho = item.maPhieuNhapKho,
                MaNhaCungCap = item.maNhaCungCap,
                MaNhanVienThuKho = item.maNhanVienThuKho,
                MaNhanVienKeToanKho = item.maNhanVienKeToanKho,
                NgayLap = item.ngayLap,
                TongTien = item.tongTien,
                TrangThai = item.trangThai
            };
        }
        public bool themPhieuNhapKho(ePhieuNhapKho item)
        {
            try
            {
                data.PhieuNhapKhos.InsertOnSubmit(new PhieuNhapKho()
                {
                    maPhieuNhapKho = item.MaPhieuNhapKho,
                    maNhaCungCap = item.MaNhaCungCap,
                    maNhanVienThuKho = item.MaNhanVienThuKho,
                    maNhanVienKeToanKho = item.MaNhanVienKeToanKho,
                    ngayLap = item.NgayLap,
                    tongTien = item.TongTien,
                    trangThai = item.TrangThai
                });
                data.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void suaPhieuNhapKho(ePhieuNhapKho item)
        {
            PhieuNhapKho n = data.PhieuNhapKhos.Single(m => m.maPhieuNhapKho == item.MaPhieuNhapKho);
            n.maPhieuNhapKho = item.MaPhieuNhapKho;
            n.maNhaCungCap = item.MaNhaCungCap;
            n.maNhanVienThuKho = item.MaNhanVienThuKho;
            n.maNhanVienKeToanKho = item.MaNhanVienKeToanKho;
            n.ngayLap = item.NgayLap;
            n.tongTien = item.TongTien;
            n.trangThai = item.TrangThai;

            data.SubmitChanges();
        }
        public void xoaPhieuNhapKho(string ma)
        {
            PhieuNhapKho item = data.PhieuNhapKhos.Single(n => n.maPhieuNhapKho == ma);
            data.PhieuNhapKhos.DeleteOnSubmit(item);
            data.SubmitChanges();
        }
        public DataSet inPhieuNhapKho(string maPhieuNhapKho)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["QuanLyLinhKien.Properties.Settings.QuanLyLinhKienConnectionString"].ToString();
            string sql = "SELECT * FROM vw_inPhieuNhapKho WHERE maPhieuNhapKho = N'" + maPhieuNhapKho + "'";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

            DataSet ds = new DataSet();

            adapter.Fill(ds, "vw_inPhieuNhapKho");
            return ds;
        }
        public DataSet inThongKe(DateTime ngayBatDau, DateTime ngayKetThuc, decimal loai, string tenLoai, decimal soLuong)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["QuanLyLinhKien.Properties.Settings.QuanLyLinhKienConnectionString"].ToString();
            string sql = "" +
                "SELECT TOP " + soLuong + " stt = CONVERT(INT,REPLACE(maPhieuNhapKho,'PNK-','')),maPhieuNhapKho,tenNhanVienKeToanKho,tenNhanVienThuKho ,tenNhaCungCap,tongTien ,ngayLap,ngayBatDau = N'" + ngayBatDau.ToShortDateString() + "', ngayKetThuc = N'" + ngayKetThuc.ToShortDateString() + "', loai = N'" + tenLoai + "' " +
                "FROM [dbo].[vw_ThongKePhieuNhapKho] " +
                "WHERE ngayLap BETWEEN '" + ngayBatDau.Year + "/" + ngayBatDau.Month + "/" + ngayBatDau.Day + "' AND '" + ngayKetThuc.Year + "/" + ngayKetThuc.Month + "/" + ngayKetThuc.Day + "' ";
            if (loai == 1)
                sql += "ORDER BY tongTien DESC";
            else
                sql += "ORDER BY stt";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

            DataSet ds = new DataSet();

            adapter.Fill(ds, "[dbo].[vw_ThongKePhieuNhapKho]");
            return ds;
        }
    }
}
