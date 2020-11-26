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
    public class bKhachHang
    {
        DataQuanLyLinhKienDataContext data;
        public bKhachHang()
        {
            data = new DataQuanLyLinhKienDataContext();
        }
        public List<eKhachHang> layDanhSachKhachHang()
        {
            var kh = data.KhachHangs;
            List<eKhachHang> ls = new List<eKhachHang>();
            foreach (var item in kh)
            {
                ls.Add(new eKhachHang()
                {
                    DiaChi = item.diaChi,
                    EMail = item.eMail,
                    MaKhachHang = item.maKhachHang,
                    SoDienThoai = item.soDienThoai,
                    TenKhachHang = item.tenKhachHang
                });
            }
            return ls;
        }
        public eKhachHang thongTinKhachHang(string ma)
        {
            KhachHang kh = data.KhachHangs.Single(n => n.maKhachHang == ma);
            return new eKhachHang()
            {
                DiaChi = kh.diaChi,
                EMail = kh.eMail,
                MaKhachHang = kh.maKhachHang,
                SoDienThoai = kh.soDienThoai,
                TenKhachHang = kh.tenKhachHang
            };
        }
        public bool themKhachHang(eKhachHang kh)
        {
            try
            {
                data.KhachHangs.InsertOnSubmit(new KhachHang()
                {
                    diaChi = kh.DiaChi,
                    eMail = kh.EMail,
                    maKhachHang = kh.MaKhachHang,
                    soDienThoai = kh.SoDienThoai,
                    tenKhachHang = kh.TenKhachHang
                });
                data.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void suaKhachHang(eKhachHang kh)
        {
            KhachHang k = data.KhachHangs.Single(n => n.maKhachHang == kh.MaKhachHang);
            k.diaChi = kh.DiaChi;
            k.eMail = kh.EMail;
            k.maKhachHang = kh.MaKhachHang;
            k.soDienThoai = kh.SoDienThoai;
            k.tenKhachHang = kh.TenKhachHang;

            data.SubmitChanges();
        }
        public void xoaKhachHang(string ma)
        {
            data.TaiKhoans.DeleteOnSubmit(data.TaiKhoans.Single(n => n.maTaiKhoan == ma));
            data.SubmitChanges();
        }
        public DataSet inThongKe(DateTime ngayBatDau, DateTime ngayKetThuc, decimal loai, string tenLoai, decimal soLuong)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["QuanLyLinhKien.Properties.Settings.QuanLyLinhKienConnectionString"].ToString();
            string sql = "SELECT TOP 10 MaKhachHang,TenKhachHang,TongSoDonDatHang,soLuong = SUM(soLuong),thanhTien = SUM(thanhTien ), ngayBatDau = N'" + ngayBatDau.ToShortDateString() + "', ngayKetThuc = N'" + ngayKetThuc.ToShortDateString() + "', loai = N'" + tenLoai + "'" +
                "FROM [dbo].[vw_ThongKeKhachHang] " +
                "WHERE ngayMua BETWEEN '" + ngayBatDau.Year + "/" + ngayBatDau.Month + "/" + ngayBatDau.Day + "' AND '" + ngayKetThuc.Year + "/" + ngayKetThuc.Month + "/" + ngayKetThuc.Day + "' " +
                "GROUP BY MaKhachHang,TenKhachHang,TongSoDonDatHang ";
            if (loai ==0 )
            {
                sql += "ORDER BY CONVERT(INT,REPLACE(MaKhachHang,'KH-','')) ";
            }
            else
            {
                sql += "ORDER BY thanhTien DESC ";
            }
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

            DataSet ds = new DataSet();

            adapter.Fill(ds, "[dbo].[vw_ThongKeKhachHang]");
            return ds;
        }
    }
}
