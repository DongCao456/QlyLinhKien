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
    public partial class ucThongKeDonDatHang : UserControl
    {
        private bNhanVien htNhanVien;
        private bDonDatHang htDonDatHang;
        private bChiTietDonDatHang htChiTietDonDatHang;
        private bKhachHang htKhachHang;
        private bool capNhatMoi = false;
        private System.Windows.Forms.TabControl tabFather;

        public bool CapNhatMoi
        {
            set
            {
                if (value == true)
                {
                    chuanBiDuLieu();
                    rdoMacDinh.Checked = true;
                    capNhatDanhSachHoaDon();
                }
                capNhatMoi = value;
            }
        }

        public ucThongKeDonDatHang(System.Windows.Forms.TabControl tabFather)
        {
            InitializeComponent();
            chuanBiDuLieu();
            
            this.tabFather = tabFather;
            capNhatDanhSachHoaDon();
        }
        private void chuanBiDuLieu()
        {
            htNhanVien = new bNhanVien();
            htDonDatHang = new bDonDatHang();
            htChiTietDonDatHang = new bChiTietDonDatHang();
            htKhachHang = new bKhachHang();
            dtmNgayBatDau.Value = DateTime.Now.AddMonths(-1);
            dtmNgayKetThuc.Value = DateTime.Now;
        }
        private void capNhatDanhSachHoaDon()
        {
            htNhanVien = new bNhanVien();
            htDonDatHang = new bDonDatHang();
            htChiTietDonDatHang = new bChiTietDonDatHang();
            dgvBaoCao.Rows.Clear();
            var ls = htDonDatHang.layDanhSachDonDatHang()
                .Where(n => n.NgayLap >= dtmNgayBatDau.Value && n.NgayLap <= dtmNgayKetThuc.Value && n.TrangThai != "Chưa thanh toán")
                .Select(n => new
                {
                    stt = int.Parse(n.MaDonDatHang.Split('-')[1]),
                    maHoaDon = n.MaDonDatHang,
                    nhanVienTuVan = htNhanVien.thongTinNhanVien(n.MaNhanVienTuVan).TenNhanVien,
                    nhanVienThuNgan = htNhanVien.thongTinNhanVien(n.MaNhanVienThuNgan).TenNhanVien,
                    tenKhachHang = htKhachHang.thongTinKhachHang(n.MaKhachHang).TenKhachHang,
                    tongDoanhThu = n.TongTien
                })
                .OrderBy(n => n.stt)
                .ToList();

            if (rdoDoanhThuCaoNhat.Checked)
                ls = ls.OrderByDescending(n => n.tongDoanhThu).ToList();
            ls = ls.Take(int.Parse(nudTongSoLuong.Value.ToString())).ToList();
            foreach (var item in ls)
            {
                dgvBaoCao.Rows.Add();
                int stt = dgvBaoCao.Rows.Count - 1;
                dgvBaoCao.Rows[stt].Cells[0].Value = item.maHoaDon;
                dgvBaoCao.Rows[stt].Cells[1].Value = item.nhanVienTuVan;
                dgvBaoCao.Rows[stt].Cells[2].Value = item.nhanVienThuNgan;
                dgvBaoCao.Rows[stt].Cells[3].Value = item.tenKhachHang;
                dgvBaoCao.Rows[stt].Cells[4].Value = item.tongDoanhThu;
            }
            llblTongChi.Text = ls.Sum(n => n.tongDoanhThu).ToString("#,### VND");
        }

        private void rdTongQuan_CheckedChanged(object sender, EventArgs e)
        {
            capNhatDanhSachHoaDon();
        }

        private void dtNgayBatDau_ValueChanged(object sender, EventArgs e)
        {
            capNhatDanhSachHoaDon();
        }

        private void dtNgayKetThuc_ValueChanged(object sender, EventArgs e)
        {
            capNhatDanhSachHoaDon();
        }

        private void nudTongSoLuong_ValueChanged(object sender, EventArgs e)
        {
            capNhatDanhSachHoaDon();
        }
        public void truyXuatDonDatHang()
        {
            if (dgvBaoCao.SelectedRows.Count > 0)
            {
                ((ucTruyXuatDonDatHang)tabFather.TabPages[11].Controls[0]).Ddh = htDonDatHang.thongTinDonDatHang(dgvBaoCao.SelectedRows[0].Cells[0].Value.ToString());
                ((ucTruyXuatDonDatHang)tabFather.TabPages[11].Controls[0]).lastTabIndex = 20;
                tabFather.SelectedIndex = 11;
            }
            else
            {
                MessageBoxEx.Show(this, "Mời chọn đơn đặt hàng cần truy xuất đơn đặt hàng...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnInThongKe_Click(object sender, EventArgs e)
        {
            if (rdoDoanhThuCaoNhat.Checked)
                ((ucReport)tabFather.TabPages[19].Controls[0]).thongKeDonDatHang(dtmNgayBatDau.Value, dtmNgayKetThuc.Value, 1 ,rdoDoanhThuCaoNhat.Text, nudTongSoLuong.Value);
            else
                ((ucReport)tabFather.TabPages[19].Controls[0]).thongKeDonDatHang(dtmNgayBatDau.Value, dtmNgayKetThuc.Value, 0,rdoMacDinh.Text, nudTongSoLuong.Value);
            tabFather.SelectedIndex = 19;
        }
    }
}
