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
    public partial class ucQuanLyNhaCungCap : UserControl
    {
        private bNhaCungCap htNhaCungCap;
        private bChiTietPhieuNhapKho htChiTietPhieuNhapKho;
        private bLinhKien htLinhKien;
        private byte loaiTacVu = 0;
        private System.Windows.Forms.TabControl tabFather;
        private List<eNhaCungCap> ls_temp;
        public bool nhapHang = false;
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
                    dockNhaCungCap.Text = "Tìm kiếm";
                    tabNhaCungCap.SelectedIndex = 1;
                }
                else
                {
                    dockNhaCungCap.Text = "Thông tin";
                    tabNhaCungCap.SelectedIndex = 0;
                }
            }
        }

        public ucQuanLyNhaCungCap(System.Windows.Forms.TabControl tabFather)
        {
            InitializeComponent();
            this.tabFather = tabFather;
            hideTabMenu();
            capNhatDanhSach();
        }

        private void hideTabMenu()
        {
            tabNhaCungCap.Appearance = TabAppearance.FlatButtons;
            tabNhaCungCap.ItemSize = new Size(0, 1);
            tabNhaCungCap.SizeMode = TabSizeMode.Fixed;
        }

        public void capNhatDanhSach(List<eNhaCungCap> ls = null)
        {
            htNhaCungCap = new bNhaCungCap();
            htChiTietPhieuNhapKho = new bChiTietPhieuNhapKho();
            htLinhKien = new bLinhKien();
            dgvNhaCungCap.Rows.Clear();
            if (ls == null)
            {
                ls_temp = htNhaCungCap.layDanhSachNhaCungCap();
            }
            else
            {
                ls_temp = ls;
            }
            var lsAll = ls_temp.Select(n => new
            {
                stt = int.Parse(n.MaNhaCungCap.Split('-')[1]),
                MaNhaCungCap = n.MaNhaCungCap,
                TenNhaCungCap = n.TenNhaCungCap,
                SoDienThoai = n.SoDienThoai,
                EMail = n.EMail,
                QuocGia = n.QuocGia,
                DiaChi = n.DiaChi
            }).OrderBy(n => n.stt);
            foreach (var item in lsAll)
            {
                dgvNhaCungCap.Rows.Add();
                int stt = dgvNhaCungCap.Rows.Count - 1;
                dgvNhaCungCap.Rows[stt].Cells[0].Value = item.MaNhaCungCap;
                dgvNhaCungCap.Rows[stt].Cells[1].Value = item.TenNhaCungCap;
                dgvNhaCungCap.Rows[stt].Cells[2].Value = item.SoDienThoai;
                dgvNhaCungCap.Rows[stt].Cells[3].Value = item.EMail;
                dgvNhaCungCap.Rows[stt].Cells[4].Value = item.QuocGia;
                dgvNhaCungCap.Rows[stt].Cells[5].Value = item.DiaChi;
            }
            ((ucQuanLyLinhKien)tabFather.TabPages[6].Controls[0]).themDuLieuVaoCB();
        }

        private void listResize()
        {
            dgvNhaCungCap.Columns[0].Width = dgvNhaCungCap.Width * 16 / 100;
            dgvNhaCungCap.Columns[1].Width = dgvNhaCungCap.Width * 16 / 100;
            dgvNhaCungCap.Columns[2].Width = dgvNhaCungCap.Width * 16 / 100;
            dgvNhaCungCap.Columns[3].Width = dgvNhaCungCap.Width * 16 / 100;
            dgvNhaCungCap.Columns[3].Width = dgvNhaCungCap.Width * 16 / 100;
        }

        private void tatMoTextBox(bool n)
        {
            txtDiaChi.Enabled = txtEmail.Enabled = txtSDT.Enabled = txtTenNhaCungCap.Enabled = txtQuocGia.Enabled = n;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = dgvNhaCungCap.Enabled = !n;
            btnXacNhan.Enabled = btnQuayLai.Enabled = n;
        }

        private void clearText()
        {
            txtDiaChi.Clear();
            txtEmail.Clear();
            txtMaNhaCungCap.Clear();
            txtSDT.Clear();
            txtTenNhaCungCap.Clear();
            txtQuocGia.Clear();
        }

        private void lsNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            txtMaNhaCungCap.Text = dgvNhaCungCap.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtTenNhaCungCap.Text = dgvNhaCungCap.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtSDT.Text = dgvNhaCungCap.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvNhaCungCap.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtQuocGia.Text = dgvNhaCungCap.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtDiaChi.Text = dgvNhaCungCap.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            clearText();
            txtMaNhaCungCap.Text = "NCC-" + (htNhaCungCap.layDanhSachNhaCungCap().Select(n => new { stt = int.Parse(n.MaNhaCungCap.Split('-')[1]) }).Max(n => n.stt) + 1);
            tatMoTextBox(true);
            loaiTacVu = 1;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaNhaCungCap.Text.Trim().Length == 0)
            {
                MessageBoxEx.Show(this, "Mời chọn nhà cung cấp cần sửa...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            tatMoTextBox(true);
            loaiTacVu = 2;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaNhaCungCap.Text.Trim().Length > 0)
            {
                if (htChiTietPhieuNhapKho.layDanhSachChiTietPhieuNhapKho()
                    .Where(n => htLinhKien.thongTinLinhKien(n.MaLinhKien).MaNhaCungCap == txtMaNhaCungCap.Text).Count() > 0 ||
                    htLinhKien.layDanhSachLinhKien().Where(n => n.MaNhaCungCap == txtMaNhaCungCap.Text).Count() > 0
                    )
                {
                    MessageBoxEx.Show(this, "Không thể xoá nhà cung cấp này, vì nhà cung cấp này đã được sử dụng...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                    return;
                }
                if (MessageBoxEx.Show(this, "Bạn có muốn xoá nhà cung cấp " + txtMaNhaCungCap.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    htNhaCungCap.xoaNhaCungCap(txtMaNhaCungCap.Text);
                    capNhatDanhSach();
                    clearText();
                    MessageBoxEx.Show(this, "Xoá thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBoxEx.Show(this, "Mời chọn nhà cung cấp cần xoá...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                switch (loaiTacVu)
                {
                    case 1:
                        if (htNhaCungCap.themNhaCungCap(new eNhaCungCap()
                        {
                            DiaChi = txtDiaChi.Text,
                            EMail = txtEmail.Text,
                            MaNhaCungCap = txtMaNhaCungCap.Text,
                            SoDienThoai = txtSDT.Text,
                            TenNhaCungCap = txtTenNhaCungCap.Text,
                            QuocGia = txtQuocGia.Text
                        }))

                        {
                            MessageBoxEx.Show(this, "Thêm thành công...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        }
                        else
                        {
                            MessageBoxEx.Show(this, "Bị trùng mã hoặc nhà cung cấp đã tồn tại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            return;
                        }
                        capNhatDanhSach();
                        tatMoTextBox(false);
                        break;
                    case 2:
                        htNhaCungCap.suaNhaCungCap(new eNhaCungCap()
                        {
                            DiaChi = txtDiaChi.Text,
                            EMail = txtEmail.Text,
                            MaNhaCungCap = txtMaNhaCungCap.Text,
                            SoDienThoai = txtSDT.Text,
                            TenNhaCungCap = txtTenNhaCungCap.Text,
                            QuocGia = txtQuocGia.Text
                        });
                        tatMoTextBox(false);
                        MessageBoxEx.Show(this, "Sửa thành công...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        capNhatDanhSach();
                        break;
                }
                //capNhatBaoCao();
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

        private void lsNCC_Resize(object sender, EventArgs e)
        {
            listResize();
        }

        private void dgvNhaCungCap_DoubleClick(object sender, EventArgs e)
        {
            if (nhapHang && MessageBoxEx.Show(this, "Bạn có muốn thêm nhà cung cấp này vào phiếu nhập kho", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                ((ucQuanLyNhapKho)tabFather.TabPages[13].Controls[0]).ThongTinNhaCungCap = htNhaCungCap.thongTinNhaCungCap(txtMaNhaCungCap.Text);
                tabFather.SelectedIndex = 4;
                if (((ucQuanLyNhanVien)tabFather.TabPages[4].Controls[0]).TimKiem != true)
                    ((ucQuanLyNhanVien)tabFather.TabPages[4].Controls[0]).TimKiem = true;
                MessageBoxEx.Show(this, "Mời chọn nhân viên thủ kho", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        public void truyXuatPhieuNhapKho()
        {
            if (txtMaNhaCungCap.Text.Trim().Length > 0)
            {
                ((ucTruyXuatPhieuNhapKho)tabFather.TabPages[18].Controls[0]).Ncc = htNhaCungCap.thongTinNhaCungCap(txtMaNhaCungCap.Text);
                ((ucTruyXuatPhieuNhapKho)tabFather.TabPages[18].Controls[0]).lastTabIndex = 8;
                tabFather.SelectedIndex = 18;
            }
            else
            {
                MessageBoxEx.Show(this, "Mời chọn nhà cung cấp cần truy xuất đơn đặt hàng...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            capNhatDanhSach(htNhaCungCap.layDanhSachNhaCungCap()
                .Where(n =>
                CongCu.Loai.XoaUnicode(n.TenNhaCungCap).Contains(CongCu.Loai.XoaUnicode(txtKeyTenNhaCungCap.Text)) &&
                CongCu.Loai.XoaUnicode(n.SoDienThoai).Contains(CongCu.Loai.XoaUnicode(txtKeySDT.Text)) &&
                CongCu.Loai.XoaUnicode(n.QuocGia).Contains(CongCu.Loai.XoaUnicode(txtKeyQuocGia.Text)) &&
                CongCu.Loai.XoaUnicode(n.EMail).Contains(CongCu.Loai.XoaUnicode(txtKeyEmail.Text)) &&
                CongCu.Loai.XoaUnicode(n.DiaChi).Contains(CongCu.Loai.XoaUnicode(txtKeyDiaChi.Text))
                ).ToList());
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtKeyTenNhaCungCap.Clear();
            txtKeySDT.Clear();
            txtKeyQuocGia.Clear();
            txtKeyEmail.Clear();
            txtKeyDiaChi.Clear();
            capNhatDanhSach();
        }
    }
}
