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
    public partial class ucQuanLyLoaiNhanVien : UserControl
    {
        private bLoaiNhanVien htLoaiNhanVien;
        private bNhanVien htNhanVien;

        private byte loaiTacVu = 0;
        private System.Windows.Forms.TabControl tabFather;
        private List<eLoaiNhanVien> ls_Temp;
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
                    dockLoaiLinhKien.Text = "Tìm kiếm";
                    tabLoaiLinhKien.SelectedIndex = 1;
                }
                else
                {
                    dockLoaiLinhKien.Text = "Thông tin";
                    tabLoaiLinhKien.SelectedIndex = 0;
                }
            }
        }

        public ucQuanLyLoaiNhanVien(System.Windows.Forms.TabControl tabFather)
        {
            InitializeComponent();
            themChucNang();

            this.tabFather = tabFather;
            hideTabMenu();
            capNhatDanhSach();
        }

        private void hideTabMenu()
        {
            tabLoaiLinhKien.Appearance = TabAppearance.FlatButtons;
            tabLoaiLinhKien.ItemSize = new Size(0, 1);
            tabLoaiLinhKien.SizeMode = TabSizeMode.Fixed;
        }

        public void capNhatDanhSach(List<eLoaiNhanVien> ls = null)
        {
            htLoaiNhanVien = new bLoaiNhanVien();
            htNhanVien = new bNhanVien();
            dgvLoaiNhanVien.Rows.Clear();
            if (ls == null)
            {
                ls_Temp = htLoaiNhanVien.layDanhSachLoaiNhanVien();
            }
            else
            {
                ls_Temp = ls;
            }
            var lsAll = ls_Temp.Select(n => new
            {
                stt = int.Parse(n.MaLoaiNhanVien.Split('-')[1]),
                MaLoaiNhanVien = n.MaLoaiNhanVien,
                TenLoaiNhanVien = n.TenLoaiNhanVien
            }).OrderBy(n => n.stt);
            foreach (var item in lsAll)
            {
                dgvLoaiNhanVien.Rows.Add();
                int stt = dgvLoaiNhanVien.Rows.Count - 1;
                dgvLoaiNhanVien.Rows[stt].Cells[0].Value = item.MaLoaiNhanVien;
                dgvLoaiNhanVien.Rows[stt].Cells[1].Value = item.TenLoaiNhanVien;
            }
            ((ucQuanLyNhanVien)tabFather.TabPages[4].Controls[0]).themDuLieuVaoCB();
        }

        private void listResize()
        {
            dgvLoaiNhanVien.Columns[0].Width = dgvLoaiNhanVien.Width * 20 / 100;
        }

        private void latMoTextBox(bool n)
        {

            txtTenLoaiNhanVien.Enabled = n;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = dgvLoaiNhanVien.Enabled = dgvChucNang.Columns[1].ReadOnly = !n;
            btnXacNhan.Enabled = btnQuayLai.Enabled = n;
        }

        private void clearText()
        {
            txtMaLoaiNhanVien.Clear();
            txtTenLoaiNhanVien.Clear();
        }

        private void dgvLoaiNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            txtMaLoaiNhanVien.Text = dgvLoaiNhanVien.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtTenLoaiNhanVien.Text = dgvLoaiNhanVien.Rows[e.RowIndex].Cells[1].Value.ToString();
            
            chucNangToList(htLoaiNhanVien.thongTinLoaiNhanVien(txtMaLoaiNhanVien.Text).MaPhanQuyen);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            clearText();
            txtMaLoaiNhanVien.Text = "LNV-" + (htLoaiNhanVien.layDanhSachLoaiNhanVien().Select(n => new { stt = int.Parse(n.MaLoaiNhanVien.Split('-')[1]) }).Max(n => n.stt) + 1);
            latMoTextBox(true);
            loaiTacVu = 1;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaLoaiNhanVien.Text.Trim().Length == 0)
            {
                MessageBoxEx.Show(this, "Mời chọn loại nhân viên cần sửa...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            latMoTextBox(true);
            loaiTacVu = 2;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaLoaiNhanVien.Text.Trim().Length > 0)
            {
                if (htNhanVien.layDanhSachNhanVien().Where(n => n.MaLoaiNhanVien == txtMaLoaiNhanVien.Text).Count() > 0)
                {
                    MessageBoxEx.Show(this, "Không thể xoá loại nhân viên này, vì loại nhân viên này đã được sử dụng...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                    return;
                }
                if (MessageBoxEx.Show(this, "Bạn có muốn xoá loại nhân viên " + txtMaLoaiNhanVien.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    htLoaiNhanVien.xoaLoaiLinhKiem(txtMaLoaiNhanVien.Text);
                    capNhatDanhSach();
                    clearText();
                    MessageBoxEx.Show(this, "Xoá thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBoxEx.Show(this, "Mời chọn loại nhân viên cần xoá...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                switch (loaiTacVu)
                {
                    case 1:
                        if (htLoaiNhanVien.themLoaiNhanVien(new eLoaiNhanVien()
                        {
                            MaLoaiNhanVien = txtMaLoaiNhanVien.Text,
                            TenLoaiNhanVien = txtTenLoaiNhanVien.Text,
                            MaPhanQuyen = chucNangtoString()
                        }))

                        {
                            MessageBoxEx.Show(this, "Thêm thành công...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        }
                        else
                        {
                            MessageBoxEx.Show(this, "Bị trùng mã hoặc loại nhân viên đã tồn tại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            return;
                        }
                        capNhatDanhSach();
                        latMoTextBox(false);
                        break;
                    case 2:
                        htLoaiNhanVien.suaLoaiNhanVien(new eLoaiNhanVien()
                        {
                            MaLoaiNhanVien = txtMaLoaiNhanVien.Text,
                            TenLoaiNhanVien = txtTenLoaiNhanVien.Text,
                            MaPhanQuyen = chucNangtoString()
                        });
                        latMoTextBox(false);
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
            latMoTextBox(false);
            clearText();
            loaiTacVu = 0;
            int stt = dgvChucNang.Rows.Count;
            for (int i = 0; i < stt; i++)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dgvChucNang.Rows[i].Cells[1];
                chk.Value = false;
            }
        }

        private void dgvLoaiNhanVien_Resize(object sender, EventArgs e)
        {
            listResize();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            capNhatDanhSach(htLoaiNhanVien.layDanhSachLoaiNhanVien().Where(n => CongCu.Loai.XoaUnicode(n.TenLoaiNhanVien).Contains(CongCu.Loai.XoaUnicode(txtKeyTenLoaiNhanVien.Text))).ToList());
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtKeyTenLoaiNhanVien.Clear();
            capNhatDanhSach();
        }

        private string chucNangtoString()
        {
            int stt = dgvChucNang.Rows.Count;
            string cn = "";
            for (int i = 0; i < stt; i++)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dgvChucNang.Rows[i].Cells[1];
                if (chk.Value.Equals(false))
                    cn += 0;
                else
                    cn += 1;
            }
            return cn;
        }


        private void chucNangToList(string maChucNang)
        {
            List<char> ls = maChucNang.ToList();
            int stt = dgvChucNang.Rows.Count;
            for (int i = 0; i < stt; i++)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dgvChucNang.Rows[i].Cells[1];
                if (ls[i].Equals('1'))
                    chk.Value = true;
                else
                    chk.Value = false;
            }
        }
        private void themChucNang()
        {
            List<string> lsChucNang = new List<string>()
            {
                /*0*/"Lập đơn đặt hàng",
                /*1*/"Thành toán đơn đặt hàng",
                /*2*/"Lập phiếu nhập kho",
                /*3*/"Thanh toán phiếu nhập kho",
                /*4*/"Truy xuất đơn đặt hàng",
                /*5*/"Truy xuất phiếu nhập kho",
                /*6*/"Quản lý đơn đặt hàng",
                /*7*/"Quản lý chi tiết đơn đặt hàng",
                /*8*/"Quản lý nhân viên",
                /*9*/"Quản lý khách hàng",
                /*10*/"Quản lý linh kiện",
                /*11*/"Quản lý loại linh kiện",
                /*12*/"Quản lý nhà cung cấp",
                /*13*/"Quản lý loại nhân viên",
                /*14*/"Quản lý phiếu nhập kho",
                /*15*/"Quản lý chi tiết phiếu nhập kho",
                /*16*/"Tìm đơn đặt hàng",
                /*17*/"Tìm chi tiết đơn đặt hàng",
                /*18*/"Tìm nhân viên",
                /*19*/"Tìm khách hàng",
                /*20*/"Tìm linh kiên",
                /*21*/"Tìm loại linh kiện",
                /*22*/"Tìm nhà cung cấp",
                /*23*/"Tìm loại nhân viên",
                /*24*/"Tìm phiếu nhập kho",
                /*25*/"Tìm chi tiết phiếu nhập kho",
                /*26*/"Thống kê đơn đặt hàng",
                /*27*/"Thống kê nhân viên",
                /*28*/"Thống kê khách hàng",
                /*29*/"Thống kê linh kiện",
                /*30*/"Thống kê phiếu nhập kho",
                /*31*/"Thống kê nhà cung cấp"
            };
            foreach (string item in lsChucNang)
            {
                dgvChucNang.Rows.Add();
                int stt = dgvChucNang.Rows.Count - 1;
                dgvChucNang.Rows[stt].Cells[0].Value = item;
            }
        }
    }
}
