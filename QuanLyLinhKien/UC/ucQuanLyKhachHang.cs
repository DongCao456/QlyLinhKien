using BLL;
using DevComponents.DotNetBar;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyLinhKien.UC
{
    public partial class ucQuanLyKhachHang : UserControl
    {
        private bKhachHang htKhachHang;
        private bDonDatHang htDonDatHang;
        private bNhanVien htNhanVien;
        private bLoaiNhanVien htLoaiNhanVien;

        private byte loaiTacVu = 0;
        System.Windows.Forms.TabControl tabFather;
        private bool timKiem = false;
        public bool datHang = false;
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
                    dockKhachHang.Text = "Tìm kiếm";
                    tabKhachHang.SelectedIndex = 1;
                }
                else
                {
                    dockKhachHang.Text = "Thông tin";
                    tabKhachHang.SelectedIndex = 0;
                }
            }
        }

        public ucQuanLyKhachHang(System.Windows.Forms.TabControl tabFather)
        {
            InitializeComponent();
            this.tabFather = tabFather;
            hideTabMenu();
            capNhatDanhSach();
        }

        private void hideTabMenu()
        {
            tabKhachHang.Appearance = TabAppearance.FlatButtons;
            tabKhachHang.ItemSize = new Size(0, 1);
            tabKhachHang.SizeMode = TabSizeMode.Fixed;
        }

        public void capNhatDanhSach(List<eKhachHang> ls = null)
        {
            htKhachHang = new bKhachHang();
            htDonDatHang = new bDonDatHang();
            htNhanVien = new bNhanVien();
            htLoaiNhanVien = new bLoaiNhanVien();
            dgvKhachHang.Rows.Clear();
            List<eKhachHang> lsk;
            if (ls == null)
                lsk = htKhachHang.layDanhSachKhachHang();
            else
                lsk = ls;
            var lsAll = lsk.Select(n => new
            {
                stt = int.Parse(n.MaKhachHang.Split('-')[1]),
                MaKhachHang = n.MaKhachHang,
                TenKhachHang = n.TenKhachHang,
                SoDienThoai = n.SoDienThoai,
                EMail = n.EMail,
                DiaChi = n.DiaChi
            }).OrderBy(n => n.stt).ToList();
            foreach (var item in lsAll)
            {
                dgvKhachHang.Rows.Add();
                int stt = dgvKhachHang.Rows.Count - 1;
                dgvKhachHang.Rows[stt].Cells[0].Value = item.MaKhachHang;
                dgvKhachHang.Rows[stt].Cells[1].Value = item.TenKhachHang;
                dgvKhachHang.Rows[stt].Cells[2].Value = item.SoDienThoai;
                dgvKhachHang.Rows[stt].Cells[3].Value = item.EMail;
                dgvKhachHang.Rows[stt].Cells[4].Value = item.DiaChi;
            }
            listResize();
        }

        private void listResize()
        {
            dgvKhachHang.Columns[0].Width = dgvKhachHang.Width * 10 / 100;
            dgvKhachHang.Columns[1].Width = dgvKhachHang.Width * 22 / 100;
            dgvKhachHang.Columns[2].Width = dgvKhachHang.Width * 12 / 100;
            dgvKhachHang.Columns[3].Width = dgvKhachHang.Width * 18 / 100;
        }

        private void tatMoTextBox(bool n)
        {
            txtDiaChi.Enabled = txtEmail.Enabled = txtSoDienThoai.Enabled = txtTenKhachHang.Enabled = n;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = dgvKhachHang.Enabled = !n;
            btnXacNhan.Enabled = btnQuayLai.Enabled = n;
        }

        private void clearText()
        {
            txtDiaChi.Clear();
            txtEmail.Clear();
            txtMaKhachHang.Clear();
            txtSoDienThoai.Clear();
            txtTenKhachHang.Clear();
        }

        private void lsKH_Resize(object sender, EventArgs e)
        {
            listResize();
        }

        private void lsKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            txtMaKhachHang.Text = dgvKhachHang.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtTenKhachHang.Text = dgvKhachHang.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtSoDienThoai.Text = dgvKhachHang.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvKhachHang.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtDiaChi.Text = dgvKhachHang.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            clearText();
            txtMaKhachHang.Text = "KH-" + (htKhachHang.layDanhSachKhachHang().Select(n => new { stt = int.Parse(n.MaKhachHang.Split('-')[1]) }).Max(n => n.stt) + 1);
            tatMoTextBox(true);
            loaiTacVu = 1;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaKhachHang.Text.Trim().Length == 0)
            {
                MessageBoxEx.Show(this, "Mời chọn khách hàng cần sửa...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            tatMoTextBox(true);
            loaiTacVu = 2;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaKhachHang.Text.Trim().Length > 0)
            {
                if (htDonDatHang.layDanhSachDonDatHang().Where(n => n.MaKhachHang == txtMaKhachHang.Text).Count() > 0)
                {
                    MessageBoxEx.Show(this, "Không thể xoá khách hàng này, vì khách hàng này đã lập đơn đặt hàng...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                    return;
                }
                if (MessageBoxEx.Show(this, "Bạn có muốn xoá khách hàng " + txtMaKhachHang.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    htKhachHang.xoaKhachHang(txtMaKhachHang.Text);
                    capNhatDanhSach();
                    clearText();
                    MessageBoxEx.Show(this, "Xoá thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBoxEx.Show(this, "Mời chọn khách hàng cần xoá...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                switch (loaiTacVu)
                {
                    case 1:
                        if (htKhachHang.themKhachHang(new eKhachHang()
                        {
                            DiaChi = txtDiaChi.Text,
                            EMail = txtEmail.Text,
                            MaKhachHang = txtMaKhachHang.Text,
                            SoDienThoai = txtSoDienThoai.Text,
                            TenKhachHang = txtTenKhachHang.Text
                        }))
                        {
                            MessageBoxEx.Show(this, "Thêm thành công...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        }
                        else
                        {
                            MessageBoxEx.Show(this, "Bị trùng mã hoặc khách hàng đã tồn tại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            return;
                        }
                        capNhatDanhSach();
                        tatMoTextBox(false);
                        break;
                    case 2:
                        htKhachHang.suaKhachHang(new eKhachHang()
                        {
                            DiaChi = txtDiaChi.Text,
                            EMail = txtEmail.Text,
                            MaKhachHang = txtMaKhachHang.Text,
                            SoDienThoai = txtSoDienThoai.Text,
                            TenKhachHang = txtTenKhachHang.Text
                        });
                        tatMoTextBox(false);
                        MessageBoxEx.Show(this, "Sửa thành công...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        capNhatDanhSach();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(this, ex.Message, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }

        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            if (timKiem == true)
            {
                capNhatDanhSach();
                return;
            }
            tatMoTextBox(false);
            loaiTacVu = 0;
        }
        
        private void dgvKhachHang_DoubleClick(object sender, EventArgs e)
        {
            if (datHang && MessageBoxEx.Show(this, "Bạn có muốn thêm khách hàng này vào đơn đặt hàng", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                ((ucQuanLyBanHang)tabFather.TabPages[1].Controls[0]).ThongTinKhachHang = htKhachHang.thongTinKhachHang(txtMaKhachHang.Text);
                tabFather.SelectedIndex = 4;
                if (((ucQuanLyNhanVien)tabFather.TabPages[4].Controls[0]).TimKiem != true)
                    ((ucQuanLyNhanVien)tabFather.TabPages[4].Controls[0]).TimKiem = true;
                MessageBoxEx.Show(this, "Mời chọn nhân viên tư vấn", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

        }

        public void truyXuatDonDatHang()
        {
            if (txtMaKhachHang.Text.Trim().Length > 0)
            {
                ((ucTruyXuatDonDatHang)tabFather.TabPages[11].Controls[0]).Kh = htKhachHang.thongTinKhachHang(txtMaKhachHang.Text);
                ((ucTruyXuatDonDatHang)tabFather.TabPages[11].Controls[0]).lastTabIndex = 5;
                tabFather.SelectedIndex = 11;
            }
            else
            {
                MessageBoxEx.Show(this, "Mời chọn khách hàng cần truy xuất đơn đặt hàng...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            capNhatDanhSach(htKhachHang.layDanhSachKhachHang()
                .Where(n =>
                CongCu.Loai.XoaUnicode(n.TenKhachHang).Contains(CongCu.Loai.XoaUnicode(txtKeyTenKhachHang.Text)) &&
                CongCu.Loai.XoaUnicode(n.EMail).Contains(CongCu.Loai.XoaUnicode(txtKeyEmail.Text)) &&
                n.SoDienThoai.Contains(txtKeySDT.Text) &&
                CongCu.Loai.XoaUnicode(n.DiaChi).Contains(CongCu.Loai.XoaUnicode(txtKeyDiaChi.Text))
            ).ToList());
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtKeyTenKhachHang.Clear();
            txtKeyEmail.Clear();
            txtKeySDT.Clear();
            txtKeyDiaChi.Clear();
            capNhatDanhSach();
        }
    }
}
