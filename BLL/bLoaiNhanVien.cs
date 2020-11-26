using DAL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class bLoaiNhanVien
    {
        DataQuanLyLinhKienDataContext data;
        public bLoaiNhanVien()
        {
            data = new DataQuanLyLinhKienDataContext();
        }
        public List<eLoaiNhanVien> layDanhSachLoaiNhanVien()
        {
            List<eLoaiNhanVien> lsLLK = new List<eLoaiNhanVien>();
            var ls = data.LoaiNhanViens;
            foreach (var item in ls)
            {
                lsLLK.Add(new eLoaiNhanVien()
                {
                    MaLoaiNhanVien = item.maLoaiNhanVien,
                    TenLoaiNhanVien = item.tenLoaiNhanVien,
                    MaPhanQuyen = item.maPhanQuyen
                });
            }
            return lsLLK;
        }
        public bool themLoaiNhanVien(eLoaiNhanVien n)
        {
            try
            {
                LoaiNhanVien m = new LoaiNhanVien()
                {
                    maLoaiNhanVien = n.MaLoaiNhanVien,
                    tenLoaiNhanVien = n.TenLoaiNhanVien,
                    maPhanQuyen = n.MaPhanQuyen
                };
                data.LoaiNhanViens.InsertOnSubmit(m);
                data.SubmitChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        public void suaLoaiNhanVien(eLoaiNhanVien n)
        {
            LoaiNhanVien m = data.LoaiNhanViens.Single(o => o.maLoaiNhanVien == n.MaLoaiNhanVien);
            m.maLoaiNhanVien = n.MaLoaiNhanVien;
            m.tenLoaiNhanVien = n.TenLoaiNhanVien;
            m.maPhanQuyen = n.MaPhanQuyen;
            data.SubmitChanges();
        }
        public void xoaLoaiLinhKiem(string ma)
        {
            LoaiNhanVien n = data.LoaiNhanViens.Single(p => p.maLoaiNhanVien == ma);
            data.LoaiNhanViens.DeleteOnSubmit(n);
            data.SubmitChanges();
        }
        public eLoaiNhanVien thongTinLoaiNhanVien(string ma)
        {
            LoaiNhanVien llk;
            llk = data.LoaiNhanViens.Single(n => n.maLoaiNhanVien == ma);
            return new eLoaiNhanVien()
            {
                MaLoaiNhanVien = llk.maLoaiNhanVien,
                TenLoaiNhanVien = llk.tenLoaiNhanVien,
                MaPhanQuyen = llk.maPhanQuyen
            };
        }
    }
}
