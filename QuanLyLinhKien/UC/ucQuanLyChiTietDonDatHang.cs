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
    public partial class ucQuanLyChiTietDonDatHang : UserControl
    {
        private bChiTietDonDatHang htChiTietDonDatHang;
        private bLinhKien htLinhKien;
        private bDonDatHang htDonDatHang;

        private List<eChiTietDonDatHang> ls_Temp;
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
                    tabChiTietDonDatHang.SelectedIndex = 1;
                }
                else
                {
                    dockChiTietHoaDon.Text = "Thông tin";
                    tabChiTietDonDatHang.SelectedIndex = 0;
                }
            }
        }

        public ucQuanLyChiTietDonDatHang(System.Windows.Forms.TabControl tabFather)
        {
            InitializeComponent();
            this.tabFather = tabFather;
            hideTabMenu();
            capNhatDanhSach();
        }

        private void hideTabMenu()
        {
            tabChiTietDonDatHang.Appearance = TabAppearance.FlatButtons;
            tabChiTietDonDatHang.ItemSize = new Size(0, 1);
            tabChiTietDonDatHang.SizeMode = TabSizeMode.Fixed;
        }

        public void capNhatDanhSach(List<eChiTietDonDatHang> ls = null)
        {
            htChiTietDonDatHang = new bChiTietDonDatHang();
            htDonDatHang = new bDonDatHang();
            htLinhKien = new bLinhKien();
            dgvChiTietDonDatHang.Rows.Clear();
            if (ls == null)
            {
                ls_Temp = htChiTietDonDatHang.layDanhSachChiTietDonDatHang();
            }
            else
            {
                ls_Temp = ls;
            }
            var lsAll = ls_Temp.Select(n => new
            {
                stt = int.Parse(n.MaDonDatHang.Split('-')[1]),
                MaDonDatHang = n.MaDonDatHang,
                TenLinhKien = htLinhKien.thongTinLinhKien(n.MaLinhKien).TenLinhKien,
                SoLuong = n.SoLuong,
                GiaBan = n.GiaBan,
                MucGiamGia = n.MucGiamGia,
                ThanhTien = n.ThanhTien
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
                dgvChiTietDonDatHang.Rows[stt].Cells[5].Value = item.ThanhTien;
            }
            listResize();
        }

        private void listResize()
        {
            dgvChiTietDonDatHang.Columns[0].Width = dgvChiTietDonDatHang.Width * 12 / 100;
            dgvChiTietDonDatHang.Columns[1].Width = dgvChiTietDonDatHang.Width * 20 / 100;
            dgvChiTietDonDatHang.Columns[2].Width = dgvChiTietDonDatHang.Width * 12 / 100;
            dgvChiTietDonDatHang.Columns[3].Width = dgvChiTietDonDatHang.Width * 12 / 100;
            dgvChiTietDonDatHang.Columns[4].Width = dgvChiTietDonDatHang.Width * 12 / 100;
        }

        private void clearText()
        {
            txtGiaBan.Clear();
            txtMaDonDatHang.Clear();
            txtMaLinhKien.Clear();
            txtMucGiamGia.Clear();
            txtSoLuong.Clear();
        }

        private void lsCTDDH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            txtMaDonDatHang.Text = dgvChiTietDonDatHang.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtMaLinhKien.Text = dgvChiTietDonDatHang.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtSoLuong.Text = dgvChiTietDonDatHang.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtGiaBan.Text = dgvChiTietDonDatHang.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtMucGiamGia.Text = dgvChiTietDonDatHang.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void lsCTDDH_Resize(object sender, EventArgs e)
        {
            listResize();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaDonDatHang.Text.Trim().Length > 0)
            {
                if (htDonDatHang.thongTinDonDatHang(txtMaDonDatHang.Text).TrangThai == "Đã thanh toán")
                {
                    MessageBoxEx.Show(this, "Không thể xoá chi tiết đơn đặt hàng khi đã thanh toán...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                    return;
                }
                if (MessageBoxEx.Show(this, "Bạn có muốn xoá chi tiết đơn đặt hàng " + txtMaDonDatHang.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if(htChiTietDonDatHang.layDanhSachChiTietDonDatHang().Where(n=>n.MaDonDatHang == txtMaDonDatHang.Text).Count() == 1)
                    {
                        htDonDatHang.xoaDonDatHang(txtMaDonDatHang.Text);
                    }
                    else
                    {
                        htChiTietDonDatHang.xoaChiTietDonDatHang(txtMaDonDatHang.Text, htLinhKien.layDanhSachLinhKien().Single(n => n.TenLinhKien == txtMaLinhKien.Text).MaLinhKien);
                    }
                    capNhatDanhSach();
                    clearText();
                    MessageBoxEx.Show(this, "Xoá chi tiết đơn đặt hàng thành công" + txtMaDonDatHang.Text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                }

            }
            else
            {
                MessageBoxEx.Show(this, "Mời chọn chi tiết đơn đặt hàng cần xoá...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            capNhatDanhSach(htChiTietDonDatHang.layDanhSachChiTietDonDatHang()
                .Where(n =>
                CongCu.Loai.XoaUnicode(htLinhKien.thongTinLinhKien(n.MaLinhKien).TenLinhKien).Contains(CongCu.Loai.XoaUnicode(txtKeyTenLinhKien.Text)) &&
                n.SoLuong.ToString().Contains(txtKeySoLuong.Text) &&
                n.GiaBan.ToString().Contains(CongCu.Loai.XoaUnicode(txtKeyGiaBan.Text)) &&
                n.MucGiamGia.ToString().Contains(CongCu.Loai.XoaUnicode(txtKeyMuaGiamGia.Text))
                ).ToList());
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtKeyTenLinhKien.Clear();
            txtKeySoLuong.Clear();
            txtKeyGiaBan.Clear();
            txtKeyMuaGiamGia.Clear();
            capNhatDanhSach();
        }
    }
}
