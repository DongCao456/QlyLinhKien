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
    public partial class ucThongKeKhachHang : UserControl
    {
        private bKhachHang htKhachHang;
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
                    chuanBiDuLieu();
                    capNhatDanhSachKhachHang();
                }
                capNhatMoi = value;
            }
        }
        public ucThongKeKhachHang(System.Windows.Forms.TabControl tabFather)
        {
            InitializeComponent();
            
            chuanBiDuLieu();
            this.tabFather = tabFather;
            capNhatDanhSachKhachHang();
        }
        public void chuanBiDuLieu()
        {
            htKhachHang = new bKhachHang();
            htDonDatHang = new bDonDatHang();
            htChiTietDonDatHang = new bChiTietDonDatHang();
            dtmNgayBatDau.Value = DateTime.Now.AddMonths(-1);
            dtmNgayKetThuc.Value = DateTime.Now;
        }
        public void capNhatDanhSachKhachHang()
        {
            htKhachHang = new bKhachHang();
            htDonDatHang = new bDonDatHang();
            htChiTietDonDatHang = new bChiTietDonDatHang();
            dgvBaoCao.Rows.Clear();

            var ls = htDonDatHang.layDanhSachDonDatHang()
                .Where(n => n.NgayLap >= dtmNgayBatDau.Value && n.NgayLap <= dtmNgayKetThuc.Value && n.TrangThai != "Chưa thanh toán") // tìm DDH có trong thời gian
                .Join(htChiTietDonDatHang.layDanhSachChiTietDonDatHang(), n => n.MaDonDatHang, m => m.MaDonDatHang, (n, m) => new
                {
                    maKhachHang = n.MaKhachHang,
                    soLuong = m.SoLuong,
                    doanhThu = m.ThanhTien
                })
                .GroupBy(n => n.maKhachHang)
                .Select(n => new
                {
                    stt = int.Parse(n.Key.Split('-')[1]),
                    maKhachHang = n.Key,
                    tenKhachHang = htKhachHang.thongTinKhachHang(n.Key).TenKhachHang,
                    tongDonDatHang = htDonDatHang.layDanhSachDonDatHang().Where(m => m.NgayLap >= dtmNgayBatDau.Value && m.NgayLap <= dtmNgayKetThuc.Value && m.MaKhachHang == n.Key && m.TrangThai != "Chưa thanh toán").Count(),
                    tongSoLuong = n.Sum(m => m.soLuong),
                    tongDoanhThu = n.Sum(m => m.doanhThu)
                })
                .ToList();

            if (rdoTongThanhToanCaoNhat.Checked)
            {
                ls = ls.OrderByDescending(n => n.tongDoanhThu).ToList();
            }
            else
            {
                ls = ls.OrderBy(n => n.stt).ToList();
            }

            ls = ls.Take(int.Parse(nudTongSoLuong.Value.ToString())).ToList();

            foreach (var item in ls)
            {
                dgvBaoCao.Rows.Add();
                int stt = dgvBaoCao.Rows.Count - 1;
                dgvBaoCao.Rows[stt].Cells[0].Value = item.maKhachHang;
                dgvBaoCao.Rows[stt].Cells[1].Value = item.tenKhachHang;
                dgvBaoCao.Rows[stt].Cells[2].Value = item.tongDonDatHang;
                dgvBaoCao.Rows[stt].Cells[3].Value = item.tongSoLuong;
                dgvBaoCao.Rows[stt].Cells[4].Value = item.tongDoanhThu;
            }
        }

        private void rdTongThanhToanCaoNhat_CheckedChanged(object sender, EventArgs e)
        {
            capNhatDanhSachKhachHang();
        }

        private void rdTongQuan_CheckedChanged(object sender, EventArgs e)
        {
            capNhatDanhSachKhachHang();
        }

        private void dtNgayBatDau_ValueChanged(object sender, EventArgs e)
        {
            capNhatDanhSachKhachHang();
        }

        private void dtNgayKetThuc_ValueChanged(object sender, EventArgs e)
        {
            capNhatDanhSachKhachHang();
        }

        private void nudTongSoLuong_ValueChanged(object sender, EventArgs e)
        {
            capNhatDanhSachKhachHang();
        }
        public void truyXuatDonDatHang()
        {
            if (dgvBaoCao.SelectedRows.Count > 0)
            {
                ((ucTruyXuatDonDatHang)tabFather.TabPages[11].Controls[0])
                    .capNhatDanhSachDonDatHang(
                    htDonDatHang.layDanhSachDonDatHang().Where(n => 
                    n.TrangThai == "Đã thanh toán" && 
                    n.MaKhachHang == dgvBaoCao.SelectedRows[0].Cells[0].Value.ToString() &&
                    n.NgayLap >= dtmNgayBatDau.Value &&
                    n.NgayLap <= dtmNgayKetThuc.Value
                    ).ToList()
                    );
                ((ucTruyXuatDonDatHang)tabFather.TabPages[11].Controls[0]).lastTabIndex = 22;
                tabFather.SelectedIndex = 11;
            }
            else
            {
                MessageBoxEx.Show(this, "Mời chọn khách hàng cần truy xuất đơn đặt hàng...", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnInThongKe_Click(object sender, EventArgs e)
        {
            if (rdoTongThanhToanCaoNhat.Checked)
                ((ucReport)tabFather.TabPages[19].Controls[0]).thongKeKhachHang(dtmNgayBatDau.Value, dtmNgayKetThuc.Value, 1, rdoTongThanhToanCaoNhat.Text, nudTongSoLuong.Value);
            else
                ((ucReport)tabFather.TabPages[19].Controls[0]).thongKeKhachHang(dtmNgayBatDau.Value, dtmNgayKetThuc.Value, 0, rdoMacDinh.Text, nudTongSoLuong.Value);
            tabFather.SelectedIndex = 19;
        }
    }
}
