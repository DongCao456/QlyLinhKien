using DAL;
using Entity;
using System;
using System.Linq;

namespace BLL
{
    public class bTaiKhoan
    {
        DataQuanLyLinhKienDataContext data;
        public bTaiKhoan()
        {
            data = new DataQuanLyLinhKienDataContext();
        }
        public string layMatKhauTheoMaTaiKhoan(string ma)
        {
            try
            {
                return data.TaiKhoans.Single(n => n.maTaiKhoan == ma).matKhau;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public eTaiKhoan thongTinTaiKhoan(string maTaiKhoan)
        {
            TaiKhoan tk = data.TaiKhoans.Single(n => n.maTaiKhoan == maTaiKhoan);

            return new eTaiKhoan
            {
                MaTaiKhoan = tk.maTaiKhoan,
                MatKhau = tk.maTaiKhoan
            };
        }
        public void suaTaiKhoan(eTaiKhoan tk)
        {
            TaiKhoan t = data.TaiKhoans.Single(n => n.maTaiKhoan == tk.MaTaiKhoan);
            t.matKhau = tk.MatKhau;
            data.SubmitChanges();
        }
    }
}
