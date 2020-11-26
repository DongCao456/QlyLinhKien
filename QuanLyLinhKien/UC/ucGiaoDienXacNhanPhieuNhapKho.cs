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

namespace QuanLyLinhKien.UC
{
    public partial class ucGiaoDienXacNhanPhieuNhapKho : UserControl
    {
        private System.Windows.Forms.TabControl tabFather;
        private List<eChiTietPhieuNhapKho> lsChiTietDonPhieuNhapKho;

        private bPhieuNhapKho htPhieuNhapKho;
        private bChiTietPhieuNhapKho htChiTietPhieuNhapKho;
        private bLinhKien htLinhKien;

        private eNhaCungCap thongTinNhaCungCap;
        private eNhanVien nvKeToanKho;
        private eNhanVien nvThuKho;

        public ucGiaoDienXacNhanPhieuNhapKho(List<eChiTietPhieuNhapKho> ls, eNhaCungCap thongTinNhaCungCap, System.Windows.Forms.TabControl tabFather, eNhanVien nvKeToanKho, eNhanVien nvThuKho)
        {
            InitializeComponent();
            this.lsChiTietDonPhieuNhapKho = ls;
            this.tabFather = tabFather;
            this.thongTinNhaCungCap = thongTinNhaCungCap;
            htPhieuNhapKho = new bPhieuNhapKho();
            htChiTietPhieuNhapKho = new bChiTietPhieuNhapKho();
            htLinhKien = new bLinhKien();
            this.nvKeToanKho = nvKeToanKho;
            this.nvThuKho = nvThuKho;


            capNhatDanhSach();
            dienThongTinKhachHang();
            if (htPhieuNhapKho.layDanhSachPhieuNhapKho().Count == 0)
                txtMaPhieuNhapKho.Text = "PNK-1";
            else
                txtMaPhieuNhapKho.Text = "PNK-" + (htPhieuNhapKho.layDanhSachPhieuNhapKho().Select(n => new { stt = int.Parse(n.MaPhieuNhapKho.Split('-')[1]) }).Max(n => n.stt) + 1);
        }
        private void dienThongTinKhachHang()
        {
            txtKeToanKho.Text = nvKeToanKho.TenNhanVien;
            txtThuKho.Text = nvThuKho.TenNhanVien;
            txtNhaCungCap.Text = thongTinNhaCungCap.TenNhaCungCap;
            dtmNgayLap.Value = DateTime.Now;
        }
        private void capNhatDanhSach()
        {
            dgvChiTietPhieuNhapKho.Rows.Clear();
            foreach (eChiTietPhieuNhapKho item in lsChiTietDonPhieuNhapKho)
            {
                dgvChiTietPhieuNhapKho.Rows.Add();
                int stt = dgvChiTietPhieuNhapKho.Rows.Count - 1;
                dgvChiTietPhieuNhapKho.Rows[stt].Cells[0].Value = item.MaPhieuNhapKho;
                dgvChiTietPhieuNhapKho.Rows[stt].Cells[1].Value = htLinhKien.thongTinLinhKien(item.MaLinhKien).TenLinhKien;
                dgvChiTietPhieuNhapKho.Rows[stt].Cells[2].Value = item.SoLuong;
                dgvChiTietPhieuNhapKho.Rows[stt].Cells[3].Value = item.GiaMua;
                dgvChiTietPhieuNhapKho.Rows[stt].Cells[4].Value = item.ThanhTien;
            }

            listResize();

            double tongTien = 0;
            for (int i = 0; i < dgvChiTietPhieuNhapKho.Rows.Count; i++)
            {
                tongTien = tongTien + double.Parse(dgvChiTietPhieuNhapKho.Rows[i].Cells[4].Value.ToString());
            }
            llblTongTien.Text = "Tổng tiền : " + tongTien.ToString("#,###");
        }
        private void listResize()
        {
            dgvChiTietPhieuNhapKho.Columns[0].Width = dgvChiTietPhieuNhapKho.Width * 20 / 100;
            dgvChiTietPhieuNhapKho.Columns[1].Width = dgvChiTietPhieuNhapKho.Width * 20 / 100;
            dgvChiTietPhieuNhapKho.Columns[2].Width = dgvChiTietPhieuNhapKho.Width * 20 / 100;
            dgvChiTietPhieuNhapKho.Columns[3].Width = dgvChiTietPhieuNhapKho.Width * 20 / 100;
        }

        private void txtMaPhieuNhapKho_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvChiTietPhieuNhapKho.Rows.Count; i++)
            {
                dgvChiTietPhieuNhapKho.Rows[i].Cells[0].Value = txtMaPhieuNhapKho.Text;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            htPhieuNhapKho.themPhieuNhapKho(new ePhieuNhapKho()
            {
                MaPhieuNhapKho = txtMaPhieuNhapKho.Text,
                MaNhaCungCap = thongTinNhaCungCap.MaNhaCungCap,
                MaNhanVienKeToanKho = nvKeToanKho.MaNhanVien,
                MaNhanVienThuKho = nvThuKho.MaNhanVien,
                NgayLap = dtmNgayLap.Value,
                TrangThai = "Chưa thanh toán",
                TongTien = double.Parse(llblTongTien.Text.Split(':')[1].Trim())
            });

            for (int i = 0; i < dgvChiTietPhieuNhapKho.Rows.Count; i++)
            {
                eLinhKien m = htLinhKien.thongTinLinhKien(htLinhKien.layDanhSachLinhKien().Single(n => n.TenLinhKien == dgvChiTietPhieuNhapKho.Rows[i].Cells[1].Value.ToString()).MaLinhKien);
                htChiTietPhieuNhapKho.themChiTietPhieuNhapKho(new eChiTietPhieuNhapKho()
                {
                    MaPhieuNhapKho = txtMaPhieuNhapKho.Text,
                    MaLinhKien = m.MaLinhKien,
                    SoLuong = int.Parse(dgvChiTietPhieuNhapKho.Rows[i].Cells[2].Value.ToString()),
                    GiaMua = m.GiaMua,
                    ThanhTien = double.Parse(dgvChiTietPhieuNhapKho.Rows[i].Cells[4].Value.ToString())
                });
            }
            if (MessageBoxEx.Show(this, "Lập thành công phiếu nhập kho, bạn có muốn in phiếu nhập kho ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                ((ucReport)tabFather.TabPages[19].Controls[0]).inPhieuNhapKho(txtMaPhieuNhapKho.Text);
                tabFather.SelectedIndex = 19;
            }

            ((ucQuanLyNhapKho)(tabFather.TabPages[13].Controls[0])).ThongTinNhaCungCap = null;
            ((ucQuanLyNhapKho)(tabFather.TabPages[13].Controls[0])).thongTinThuKho = null;
            ((FormGiaoDienChinh)(tabFather.Parent)).anChucNangDeNhapHang(false);
            this.Parent.Controls.Remove(this);
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            tabFather.SelectedIndex = 13;
            this.Parent.Controls.Remove(this);
        }

        private void dgvChiTietPhieuNhapKho_Resize(object sender, EventArgs e)
        {
            listResize();
        }
    }
}
