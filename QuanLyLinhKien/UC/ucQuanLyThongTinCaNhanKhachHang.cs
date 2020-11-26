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
    public partial class ucQuanLyThongTinCaNhanKhachHang : UserControl
    {
        private bKhachHang htKhachHang;
        private bTaiKhoan htTaiKhoan;
        private string maNguoiDung;

        public ucQuanLyThongTinCaNhanKhachHang(string maNguoiDung)
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
            htKhachHang = new bKhachHang();
            htTaiKhoan = new bTaiKhoan();
            
            eKhachHang nv = htKhachHang.thongTinKhachHang(maNguoiDung);
            eTaiKhoan tk = htTaiKhoan.thongTinTaiKhoan(maNguoiDung);

            txtMaKhachHang.Text = nv.MaKhachHang;
            txtTenTenKhachHang.Text = nv.TenKhachHang;
            txtDiaChi.Text = nv.DiaChi;
            txtEmail.Text = nv.EMail;
            txtSoDienThoai.Text = nv.SoDienThoai;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            tatMo(true);
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                htKhachHang.suaKhachHang(new eKhachHang
                {
                    DiaChi = txtDiaChi.Text,
                    EMail = txtEmail.Text,
                    MaKhachHang = txtMaKhachHang.Text,
                    SoDienThoai = txtSoDienThoai.Text,
                    TenKhachHang = txtTenTenKhachHang.Text
                });
            }
            catch (System.Exception ex)
            {
                MessageBoxEx.Show(this, ex.Message, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                return;
            }

            if (chkDoiMatKhau.Checked)
            {
                if (htTaiKhoan.layMatKhauTheoMaTaiKhoan(txtMaKhachHang.Text) == txtMatKhau.Text.GetHashCode().ToString() && txtMatKhauMoi.Text == txtNhapLaiMatKhauMoi.Text)
                {
                    htTaiKhoan.suaTaiKhoan(new eTaiKhoan
                    {
                        MaTaiKhoan = txtMaKhachHang.Text,
                        MatKhau = txtNhapLaiMatKhauMoi.Text.GetHashCode().ToString()
                    });
                    MessageBoxEx.Show(this, "Cập nhật thông tin tài khoản và mật khẩu thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    tatMo(false);
                    return;
                }
                else
                {
                    MessageBox.Show("" + htTaiKhoan.layMatKhauTheoMaTaiKhoan(txtMaKhachHang.Text) + "    " + txtMatKhau.Text.GetHashCode().ToString() + "  " + txtMatKhauMoi.Text + "  " + txtNhapLaiMatKhauMoi.Text);
                    MessageBoxEx.Show(this, "Mật khẩu sai hoặc nhập mật khẩu mới không giống nhau, mời nhập lại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            MessageBoxEx.Show(this, "Cập nhật thông tin tài khoản thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            tatMo(false);
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            tatMo(false);
        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {
            if (txtMatKhau.Text.Trim().Length > 0)
                txtMatKhauMoi.Enabled = txtNhapLaiMatKhauMoi.Enabled = true;
            else
                txtMatKhauMoi.Enabled = txtNhapLaiMatKhauMoi.Enabled = false;
        }

        private void chkDoiMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            grpMatKhau.Enabled = chkDoiMatKhau.Checked;
        }
    }
}
