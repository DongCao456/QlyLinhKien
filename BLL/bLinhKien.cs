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
    public class bLinhKien
    {
        DataQuanLyLinhKienDataContext data;
        public bLinhKien()
        {
            data = new DataQuanLyLinhKienDataContext();
        }
        public List<eLinhKien> layDanhSachLinhKien()
        {
            List<eLinhKien> ls = new List<eLinhKien>();
            var lk = data.Linhkiens;
            foreach (var item in lk)
            {
                ls.Add(new eLinhKien()
                {
                    GiaBan = item.giaBan,
                    MaLoai = item.maLoai,
                    MaLinhKien = item.maLinhKien,
                    MaNhaCungCap = item.maNhaCungCap,
                    SoLuong = item.soLuong,
                    TenLinhKien = item.tenLinhKien,
                    GiaMua = item.giaMua,
                    MucGiamGia = item.mucGiamGia
                });
            }
            return ls;
        }
        public eLinhKien thongTinLinhKien(string ma)
        {
            Linhkien lk;
            lk = data.Linhkiens.Single(n => n.maLinhKien == ma);
            return new eLinhKien()
            {
                GiaBan = lk.giaBan,
                MaLinhKien = lk.maLinhKien,
                MaLoai = lk.maLoai,
                MaNhaCungCap = lk.maNhaCungCap,
                SoLuong = lk.soLuong,
                TenLinhKien = lk.tenLinhKien,
                GiaMua = lk.giaMua,
                MucGiamGia = lk.mucGiamGia
            };
        }
        public bool themLinhKien(eLinhKien lk)
        {
            try
            {
                Linhkien l = new Linhkien()
                {
                    giaBan = lk.GiaBan,
                    maLinhKien = lk.MaLinhKien,
                    maLoai = lk.MaLoai,
                    maNhaCungCap = lk.MaNhaCungCap,
                    soLuong = lk.SoLuong,
                    tenLinhKien = lk.TenLinhKien,
                    giaMua = lk.GiaMua,
                    mucGiamGia = lk.MucGiamGia
                };
                data.Linhkiens.InsertOnSubmit(l);
                data.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void suaLinhKien(eLinhKien lk)
        {
            Linhkien l = data.Linhkiens.Single(n => n.maLinhKien == lk.MaLinhKien);
            l.giaBan = lk.GiaBan;
            l.maLinhKien = lk.MaLinhKien;
            l.maLoai = lk.MaLoai;
            l.maNhaCungCap = lk.MaNhaCungCap;
            l.soLuong = lk.SoLuong;
            l.tenLinhKien = lk.TenLinhKien;
            l.giaMua = lk.GiaMua;
            l.mucGiamGia = lk.MucGiamGia;
            data.SubmitChanges();
        }
        public void xoaLinhKien(string ma)
        {
            Linhkien l = data.Linhkiens.Single(n => n.maLinhKien == ma);
            data.Linhkiens.DeleteOnSubmit(l);
            data.SubmitChanges();
        }
        public DataSet inThongKe(DateTime ngayBatDau, DateTime ngayKetThuc, decimal loai, string tenLoai, decimal soLuong)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["QuanLyLinhKien.Properties.Settings.QuanLyLinhKienConnectionString"].ToString();
            string sql = "SELECT TOP " + soLuong + " maLinhKien,tenLinhKien,tenNhaCungCap,soLuongTon,DaBan = SUM(DaBan),ngayBatDau = N'" + ngayBatDau.ToShortDateString() + "', ngayKetThuc = N'" + ngayKetThuc.ToShortDateString() + "', loai = N'" + tenLoai + "'" +
                "FROM dbo.vw_ThongKeLinhKien " +
                "WHERE ngayLap BETWEEN '" + ngayBatDau.Year + "/" + ngayBatDau.Month + "/" + ngayBatDau.Day + "' AND '" + ngayKetThuc.Year + "/" + ngayKetThuc.Month + "/" + ngayKetThuc.Day + "' ";


            if (loai == 1)
            {
                sql += "GROUP BY maLinhKien,tenLinhKien,tenNhaCungCap,soLuongTon " +
                    "ORDER BY DaBan DESC";
            }
            else if (loai == 2)
            {
                sql = "SELECT maLinhKien,tenLinhKien,tenNhaCungCap,soLuongTon = a.soLuong,loai = N'" + tenLoai + "' " +
                    "FROM dbo.Linhkien a JOIN dbo.NhaCungCap b " +
                    "ON b.maNhaCungCap = a.maNhaCungCap " +
                    "WHERE a.soLuong > 0 " +
                    "ORDER BY CONVERT(INT, REPLACE(maLinhKien, 'LK-', '')) ";
            }
            else if (loai == 3)
            {
                sql = "SELECT maLinhKien,tenLinhKien,tenNhaCungCap,soLuongTon = a.soLuong,loai = N'" + tenLoai + "' " +
                    "FROM dbo.Linhkien a JOIN dbo.NhaCungCap b " +
                    "ON b.maNhaCungCap = a.maNhaCungCap " +
                    "WHERE a.soLuong = 0 " +
                    "ORDER BY CONVERT(INT, REPLACE(maLinhKien, 'LK-', '')) ";
            }
            else
            {
                sql += "GROUP BY maLinhKien,tenLinhKien,tenNhaCungCap,soLuongTon " +
                    "ORDER BY CONVERT(INT, REPLACE(maLinhKien, 'LK-', '')) ";
            }

            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

            DataSet ds = new DataSet();

            adapter.Fill(ds, "dbo.vw_ThongKeLinhKien");
            return ds;
        }
    }
}
