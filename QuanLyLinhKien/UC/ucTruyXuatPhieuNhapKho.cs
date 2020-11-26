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

namespace QuanLyLinhKien.UC
{
    public partial class ucTruyXuatPhieuNhapKho : UserControl
    {
        private bPhieuNhapKho htPhieuNhapKho;
        private bChiTietPhieuNhapKho htChiTietPhieuNhapKho;
        private bLinhKien htLinhKien;
        private bNhaCungCap htNhaCungCap;
        private bNhanVien htNhanVien;

        private System.Windows.Forms.TabControl tabFather;
        public int lastTabIndex = 0;

        private eNhanVien nv = null;
        private eNhaCungCap ncc = null;
        private ePhieuNhapKho pnk = null;

        private List<ePhieuNhapKho> ls_Temp;

        public eNhanVien Nv
        {
            set
            {
                capNhatDuLieu();
                ls_Temp = htPhieuNhapKho.layDanhSachPhieuNhapKho().Where(n => n.MaNhanVienThuKho == value.MaNhanVien || n.MaNhanVienKeToanKho == value.MaNhanVien).Where(n=>n.TrangThai !="Chưa thanh toán").ToList();
                capNhatDanhSachPhieuNhapKho();
                nv = value;
            }
        }
        public eNhaCungCap Ncc
        {
            set
            {
                capNhatDuLieu();
                ls_Temp = htPhieuNhapKho.layDanhSachPhieuNhapKho().Where(n => n.MaNhaCungCap == value.MaNhaCungCap).Where(n => n.TrangThai != "Chưa thanh toán").ToList();
                capNhatDanhSachPhieuNhapKho();
                ncc = value;
            }
        }

        public ePhieuNhapKho Pnk
        {
            set
            {
                capNhatDuLieu();
                ls_Temp = htPhieuNhapKho.layDanhSachPhieuNhapKho().Where(n => n.MaPhieuNhapKho == value.MaPhieuNhapKho).ToList();
                capNhatDanhSachPhieuNhapKho();
                pnk = value;
            }
        }

        public ucTruyXuatPhieuNhapKho(System.Windows.Forms.TabControl tabFather)
        {
            InitializeComponent();
            this.tabFather = tabFather;
        }

        private void capNhatDuLieu()
        {
            htPhieuNhapKho = new bPhieuNhapKho();
            htChiTietPhieuNhapKho = new bChiTietPhieuNhapKho();
            htLinhKien = new bLinhKien();
            htNhaCungCap = new bNhaCungCap();
            htNhanVien = new bNhanVien();
        }

        public void capNhatDanhSachPhieuNhapKho(List<ePhieuNhapKho> ls = null)
        {
            capNhatDuLieu();
            dgvPhieuNhapKho.Rows.Clear();
            dgvChiTietPhieuNhapKho.Rows.Clear();
            if (ls != null)
                ls_Temp = ls;
            var lsAll = ls_Temp.Select(n => new
            {
                stt = int.Parse(n.MaPhieuNhapKho.Split('-')[1]),
                MaPhieuNhapKho = n.MaPhieuNhapKho,
                TenNhaCungCap = htNhaCungCap.thongTinNhaCungCap(n.MaNhaCungCap).TenNhaCungCap,
                TenNhanVienThuKho = htNhanVien.thongTinNhanVien(n.MaNhanVienThuKho).TenNhanVien,
                TenNhanVienKeToanKho = htNhanVien.thongTinNhanVien(n.MaNhanVienKeToanKho).TenNhanVien,
                NgayLap = n.NgayLap

            }).OrderBy(n => n.stt);
            foreach (var item in lsAll)
            {
                dgvPhieuNhapKho.Rows.Add();
                int stt = dgvPhieuNhapKho.Rows.Count - 1;
                dgvPhieuNhapKho.Rows[stt].Cells[0].Value = item.MaPhieuNhapKho;
                dgvPhieuNhapKho.Rows[stt].Cells[1].Value = item.TenNhaCungCap;
                dgvPhieuNhapKho.Rows[stt].Cells[2].Value = item.TenNhanVienThuKho;
                dgvPhieuNhapKho.Rows[stt].Cells[3].Value = item.TenNhanVienKeToanKho;
                dgvPhieuNhapKho.Rows[stt].Cells[4].Value = item.NgayLap;
            }
        }

        private void capNhatDanhSachChiTietDonDatHang(List<eChiTietPhieuNhapKho> ls)
        {
            dgvChiTietPhieuNhapKho.Rows.Clear();
            var lsAll = ls.Select(n => new
            {
                stt = int.Parse(n.MaPhieuNhapKho.Split('-')[1]),
                MaPhieuNhapKho = n.MaPhieuNhapKho,
                TenLinhKien = htLinhKien.thongTinLinhKien(n.MaLinhKien).TenLinhKien,
                SoLuong = n.SoLuong,
                GiaMua = n.GiaMua
            }).OrderBy(n => n.stt);
            foreach (var item in lsAll)
            {
                dgvChiTietPhieuNhapKho.Rows.Add();
                int stt = dgvChiTietPhieuNhapKho.Rows.Count - 1;
                dgvChiTietPhieuNhapKho.Rows[stt].Cells[0].Value = item.MaPhieuNhapKho;
                dgvChiTietPhieuNhapKho.Rows[stt].Cells[1].Value = item.TenLinhKien;
                dgvChiTietPhieuNhapKho.Rows[stt].Cells[2].Value = item.SoLuong;
                dgvChiTietPhieuNhapKho.Rows[stt].Cells[3].Value = item.GiaMua;
            }
        }

        private void ucTruyXuatPhieuNhapKho_Load(object sender, EventArgs e)
        {
            bar1.Font = new Font("Microsoft Sans Serif", 10);
            dgvPhieuNhapKho.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9.25f);
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            tabFather.SelectedIndex = lastTabIndex;
        }

        private void dgvPhieuNhapKho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            capNhatDanhSachChiTietDonDatHang(htChiTietPhieuNhapKho.layDanhSachChiTietPhieuNhapKho().Where(n => n.MaPhieuNhapKho == dgvPhieuNhapKho.Rows[e.RowIndex].Cells[0].Value.ToString()).ToList());
        }

        private void dgvChiTietPhieuNhapKho_Resize(object sender, EventArgs e)
        {
            dgvChiTietPhieuNhapKho.Columns[0].Width = dgvChiTietPhieuNhapKho.Width * 20 / 100;
            dgvChiTietPhieuNhapKho.Columns[1].Width = dgvChiTietPhieuNhapKho.Width * 30 / 100;
            dgvChiTietPhieuNhapKho.Columns[2].Width = dgvChiTietPhieuNhapKho.Width * 20 / 100;
        }

        private void dgvPhieuNhapKho_Resize(object sender, EventArgs e)
        {
            dgvPhieuNhapKho.Columns[0].Width = dgvPhieuNhapKho.Width * 14 / 100;
            dgvPhieuNhapKho.Columns[1].Width = dgvPhieuNhapKho.Width * 24 / 100;
            dgvPhieuNhapKho.Columns[2].Width = dgvPhieuNhapKho.Width * 24 / 100;
            dgvPhieuNhapKho.Columns[3].Width = dgvPhieuNhapKho.Width * 24 / 100;
        }
    }
}
