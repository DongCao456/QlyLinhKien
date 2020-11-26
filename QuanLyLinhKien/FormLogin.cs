using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using BLL;
using Entity;

namespace QuanLyLinhKien
{
    public partial class FormLogin : Office2007Form
    {
        private bTaiKhoan htTaiKhoan;
        public FormLogin()
        {
            (new FormPlashScreen()).ShowDialog();
            InitializeComponent();
            EnableGlass = false;
            htTaiKhoan = new bTaiKhoan();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            kiemTraTaiKhoan();
        }
        private void kiemTraTaiKhoan()
        {
            string matKhau = "";
            try
            {
                matKhau = htTaiKhoan.layMatKhauTheoMaTaiKhoan(txtMaTaiKhoan.Text.ToUpper());
            }
            catch (Exception)
            {
                MessageBoxEx.Show(this, "Sai mật khẩu hoặc tài khoản !!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }

            if (txtMatKhau.Text.Trim().Length != 0 && txtMatKhau.Text.GetHashCode().ToString() == matKhau)
            {
                this.Hide();
                (new FormGiaoDienChinh(txtMaTaiKhoan.Text.ToUpper())).ShowDialog();
                txtMatKhau.Clear();
                txtMaTaiKhoan.Clear();
                this.Show();
                htTaiKhoan = new bTaiKhoan();
            }
            else
            {
                MessageBoxEx.Show(this, "Sai mật khẩu hoặc tài khoản !!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }


        }
    }
}
