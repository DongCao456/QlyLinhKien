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
    public partial class ucQuanLyLoaiLinhKien : UserControl
    {
        private bLoaiLinhKien htLoaiLinhKien;
        private bLinhKien htLinhKien;
        private byte loaiTacVu = 0;
        private System.Windows.Forms.TabControl tabFather;
        private List<eLoaiLinhKien> ls_Temp;
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

        public ucQuanLyLoaiLinhKien(System.Windows.Forms.TabControl tabFather)
        {
            InitializeComponent();
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

        public void capNhatDanhSach(List<eLoaiLinhKien> ls = null)
        {
            htLoaiLinhKien = new bLoaiLinhKien();
            htLinhKien = new bLinhKien();
            dgvLoaiLinhKien.Rows.Clear();
            if (ls == null)
            {
                ls_Temp = htLoaiLinhKien.layDanhSachLoaiLinhKien();
            }
            else
            {
                ls_Temp = ls;
            }
            var lsALL = ls_Temp.Select(n => new
            {
                stt = int.Parse(n.MaLoai.Split('-')[1]),
                MaLoai = n.MaLoai,
                TenLoai = n.TenLoai
            }).OrderBy(n => n.stt);
            foreach (var item in lsALL)
            {
                dgvLoaiLinhKien.Rows.Add();
                int stt = dgvLoaiLinhKien.Rows.Count - 1;
                dgvLoaiLinhKien.Rows[stt].Cells[0].Value = item.MaLoai;
                dgvLoaiLinhKien.Rows[stt].Cells[1].Value = item.TenLoai;
            }
            ((ucQuanLyLinhKien)tabFather.TabPages[6].Controls[0]).themDuLieuVaoCB();
        }

        private void listResize()
        {
            dgvLoaiLinhKien.Columns[0].Width = dgvLoaiLinhKien.Width * 20 / 100;
        }

        private void latMoTextBox(bool n)
        {
            txtTenLoaiLinhKien.Enabled = n;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = dgvLoaiLinhKien.Enabled = !n;
            btnXacNhan.Enabled = btnQuayLai.Enabled = n;
        }

        private void clearText()
        {
            txtMaLoaiLinhKien.Clear();
            txtTenLoaiLinhKien.Clear();
        }

        private void lsLLK_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            txtMaLoaiLinhKien.Text = dgvLoaiLinhKien.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtTenLoaiLinhKien.Text = dgvLoaiLinhKien.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            clearText();
            txtMaLoaiLinhKien.Text = "LLK-" + (htLoaiLinhKien.layDanhSachLoaiLinhKien().Select(n => new { stt = int.Parse(n.MaLoai.Split('-')[1]) }).Max(n => n.stt) + 1);
            latMoTextBox(true);
            loaiTacVu = 1;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaLoaiLinhKien.Text.Trim().Length == 0)
            {
                MessageBoxEx.Show(this, "Mời chọn loại linh kiện cần sửa...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            latMoTextBox(true);
            loaiTacVu = 2;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaLoaiLinhKien.Text.Trim().Length > 0)
            {
                if (htLinhKien.layDanhSachLinhKien().Where(n => n.MaLoai == txtMaLoaiLinhKien.Text).Count() > 0)
                {
                    MessageBoxEx.Show(this, "Không thể xoá loại linh kiện này, vì loại linh kiện này đã được sử dụng...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                    return;
                }
                if (MessageBoxEx.Show(this, "Bạn có muốn xoá loại linh kiện " + txtMaLoaiLinhKien.Text, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    htLoaiLinhKien.xoaLoaiLinhKiem(txtMaLoaiLinhKien.Text);
                    capNhatDanhSach();
                    clearText();
                    MessageBoxEx.Show(this, "Xoá thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBoxEx.Show(this, "Mời chọn loại linh kiện cần xoá...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                switch (loaiTacVu)
                {
                    case 1:
                        if (htLoaiLinhKien.themLoaiLinhKien(new eLoaiLinhKien()
                        {
                            MaLoai = txtMaLoaiLinhKien.Text,
                            TenLoai = txtTenLoaiLinhKien.Text
                        }))

                        {
                            MessageBoxEx.Show(this, "Thêm thành công...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        }
                        else
                        {
                            MessageBoxEx.Show(this, "Bị trùng mã hoặc loại linh kiện đã tồn tại...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                            return;
                        }
                        capNhatDanhSach();
                        latMoTextBox(false);
                        break;
                    case 2:
                        htLoaiLinhKien.suaLoaiLinhKien(new eLoaiLinhKien()
                        {
                            MaLoai = txtMaLoaiLinhKien.Text,
                            TenLoai = txtTenLoaiLinhKien.Text,
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
            clearText();
            latMoTextBox(false);
            loaiTacVu = 0;
        }

        private void lsLLK_Resize(object sender, EventArgs e)
        {
            listResize();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            capNhatDanhSach(htLoaiLinhKien.layDanhSachLoaiLinhKien()
                .Where(n => CongCu.Loai.XoaUnicode(n.TenLoai).Contains(CongCu.Loai.XoaUnicode(txtKeyTenLoaiLinhKien.Text)))
                .ToList());
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtKeyTenLoaiLinhKien.Clear();
            capNhatDanhSach();
        }
    }
}
