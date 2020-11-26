using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entity;

namespace BLL
{
    public class bChiTietPhieuNhapKho
    {
        DataQuanLyLinhKienDataContext data;
        public bChiTietPhieuNhapKho()
        {
            data = new DataQuanLyLinhKienDataContext();
        }
        public List<eChiTietPhieuNhapKho> layDanhSachChiTietPhieuNhapKho()
        {
            var kh = data.ChiTietPhieuNhapKhos;
            List<eChiTietPhieuNhapKho> ls = new List<eChiTietPhieuNhapKho>();
            foreach (var item in kh)
            {
                ls.Add(new eChiTietPhieuNhapKho()
                {
                    GiaMua = item.giaMua,
                    MaPhieuNhapKho = item.maPhieuNhapKho,
                    MaLinhKien = item.maLinhKien,
                    SoLuong = item.soLuong,
                    ThanhTien = item.thanhTien
                });
            }
            return ls;
        }
        public eChiTietPhieuNhapKho thongTinChiTietPhieuNhapKho(string ma,string maLinhKien)
        {
            ChiTietPhieuNhapKho item = data.ChiTietPhieuNhapKhos.Single(n => n.maPhieuNhapKho == ma);
            return new eChiTietPhieuNhapKho()
            {
                GiaMua = item.giaMua,
                MaPhieuNhapKho = item.maPhieuNhapKho,
                MaLinhKien = item.maLinhKien,
                SoLuong = item.soLuong,
                ThanhTien = item.thanhTien
            };
        }
        public bool themChiTietPhieuNhapKho(eChiTietPhieuNhapKho item)
        {
            try
            {
                data.ChiTietPhieuNhapKhos.InsertOnSubmit(new ChiTietPhieuNhapKho()
                {
                    giaMua = item.GiaMua,
                    maPhieuNhapKho = item.MaPhieuNhapKho,
                    maLinhKien = item.MaLinhKien,
                    soLuong = item.SoLuong,
                    thanhTien = item.ThanhTien
                });
                data.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void suaChiTietPhieuNhapKho(eChiTietPhieuNhapKho item)
        {
            ChiTietPhieuNhapKho n = data.ChiTietPhieuNhapKhos.Single(m => m.maPhieuNhapKho == item.MaPhieuNhapKho && m.maLinhKien == item.MaLinhKien);
            n.giaMua = item.GiaMua;
            n.maPhieuNhapKho = item.MaPhieuNhapKho;
            n.maLinhKien = item.MaLinhKien;
            n.soLuong = item.SoLuong;
            n.thanhTien = item.ThanhTien;

            data.SubmitChanges();
        }
        public void xoaChiTietPhieuNhapKho(string maPNK, string maLK)
        {
            ChiTietPhieuNhapKho ct = data.ChiTietPhieuNhapKhos.Single(n => n.maPhieuNhapKho == maPNK && n.maLinhKien == maLK);
            data.ChiTietPhieuNhapKhos.DeleteOnSubmit(ct);
            data.SubmitChanges();
        }
    }
}
