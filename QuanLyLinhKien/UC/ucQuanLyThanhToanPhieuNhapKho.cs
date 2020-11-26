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
    public partial class ucQuanLyThanhToanPhieuNhapKho : UserControl
    {
        private bPhieuNhapKho htPhieuNhapKho;
        private bNhaCungCap htNhaCungCap;
        private bNhanVien htNhanVien;
        private bLinhKien htLinhKien;
        private bChiTietPhieuNhapKho htChiTietPhieuNhapKho;

        private List<ePhieuNhapKho> lsPhieuNhapKho;
        private System.Windows.Forms.TabControl tabFather;

        public ucQuanLyThanhToanPhieuNhapKho(System.Windows.Forms.TabControl tabFather)
        {
            InitializeComponent();
            this.tabFather = tabFather;
            capNhatDanhSach();
        }
        public void capNhatDanhSach()
        {
            htPhieuNhapKho = new bPhieuNhapKho();
            htNhaCungCap = new bNhaCungCap();
            htNhanVien = new bNhanVien();
            htLinhKien = new bLinhKien();
            htChiTietPhieuNhapKho = new bChiTietPhieuNhapKho();
            dgvPhieuNhapKho.Rows.Clear();

            lsPhieuNhapKho = htPhieuNhapKho.layDanhSachPhieuNhapKho().Where(n => n.TrangThai == "Chưa thanh toán").ToList();
            var lsAll = lsPhieuNhapKho.Select(n => new
            {
                stt = int.Parse(n.MaPhieuNhapKho.Split('-')[1]),
                MaPhieuNhapKho = n.MaPhieuNhapKho,
                TenNhanVien = htNhanVien.thongTinNhanVien(n.MaNhanVienThuKho).TenNhanVien,
                TenNhaCungCap = htNhaCungCap.thongTinNhaCungCap(n.MaNhaCungCap).TenNhaCungCap,
                NgayLap = n.NgayLap,
                TongTien = n.TongTien,
                TrangThai = n.TrangThai
            }).OrderBy(n => n.stt);
            foreach (var item in lsAll)
            {
                dgvPhieuNhapKho.Rows.Add();
                int stt = dgvPhieuNhapKho.Rows.Count - 1;
                dgvPhieuNhapKho.Rows[stt].Cells[0].Value = item.MaPhieuNhapKho;
                dgvPhieuNhapKho.Rows[stt].Cells[1].Value = item.TenNhanVien;
                dgvPhieuNhapKho.Rows[stt].Cells[2].Value = item.TenNhaCungCap;
                dgvPhieuNhapKho.Rows[stt].Cells[3].Value = item.NgayLap;
                dgvPhieuNhapKho.Rows[stt].Cells[4].Value = item.TongTien;
                dgvPhieuNhapKho.Rows[stt].Cells[5].Value = item.TrangThai;
            }
        }

        private void btnXacNhanThanhToan_Click(object sender, EventArgs e)
        {
            if (dgvPhieuNhapKho.SelectedRows.Count > 0)
            {
                if (MessageBoxEx.Show(this, "Xác nhận thanh toán...", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    ePhieuNhapKho n = lsPhieuNhapKho.Single(m => m.MaPhieuNhapKho == dgvPhieuNhapKho.SelectedRows[0].Cells[0].Value.ToString());
                    htPhieuNhapKho.suaPhieuNhapKho(new ePhieuNhapKho()
                    {
                        MaPhieuNhapKho = n.MaPhieuNhapKho,
                        MaNhaCungCap = n.MaNhaCungCap,
                        MaNhanVienThuKho = n.MaNhanVienThuKho,
                        MaNhanVienKeToanKho = n.MaNhanVienKeToanKho,
                        NgayLap = n.NgayLap,
                        TrangThai = "Đã thanh toán",
                        TongTien = n.TongTien
                    });
                    List<eChiTietPhieuNhapKho> ls = htChiTietPhieuNhapKho.layDanhSachChiTietPhieuNhapKho().Where(m => m.MaPhieuNhapKho == n.MaPhieuNhapKho).ToList();
                    foreach (eChiTietPhieuNhapKho item in ls)
                    {
                        eLinhKien lk = htLinhKien.thongTinLinhKien(item.MaLinhKien);
                        lk.SoLuong = lk.SoLuong + item.SoLuong;
                        htLinhKien.suaLinhKien(lk);
                    }

                    capNhatDanhSach();
                    MessageBoxEx.Show(this, "Thanh toán thành công ?", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBoxEx.Show(this, "Mời chọn phiếu nhập kho cần thanh toán...", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }
    }
}
