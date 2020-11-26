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
    public partial class ucQuanLyDonDatHang : UserControl
    {
        private bDonDatHang htDonDatHang;
        private bKhachHang htKhachHang;
        private bNhanVien htNhanVien;

        private List<eDonDatHang> ls_Temp;
        private System.Windows.Forms.TabControl tabFather;
        private bool timKiem = false;
        public bool TimKiem
        {
            get
            {
                return timKiem;
            }
            set
            {
                timKiem = value;
                if (value == true)
                {
                    dockDonDatHang.Text = "Tìm kiếm";
                    tabDonDatHang.SelectedIndex = 1;
                }
                else
                {
                    dockDonDatHang.Text = "Thông tin";
                    tabDonDatHang.SelectedIndex = 0;
                }
            }
        }

        public ucQuanLyDonDatHang(System.Windows.Forms.TabControl tabFather)
        {
            InitializeComponent();
            this.tabFather = tabFather;
            hideTabMenu();
            capNhatDanhSach();
        }

        private void hideTabMenu()
        {
            tabDonDatHang.Appearance = TabAppearance.FlatButtons;
            tabDonDatHang.ItemSize = new Size(0, 1);
            tabDonDatHang.SizeMode = TabSizeMode.Fixed;
        }

        public void capNhatDanhSach(List<eDonDatHang> ls = null)
        {
            htDonDatHang = new bDonDatHang();
            htKhachHang = new bKhachHang();
            htNhanVien = new bNhanVien();
            dgvDonDatHang.Rows.Clear();
            if (ls == null)
                ls_Temp = htDonDatHang.layDanhSachDonDatHang();
            else
                ls_Temp = ls;
            var lsAll = ls_Temp.Select(n => new
            {
                stt = int.Parse(n.MaDonDatHang.Split('-')[1]),
                MaDonDatHang = n.MaDonDatHang,
                TenKhachHang = htKhachHang.thongTinKhachHang(n.MaKhachHang).TenKhachHang,
                TenNhanVienTuVan = htNhanVien.thongTinNhanVien(n.MaNhanVienTuVan).TenNhanVien,
                TenNhanVienThuNgan = htNhanVien.thongTinNhanVien(n.MaNhanVienThuNgan).TenNhanVien,
                NgayLap = n.NgayLap,
                TongTien = n.TongTien,
                TrangThai = n.TrangThai,
                NoiNhanHang = n.NoiNhanHang
            }).OrderBy(n => n.stt);
            foreach (var item in lsAll)
            {
                dgvDonDatHang.Rows.Add();
                int stt = dgvDonDatHang.Rows.Count - 1;
                dgvDonDatHang.Rows[stt].Cells[0].Value = item.MaDonDatHang;
                dgvDonDatHang.Rows[stt].Cells[1].Value = item.TenKhachHang;
                dgvDonDatHang.Rows[stt].Cells[2].Value = item.TenNhanVienTuVan; 
                dgvDonDatHang.Rows[stt].Cells[3].Value = item.TenNhanVienThuNgan;
                dgvDonDatHang.Rows[stt].Cells[4].Value = item.NgayLap.ToShortDateString();
                dgvDonDatHang.Rows[stt].Cells[5].Value = item.TongTien;
                dgvDonDatHang.Rows[stt].Cells[6].Value = item.TrangThai;
                dgvDonDatHang.Rows[stt].Cells[7].Value = item.NoiNhanHang;
            }
        }

        private void listResize()
        {
            dgvDonDatHang.Columns[0].Width = dgvDonDatHang.Width * 10 / 100;
            dgvDonDatHang.Columns[1].Width = dgvDonDatHang.Width * 12 / 100;
            dgvDonDatHang.Columns[2].Width = dgvDonDatHang.Width * 12 / 100;
            dgvDonDatHang.Columns[3].Width = dgvDonDatHang.Width * 12 / 100;
            dgvDonDatHang.Columns[4].Width = dgvDonDatHang.Width * 10 / 100;
            dgvDonDatHang.Columns[5].Width = dgvDonDatHang.Width * 10 / 100;
            dgvDonDatHang.Columns[6].Width = dgvDonDatHang.Width * 10 / 100;
        }

        private void lsHD_Resize(object sender, EventArgs e)
        {
            listResize();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtTrangThai.Text != "Đã thanh toán")
            {
                if (txtMaDonDatHang.Text.Trim().Length > 0)
                {
                    if (MessageBoxEx.Show(this, "Bạn có muốn xoá đơn đặt hàng " + txtMaDonDatHang.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        htDonDatHang.xoaDonDatHang(txtMaDonDatHang.Text);
                        capNhatDanhSach();
                        ((ucQuanLyChiTietDonDatHang)tabFather.TabPages[3].Controls[0]).capNhatDanhSach();
                        txtMaDonDatHang.Clear();
                        txtMaKhachHang.Clear();
                        txtNoiNhanHang.Clear();
                        txtTenNhanVienTuVan.Clear();
                        txtThuNgan.Clear();
                        txtTongTien.Clear();
                        txtTrangThai.Clear();
                        dtmNgayLap.Value = DateTime.Now;
                        MessageBoxEx.Show(this, "Xoá đơn đặt hàng thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    MessageBoxEx.Show(this, "Mời chọn đơn đặt hàng cần xoá...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBoxEx.Show(this, "Chỉ có thể xoá được những hoá đơn chưa thanh toán", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
            }
        }

        private void lsHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            txtMaDonDatHang.Text = dgvDonDatHang.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtMaKhachHang.Text = dgvDonDatHang.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtTenNhanVienTuVan.Text = dgvDonDatHang.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtThuNgan.Text = dgvDonDatHang.Rows[e.RowIndex].Cells[3].Value.ToString();
            dtmNgayLap.Text = dgvDonDatHang.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtTongTien.Text = dgvDonDatHang.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtTrangThai.Text = dgvDonDatHang.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtNoiNhanHang.Text = dgvDonDatHang.Rows[e.RowIndex].Cells[7].Value.ToString();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            List<eDonDatHang> ls = htDonDatHang.layDanhSachDonDatHang()
                .Where(n =>
                CongCu.Loai.XoaUnicode(htKhachHang.thongTinKhachHang(n.MaKhachHang).TenKhachHang).Contains(CongCu.Loai.XoaUnicode(txtKeyTenKhachHang.Text)) &&
                CongCu.Loai.XoaUnicode(htNhanVien.thongTinNhanVien(n.MaNhanVienTuVan).TenNhanVien).Contains(CongCu.Loai.XoaUnicode(txtKeyNhanVienTuVan.Text)) &&
                CongCu.Loai.XoaUnicode(htNhanVien.thongTinNhanVien(n.MaNhanVienThuNgan).TenNhanVien).Contains(CongCu.Loai.XoaUnicode(txtKeyThuNgan.Text)) &&
                CongCu.Loai.XoaUnicode(n.NoiNhanHang).Contains(CongCu.Loai.XoaUnicode(txtKeyNoiNhanHang.Text))
                ).ToList();
            if (dtmKeyNgayLap.Value.Year.ToString() != "1")
                ls = ls.Where(n => n.NgayLap == dtmKeyNgayLap.Value).ToList();
            if (cboKeyTrangThai.SelectedIndex != -1)
                ls = ls.Where(n => n.TrangThai.Contains(cboKeyTrangThai.SelectedItem.ToString())).ToList();
            capNhatDanhSach(ls);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtKeyTenKhachHang.Clear();
            txtKeyNhanVienTuVan.Clear();
            txtKeyThuNgan.Clear();
            txtKeyNoiNhanHang.Clear();
            cboKeyTrangThai.SelectedIndex = -1;
            dtmNgayLap.ResetValue();
            capNhatDanhSach();
        }
        public void truyXuatDonDatHang()
        {
            if (txtMaDonDatHang.Text.Trim().Length > 0)
            {
                ((ucTruyXuatDonDatHang)tabFather.TabPages[11].Controls[0]).Ddh = htDonDatHang.thongTinDonDatHang(dgvDonDatHang.SelectedRows[0].Cells[0].Value.ToString());
                ((ucTruyXuatDonDatHang)tabFather.TabPages[11].Controls[0]).lastTabIndex = 2;
                tabFather.SelectedIndex = 11;
            }
            else
            {
                MessageBoxEx.Show(this, "Mời chọn đơn đặt hàng cần truy xuất đơn đặt hàng...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void dgvDonDatHang_DoubleClick(object sender, EventArgs e)
        {
            if (dgvDonDatHang.SelectedRows[0].Cells[6].Value.ToString() == "Đã thanh toán" && MessageBoxEx.Show(this, "Bạn có muốn in đơn đặt hàng với mã " + dgvDonDatHang.SelectedRows[0].Cells[0].Value.ToString() + " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                ((ucReport)tabFather.TabPages[19].Controls[0]).inDonDatHang(dgvDonDatHang.SelectedRows[0].Cells[0].Value.ToString());
                tabFather.SelectedIndex = 19;
            }
        }
    }
}
