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
    public partial class ucQuanLyChiTietPhieuNhapKho : UserControl
    {
        private bChiTietPhieuNhapKho htChiTietPhieuNhapKho;
        private bLinhKien htLinhKien;
        private bPhieuNhapKho htPhieuNhapKho;

        private List<eChiTietPhieuNhapKho> ls_Temp;
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
                    dockChiTietHoaDon.Text = "Tìm kiếm";
                    tabChiTietDonNhapHang.SelectedIndex = 1;
                }
                else
                {
                    dockChiTietHoaDon.Text = "Thông tin";
                    tabChiTietDonNhapHang.SelectedIndex = 0;
                }
            }
        }
        public ucQuanLyChiTietPhieuNhapKho(System.Windows.Forms.TabControl tabFather)
        {
            InitializeComponent();
            this.tabFather = tabFather;
            hideTabMenu();
            capNhatDanhSach();
        }

        private void hideTabMenu()
        {
            tabChiTietDonNhapHang.Appearance = TabAppearance.FlatButtons;
            tabChiTietDonNhapHang.ItemSize = new Size(0, 1);
            tabChiTietDonNhapHang.SizeMode = TabSizeMode.Fixed;
        }

        public void capNhatDanhSach(List<eChiTietPhieuNhapKho> ls = null)
        {
            htChiTietPhieuNhapKho = new bChiTietPhieuNhapKho();
            htPhieuNhapKho = new bPhieuNhapKho();
            htLinhKien = new bLinhKien();
            dgvChiTietDonNhanHang.Rows.Clear();
            if (ls == null)
            {
                ls_Temp = htChiTietPhieuNhapKho.layDanhSachChiTietPhieuNhapKho();
            }
            else
            {
                ls_Temp = ls;
            }

            var lsAll = ls_Temp.Select(n => new
            {
                stt = int.Parse(n.MaPhieuNhapKho.Split('-')[1]),
                MaPhieuNhapKho = n.MaPhieuNhapKho,
                TenLinhKien = htLinhKien.thongTinLinhKien(n.MaLinhKien).TenLinhKien,
                SoLuong = n.SoLuong,
                GiaMua = n.GiaMua,
                ThanhTien = n.ThanhTien
            }).OrderBy(n => n.stt);

            foreach (var item in lsAll)
            {
                dgvChiTietDonNhanHang.Rows.Add();
                int stt = dgvChiTietDonNhanHang.Rows.Count - 1;
                dgvChiTietDonNhanHang.Rows[stt].Cells[0].Value = item.MaPhieuNhapKho;
                dgvChiTietDonNhanHang.Rows[stt].Cells[1].Value = item.TenLinhKien;
                dgvChiTietDonNhanHang.Rows[stt].Cells[2].Value = item.SoLuong;
                dgvChiTietDonNhanHang.Rows[stt].Cells[3].Value = item.GiaMua;
                dgvChiTietDonNhanHang.Rows[stt].Cells[4].Value = item.ThanhTien;
            }
            listResize();
        }
        private void listResize()
        {
            dgvChiTietDonNhanHang.Columns[0].Width = dgvChiTietDonNhanHang.Width * 15 / 100;
            dgvChiTietDonNhanHang.Columns[1].Width = dgvChiTietDonNhanHang.Width * 20 / 100;
            dgvChiTietDonNhanHang.Columns[2].Width = dgvChiTietDonNhanHang.Width * 15 / 100;
            dgvChiTietDonNhanHang.Columns[3].Width = dgvChiTietDonNhanHang.Width * 15 / 100;
        }
        private void clearText()
        {
            txtGiaMua.Clear();
            txtMaPhieuNhapKho.Clear();
            txtTenLinhKien.Clear();
            txtSoLuong.Clear();
            txtThanhTien.Clear();
        }

        private void dgvChiTietDonNhanHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            txtMaPhieuNhapKho.Text = dgvChiTietDonNhanHang.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtTenLinhKien.Text = dgvChiTietDonNhanHang.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtSoLuong.Text = dgvChiTietDonNhanHang.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtGiaMua.Text = dgvChiTietDonNhanHang.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtThanhTien.Text = dgvChiTietDonNhanHang.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void dgvChiTietDonNhanHang_Resize(object sender, EventArgs e)
        {
            listResize();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaPhieuNhapKho.Text.Trim().Length == 0)
            {
                MessageBoxEx.Show(this, "Mời chọn chi tiết phiếu nhập kho...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaPhieuNhapKho.Text.Trim().Length > 0)
            {
                if (htPhieuNhapKho.thongTinPhieuNhapKho(txtMaPhieuNhapKho.Text).TrangThai == "Đã thanh toán")
                {
                    MessageBoxEx.Show(this, "Không thể xoá chi tiết phiếu nhập kho khi đã thanh toán...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                    return;
                }
                if (MessageBoxEx.Show(this, "Bạn có muốn xoá chi tiết phiếu nhập kho " + txtMaPhieuNhapKho.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (htChiTietPhieuNhapKho.layDanhSachChiTietPhieuNhapKho().Where(n => n.MaPhieuNhapKho == txtMaPhieuNhapKho.Text).Count() == 1)
                    {
                        htPhieuNhapKho.xoaPhieuNhapKho(txtMaPhieuNhapKho.Text);
                    }
                    else
                    {
                        htChiTietPhieuNhapKho.xoaChiTietPhieuNhapKho(txtMaPhieuNhapKho.Text,htLinhKien.layDanhSachLinhKien().Single(n=>n.TenLinhKien == txtTenLinhKien.Text).MaLinhKien);
                    }
                    MessageBoxEx.Show(this, "Xoá thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    capNhatDanhSach();
                    clearText();
                }

            }
            else
            {
                MessageBoxEx.Show(this, "Mời chọn chi tiết đơn nhập hàng cần xoá...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            capNhatDanhSach(htChiTietPhieuNhapKho.layDanhSachChiTietPhieuNhapKho()
                .Where(n =>
                CongCu.Loai.XoaUnicode(htLinhKien.thongTinLinhKien(n.MaLinhKien).TenLinhKien).Contains(CongCu.Loai.XoaUnicode(txtKeyTenLinhKien.Text)) &&
                n.SoLuong.ToString().Contains(txtKeySoLuong.Text) &&
                n.GiaMua.ToString().Contains(txtKeyGiaMua.Text) &&
                n.ThanhTien.ToString().Contains(txtKeyThanhTien.Text)
                ).ToList());
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtKeyGiaMua.Clear();
            txtKeyTenLinhKien.Clear();
            txtKeySoLuong.Clear();
            txtKeyThanhTien.Clear();
            capNhatDanhSach();
        }
    }
}
