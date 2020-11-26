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
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar;

namespace QuanLyLinhKien.UC
{
    public partial class ucGiaoDienXacNhanDonDatHang : UserControl
    {
        private System.Windows.Forms.TabControl tabFather;
        private List<eChiTietDonDatHang> lsChiTietDonDatHang;

        private bDonDatHang htDonDatHang;
        private bChiTietDonDatHang htChiTietDonDatHang;
        private bLinhKien htLinhKien;

        private eKhachHang thongTinKhachHang;
        private eNhanVien nvThuNgan;
        private eNhanVien nvTuVan;

        public ucGiaoDienXacNhanDonDatHang(List<eChiTietDonDatHang> ls, eKhachHang thongTinKhachHang, System.Windows.Forms.TabControl tabFather, eNhanVien nvThuNgan,eNhanVien nvTuVan)
        {
            InitializeComponent();
            this.lsChiTietDonDatHang = ls;
            this.tabFather = tabFather;
            this.thongTinKhachHang = thongTinKhachHang;
            htDonDatHang = new bDonDatHang();
            htChiTietDonDatHang = new bChiTietDonDatHang();
            htLinhKien = new bLinhKien();
            this.nvThuNgan = nvThuNgan;
            this.nvTuVan = nvTuVan;


            capNhatDanhSach();
            dienThongTinKhachHang();
            if (htDonDatHang.layDanhSachDonDatHang().Count == 0)
                txtMaDonDatHang.Text = "DDH-1";
            else
                txtMaDonDatHang.Text = "DDH-" + (htDonDatHang.layDanhSachDonDatHang().Select(n => new { stt = int.Parse(n.MaDonDatHang.Split('-')[1]) }).Max(n => n.stt) + 1);
        }
        private void dienThongTinKhachHang()
        {
            txtMaNhanVien.Text = nvTuVan.TenNhanVien;
            txtMaKhachHang.Text = thongTinKhachHang.TenKhachHang;
            txtNoiNhanHang.Text = thongTinKhachHang.DiaChi;
            dtmNgayLap.Value = DateTime.Now;
        }
        private void capNhatDanhSach()
        {
            dgvChiTietDonDatHang.Rows.Clear();
            foreach (eChiTietDonDatHang item in lsChiTietDonDatHang)
            {
                dgvChiTietDonDatHang.Rows.Add();
                int stt = dgvChiTietDonDatHang.Rows.Count - 1;
                dgvChiTietDonDatHang.Rows[stt].Cells[0].Value = item.MaDonDatHang;
                dgvChiTietDonDatHang.Rows[stt].Cells[1].Value = htLinhKien.thongTinLinhKien(item.MaLinhKien).TenLinhKien;
                dgvChiTietDonDatHang.Rows[stt].Cells[2].Value = item.SoLuong;
                dgvChiTietDonDatHang.Rows[stt].Cells[3].Value = item.GiaBan;
                dgvChiTietDonDatHang.Rows[stt].Cells[4].Value = item.MucGiamGia;
            }

            listResize();

            double tongTien = 0;
            for (int i = 0; i < dgvChiTietDonDatHang.Rows.Count; i++)
            {
                double t = double.Parse(dgvChiTietDonDatHang.Rows[i].Cells[2].Value.ToString()) * double.Parse(dgvChiTietDonDatHang.Rows[i].Cells[3].Value.ToString());
                t = t - t * double.Parse(dgvChiTietDonDatHang.Rows[i].Cells[4].Value.ToString());
                tongTien = tongTien + t;
            }
            llblTongTien.Text = "Tổng tiền : " + tongTien.ToString("#,###");
        }
        private void listResize()
        {
            dgvChiTietDonDatHang.Columns[0].Width = dgvChiTietDonDatHang.Width * 20 / 100;
            dgvChiTietDonDatHang.Columns[1].Width = dgvChiTietDonDatHang.Width * 20 / 100;
            dgvChiTietDonDatHang.Columns[2].Width = dgvChiTietDonDatHang.Width * 20 / 100;
            dgvChiTietDonDatHang.Columns[3].Width = dgvChiTietDonDatHang.Width * 20 / 100;
            dgvChiTietDonDatHang.Columns[4].Width = dgvChiTietDonDatHang.Width * 20 / 100;
        }
        private void lsLK_Resize(object sender, EventArgs e)
        {
            listResize();
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            tabFather.SelectedIndex = 1;
            this.Parent.Controls.Remove(this);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            List<eDonDatHang> ls = htDonDatHang.layDanhSachDonDatHang();
            foreach (var item in ls)
            {
                if (txtMaDonDatHang.Text == item.MaDonDatHang)
                {
                    MessageBoxEx.Show(this, "Đã tồn tại mã hoá đôn này, mời nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            try
            {
                htDonDatHang.themDonDatHang(new eDonDatHang()
                {
                    MaDonDatHang = txtMaDonDatHang.Text,
                    MaKhachHang = thongTinKhachHang.MaKhachHang,
                    MaNhanVienThuNgan = nvThuNgan.MaNhanVien,
                    MaNhanVienTuVan = nvTuVan.MaNhanVien,
                    NgayLap = dtmNgayLap.Value,
                    NoiNhanHang = txtNoiNhanHang.Text,
                    TrangThai = "Chưa thanh toán",
                    TongTien = double.Parse(llblTongTien.Text.Split(':')[1].Trim())
                });
            }
            catch (Exception)
            {
                MessageBoxEx.Show(this, "Nơi nhận hàng không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }

            for (int i = 0; i < dgvChiTietDonDatHang.Rows.Count; i++)
            {
                eLinhKien m = htLinhKien.thongTinLinhKien(htLinhKien.layDanhSachLinhKien().Single(n=>n.TenLinhKien == dgvChiTietDonDatHang.Rows[i].Cells[1].Value.ToString()).MaLinhKien);
                htChiTietDonDatHang.themChiTietDonDatHang(new eChiTietDonDatHang()
                {
                    MaDonDatHang = txtMaDonDatHang.Text,
                    MaLinhKien = m.MaLinhKien,
                    SoLuong = int.Parse(dgvChiTietDonDatHang.Rows[i].Cells[2].Value.ToString()),
                    GiaBan = m.GiaBan,
                    MucGiamGia = double.Parse(dgvChiTietDonDatHang.Rows[i].Cells[4].Value.ToString()),
                    ThanhTien = (100 - double.Parse(dgvChiTietDonDatHang.Rows[i].Cells[4].Value.ToString())) * int.Parse(dgvChiTietDonDatHang.Rows[i].Cells[2].Value.ToString()) * m.GiaBan
                });
            }
            MessageBoxEx.Show(this, "Lập thành công đơn đặt hàng...", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            
            if (txtMaNhanVien.Text == "NV-1")
            {
                tabFather.SelectedIndex = 1;
            }
            else
            {
                ((ucQuanLyBanHang)(tabFather.TabPages[1].Controls[0])).ThongTinKhachHang = null;
                ((ucQuanLyBanHang)(tabFather.TabPages[1].Controls[0])).nvTuVan = null;
                ((FormGiaoDienChinh)(tabFather.Parent)).anChucNangDeDatHang(false);
                tabFather.SelectedIndex = 0;
            }
            this.Parent.Controls.Remove(this);
        }

        private void tbMaDonDatHang_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvChiTietDonDatHang.Rows.Count; i++)
            {
                dgvChiTietDonDatHang.Rows[i].Cells[0].Value = txtMaDonDatHang.Text;
            }
        }
    }
}
