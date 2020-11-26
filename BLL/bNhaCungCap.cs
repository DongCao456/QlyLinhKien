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
    public class bNhaCungCap
    {
        DataQuanLyLinhKienDataContext data;
        public bNhaCungCap()
        {
            data = new DataQuanLyLinhKienDataContext();
        }
        public List<eNhaCungCap> layDanhSachNhaCungCap()
        {
            List<eNhaCungCap> lsNhaCungCap = new List<eNhaCungCap>();
            var ls = data.NhaCungCaps;
            foreach (var item in ls)
            {
                lsNhaCungCap.Add(new eNhaCungCap()
                {
                    DiaChi = item.diaChi,
                    EMail = item.eMail,
                    MaNhaCungCap = item.maNhaCungCap,
                    SoDienThoai = item.soDienThoai,
                    TenNhaCungCap = item.tenNhaCungCap,
                    QuocGia = item.quocGia
                });
            }
            return lsNhaCungCap;
        }
        public bool themNhaCungCap(eNhaCungCap n)
        {
            try
            {
                NhaCungCap ncc = new NhaCungCap()
                {
                    diaChi = n.DiaChi,
                    eMail = n.EMail,
                    maNhaCungCap = n.MaNhaCungCap,
                    soDienThoai = n.SoDienThoai,
                    tenNhaCungCap = n.TenNhaCungCap,
                    quocGia = n.QuocGia
                };
                data.NhaCungCaps.InsertOnSubmit(ncc);
                data.SubmitChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public void suaNhaCungCap(eNhaCungCap n)
        {
            NhaCungCap ncc = data.NhaCungCaps.Single(m => m.maNhaCungCap == n.MaNhaCungCap);
            ncc.diaChi = n.DiaChi;
            ncc.eMail = n.EMail;
            ncc.maNhaCungCap = n.MaNhaCungCap;
            ncc.soDienThoai = n.SoDienThoai;
            ncc.tenNhaCungCap = n.TenNhaCungCap;
            ncc.quocGia = n.QuocGia;
            data.SubmitChanges();
        }
        public void xoaNhaCungCap(string ma)
        {
            NhaCungCap ncc = data.NhaCungCaps.Single(m => m.maNhaCungCap == ma);
            data.NhaCungCaps.DeleteOnSubmit(ncc);
            data.SubmitChanges();
        }
        public eNhaCungCap thongTinNhaCungCap(string ma)
        {
            NhaCungCap ncc = data.NhaCungCaps.Single(n => n.maNhaCungCap == ma);
            return new eNhaCungCap()
            {
                DiaChi = ncc.diaChi,
                EMail = ncc.eMail,
                MaNhaCungCap = ncc.maNhaCungCap,
                SoDienThoai = ncc.soDienThoai,
                TenNhaCungCap = ncc.tenNhaCungCap,
                QuocGia = ncc.quocGia
            };
        }
        public DataSet inThongKe(DateTime ngayBatDau, DateTime ngayKetThuc, decimal loai, string tenLoai, decimal soLuong)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["QuanLyLinhKien.Properties.Settings.QuanLyLinhKienConnectionString"].ToString();
            string sql = "SELECT TOP 10 maNhaCungCap,tenNhaCungCap,tongSoPhieuNhapKho,soLuong = SUM(soLuong),thanhTien = SUM(thanhTien ), ngayBatDau = N'" + ngayBatDau.ToShortDateString() + "', ngayKetThuc = N'" + ngayKetThuc.ToShortDateString() + "', loai = N'" + tenLoai + "'" +
                "FROM vw_ThongKeNhaCungCap " +
                "WHERE ngayLap BETWEEN '" + ngayBatDau.Year + "/" + ngayBatDau.Month + "/" + ngayBatDau.Day + "' AND '" + ngayKetThuc.Year + "/" + ngayKetThuc.Month + "/" + ngayKetThuc.Day + "' " +
                "GROUP BY maNhaCungCap,tenNhaCungCap,tongSoPhieuNhapKho ";
            if (loai == 0)
            {
                sql += "ORDER BY CONVERT(INT,REPLACE(maNhaCungCap,'NCC-','')) ";
            }
            else
            {
                sql += "ORDER BY thanhTien DESC ";
            }
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

            DataSet ds = new DataSet();

            adapter.Fill(ds, "vw_ThongKeNhaCungCap");
            return ds;
        }
    }
}
