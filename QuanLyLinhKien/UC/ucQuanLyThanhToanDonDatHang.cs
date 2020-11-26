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
    public partial class ucQuanLyThanhToanDonDatHang : UserControl
    {
        private bDonDatHang htDonDatHang;
        private bKhachHang htKhachHang;

        private List<eDonDatHang> lsDonDatHang;
        private System.Windows.Forms.TabControl tabFather;

        public ucQuanLyThanhToanDonDatHang(System.Windows.Forms.TabControl tabFather)
        {
            InitializeComponent();
            this.tabFather = tabFather;
        }
        public void capNhatDanhSach()
        {
            htDonDatHang = new bDonDatHang();
            htKhachHang = new bKhachHang();

            dgvDonDatHang.Rows.Clear();
            lsDonDatHang = htDonDatHang.layDanhSachDonDatHang().Where(n => n.TrangThai == "Chưa thanh toán").ToList();

            var lsAll = lsDonDatHang.Select(n => new
            {
                stt = int.Parse(n.MaDonDatHang.Split('-')[1]),
                MaDonDatHang = n.MaDonDatHang,
                TenKhachHang = htKhachHang.thongTinKhachHang(n.MaKhachHang).TenKhachHang,
                NgayLap = n.NgayLap,
                TongTien = n.TongTien,
                TrangThai = n.TrangThai
            }).OrderBy(n => n.stt);

            foreach (var item in lsAll)
            {
                dgvDonDatHang.Rows.Add();
                int stt = dgvDonDatHang.Rows.Count - 1;
                dgvDonDatHang.Rows[stt].Cells[0].Value = item.MaDonDatHang;
                dgvDonDatHang.Rows[stt].Cells[1].Value = item.TenKhachHang;
                dgvDonDatHang.Rows[stt].Cells[2].Value = item.NgayLap;
                dgvDonDatHang.Rows[stt].Cells[3].Value = item.TongTien;
                dgvDonDatHang.Rows[stt].Cells[4].Value = item.TrangThai;
            }
        }

        private void btnXacNhanThanhToan_Click(object sender, EventArgs e)
        {
            if (dgvDonDatHang.SelectedRows.Count > 0)
            {
                if (MessageBoxEx.Show(this, "Xác nhận thanh toán...", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    eDonDatHang n = lsDonDatHang.Single(m => m.MaDonDatHang == dgvDonDatHang.SelectedRows[0].Cells[0].Value.ToString());
                    n.TrangThai = "Đã thanh toán";
                    htDonDatHang.suaDonDatHang(n);

                    capNhatDanhSach();

                    if (MessageBoxEx.Show(this, "Thanh toán thành công, bạn có muốn in hoá đơn không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        ((ucReport)tabFather.TabPages[19].Controls[0]).inDonDatHang(n.MaDonDatHang);
                        tabFather.SelectedIndex = 19;
                    }
                }
            }
            else
            {
                MessageBoxEx.Show(this, "Mời chọn đơn đặt hàng cần thanh toán...", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }
    }
}
