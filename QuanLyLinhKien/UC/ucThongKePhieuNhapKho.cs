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
using DevComponents.DotNetBar;

namespace QuanLyLinhKien.UC
{
    public partial class ucThongKePhieuNhapKho : UserControl
    {
        private bNhanVien htNhanVien;
        private bPhieuNhapKho htPhieuNhapKho;
        private bChiTietPhieuNhapKho htChiTietDonDatHang;
        private bNhaCungCap htNhaCungCap;
        private bool capNhatMoi = false;
        private System.Windows.Forms.TabControl tabFather;

        public bool CapNhatMoi
        {
            set
            {
                if (value == true)
                {
                    chuanBiDuLieu();
                    capNhatDanhSach();
                    rdoMacDinh.Checked = true;
                }
                capNhatMoi = value;
            }
        }

        public ucThongKePhieuNhapKho(System.Windows.Forms.TabControl tabFather)
        {
            InitializeComponent();
            chuanBiDuLieu();
            this.tabFather = tabFather;
            capNhatDanhSach();
        }
        private void chuanBiDuLieu()
        {
            dtmNgayBatDau.Value = DateTime.Now.AddMonths(-1);
            dtmNgayKetThuc.Value = DateTime.Now;
        }
        private void capNhatDanhSach()
        {
            htNhanVien = new bNhanVien();
            htPhieuNhapKho = new bPhieuNhapKho();
            htChiTietDonDatHang = new bChiTietPhieuNhapKho();
            htNhaCungCap = new bNhaCungCap();

            dgvBaoCao.Rows.Clear();
            var ls = htPhieuNhapKho.layDanhSachPhieuNhapKho()
                .Where(n => n.NgayLap >= dtmNgayBatDau.Value && n.NgayLap <= dtmNgayKetThuc.Value && n.TrangThai != "Chưa thanh toán")
                .Select(n => new
                {
                    stt = int.Parse(n.MaPhieuNhapKho.Split('-')[1]),
                    maHoaDon = n.MaPhieuNhapKho,
                    nhanVienTuVan = htNhanVien.thongTinNhanVien(n.MaNhanVienThuKho).TenNhanVien,
                    nhanVienThuNgan = htNhanVien.thongTinNhanVien(n.MaNhanVienKeToanKho).TenNhanVien,
                    tenKhachHang = htNhaCungCap.thongTinNhaCungCap(n.MaNhaCungCap).TenNhaCungCap,
                    tongChi = n.TongTien
                })
                .OrderBy(n=>n.stt)
                .ToList();

            if (rdoChiCaoNhat.Checked)
                ls = ls.OrderByDescending(n => n.tongChi).ToList();

            ls = ls.Take(int.Parse(nudTongSoLuong.Value.ToString())).ToList();

            foreach (var item in ls)
            {
                dgvBaoCao.Rows.Add();
                int stt = dgvBaoCao.Rows.Count - 1;
                dgvBaoCao.Rows[stt].Cells[0].Value = item.maHoaDon;
                dgvBaoCao.Rows[stt].Cells[1].Value = item.nhanVienTuVan;
                dgvBaoCao.Rows[stt].Cells[2].Value = item.nhanVienThuNgan;
                dgvBaoCao.Rows[stt].Cells[3].Value = item.tenKhachHang;
                dgvBaoCao.Rows[stt].Cells[4].Value = item.tongChi;
            }
            llblTongDoanhThu.Text = ls.Sum(n => n.tongChi).ToString("#,### VND");
        }

        private void rdoDoanhThuCaoNhat_CheckedChanged(object sender, EventArgs e)
        {
            capNhatDanhSach();
        }

        private void nudTongSoLuong_ValueChanged(object sender, EventArgs e)
        {
            capNhatDanhSach();
        }

        private void dtmNgayBatDau_ValueChanged(object sender, EventArgs e)
        {
            capNhatDanhSach();
        }

        private void dtmNgayKetThuc_ValueChanged(object sender, EventArgs e)
        {
            capNhatDanhSach();
        }
        public void truyXuatPhieuNhapKho()
        {
            if (dgvBaoCao.SelectedRows.Count > 0)
            {
                ((ucTruyXuatPhieuNhapKho)tabFather.TabPages[18].Controls[0]).Pnk = htPhieuNhapKho.thongTinPhieuNhapKho(dgvBaoCao.SelectedRows[0].Cells[0].Value.ToString());
                ((ucTruyXuatPhieuNhapKho)tabFather.TabPages[18].Controls[0]).lastTabIndex = 24;
                tabFather.SelectedIndex = 18;
            }
            else
            {
                MessageBoxEx.Show(this, "Mời chọn phiếu nhập kho cần truy xuất đơn đặt hàng...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnInThongKe_Click(object sender, EventArgs e)
        {
            if (rdoChiCaoNhat.Checked)
                ((ucReport)tabFather.TabPages[19].Controls[0]).thongKePhieuNhapKho(dtmNgayBatDau.Value, dtmNgayKetThuc.Value, 1, rdoChiCaoNhat.Text, nudTongSoLuong.Value);
            else
                ((ucReport)tabFather.TabPages[19].Controls[0]).thongKePhieuNhapKho(dtmNgayBatDau.Value, dtmNgayKetThuc.Value, 0, rdoMacDinh.Text, nudTongSoLuong.Value);
            tabFather.SelectedIndex = 19;
        }
    }
}
