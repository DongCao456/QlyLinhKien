using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using BLL;
using Entity;

namespace QuanLyLinhKien.UC
{
    public partial class ucQuanLyNhanVien : UserControl
    {
        private bNhanVien htNhanVien;
        private bDonDatHang htDonDatHang;
        private bLoaiNhanVien htLoaiNhanVien;
        private bPhieuNhapKho htPhieuNhapKho;

        private List<eNhanVien> ls_Temp;
        private byte loaiTacVu = 0;
        private System.Windows.Forms.TabControl tabFather;
        public bool datHang = false;
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
                    dockNhanVien.Text = "Tìm kiếm";
                    tabNhanVien.SelectedIndex = 1;
                }
                else
                {
                    dockNhanVien.Text = "Thông tin";
                    tabNhanVien.SelectedIndex = 0;
                }
            }
        }

        public ucQuanLyNhanVien(System.Windows.Forms.TabControl tabFather)
        {
            InitializeComponent();
            this.tabFather = tabFather;
            hideTabMenu();
            themDuLieuVaoCB();
            capNhatDanhSach();
        }

        private void hideTabMenu()
        {
            tabNhanVien.Appearance = TabAppearance.FlatButtons;
            tabNhanVien.ItemSize = new Size(0, 1);
            tabNhanVien.SizeMode = TabSizeMode.Fixed;
        }

        public void themDuLieuVaoCB()
        {
            htLoaiNhanVien = new bLoaiNhanVien();
            cboKeyLoai.DataSource = htLoaiNhanVien.layDanhSachLoaiNhanVien();
            cboLoaiNhanVien.DataSource = htLoaiNhanVien.layDanhSachLoaiNhanVien();
            cboKeyLoai.DisplayMember = cboLoaiNhanVien.DisplayMember = "tenLoaiNhanVien";
            cboKeyTrangThai.SelectedIndex = 0;
            cboKeyLoai.SelectedIndex = -1;
        }

        public void capNhatDanhSach(List<eNhanVien> ls = null)
        {
            htNhanVien = new bNhanVien();
            htDonDatHang = new bDonDatHang();
            htPhieuNhapKho = new bPhieuNhapKho();

            dgvNhanVien.Rows.Clear();
            if (ls == null)
            {
                ls_Temp = htNhanVien.layDanhSachNhanVien();
            }
            else
            {
                ls_Temp = ls;
            }
            var lsALL = ls_Temp.Select(n => new
            {
                stt = int.Parse(n.MaNhanVien.Split('-')[1]),
                MaNhanVien = n.MaNhanVien,
                TenNhanVien = n.TenNhanVien,
                CMND = n.CMND,
                SoDienThoai = n.SoDienThoai,
                EMail = n.EMail,
                TrangThai = n.TrangThai,
                TenLoaiNhanVien = htLoaiNhanVien.thongTinLoaiNhanVien(n.MaLoaiNhanVien).TenLoaiNhanVien,
                DiaChi = n.DiaChi
            }).OrderBy(n => n.stt).ToList();
            foreach (var item in lsALL)
            {
                dgvNhanVien.Rows.Add();
                int stt = dgvNhanVien.Rows.Count - 1;
                dgvNhanVien.Rows[stt].Cells[0].Value = item.MaNhanVien;
                dgvNhanVien.Rows[stt].Cells[1].Value = item.TenNhanVien;
                dgvNhanVien.Rows[stt].Cells[2].Value = item.CMND;
                dgvNhanVien.Rows[stt].Cells[3].Value = item.SoDienThoai;
                dgvNhanVien.Rows[stt].Cells[4].Value = item.EMail;
                dgvNhanVien.Rows[stt].Cells[5].Value = item.TrangThai;
                dgvNhanVien.Rows[stt].Cells[6].Value = item.TenLoaiNhanVien;
                dgvNhanVien.Rows[stt].Cells[7].Value = item.DiaChi;
            }

        }

        private void listResize()
        {
            dgvNhanVien.Columns[0].Width = dgvNhanVien.Width * 12 / 100;
            dgvNhanVien.Columns[1].Width = dgvNhanVien.Width * 17 / 100;
            dgvNhanVien.Columns[2].Width = dgvNhanVien.Width * 8 / 100;
            dgvNhanVien.Columns[3].Width = dgvNhanVien.Width * 12 / 100;
            dgvNhanVien.Columns[4].Width = dgvNhanVien.Width * 12 / 100;
            dgvNhanVien.Columns[6].Width = dgvNhanVien.Width * 12 / 100;

        }

        private void tatMoTextBox(bool n)
        {
            cboTrangThai.Enabled = cboLoaiNhanVien.Enabled = txtChungMinhNhanDan.Enabled = txtDiaChi.Enabled = txtEmail.Enabled = txtSoDienThoai.Enabled = txtTenNhanVien.Enabled = n;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = dgvNhanVien.Enabled = !n;
            btnXacNhan.Enabled = btnQuayLai.Enabled = n;
        }

        private void clearText()
        {
            txtDiaChi.Clear();
            txtEmail.Clear();
            txtMaNhanVien.Clear();
            txtSoDienThoai.Clear();
            txtChungMinhNhanDan.Clear();
            txtTenNhanVien.Clear();
            cboKeyTrangThai.SelectedIndex = 0;
            cboKeyTrangThai.SelectedIndex = 1;
        }

        private void lsNV_Resize(object sender, EventArgs e)
        {
            listResize();
        }

        private void lsNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || dgvNhanVien.Rows[e.RowIndex].Cells[0].Value.ToString() == "NV-0") return;
            txtMaNhanVien.Text = dgvNhanVien.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtTenNhanVien.Text = dgvNhanVien.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtChungMinhNhanDan.Text = dgvNhanVien.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtSoDienThoai.Text = dgvNhanVien.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtEmail.Text = dgvNhanVien.Rows[e.RowIndex].Cells[4].Value.ToString();
            cboTrangThai.Text = dgvNhanVien.Rows[e.RowIndex].Cells[5].Value.ToString();
            cboLoaiNhanVien.Text = dgvNhanVien.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtDiaChi.Text = dgvNhanVien.Rows[e.RowIndex].Cells[7].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            clearText();
            txtMaNhanVien.Text = "NV-" + (htNhanVien.layDanhSachNhanVien().Select(n => new { stt = int.Parse(n.MaNhanVien.Split('-')[1]) }).Max(n => n.stt) + 1);
            tatMoTextBox(true);
            cboTrangThai.Enabled = false;
            cboTrangThai.Text = "Đang làm";
            loaiTacVu = 1;

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaNhanVien.Text.Trim().Length == 0)
            {
                MessageBoxEx.Show(this, "Mời chọn nhân viên cần sửa...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            tatMoTextBox(true);
            loaiTacVu = 2;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaNhanVien.Text.Trim().Length > 0)
            {
                if (htDonDatHang.layDanhSachDonDatHang().Where(n => n.MaNhanVienThuNgan == txtMaNhanVien.Text || n.MaNhanVienTuVan == txtMaNhanVien.Text).Count() > 0 ||
                    htPhieuNhapKho.layDanhSachPhieuNhapKho().Where(n => n.MaNhanVienKeToanKho == txtMaNhanVien.Text || n.MaNhanVienThuKho == txtMaNhanVien.Text).Count() > 0
                    )
                {
                    MessageBoxEx.Show(this, "Không thể xoá nhân viên này, vì nhân viên này đã lập đơn đặt hàng...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                    return;
                }
                if (MessageBoxEx.Show(this, "Bạn có muốn xoá nhân viên " + txtMaNhanVien.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    htNhanVien.xoaNhanVien(txtMaNhanVien.Text);
                    capNhatDanhSach();
                    clearText();
                    MessageBoxEx.Show(this, "Xoá thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBoxEx.Show(this, "Mời chọn nhân viên cần xoá...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                switch (loaiTacVu)
                {
                    case 1:
                        if (htNhanVien.themNhanVien(new eNhanVien()
                        {
                            CMND = txtChungMinhNhanDan.Text,
                            DiaChi = txtDiaChi.Text,
                            EMail = txtEmail.Text,
                            MaNhanVien = txtMaNhanVien.Text,
                            SoDienThoai = txtSoDienThoai.Text,
                            TenNhanVien = txtTenNhanVien.Text,
                            TrangThai = cboTrangThai.SelectedItem.ToString(),
                            MaLoaiNhanVien = ((eLoaiNhanVien)cboLoaiNhanVien.SelectedItem).MaLoaiNhanVien
                        }))
                        {
                            MessageBoxEx.Show(this, "Thêm thành công...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        }
                        else
                        {
                            MessageBoxEx.Show(this, "Bị trùng mã hoặc nhân viên đã tồn tại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            return;
                        }
                        capNhatDanhSach();
                        tatMoTextBox(false);
                        break;
                    case 2:
                        htNhanVien.suaNhanVien(new eNhanVien()
                        {
                            CMND = txtChungMinhNhanDan.Text,
                            DiaChi = txtDiaChi.Text,
                            EMail = txtEmail.Text,
                            MaNhanVien = txtMaNhanVien.Text,
                            SoDienThoai = txtSoDienThoai.Text,
                            TenNhanVien = txtTenNhanVien.Text,
                            TrangThai = cboTrangThai.SelectedItem.ToString(),
                            MaLoaiNhanVien = ((eLoaiNhanVien)cboLoaiNhanVien.SelectedItem).MaLoaiNhanVien
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
            loaiTacVu = 0;
        }


        private void dgvNhanVien_DoubleClick(object sender, EventArgs e)
        {
            if (datHang)
            {
                if (cboLoaiNhanVien.Text != "Nhân viên tư vấn" && cboLoaiNhanVien.Text != "Quản lý")
                {
                    MessageBoxEx.Show(this, "Đây không phải nhân viên tư vấn, mời chọn lại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }
                if (MessageBoxEx.Show(this, "Bạn có muốn thêm nhân viên này vào đơn đặt hàng", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    ((ucQuanLyBanHang)tabFather.TabPages[1].Controls[0]).nvTuVan = htNhanVien.thongTinNhanVien(txtMaNhanVien.Text);
                    tabFather.SelectedIndex = 1;
                    MessageBoxEx.Show(this, "Đã nhập xong thông tin, tiến hành đặt hàng", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            else if (nhapHang)
            {
                if (cboLoaiNhanVien.Text == "Thủ kho" || cboLoaiNhanVien.Text == "Quản lý")
                {
                    if (MessageBoxEx.Show(this, "Bạn có muốn thêm nhân viên này vào phiếu nhập kho", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        ((ucQuanLyNhapKho)tabFather.TabPages[13].Controls[0]).thongTinThuKho = htNhanVien.thongTinNhanVien(txtMaNhanVien.Text);
                        tabFather.SelectedIndex = 13;
                        MessageBoxEx.Show(this, "Đã nhập xong thông tin, tiến hành chọn linh kiện", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    }
                }
                else
                {
                    MessageBoxEx.Show(this, "Đây không phải nhân viên thủ kho, mời chọn lại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
        }

        public void truyXuatDonDatHang()
        {
            if (txtMaNhanVien.Text.Trim().Length > 0)
            {
                ((ucTruyXuatDonDatHang)tabFather.TabPages[11].Controls[0]).Nv = htNhanVien.thongTinNhanVien(txtMaNhanVien.Text);
                ((ucTruyXuatDonDatHang)tabFather.TabPages[11].Controls[0]).lastTabIndex = 4;
                tabFather.SelectedIndex = 11;
            }
            else
            {
                MessageBoxEx.Show(this, "Mời chọn nhân viên cần truy xuất đơn đặt hàng...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        public void truyXuatPhieuNhapKho()
        {
            if (txtMaNhanVien.Text.Trim().Length > 0)
            {
                ((ucTruyXuatPhieuNhapKho)tabFather.TabPages[18].Controls[0]).Nv = htNhanVien.thongTinNhanVien(txtMaNhanVien.Text);
                ((ucTruyXuatPhieuNhapKho)tabFather.TabPages[18].Controls[0]).lastTabIndex = 8;
                tabFather.SelectedIndex = 18;
            }
            else
            {
                MessageBoxEx.Show(this, "Mời chọn nhân viên cần truy xuất phiếu nhập kho...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtKeyTenNhanVien.Clear();
            txtKeyCMND.Clear();
            txtKeyEmail.Clear();
            txtKeySDT.Clear();
            txtKeyDiaChi.Clear();
            cboKeyLoai.SelectedIndex = -1;
            cboKeyTrangThai.SelectedIndex = 0;
            capNhatDanhSach();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            List<eNhanVien> ls = htNhanVien.layDanhSachNhanVien()
                .Where(n =>
                CongCu.Loai.XoaUnicode(n.TenNhanVien).Contains(CongCu.Loai.XoaUnicode(txtKeyTenNhanVien.Text)) &&
                n.CMND.Contains(txtKeyCMND.Text) &&
                CongCu.Loai.XoaUnicode(n.EMail).Contains(CongCu.Loai.XoaUnicode(txtKeyEmail.Text)) &&
                n.SoDienThoai.Contains(txtKeySDT.Text) &&
                CongCu.Loai.XoaUnicode(n.DiaChi).Contains(CongCu.Loai.XoaUnicode(txtKeyDiaChi.Text)) &&
                n.TrangThai.Contains(cboKeyTrangThai.SelectedItem.ToString())
                ).ToList();
            if (cboKeyLoai.SelectedIndex != -1)
                ls = ls.Where(n => n.MaLoaiNhanVien.Contains(((eLoaiNhanVien)cboKeyLoai.SelectedItem).MaLoaiNhanVien)).ToList();
            capNhatDanhSach(ls);
        }
    }
}
