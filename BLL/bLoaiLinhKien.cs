using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL;
namespace BLL
{
    public class bLoaiLinhKien
    {
        DataQuanLyLinhKienDataContext data;
        public bLoaiLinhKien()
        {
            data = new DataQuanLyLinhKienDataContext();
        }
        public List<eLoaiLinhKien> layDanhSachLoaiLinhKien()
        {
            List<eLoaiLinhKien> lsLLK = new List<eLoaiLinhKien>();
            var ls = data.LoaiLinhKiens;
            foreach (var item in ls)
            {
                lsLLK.Add(new eLoaiLinhKien()
                {
                    MaLoai = item.maLoai,
                    TenLoai = item.tenLoai
                });
            }
            return lsLLK;
        }
        public bool themLoaiLinhKien(eLoaiLinhKien n)
        {
            try
            {
                LoaiLinhKien m = new LoaiLinhKien()
                {
                    maLoai = n.MaLoai,
                    tenLoai = n.TenLoai
                };
                data.LoaiLinhKiens.InsertOnSubmit(m);
                data.SubmitChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        public void suaLoaiLinhKien(eLoaiLinhKien n)
        {
            LoaiLinhKien m = data.LoaiLinhKiens.Single(o => o.maLoai == n.MaLoai);
            m.maLoai = n.MaLoai;
            m.tenLoai = n.TenLoai;
            data.SubmitChanges();
        }
        public void xoaLoaiLinhKiem(string ma)
        {
            LoaiLinhKien n = data.LoaiLinhKiens.Single(p => p.maLoai == ma);
            data.LoaiLinhKiens.DeleteOnSubmit(n);
            data.SubmitChanges();
        }
        public eLoaiLinhKien thongTinLoaiLinhKien(string ma)
        {
            LoaiLinhKien llk;
            llk = data.LoaiLinhKiens.Single(n => n.maLoai == ma);
            return new eLoaiLinhKien()
            {
                MaLoai = llk.maLoai,
                TenLoai = llk.tenLoai
            };
        }

    }
}
