using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using BLL;
using Entity;

namespace QuanLyLinhKien.UC
{
    public partial class ucThongKeLinhKien : UserControl
    {
        private bLoaiLinhKien htLoaiLinhKien;
        private bNhaCungCap htNhaCungCap;
        private bLinhKien htLinhKien;
        private bDonDatHang htDonDatHang;
        private bChiTietDonDatHang htChiTietDonDatHang;
        private List<eLinhKien> ls_Temp;
        private System.Windows.Forms.TabControl tabFather;
        private bool capNhatMoi = false;

        public bool CapNhatMoi
        {
            set
            {
                if (value == true)
                {
                    rdoMacDinh.Checked = true;
                    chuanBiDuLieu();
                    capNhatDanhSach();
                }
                capNhatMoi = value;
            }
        }

        public ucThongKeLinhKien(System.Windows.Forms.TabControl tabFather)
        {
            InitializeComponent();
            
            chuanBiDuLieu();
            this.tabFather = tabFather;
            ls_Temp = new List<eLinhKien>();
        }
        public void chuanBiDuLieu()
        {
            htLoaiLinhKien = new bLoaiLinhKien();
            htNhaCungCap = new bNhaCungCap();
            htChiTietDonDatHang = new bChiTietDonDatHang();
            htLinhKien = new bLinhKien();
            htDonDatHang = new bDonDatHang();
            dtmNgayBatDau.Value = DateTime.Now.AddMonths(-1);
            dtmNgayKetThuc.Value = DateTime.Now;
        }
        public void capNhatDanhSach()
        {
            dgvBaoCao.Rows.Clear();
            if (rdoConHang.Checked || rdoHetHang.Checked)
            {
                nudTongSoLuong.Enabled = dtmNgayBatDau.Enabled = dtmNgayKetThuc.Enabled = false;
                var ls = htLinhKien.layDanhSachLinhKien()
                     .Select(n => new
                     {
                         stt = int.Parse(n.MaLinhKien.Split('-')[1]),
                         maLinhKien = n.MaLinhKien,
                         tenLinhKien = n.TenLinhKien,
                         nhaCungCap = htNhaCungCap.thongTinNhaCungCap(n.MaNhaCungCap).TenNhaCungCap,
                         soLuongDaBan = "-",
                         soLuongTon = n.SoLuong
                     }).OrderBy(n => n.stt).ToList();
                if (rdoConHang.Checked)
                    ls = ls.Where(n => n.soLuongTon > 0).ToList();
                else
                    ls = ls.Where(n => n.soLuongTon == 0).ToList();
                foreach (var item in ls)
                {
                    dgvBaoCao.Rows.Add();
                    int stt = dgvBaoCao.Rows.Count - 1;
                    dgvBaoCao.Rows[stt].Cells[0].Value = item.maLinhKien;
                    dgvBaoCao.Rows[stt].Cells[1].Value = item.tenLinhKien;
                    dgvBaoCao.Rows[stt].Cells[2].Value = item.nhaCungCap;
                    dgvBaoCao.Rows[stt].Cells[3].Value = item.soLuongTon;
                    dgvBaoCao.Rows[stt].Cells[4].Value = item.soLuongDaBan;
                }
            }
            else
            {
                nudTongSoLuong.Enabled = dtmNgayBatDau.Enabled = dtmNgayKetThuc.Enabled = true;
                var ls = htChiTietDonDatHang.layDanhSachChiTietDonDatHang()
                .Where(n =>
                htDonDatHang.thongTinDonDatHang(n.MaDonDatHang).NgayLap >= dtmNgayBatDau.Value
                && htDonDatHang.thongTinDonDatHang(n.MaDonDatHang).NgayLap <= dtmNgayKetThuc.Value
                && htDonDatHang.thongTinDonDatHang(n.MaDonDatHang).TrangThai != "Chưa thanh toán")
                .GroupBy(n => n.MaLinhKien)
                .Select(n => new
                {
                    stt = int.Parse(n.Key.Split('-')[1]),
                    maLinhKien = n.Key,
                    tenLinhKien = htLinhKien.thongTinLinhKien(n.Key).TenLinhKien,
                    nhaCungCap = htNhaCungCap.thongTinNhaCungCap(htLinhKien.thongTinLinhKien(n.Key).MaNhaCungCap).TenNhaCungCap,
                    soLuongDaBan = n.Sum(p => p.SoLuong),
                    soLuongTon = htLinhKien.thongTinLinhKien(n.Key).SoLuong
                })
                .OrderBy(n => n.stt)
                .ToList();
                if (rdoBanChayNhat.Checked)
                    ls = ls.OrderByDescending(n => n.soLuongDaBan).ToList();
                ls = ls.Take(int.Parse(nudTongSoLuong.Value.ToString())).ToList();
                foreach (var item in ls)
                {
                    dgvBaoCao.Rows.Add();
                    int stt = dgvBaoCao.Rows.Count - 1;
                    dgvBaoCao.Rows[stt].Cells[0].Value = item.maLinhKien;
                    dgvBaoCao.Rows[stt].Cells[1].Value = item.tenLinhKien;
                    dgvBaoCao.Rows[stt].Cells[2].Value = item.nhaCungCap;
                    dgvBaoCao.Rows[stt].Cells[3].Value = item.soLuongTon;
                    dgvBaoCao.Rows[stt].Cells[4].Value = item.soLuongDaBan;
                }
            }
        }


