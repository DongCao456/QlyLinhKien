using BLL;
using DevComponents.DotNetBar;
using Entity;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyLinhKien.UC
{
    public partial class ucQuanLyThongTinCaNhan : UserControl
    {
        private bNhanVien htNhanVien;
        private bLoaiNhanVien htLoaiNhanVien;
        private bTaiKhoan htTaiKhoan;
        private string maNguoiDung;

        public ucQuanLyThongTinCaNhan(string maNguoiDung)
        {
            InitializeComponent();
            this.maNguoiDung = maNguoiDung;
            capNhatThongTin();
        }
        private void tatMo(bool n)
        {
            if (!n)
            {
                chkDoiMatKhau.Checked = false;
                txtMatKhau.Clear();
                txtMatKhauMoi.Clear();
                txtNhapLaiMatKhauMoi.Clear();
            }
            grpThongTinCaNhan.Enabled = btnQuayLai.Enabled = btnXacNhan.Enabled = chkDoiMatKhau.Enabled = n;
            btnSua.Enabled = !n;
        }
        public void capNhatThongTin()
        {
            htNhanVien = new bNhanVien();
            htLoaiNhanVien = new bLoaiNhanVien();
            htTaiKhoan = new bTaiKhoan();



            eNhanVien nv = htNhanVien.thongTinNhanVien(maNguoiDung);
            eTaiKhoan tk = htTaiKhoan.thongTinTaiKhoan(maNguoiDung);

            txtMaNhanVien.Text = nv.MaNhanVien;
            txtTenNhanVien.Text = nv.TenNhanVien;
            txtTrangThai.Text = nv.TrangThai;
            txtLoai.Text = htLoaiNhanVien.thongTinLoaiNhanVien(nv.MaLoaiNhanVien).TenLoaiNhanVien;
            txtDiaChi.Text = nv.DiaChi;
            txtChungMinhNhanDan.Text = nv.CMND;
            txtEmail.Text = nv.EMail;
            txtSoDienThoai.Text = nv.SoDienThoai;
        }

        private void txtMatKhau_TextChanged(object sender, System.EventArgs e)
        {
            if (txtMatKhau.Text.Trim().Length > 0)
                txtMatKhauMoi.Enabled = txtNhapLaiMatKhauMoi.Enabled = true;
            else
                txtMatKhauMoi.Enabled = txtNhapLaiMatKhauMoi.Enabled = false;
        }

        private void btnSua_Click(object sender, System.EventArgs e)
        {
            tatMo(true);
        }

        private void btnXacNhan_Click(object sender, System.EventArgs e)
        {
            try
            {
                htNhanVien.suaNhanVien(new eNhanVien
                {
                    CMND = txtChungMinhNhanDan.Text,
                    DiaChi = txtDiaChi.Text,
                    EMail = txtEmail.Text,
                    MaLoaiNhanVien = htLoaiNhanVien.layDanhSachLoaiNhanVien().Single(n => n.TenLoaiNhanVien == txtLoai.Text).MaLoaiNhanVien,
                    MaNhanVien = txtMaNhanVien.Text,
                    SoDienThoai = txtSoDienThoai.Text,
                    TenNhanVien = txtTenNhanVien.Text,
                    TrangThai = txtTrangThai.Text
                });
            }
            catch (System.Exception ex)
            {
                MessageBoxEx.Show(this,ex.Message, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                return;
            }
            
            if (chkDoiMatKhau.Checked)
            {
                if (htTaiKhoan.layMatKhauTheoMaTaiKhoan(txtMaNhanVien.Text) == txtMatKhau.Text.GetHashCode().ToString() && txtMatKhauMoi.Text == txtNhapLaiMatKhauMoi.Text)
                {
                    htTaiKhoan.suaTaiKhoan(new eTaiKhoan
                    {
                        MaTaiKhoan = txtMaNhanVien.Text,
                        MatKhau = txtNhapLaiMatKhauMoi.Text.GetHashCode().ToString()
                    });
                    MessageBoxEx.Show(this, "Cập nhật thông tin tài khoản và mật khẩu thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    tatMo(false);
                    return;
                }
                else
                {
                    MessageBoxEx.Show(this, "Mật khẩu sai hoặc nhập mật khẩu mới không giống nhau, mời nhập lại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            MessageBoxEx.Show(this, "Cập nhật thông tin tài khoản thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            tatMo(false);
        }

        private void btnQuayLai_Click(object sender, System.EventArgs e)
        {
            tatMo(false);
        }

        private void chkDoiMatKhau_CheckedChanged(object sender, System.EventArgs e)
        {
            grpMatKhau.Enabled = chkDoiMatKhau.Checked;
        }
    }
}
