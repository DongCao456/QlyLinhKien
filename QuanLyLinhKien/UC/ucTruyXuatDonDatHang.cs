using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using Entity;
using DevComponents.DotNetBar;

namespace QuanLyLinhKien.UC
{
    public partial class ucTruyXuatDonDatHang : UserControl
    {
        private bDonDatHang htDonDatHang;
        private bChiTietDonDatHang htChiTietDonDatHang;
        private bLinhKien htLinhKien;
        private bKhachHang htKhachhang;
        private bNhanVien htNhanVien;

        private System.Windows.Forms.TabControl tabFather;
        public int lastTabIndex = 0;

        private eNhanVien nv = null;
        private eKhachHang kh = null;
        private eLinhKien lk = null;
        private eDonDatHang ddh = null;
        

        private List<eDonDatHang> ls_Temp;

        public eNhanVien Nv
        {
            set
            {
                capNhatDuLieu();
                ls_Temp = htDonDatHang.layDanhSachDonDatHang().Where(n => n.MaNhanVienThuNgan == value.MaNhanVien || n.MaNhanVienTuVan == value.MaNhanVien).Where(n => n.TrangThai != "Chưa thanh toán").ToList();
                capNhatDanhSachDonDatHang();
                nv = value;
            }
        }
        public eKhachHang Kh
        {
            set
            {
                capNhatDuLieu();
                ls_Temp = htDonDatHang.layDanhSachDonDatHang().Where(n => n.MaKhachHang == value.MaKhachHang).Where(n => n.TrangThai != "Chưa thanh toán").ToList();
                capNhatDanhSachDonDatHang();
                kh = value;
            }
        }

        public eLinhKien Lk
        {
            set
            {
                capNhatDuLieu();
                ls_Temp = htDonDatHang.layDanhSachDonDatHang()
                    .Where(n => htChiTietDonDatHang.layDanhSachChiTietDonDatHang()
                    .Where(m => m.MaLinhKien == value.MaLinhKien).Any(m => m.MaDonDatHang == n.MaDonDatHang) && n.TrangThai != "Chưa thanh toán").ToList();
                capNhatDanhSachDonDatHang();
                lk = value;
            }
        }

        public eDonDatHang Ddh
        {
            set
            {
                capNhatDuLieu();
                ls_Temp = htDonDatHang.layDanhSachDonDatHang().Where(n => n.MaDonDatHang == value.MaDonDatHang).ToList();
                capNhatDanhSachDonDatHang();
                ddh = value;
            }
        }

        public ucTruyXuatDonDatHang(System.Windows.Forms.TabControl tabFather)
        {
            InitializeComponent();
            this.tabFather = tabFather;
        }
        private void capNhatDuLieu()
        {
            htDonDatHang = new bDonDatHang();
            htChiTietDonDatHang = new bChiTietDonDatHang();
            htLinhKien = new bLinhKien();
            htKhachhang = new bKhachHang();
            htNhanVien = new bNhanVien();
        }
        public void capNhatDanhSachDonDatHang(List<eDonDatHang> ls = null)
        {
            
            dgvDonDatHang.Rows.Clear();
            dgvChiTietDonDatHang.Rows.Clear();
            if (ls != null)
            {
                capNhatDuLieu();
                ls_Temp = ls;
            }
            var lsAll = ls_Temp.Select(n => new
            {
                stt = int.Parse(n.MaDonDatHang.Split('-')[1]),
                MaDonDatHang = n.MaDonDatHang,
                TenKhachHang = htKhachhang.thongTinKhachHang(n.MaKhachHang).TenKhachHang,
                TenNhanVienTuVan = htNhanVien.thongTinNhanVien(n.MaNhanVienTuVan).TenNhanVien,
                TenNhanVienThuNgan = htNhanVien.thongTinNhanVien(n.MaNhanVienThuNgan).TenNhanVien,
                NgayLap = n.NgayLap
            }).OrderBy(n => n.stt);
            foreach (var item in lsAll)
            {
                dgvDonDatHang.Rows.Add();
                int stt = dgvDonDatHang.Rows.Count - 1;
                dgvDonDatHang.Rows[stt].Cells[0].Value = item.MaDonDatHang;
                dgvDonDatHang.Rows[stt].Cells[1].Value = item.TenKhachHang;
                dgvDonDatHang.Rows[stt].Cells[2].Value = item.TenNhanVienTuVan;
                dgvDonDatHang.Rows[stt].Cells[3].Value = item.TenNhanVienThuNgan;
                dgvDonDatHang.Rows[stt].Cells[4].Value = item.NgayLap;
            }
        }

        private void capNhatDanhSachChiTietDonDatHang(List<eChiTietDonDatHang> ls)
        {
            dgvChiTietDonDatHang.Rows.Clear();
            var lsAll = ls.Select(n => new
            {
                stt = int.Parse(n.MaDonDatHang.Split('-')[1]),
                MaDonDatHang = n.MaDonDatHang,
                TenLinhKien = htLinhKien.thongTinLinhKien(n.MaLinhKien).TenLinhKien,
                SoLuong = n.SoLuong,
                GiaBan = n.GiaBan,
                MucGiamGia = n.MucGiamGia
            }).OrderBy(n => n.stt);
            foreach (var item in lsAll)
            {
                dgvChiTietDonDatHang.Rows.Add();
                int stt = dgvChiTietDonDatHang.Rows.Count - 1;
                dgvChiTietDonDatHang.Rows[stt].Cells[0].Value = item.MaDonDatHang;
                dgvChiTietDonDatHang.Rows[stt].Cells[1].Value = item.TenLinhKien;
                dgvChiTietDonDatHang.Rows[stt].Cells[2].Value = item.SoLuong;
                dgvChiTietDonDatHang.Rows[stt].Cells[3].Value = item.GiaBan;
                dgvChiTietDonDatHang.Rows[stt].Cells[4].Value = item.MucGiamGia;
            }
        }

        private void ucTruyXuatHoaDon_Load(object sender, EventArgs e)
        {
            bar1.Font = new Font("Microsoft Sans Serif", 10);
            dgvDonDatHang.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9.25f);
        }

        private void lsDDH_Resize(object sender, EventArgs e)
        {
            dgvDonDatHang.Columns[0].Width = dgvDonDatHang.Width * 14 / 100;
            dgvDonDatHang.Columns[1].Width = dgvDonDatHang.Width * 24 / 100;
            dgvDonDatHang.Columns[2].Width = dgvDonDatHang.Width * 24 / 100;
            dgvDonDatHang.Columns[3].Width = dgvDonDatHang.Width * 24 / 100;
        }

        private void dgvChiTietDonDatHang_Resize(object sender, EventArgs e)
        {
            dgvChiTietDonDatHang.Columns[0].Width = dgvChiTietDonDatHang.Width * 20 / 100;
            dgvChiTietDonDatHang.Columns[1].Width = dgvChiTietDonDatHang.Width * 20 / 100;
            dgvChiTietDonDatHang.Columns[2].Width = dgvChiTietDonDatHang.Width * 20 / 100;
            dgvChiTietDonDatHang.Columns[3].Width = dgvChiTietDonDatHang.Width * 20 / 100;
        }

        private void lsDDH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
                capNhatDanhSachChiTietDonDatHang(htChiTietDonDatHang.layDanhSachChiTietDonDatHang().Where(n => n.MaDonDatHang == dgvDonDatHang.Rows[e.RowIndex].Cells[0].Value.ToString()).ToList());
        }


        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            tabFather.SelectedIndex = lastTabIndex;
        }
    }
}
