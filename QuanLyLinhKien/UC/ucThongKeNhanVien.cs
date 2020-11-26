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
    public partial class ucThongKeNhanVien : UserControl
    {
        private bNhanVien htNhanVien;
        private bDonDatHang htDonDatHang;
        private bChiTietDonDatHang htChiTietDonDatHang;
        private bool capNhatMoi = false;
        private System.Windows.Forms.TabControl tabFather;

        public bool CapNhatMoi
        {
            set
            {
                if (value == true)
                {
                    rdoMacDinh.Checked = true;
                    capNhatDanhSach();
                }
                capNhatMoi = value;
            }
        }
        public ucThongKeNhanVien(System.Windows.Forms.TabControl tabFather)
        {
            InitializeComponent();
            
            chuanBiDuLieu();
            this.tabFather = tabFather;
            capNhatDanhSach();
        }
        public void chuanBiDuLieu()
        {
            htNhanVien = new bNhanVien();
            htDonDatHang = new bDonDatHang();
            htChiTietDonDatHang = new bChiTietDonDatHang();
            dtmNgayBatDau.Value = DateTime.Now.AddMonths(-1);
            dtmNgayKetThuc.Value = DateTime.Now;
        }
        public void capNhatDanhSach()
        {
            htNhanVien = new bNhanVien();
            htDonDatHang = new bDonDatHang();
            htChiTietDonDatHang = new bChiTietDonDatHang();
            dgvBaoCao.Rows.Clear();
            List<eDonDatHang> lsDDH = htDonDatHang.layDanhSachDonDatHang()
                .Where(n => n.NgayLap >= dtmNgayBatDau.Value && n.NgayLap <= dtmNgayKetThuc.Value && n.TrangThai == "Đã thanh toán").ToList();
            var ls = htNhanVien.layDanhSachNhanVien().Where(n => lsDDH.Any(m => m.MaNhanVienTuVan == n.MaNhanVien))
                .Select(n => new
                {
                    stt = int.Parse(n.MaNhanVien.Split('-')[1]),
                    maNhanVien = n.MaNhanVien,
                    tenNhanVien = n.TenNhanVien,
                    tongDonDatHang = lsDDH.Where(m => m.MaNhanVienTuVan == n.MaNhanVien).Count(),
                    tongSoLuong = lsDDH.Join(htChiTietDonDatHang.layDanhSachChiTietDonDatHang(), p => p.MaDonDatHang, l => l.MaDonDatHang, (p, l) => new
                    {
                        MaNhanVienThuNgan = p.MaNhanVienThuNgan,
                        MaNhanVienTuVan = p.MaNhanVienTuVan,
                        TrangThai = p.TrangThai,
                        SoLuong = l.SoLuong
                    }).Where(m => m.MaNhanVienTuVan == n.MaNhanVien).Sum(m => m.SoLuong),
                    tongDoanhThu = lsDDH.Where(m => m.MaNhanVienTuVan == n.MaNhanVien).Sum(m => m.TongTien)
                }).OrderBy(n => n.stt).ToList();

            if (rdoDoanhThuCaoNhat.Checked)
                ls = ls.OrderByDescending(n => n.tongDoanhThu).ToList();
            ls = ls.Take(int.Parse(nudTongSoLuong.Value.ToString())).ToList();
            foreach (var item in ls)
            {
                dgvBaoCao.Rows.Add();
                int stt = dgvBaoCao.Rows.Count - 1;
                dgvBaoCao.Rows[stt].Cells[0].Value = item.maNhanVien;
                dgvBaoCao.Rows[stt].Cells[1].Value = item.tenNhanVien;
                dgvBaoCao.Rows[stt].Cells[2].Value = item.tongDonDatHang;
                dgvBaoCao.Rows[stt].Cells[3].Value = item.tongSoLuong;
                dgvBaoCao.Rows[stt].Cells[4].Value = item.tongDoanhThu;
            }
        }
        private void rdTongQuan_CheckedChanged(object sender, EventArgs e)
        {
            capNhatDanhSach();
        }

        private void rdDoanhThuCaoNhat_CheckedChanged(object sender, EventArgs e)
        {
            capNhatDanhSach();
        }

        private void rdLapNhieuDonDatHangNhat_CheckedChanged(object sender, EventArgs e)
        {
            capNhatDanhSach();
        }

        private void rdBanNhieuLinhKienNhat_CheckedChanged(object sender, EventArgs e)
        {
            capNhatDanhSach();
        }

        private void dtNgayBatDau_ValueChanged(object sender, EventArgs e)
        {
            capNhatDanhSach();
        }

        private void dtNgayKetThuc_ValueChanged(object sender, EventArgs e)
        {
            capNhatDanhSach();
        }

        private void nudTongSoLuong_ValueChanged(object sender, EventArgs e)
        {
            capNhatDanhSach();
        }
        public void truyXuatDonDatHang()
        {
            if (dgvBaoCao.SelectedRows.Count > 0)
            {
                ((ucTruyXuatDonDatHang)tabFather.TabPages[11].Controls[0]).capNhatDanhSachDonDatHang(htDonDatHang.layDanhSachDonDatHang().Where(
                    n => n.MaNhanVienTuVan == dgvBaoCao.SelectedRows[0].Cells[0].Value.ToString() &&
                    n.NgayLap >= dtmNgayBatDau.Value && n.NgayLap <= dtmNgayKetThuc.Value && n.TrangThai == "Đã thanh toán"
                    ).ToList());
                ((ucTruyXuatDonDatHang)tabFather.TabPages[11].Controls[0]).lastTabIndex = 21;
                tabFather.SelectedIndex = 11;
            }
            else
            {
                MessageBoxEx.Show(this, "Mời chọn đơn đặt hàng cần truy xuất đơn đặt hàng...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnInThongKe_Click(object sender, EventArgs e)
        {
            if (rdoMacDinh.Checked)
                ((ucReport)tabFather.TabPages[19].Controls[0]).thongKeNhanVien(dtmNgayBatDau.Value, dtmNgayKetThuc.Value,0, rdoMacDinh.Text,nudTongSoLuong.Value);
            else if(rdoDoanhThuCaoNhat.Checked)
                ((ucReport)tabFather.TabPages[19].Controls[0]).thongKeNhanVien(dtmNgayBatDau.Value, dtmNgayKetThuc.Value, 1, rdoDoanhThuCaoNhat.Text, nudTongSoLuong.Value);
            tabFather.SelectedIndex = 19;
        }
    }
}
