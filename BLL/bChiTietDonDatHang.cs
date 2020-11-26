using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL;

namespace BLL
{
    public class bChiTietDonDatHang
    {
        DataQuanLyLinhKienDataContext data;
        public bChiTietDonDatHang()
        {
            data = new DataQuanLyLinhKienDataContext();
        }
        public List<eChiTietDonDatHang> layDanhSachChiTietDonDatHang()
        {
            var kh = data.ChiTietDonDatHangs;
            List<eChiTietDonDatHang> ls = new List<eChiTietDonDatHang>();
            foreach (var item in kh)
            {
                ls.Add(new eChiTietDonDatHang()
                {
                    MaDonDatHang = item.maDonDatHang,
                    MaLinhKien = item.maLinhKien,
                    SoLuong = item.soLuong,
                    GiaBan = item.giaBan,
                    MucGiamGia = item.mucGiamGia,
                    ThanhTien = item.thanhTien
                });
            }
            return ls;
        }
        public eChiTietDonDatHang thongTinChiTietDonDatHang(string ma)
        {
            ChiTietDonDatHang ct = data.ChiTietDonDatHangs.Single(n => n.maDonDatHang == ma);
            return new eChiTietDonDatHang()
            {
                MaDonDatHang = ct.maDonDatHang,
                GiaBan = ct.giaBan,
                MaLinhKien = ct.maLinhKien,
                MucGiamGia = ct.mucGiamGia,
                SoLuong = ct.soLuong,
                ThanhTien = ct.thanhTien
            };
        }
        public bool themChiTietDonDatHang(eChiTietDonDatHang ct)
        {
            try
            {
                data.ChiTietDonDatHangs.InsertOnSubmit(new ChiTietDonDatHang()
                {
                    maDonDatHang = ct.MaDonDatHang,
                    giaBan = ct.GiaBan,
                    maLinhKien = ct.MaLinhKien,
                    mucGiamGia = ct.MucGiamGia,
                    soLuong = ct.SoLuong,
                    thanhTien = ct.ThanhTien
                });
                data.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void suaChiTietDonDatHang(eChiTietDonDatHang ctddh)
        {
            ChiTietDonDatHang n = data.ChiTietDonDatHangs.Single(m => m.maDonDatHang == ctddh.MaDonDatHang && m.maLinhKien==ctddh.MaLinhKien);
            n.maDonDatHang = ctddh.MaDonDatHang;
            n.giaBan = ctddh.GiaBan;
            n.maLinhKien = ctddh.MaLinhKien;
            n.mucGiamGia = ctddh.MucGiamGia;
            n.soLuong = ctddh.SoLuong;
            n.thanhTien = ctddh.ThanhTien;

            data.SubmitChanges();
        }
        public void xoaChiTietDonDatHang(string maDDH, string maLK)
        {
            ChiTietDonDatHang ct = data.ChiTietDonDatHangs.Single(n => n.maDonDatHang == maDDH && n.maLinhKien == maLK);
            data.ChiTietDonDatHangs.DeleteOnSubmit(ct);
            data.SubmitChanges();
        }
        
    }
}
