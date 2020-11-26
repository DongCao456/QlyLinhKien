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
    public partial class ucQuanLyPhieuNhapKho : UserControl
    {
        private bPhieuNhapKho htPhieuNhapKho;
        private bNhaCungCap htNhaCungCap;
        private bNhanVien htNhanVien;

        private List<ePhieuNhapKho> ls_Temp;
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
                    tabDonNhapHang.SelectedIndex = 1;
                }
                else
                {
                    dockDonDatHang.Text = "Thông tin";
                    tabDonNhapHang.SelectedIndex = 0;
                }
            }
        }
        public ucQuanLyPhieuNhapKho(System.Windows.Forms.TabControl tabFather)
        {
            InitializeComponent();
            this.tabFather = tabFather;
            hideTabMenu();
            capNhatDanhSach();
        }
        private void hideTabMenu()
        {
            tabDonNhapHang.Appearance = TabAppearance.FlatButtons;
            tabDonNhapHang.ItemSize = new Size(0, 1);
            tabDonNhapHang.SizeMode = TabSizeMode.Fixed;
        }
        public void capNhatDanhSach(List<ePhieuNhapKho> ls = null)
        {
            htPhieuNhapKho = new bPhieuNhapKho();
            htNhaCungCap = new bNhaCungCap();
            htNhanVien = new bNhanVien();
            dgvDonNhapHang.Rows.Clear();
            if (ls == null)
                ls_Temp = htPhieuNhapKho.layDanhSachPhieuNhapKho();
            else
                ls_Temp = ls;

            var lsAll = ls_Temp.Select(n => new
            {
                stt = int.Parse(n.MaPhieuNhapKho.Split('-')[1]),
                MaPhieuNhapKho = n.MaPhieuNhapKho,
                TenNhaCungCap = htNhaCungCap.thongTinNhaCungCap(n.MaNhaCungCap).TenNhaCungCap,
                TenNhanVienThuKho = htNhanVien.thongTinNhanVien(n.MaNhanVienThuKho).TenNhanVien,
                TenNhanVienKeToanKho = htNhanVien.thongTinNhanVien(n.MaNhanVienKeToanKho).TenNhanVien,
                NgayLap = n.NgayLap,
                TongTien = n.TongTien,
                TrangThai = n.TrangThai
            }).OrderBy(n => n.stt);

            foreach (var item in lsAll)
            {
                dgvDonNhapHang.Rows.Add();
                int stt = dgvDonNhapHang.Rows.Count - 1;
                dgvDonNhapHang.Rows[stt].Cells[0].Value = item.MaPhieuNhapKho;
                dgvDonNhapHang.Rows[stt].Cells[1].Value = item.TenNhaCungCap;
                dgvDonNhapHang.Rows[stt].Cells[2].Value = item.TenNhanVienThuKho;
                dgvDonNhapHang.Rows[stt].Cells[3].Value = item.TenNhanVienKeToanKho;
                dgvDonNhapHang.Rows[stt].Cells[4].Value = item.NgayLap.ToShortDateString();
                dgvDonNhapHang.Rows[stt].Cells[5].Value = item.TongTien;
                dgvDonNhapHang.Rows[stt].Cells[6].Value = item.TrangThai;
            }
        }

        private void listResize()
        {
            dgvDonNhapHang.Columns[0].Width = dgvDonNhapHang.Width * 10 / 100;
            dgvDonNhapHang.Columns[1].Width = dgvDonNhapHang.Width * 12 / 100;
            dgvDonNhapHang.Columns[2].Width = dgvDonNhapHang.Width * 12 / 100;
            dgvDonNhapHang.Columns[3].Width = dgvDonNhapHang.Width * 12 / 100;
            dgvDonNhapHang.Columns[4].Width = dgvDonNhapHang.Width * 10 / 100;
            dgvDonNhapHang.Columns[5].Width = dgvDonNhapHang.Width * 10 / 100;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtTrangThai.Text != "Đã thanh toán")
            {
                if (txtMaPhieuNhapKho.Text.Trim().Length > 0)
                {
                    if (MessageBoxEx.Show(this, "Bạn có muốn xoá phiếu nhập kho " + txtMaPhieuNhapKho.Text + "\n Lưu ý: tất cả chi tiết phiếu nhập kho sẽ bị xoá ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        htPhieuNhapKho.xoaPhieuNhapKho(txtMaPhieuNhapKho.Text);
                        capNhatDanhSach();
                        MessageBoxEx.Show(this, "Xoá phiếu nhập kho thành công...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        txtKeToanKho.Clear();
                        txtMaPhieuNhapKho.Clear();
                        txtNhaCungCap.Clear();
                        txtThuKho.Clear();
                        txtTongTien.Clear();
                        txtTrangThai.Clear();
                        dtmNgayLap.Value = DateTime.Now;
                    }
                }
                else
                {
                    MessageBoxEx.Show(this, "Mời chọn đơn phiếu nhập kho cần xoá...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBoxEx.Show(this, "Chỉ có thể xoá được những phiếu nhập kho chưa thanh toán", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
            }
        }

        private void dgvDonNhapHang_Resize(object sender, EventArgs e)
        {
            listResize();
        }

        private void dgvDonNhapHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            txtMaPhieuNhapKho.Text = dgvDonNhapHang.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtNhaCungCap.Text = dgvDonNhapHang.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtThuKho.Text = dgvDonNhapHang.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtKeToanKho.Text = dgvDonNhapHang.Rows[e.RowIndex].Cells[3].Value.ToString();
            dtmNgayLap.Text = dgvDonNhapHang.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtTongTien.Text = dgvDonNhapHang.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtTrangThai.Text = dgvDonNhapHang.Rows[e.RowIndex].Cells[6].Value.ToString();
        }
        public void truyXuatPhieuNhapKho()
        {
            if (txtMaPhieuNhapKho.Text.Trim().Length > 0)
            {
                ((ucTruyXuatPhieuNhapKho)tabFather.TabPages[18].Controls[0]).Pnk = htPhieuNhapKho.thongTinPhieuNhapKho(txtMaPhieuNhapKho.Text);
                ((ucTruyXuatPhieuNhapKho)tabFather.TabPages[18].Controls[0]).lastTabIndex = 14;
                tabFather.SelectedIndex = 18;
            }
            else
            {
                MessageBoxEx.Show(this, "Mời chọn phiếu nhập kho cần truy xuất phiếu nhập kho...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            List<ePhieuNhapKho> ls = htPhieuNhapKho.layDanhSachPhieuNhapKho()
                .Where(n =>
                CongCu.Loai.XoaUnicode(htNhaCungCap.thongTinNhaCungCap(n.MaNhaCungCap).TenNhaCungCap).Contains(CongCu.Loai.XoaUnicode(txtKeyNhaCungCap.Text)) &&
                CongCu.Loai.XoaUnicode(htNhanVien.thongTinNhanVien(n.MaNhanVienThuKho).TenNhanVien).Contains(CongCu.Loai.XoaUnicode(txtKeyThuKho.Text)) &&
                CongCu.Loai.XoaUnicode(htNhanVien.thongTinNhanVien(n.MaNhanVienKeToanKho).TenNhanVien).Contains(CongCu.Loai.XoaUnicode(txtKeyKeToanKho.Text)) &&
                n.TongTien.ToString().Contains(txtKeyTongTien.Text)
                ).ToList();
            if (dtmKeyNgayLap.Value.Year.ToString() != "1")
                ls = ls.Where(n => n.NgayLap == dtmKeyNgayLap.Value).ToList();
            if (cboKeyTrangThai.SelectedIndex != -1)
                ls = ls.Where(n => n.TrangThai.Contains(cboKeyTrangThai.SelectedItem.ToString())).ToList();
            capNhatDanhSach(ls);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtKeyNhaCungCap.Clear();
            txtKeyThuKho.Clear();
            txtKeyKeToanKho.Clear();
            txtKeyTongTien.Clear();
            cboKeyTrangThai.SelectedIndex = -1;
            dtmNgayLap.ResetValue();
            capNhatDanhSach();
        }

        private void dgvDonNhapHang_DoubleClick(object sender, EventArgs e)
        {
            if(txtTrangThai.Text == "Chưa thanh toán")
            {
                if (MessageBoxEx.Show(this, "Bạn có muốn in phiếu nhập kho " + txtMaPhieuNhapKho.Text + " ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    ((ucReport)tabFather.TabPages[19].Controls[0]).inPhieuNhapKho(txtMaPhieuNhapKho.Text);
                    tabFather.SelectedIndex = 19;
                }
            }
            else
            {
                MessageBoxEx.Show(this, "Không thể in phiếu nhập kho " + txtMaPhieuNhapKho.Text + " vì đã thanh toán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
            }
            
        }
    }
}