        private void rdTongQuan_CheckedChanged(object sender, EventArgs e)
        {
            capNhatDanhSach();
        }

        private void rdConHang_CheckedChanged(object sender, EventArgs e)
        {
            capNhatDanhSach();
        }

        private void rdHetHang_CheckedChanged(object sender, EventArgs e)
        {
            capNhatDanhSach();
        }

        private void rdBanChayNhat_CheckedChanged(object sender, EventArgs e)
        {
            capNhatDanhSach();
        }

        private void cbLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            capNhatDanhSach();
        }

        private void cbNhaCungCap_SelectedIndexChanged(object sender, EventArgs e)
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
                ((ucTruyXuatDonDatHang)tabFather.TabPages[11].Controls[0]).capNhatDanhSachDonDatHang(htDonDatHang.layDanhSachDonDatHang()
                    .Where(n => n.TrangThai == "Đã thanh toán" &&
                    htChiTietDonDatHang.layDanhSachChiTietDonDatHang().Any(m => m.MaLinhKien == dgvBaoCao.SelectedRows[0].Cells[0].Value.ToString() && m.MaDonDatHang == n.MaDonDatHang) &&
                    n.NgayLap >= dtmNgayBatDau.Value && n.NgayLap <= dtmNgayKetThuc.Value
                    ).ToList());
                ((ucTruyXuatDonDatHang)tabFather.TabPages[11].Controls[0]).lastTabIndex = 23;
                tabFather.SelectedIndex = 11;
            }
            else
            {
                MessageBoxEx.Show(this, "Mời chọn linh kiện cần truy xuất đơn đặt hàng...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnInThongKe_Click(object sender, EventArgs e)
        {
            if (rdoBanChayNhat.Checked)
                ((ucReport)tabFather.TabPages[19].Controls[0]).thongKeLinhKien(dtmNgayBatDau.Value, dtmNgayKetThuc.Value, 1, rdoBanChayNhat.Text, nudTongSoLuong.Value);
            else if (rdoConHang.Checked)
                ((ucReport)tabFather.TabPages[19].Controls[0]).thongKeLinhKien(dtmNgayBatDau.Value, dtmNgayKetThuc.Value, 2, rdoConHang.Text, nudTongSoLuong.Value);
            else if (rdoHetHang.Checked)
                ((ucReport)tabFather.TabPages[19].Controls[0]).thongKeLinhKien(dtmNgayBatDau.Value, dtmNgayKetThuc.Value, 3, rdoHetHang.Text, nudTongSoLuong.Value);
            else
                ((ucReport)tabFather.TabPages[19].Controls[0]).thongKeLinhKien(dtmNgayBatDau.Value, dtmNgayKetThuc.Value, 0, rdoMacDinh.Text, nudTongSoLuong.Value);
            tabFather.SelectedIndex = 19;
        }
    }
}
