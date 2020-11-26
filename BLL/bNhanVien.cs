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
    public class bNhanVien
    {
        DataQuanLyLinhKienDataContext data;
        public bNhanVien()
        {
            data = new DataQuanLyLinhKienDataContext();
        }
        public List<eNhanVien> layDanhSachNhanVien()
        {
            List<eNhanVien> lsNhanVien = new List<eNhanVien>();
            var ls = data.NhanViens;
            foreach (var item in ls)
            {
                lsNhanVien.Add(new eNhanVien()
                {
                    CMND = item.cMND,
                    DiaChi = item.diaChi,
                    EMail = item.eMail,
                    MaNhanVien = item.maNhanVien,
                    SoDienThoai = item.soDienThoai,
                    TenNhanVien = item.tenNhanVien,
                    TrangThai = item.trangThai,
                    MaLoaiNhanVien = item.maLoaiNhanVien
                });
            }
            return lsNhanVien;
        }
        public eNhanVien thongTinNhanVien(string ma)
        {
            NhanVien nv = data.NhanViens.Single(n => n.maNhanVien == ma);
            return new eNhanVien()
            {
                CMND = nv.cMND,
                DiaChi = nv.diaChi,
                EMail = nv.eMail,
                MaNhanVien = nv.maNhanVien,
                SoDienThoai = nv.soDienThoai,
                TenNhanVien = nv.tenNhanVien,
                TrangThai = nv.trangThai,
                MaLoaiNhanVien = nv.maLoaiNhanVien
            };
        }
        public bool themNhanVien(eNhanVien nv)
        {
            try
            {
                data.NhanViens.InsertOnSubmit(new NhanVien()
                {
                    cMND = nv.CMND,
                    diaChi = nv.DiaChi,
                    eMail = nv.EMail,
                    maNhanVien = nv.MaNhanVien,
                    soDienThoai = nv.SoDienThoai,
                    tenNhanVien = nv.TenNhanVien,
                    trangThai = nv.TrangThai,
                    maLoaiNhanVien = nv.MaLoaiNhanVien
                });
                data.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void suaNhanVien(eNhanVien nv)
        {
            NhanVien n = data.NhanViens.Single(m => m.maNhanVien == nv.MaNhanVien);
            n.cMND = nv.CMND;
            n.diaChi = nv.DiaChi;
            n.eMail = nv.EMail;
            n.maNhanVien = nv.MaNhanVien;
            n.soDienThoai = nv.SoDienThoai;
            n.tenNhanVien = nv.TenNhanVien;
            n.trangThai = nv.TrangThai;
            n.maLoaiNhanVien = nv.MaLoaiNhanVien;
            data.SubmitChanges();
        }
        public bool xoaNhanVien(string ma)
        {
            try
            {
                data.TaiKhoans.DeleteOnSubmit(data.TaiKhoans.Single(n => n.maTaiKhoan == ma));
                data.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public DataSet inThongKe(DateTime ngayBatDau, DateTime ngayKetThuc, decimal loai, string tenLoai, decimal soLuong)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["QuanLyLinhKien.Properties.Settings.QuanLyLinhKienConnectionString"].ToString();
            string sql = "SELECT TOP " + soLuong + " maNhanVien,tenNhanVien,tongSoLuong = SUM(tongSoLuong),tongDonDatHang = COUNT(*),tongTien = SUM(tongTien),ngayLap=N'asdasd',ngayBatDau = N'" + ngayBatDau.ToShortDateString() + "',ngayKetThuc=N'" + ngayKetThuc.ToShortDateString() + "',loai=N'" + tenLoai + "'" +
                "FROM dbo.vw_ThongKeNhanVien " +
                "WHERE ngayLap BETWEEN '" + ngayBatDau.Year + "/" + ngayBatDau.Month + "/" + ngayBatDau.Day + "' AND '" + ngayKetThuc.Year + "/" + ngayKetThuc.Month + "/" + ngayKetThuc.Day + "' " +
                "GROUP BY maNhanVien,tenNhanVien ";
                
            if(loai==0)
                sql += "ORDER BY CONVERT(INT, REPLACE(maNhanVien,'NV-','')) ";
            else if (loai==1)
                sql += "ORDER BY tongTien DESC";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

            DataSet ds = new DataSet();

            adapter.Fill(ds, "[dbo].[vw_ThongKeNhanVien]");
            return ds;
        }
    }
}
