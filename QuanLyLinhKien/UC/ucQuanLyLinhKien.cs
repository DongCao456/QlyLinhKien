using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entity;
using BLL;
using DevComponents.DotNetBar;
using System.Text.RegularExpressions;

namespace QuanLyLinhKien.UC
{
    public partial class ucQuanLyLinhKien : UserControl
    {
        private bLinhKien htLinhKien;
        private bLoaiLinhKien htLoaiLinhKien;
        private bNhaCungCap htNhaCungCap;
        private bChiTietDonDatHang htChiTietDonDatHang;
        private bChiTietPhieuNhapKho htChiTietPhieuNhapKho;

        private List<eLinhKien> ls_Temp;
        private byte loaiTacVu = 0;
        private bool timKiem = false;
        private System.Windows.Forms.TabControl tabFather;
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
                    dockLinhKien.Text = "Tìm kiếm";
                    tabLinhKien.SelectedIndex = 1;
                }
                else
                {
                    dockLinhKien.Text = "Thông tin";
                    tabLinhKien.SelectedIndex = 0;
                }
            }
        }
        public ucQuanLyLinhKien(System.Windows.Forms.TabControl tabFather)
        {
            InitializeComponent();
            this.tabFather = tabFather;
            capNhatDanhSach();
            themDuLieuVaoCB();
            hideTabMenu();
        }
        private void hideTabMenu()
        {
            tabLinhKien.Appearance = TabAppearance.FlatButtons;
            tabLinhKien.ItemSize = new Size(0, 1);
            tabLinhKien.SizeMode = TabSizeMode.Fixed;
        }
        public void themDuLieuVaoCB()
        {
            cboKeyLoai.DataSource = cboLoai.DataSource = (new bLoaiLinhKien()).layDanhSachLoaiLinhKien();
            cboKeyLoai.DisplayMember = cboLoai.DisplayMember = "tenLoai";

            cboKeyNhaCungCap.DataSource = (new bNhaCungCap()).layDanhSachNhaCungCap();
            cboNhaCungCap.DataSource = (new bNhaCungCap()).layDanhSachNhaCungCap();
            cboKeyNhaCungCap.DisplayMember = cboNhaCungCap.DisplayMember = "tenNhaCungCap";

            cboKeyLoai.SelectedIndex = -1;
            cboKeyNhaCungCap.SelectedIndex = -1;

        }
        public void capNhatDanhSach(List<eLinhKien> ls = null)
        {
            htLinhKien = new bLinhKien();
            htNhaCungCap = new bNhaCungCap();
            htLoaiLinhKien = new bLoaiLinhKien();
            htChiTietDonDatHang = new bChiTietDonDatHang();
            htChiTietPhieuNhapKho = new bChiTietPhieuNhapKho();
            dgvLinhKien.Rows.Clear();
            if (ls == null)
            {
                ls_Temp = htLinhKien.layDanhSachLinhKien();
            }
            else
            {
                ls_Temp = ls;
            }

            var lsAll = ls_Temp.Select(n => new
            {
                stt = int.Parse(n.MaLinhKien.Split('-')[1]),
                MaLinhKien = n.MaLinhKien,
                TenLinhKien = n.TenLinhKien,
                TenLoai = htLoaiLinhKien.thongTinLoaiLinhKien(n.MaLoai).TenLoai,
                GiaMua = n.GiaMua,
                GiaBan = n.GiaBan,
                SoLuong = n.SoLuong,
                MucGiamGia = n.MucGiamGia,
                TenNhaCungCap = htNhaCungCap.thongTinNhaCungCap(n.MaNhaCungCap).TenNhaCungCap
            }).OrderBy(n => n.stt);

            foreach (var item in lsAll)
            {
                dgvLinhKien.Rows.Add();
                int stt = dgvLinhKien.Rows.Count - 1;
                dgvLinhKien.Rows[stt].Cells[0].Value = item.MaLinhKien;
                dgvLinhKien.Rows[stt].Cells[1].Value = item.TenLinhKien;
                dgvLinhKien.Rows[stt].Cells[2].Value = item.TenLoai;
                dgvLinhKien.Rows[stt].Cells[3].Value = item.GiaMua;
                dgvLinhKien.Rows[stt].Cells[4].Value = item.GiaBan;
                dgvLinhKien.Rows[stt].Cells[5].Value = item.SoLuong;
                dgvLinhKien.Rows[stt].Cells[6].Value = item.MucGiamGia;
                dgvLinhKien.Rows[stt].Cells[7].Value = item.TenNhaCungCap;
            }
        }
        private void listResize()
        {
            dgvLinhKien.Columns[0].Width = dgvLinhKien.Width * 8 / 100;
            dgvLinhKien.Columns[1].Width = dgvLinhKien.Width * 22 / 100;
            dgvLinhKien.Columns[2].Width = dgvLinhKien.Width * 10 / 100;
            dgvLinhKien.Columns[3].Width = dgvLinhKien.Width * 10 / 100;
            dgvLinhKien.Columns[4].Width = dgvLinhKien.Width * 10 / 100;
            dgvLinhKien.Columns[5].Width = dgvLinhKien.Width * 10 / 100;
            dgvLinhKien.Columns[6].Width = dgvLinhKien.Width * 15 / 100;
        }
        private void tatMoTextBox(bool n)
        {
            txtGiaBan.Enabled = txtGiaMua.Enabled = txtTenLinhKien.Enabled = cboLoai.Enabled = cboNhaCungCap.Enabled = txtMucGiamGia.Enabled = n;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = dgvLinhKien.Enabled = !n;
            btnXacNhan.Enabled = btnQuayLai.Enabled = n;
        }
        private void clearText()
        {
            txtTenLinhKien.Clear();
            txtSoLuong.Clear();
            txtGiaMua.Clear();
            txtMaLinhKien.Clear();
            txtGiaBan.Clear();
            txtMucGiamGia.Clear();
            cboNhaCungCap.Text = "";
            cboLoai.Text = "";
        }
        private void lsLK_Resize(object sender, EventArgs e)
        {
            listResize();
        }

        private void lsLK_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            txtMaLinhKien.Text = dgvLinhKien.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtTenLinhKien.Text = dgvLinhKien.Rows[e.RowIndex].Cells[1].Value.ToString();
            cboLoai.Text = dgvLinhKien.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtGiaMua.Text = dgvLinhKien.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtGiaBan.Text = dgvLinhKien.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtSoLuong.Text = dgvLinhKien.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtMucGiamGia.Text = dgvLinhKien.Rows[e.RowIndex].Cells[6].Value.ToString();
            cboNhaCungCap.Text = dgvLinhKien.Rows[e.RowIndex].Cells[7].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            clearText();
            txtMaLinhKien.Text = "LK-" + (htLinhKien.layDanhSachLinhKien().Select(n => new { stt = int.Parse(n.MaLinhKien.Split('-')[1]) }).Max(n => n.stt) + 1);
            txtSoLuong.Text = "0";
            tatMoTextBox(true);
            loaiTacVu = 1;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaLinhKien.Text.Trim().Length == 0)
            {
                MessageBoxEx.Show(this, "Mời chọn linh kiện cần sửa...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            tatMoTextBox(true);
            loaiTacVu = 2;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaLinhKien.Text.Trim().Length > 0)
            {
                if (htChiTietDonDatHang.layDanhSachChiTietDonDatHang().Where(n => n.MaLinhKien == txtMaLinhKien.Text).Count() > 0 ||
                    htChiTietPhieuNhapKho.layDanhSachChiTietPhieuNhapKho().Where(n => n.MaLinhKien == txtMaLinhKien.Text).Count() > 0
                    )
                {
                    MessageBoxEx.Show(this, "Không thể xoá linh kiện này, vì linh kiện này đã lập đơn đặt hàng...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                    return;
                }
                if (MessageBoxEx.Show(this, "Bạn có muốn xoá linh kiện " + txtMaLinhKien.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    htLinhKien.xoaLinhKien(txtMaLinhKien.Text);
                    capNhatDanhSach();
                    clearText();
                    MessageBoxEx.Show(this, "Xoá thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBoxEx.Show(this, "Mời chọn linh kiện cần xoá...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                switch (loaiTacVu)
                {

                    case 1:
                        if (htLinhKien.themLinhKien(new eLinhKien()
                        {
                            MaLinhKien = txtMaLinhKien.Text,
                            MaLoai = ((eLoaiLinhKien)cboLoai.SelectedValue).MaLoai,
                            MaNhaCungCap = ((eNhaCungCap)cboNhaCungCap.SelectedValue).MaNhaCungCap,
                            SoLuong = int.Parse(txtSoLuong.Text),
                            TenLinhKien = txtTenLinhKien.Text,
                            GiaBan = double.Parse(txtGiaBan.Text),
                            GiaMua = double.Parse(txtGiaMua.Text),
                            MucGiamGia = double.Parse(txtMucGiamGia.Text)
                        }))
                        {
                            MessageBoxEx.Show(this, "Thêm thành công...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        }
                        else
                        {
                            MessageBoxEx.Show(this, "Bị trùng mã hoặc linh kiện đã tồn tại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            return;
                        }
                        capNhatDanhSach();
                        tatMoTextBox(false);
                        break;
                    case 2:
                        htLinhKien.suaLinhKien(new eLinhKien()
                        {
                            GiaBan = double.Parse(txtGiaBan.Text),
                            MaLinhKien = txtMaLinhKien.Text,
                            MaLoai = ((eLoaiLinhKien)cboLoai.SelectedValue).MaLoai,
                            MaNhaCungCap = ((eNhaCungCap)cboNhaCungCap.SelectedValue).MaNhaCungCap,
                            SoLuong = int.Parse(txtSoLuong.Text),
                            TenLinhKien = txtTenLinhKien.Text,
                            GiaMua = double.Parse(txtGiaMua.Text),
                            MucGiamGia = double.Parse(txtMucGiamGia.Text)
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
            tatMoTextBox(false);
            clearText();
            loaiTacVu = 0;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            List<eLinhKien> ls = htLinhKien.layDanhSachLinhKien()
                .Where(n =>
                CongCu.Loai.XoaUnicode(n.TenLinhKien).Contains(CongCu.Loai.XoaUnicode(txtKeyTenLinhKien.Text)) &&
                n.GiaBan.ToString().Contains(txtKeyGiaBan.Text) &&
                n.GiaMua.ToString().Contains(txtKeyGiaMua.Text) &&
                n.SoLuong.ToString().Contains(txtKeySoLuong.Text) &&
                n.MucGiamGia.ToString().Contains(txtKeyMucGiam.Text)
                ).ToList();
            if (cboKeyLoai.SelectedIndex != -1)
                ls = ls.Where(n => n.MaLoai == ((eLoaiLinhKien)cboKeyLoai.SelectedItem).MaLoai).ToList();
            if (cboKeyNhaCungCap.SelectedIndex != -1)
                ls = ls.Where(n => n.MaNhaCungCap == ((eNhaCungCap)cboKeyNhaCungCap.SelectedItem).MaNhaCungCap).ToList();
            capNhatDanhSach(ls);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtKeyTenLinhKien.Clear();
            txtKeyGiaBan.Clear();
            txtKeyGiaMua.Clear();
            txtKeyMucGiam.Clear();
            txtKeySoLuong.Clear();
            cboKeyLoai.SelectedIndex = -1;
            cboKeyNhaCungCap.SelectedIndex = -1;
            capNhatDanhSach();
        }
    }
}
