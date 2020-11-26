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
    public partial class ucThongKeNhaCungCap : UserControl
    {
        private bNhaCungCap htNhaCungCap;
        private bPhieuNhapKho htPhieuNhapKho;
        private bChiTietPhieuNhapKho htChiTietPhieuNhapKho;
        private bool capNhatMoi = false;
        private System.Windows.Forms.TabControl tabFather;

        public bool CapNhatMoi
        {
            set
            {
                if (value == true)
                {
                    rdoMacDinh.Checked = true;
                    chuanBiDuLieu();
                    capNhatDanhSachKhachHang();
                }
                capNhatMoi = value;
            }
        }
        public ucThongKeNhaCungCap(System.Windows.Forms.TabControl tabFather)
        {
            InitializeComponent();
            
            chuanBiDuLieu();
            this.tabFather = tabFather;
            capNhatDanhSachKhachHang();
        }

        public void chuanBiDuLieu()
        {
            htNhaCungCap = new bNhaCungCap();
            htPhieuNhapKho = new bPhieuNhapKho();
            htChiTietPhieuNhapKho = new bChiTietPhieuNhapKho();
            dtmNgayBatDau.Value = DateTime.Now.AddMonths(-1);
            dtmNgayKetThuc.Value = DateTime.Now;
        }

        public void capNhatDanhSachKhachHang()
        {
            htNhaCungCap = new bNhaCungCap();
            htPhieuNhapKho = new bPhieuNhapKho();
            htChiTietPhieuNhapKho = new bChiTietPhieuNhapKho();
            dgvBaoCao.Rows.Clear();

            var ls = htPhieuNhapKho.layDanhSachPhieuNhapKho()
                .Where(n => n.NgayLap >= dtmNgayBatDau.Value && n.NgayLap <= dtmNgayKetThuc.Value && n.TrangThai !="Chưa thanh toán") // tìm DDH có trong thời gian
                .GroupBy(n => n.MaNhaCungCap)
                .Select(n => new
                {
                    stt = int.Parse(n.Key.Split('-')[1]),
                    maNhaCungCap = n.Key,
                    tenNhaCungCap = htNhaCungCap.thongTinNhaCungCap(n.Key).TenNhaCungCap,
                    tongPhieuNhapKho = n.Count(),
                    tongLinhKien = htChiTietPhieuNhapKho.layDanhSachChiTietPhieuNhapKho()
                    .Where(m => htPhieuNhapKho.layDanhSachPhieuNhapKho().Where(l => l.NgayLap >= dtmNgayBatDau.Value && l.NgayLap <= dtmNgayKetThuc.Value && l.MaNhaCungCap == n.Key && l.TrangThai != "Chưa thanh toán")
                    .Any(l => l.MaPhieuNhapKho == m.MaPhieuNhapKho)).Sum(m => m.SoLuong),
                    tongThanhToan = n.Sum(m => m.TongTien)
                }).OrderBy(n => n.stt).ToList();

            if (rdoTongThanhToanCaoNhat.Checked)
                ls = ls.OrderByDescending(n => n.tongThanhToan).ToList();

            ls = ls.Take(int.Parse(nudTongSoLuong.Value.ToString())).ToList();

            foreach (var item in ls)
            {
                dgvBaoCao.Rows.Add();
                int stt = dgvBaoCao.Rows.Count - 1;
                dgvBaoCao.Rows[stt].Cells[0].Value = item.maNhaCungCap;
                dgvBaoCao.Rows[stt].Cells[1].Value = item.tenNhaCungCap;
                dgvBaoCao.Rows[stt].Cells[2].Value = item.tongPhieuNhapKho;
                dgvBaoCao.Rows[stt].Cells[3].Value = item.tongLinhKien;
                dgvBaoCao.Rows[stt].Cells[4].Value = item.tongThanhToan;
            }
        }

        private void rdoTongQuan_CheckedChanged(object sender, EventArgs e)
        {
            capNhatDanhSachKhachHang();
        }

        private void rdoTongThanhToanCaoNhat_CheckedChanged(object sender, EventArgs e)
        {
            capNhatDanhSachKhachHang();
        }

        private void nudTongSoLuong_ValueChanged(object sender, EventArgs e)
        {
            capNhatDanhSachKhachHang();
        }

        private void dtmNgayBatDau_ValueChanged(object sender, EventArgs e)
        {
            capNhatDanhSachKhachHang();
        }

        private void dtmNgayKetThuc_ValueChanged(object sender, EventArgs e)
        {
            capNhatDanhSachKhachHang();
        }

        public void truyXuatPhieuNhapKho()
        {
            if (dgvBaoCao.SelectedRows.Count > 0)
            {
                ((ucTruyXuatPhieuNhapKho)tabFather.TabPages[18].Controls[0]).capNhatDanhSachPhieuNhapKho(
                    htPhieuNhapKho.layDanhSachPhieuNhapKho().Where(n => n.MaNhaCungCap == dgvBaoCao.SelectedRows[0].Cells[0].Value.ToString() &&
                    n.NgayLap >= dtmNgayBatDau.Value && n.NgayLap <= dtmNgayKetThuc.Value && n.TrangThai == "Đã thanh toán"
                    ).ToList());
                ((ucTruyXuatPhieuNhapKho)tabFather.TabPages[18].Controls[0]).lastTabIndex = 25;
                tabFather.SelectedIndex = 18;
            }
            else
            {
                MessageBoxEx.Show(this, "Mời chọn nhà cung cấp cần truy xuất đơn đặt hàng...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnInThongKe_Click(object sender, EventArgs e)
        {
            if (rdoTongThanhToanCaoNhat.Checked)
                ((ucReport)tabFather.TabPages[19].Controls[0]).thongKeNhaCungCap(dtmNgayBatDau.Value, dtmNgayKetThuc.Value, 1, rdoTongThanhToanCaoNhat.Text, nudTongSoLuong.Value);
            else
                ((ucReport)tabFather.TabPages[19].Controls[0]).thongKeNhaCungCap(dtmNgayBatDau.Value, dtmNgayKetThuc.Value, 0, rdoMacDinh.Text, nudTongSoLuong.Value);
            tabFather.SelectedIndex = 19;
        }
    }
}
