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
    public class bDonDatHang
    {
        DataQuanLyLinhKienDataContext data;
        public bDonDatHang()
        {
            data = new DataQuanLyLinhKienDataContext();
        }
        public List<eDonDatHang> layDanhSachDonDatHang()
        {
            var kh = data.DonDatHangs;
            List<eDonDatHang> ls = new List<eDonDatHang>();
            foreach (var item in kh)
            {
                ls.Add(new eDonDatHang()
                {
                    MaDonDatHang = item.maDonDatHang,
                    MaKhachHang = item.maKhachHang,
                    MaNhanVienThuNgan = item.maNhanVienThuNgan,
                    MaNhanVienTuVan = item.maNhanVienTuVan,
                    NgayLap = item.ngayLap,
                    NoiNhanHang = item.noiNhanHang,
                    TrangThai = item.trangThai,
                    TongTien = item.tongTien
                });
            }
            return ls;
        }
        public eDonDatHang thongTinDonDatHang(string ma)
        {
            DonDatHang ddh = data.DonDatHangs.Single(n => n.maDonDatHang == ma);
            return new eDonDatHang()
            {
                MaDonDatHang = ddh.maDonDatHang,
                MaKhachHang = ddh.maKhachHang,
                MaNhanVienThuNgan = ddh.maNhanVienThuNgan,
                MaNhanVienTuVan = ddh.maNhanVienTuVan,
                NgayLap = ddh.ngayLap,
                NoiNhanHang = ddh.noiNhanHang,
                TrangThai = ddh.trangThai,
                TongTien = ddh.tongTien
            };
        }
        public bool themDonDatHang(eDonDatHang ddh)
        {
            try
            {
                data.DonDatHangs.InsertOnSubmit(new DonDatHang()
                {
                    maDonDatHang = ddh.MaDonDatHang,
                    maKhachHang = ddh.MaKhachHang,
                    maNhanVienThuNgan = ddh.MaNhanVienThuNgan,
                    maNhanVienTuVan = ddh.MaNhanVienTuVan,
                    ngayLap = ddh.NgayLap,
                    noiNhanHang = ddh.NoiNhanHang,
                    trangThai = ddh.TrangThai,
                    tongTien = ddh.TongTien
                });
                data.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void suaDonDatHang(eDonDatHang ddh)
        {
            DonDatHang n = data.DonDatHangs.Single(m => m.maDonDatHang == ddh.MaDonDatHang);
            n.maDonDatHang = ddh.MaDonDatHang;
            n.maKhachHang = ddh.MaKhachHang;
            n.maNhanVienThuNgan = ddh.MaNhanVienThuNgan;
            n.maNhanVienTuVan = ddh.MaNhanVienTuVan;
            n.ngayLap = ddh.NgayLap;
            n.noiNhanHang = ddh.NoiNhanHang;
            n.trangThai = ddh.TrangThai;
            n.tongTien = ddh.TongTien;

            data.SubmitChanges();
        }
        public void xoaDonDatHang(string ma)
        {
            DonDatHang ddh = data.DonDatHangs.Single(n => n.maDonDatHang == ma);
            data.DonDatHangs.DeleteOnSubmit(ddh);
            data.SubmitChanges();
        }
        public DataSet inDonDatHang(string maDonDatHang)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["QuanLyLinhKien.Properties.Settings.QuanLyLinhKienConnectionString"].ToString();
            string sql = "SELECT * FROM [dbo].[vw_InHoaDon] WHERE maDonDatHang = N'" + maDonDatHang + "'";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

            DataSet ds = new DataSet();

            adapter.Fill(ds, "[dbo].[vw_InHoaDon]");
            return ds;
        }
        public DataSet inThongKe(DateTime ngayBatDau, DateTime ngayKetThuc, decimal loai, string tenLoai, decimal soLuong)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["QuanLyLinhKien.Properties.Settings.QuanLyLinhKienConnectionString"].ToString();
            string sql = "" +
                "SELECT TOP " + soLuong + " stt = CONVERT(INT,REPLACE(MaDonDatHang,'DDH-','')),MaDonDatHang,NhanVienTuVan,ThuNgan ,KhachHang,TongDoanhThu ,ngayLap,ngayBatDau = N'" + ngayBatDau.ToShortDateString() + "', ngayKetThuc = N'" + ngayKetThuc.ToShortDateString() + "', loai = N'" + tenLoai + "'" +
                "FROM [dbo].[vw_ThongKeDonDatHang] " +
                "WHERE ngayLap BETWEEN '" + ngayBatDau.Year + "/" + ngayBatDau.Month +"/"+ ngayBatDau.Day +"' AND '" + ngayKetThuc.Year + "/" + ngayKetThuc.Month + "/" + ngayKetThuc.Day +"' ";
            if (loai == 1)
                sql += "ORDER BY TongDoanhThu DESC";
            else
                sql += "ORDER BY stt";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

            DataSet ds = new DataSet();

            adapter.Fill(ds, "[dbo].[vw_ThongKeDonDatHang]");
            return ds;
        }
    }
}
