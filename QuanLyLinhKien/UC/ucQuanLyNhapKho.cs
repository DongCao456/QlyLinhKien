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
using System.Text.RegularExpressions;

namespace QuanLyLinhKien.UC
{
    public partial class ucQuanLyNhapKho : UserControl
    {
        private eNhaCungCap thongTinNhaCungCap = null;
        public eNhanVien thongTinThuKho = null;
        private string maNhanVien;

        private bLinhKien htLinhKien;
        private bLoaiLinhKien htLoaiLinhKien;
        private bNhaCungCap htNhaCungCap;
        private bNhanVien htNhanVien;

        private List<eLinhKien> ls_Temp;
        private System.Windows.Forms.TabControl tabFather;

        public eNhaCungCap ThongTinNhaCungCap
        {
            get
            {
                return thongTinNhaCungCap;
            }
            set
            {
                if (value != null)
                {
                    llblChuDe.Text = "Chi tiết đơn phiếu nhập kho của nhà cung cấp " + value.MaNhaCungCap + " - " + value.TenNhaCungCap;
                    capNhatDanhSach(htLinhKien.layDanhSachLinhKien().Where(n => n.MaNhaCungCap == value.MaNhaCungCap).ToList());
                }
                else
                    dgvLinhKienDaChon.Rows.Clear();
                thongTinNhaCungCap = value;
            }
        }

        public ucQuanLyNhapKho(System.Windows.Forms.TabControl tab, string maNhanVien = null)
        {
            InitializeComponent();
            htNhanVien = new bNhanVien();
            this.maNhanVien = maNhanVien;
            tabFather = tab;
            capNhatDanhSach();
        }
        public void lamMoiDanhSach()
        {
            dgvLinhKienDaChon.Rows.Clear();
        }
        public void capNhatDanhSach(List<eLinhKien> ls = null)
        {
            htLinhKien = new bLinhKien();
            htNhaCungCap = new bNhaCungCap();
            htLoaiLinhKien = new bLoaiLinhKien();

            dgvLinhKienGoc.Rows.Clear();
            if (ls == null)
                ls_Temp = htLinhKien.layDanhSachLinhKien();
            else
                ls_Temp = ls;

            foreach (eLinhKien item in ls_Temp)
            {
                dgvLinhKienGoc.Rows.Add();
                int stt = dgvLinhKienGoc.Rows.Count - 1;
                dgvLinhKienGoc.Rows[stt].Cells[0].Value = item.TenLinhKien;
                dgvLinhKienGoc.Rows[stt].Cells[1].Value = htLoaiLinhKien.thongTinLoaiLinhKien(item.MaLoai).TenLoai;
                dgvLinhKienGoc.Rows[stt].Cells[2].Value = item.GiaBan;
                dgvLinhKienGoc.Rows[stt].Cells[3].Value = item.SoLuong;
                dgvLinhKienGoc.Rows[stt].Cells[4].Value = htNhaCungCap.thongTinNhaCungCap(item.MaNhaCungCap).TenNhaCungCap;
                dgvLinhKienGoc.Rows[stt].Cells[5].Value = item.MucGiamGia;
            }
        }

        private void dgvLinhKienGoc_Resize(object sender, EventArgs e)
        {
            dgvLinhKienGoc.Columns[0].Width = dgvLinhKienGoc.Width * 24 / 100;
            dgvLinhKienGoc.Columns[1].Width = dgvLinhKienGoc.Width * 15 / 100;
            dgvLinhKienGoc.Columns[2].Width = dgvLinhKienGoc.Width * 15 / 100;
            dgvLinhKienGoc.Columns[3].Width = dgvLinhKienGoc.Width * 10 / 100;
            dgvLinhKienGoc.Columns[4].Width = dgvLinhKienGoc.Width * 22 / 100;
        }

        private void dgvLinhKienDaChon_Resize(object sender, EventArgs e)
        {
            dgvLinhKienDaChon.Columns[0].Width = dgvLinhKienDaChon.Width * 24 / 100;
            dgvLinhKienDaChon.Columns[1].Width = dgvLinhKienDaChon.Width * 15 / 100;
            dgvLinhKienDaChon.Columns[2].Width = dgvLinhKienDaChon.Width * 15 / 100;
            dgvLinhKienDaChon.Columns[3].Width = dgvLinhKienDaChon.Width * 10 / 100;
        }

        private void ucQuanLyNhapHang_Load(object sender, EventArgs e)
        {
            bar1.Font = new Font("Microsoft Sans Serif", 10);
            dgvLinhKienGoc.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9.25f);
        }

        private void dgvLinhKienGoc_DoubleClick(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvLinhKienDaChon.Rows.Count; i++)
            {
                if (dgvLinhKienDaChon.Rows[i].Cells[0].Value == dgvLinhKienGoc.SelectedRows[0].Cells[0].Value)
                {
                    MessageBoxEx.Show(this, "Linh kiện này đã được chọn...", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            dgvLinhKienDaChon.Rows.Add();
            int stt = dgvLinhKienDaChon.Rows.Count - 1;

            dgvLinhKienDaChon.Rows[stt].Cells[0].Value = dgvLinhKienGoc.SelectedRows[0].Cells[0].Value;
            dgvLinhKienDaChon.Rows[stt].Cells[1].Value = dgvLinhKienGoc.SelectedRows[0].Cells[1].Value;
            dgvLinhKienDaChon.Rows[stt].Cells[2].Value = dgvLinhKienGoc.SelectedRows[0].Cells[2].Value;
            dgvLinhKienDaChon.Rows[stt].Cells[3].Value = 1;
            dgvLinhKienDaChon.Rows[stt].Cells[4].Value = dgvLinhKienGoc.SelectedRows[0].Cells[4].Value;
        }

        private void dgvLinhKienDaChon_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!Regex.IsMatch(dgvLinhKienDaChon.Rows[e.RowIndex].Cells[3].Value.ToString(), @"^\d+$") || int.Parse(dgvLinhKienDaChon.Rows[e.RowIndex].Cells[3].Value.ToString()) <= 0)
            {
                dgvLinhKienDaChon.Rows[e.RowIndex].Cells[3].Value = 1;
                MessageBoxEx.Show(this, "Số lượng chỉ nhận số nguyên tối thiểu bằng 1", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (rdoTenLinhKien.Checked)
                capNhatDanhSach(htLinhKien.layDanhSachLinhKien().Where(n => n.MaNhaCungCap == thongTinNhaCungCap.MaNhaCungCap && CongCu.Loai.XoaUnicode(n.TenLinhKien.ToLower()).Contains(CongCu.Loai.XoaUnicode(txtKeyTimKiem.Text.ToLower()))).ToList());
            else
                capNhatDanhSach(htLinhKien.layDanhSachLinhKien().Where(n => n.MaNhaCungCap == thongTinNhaCungCap.MaNhaCungCap &&  CongCu.Loai.XoaUnicode(htLoaiLinhKien.thongTinLoaiLinhKien(n.MaLoai).TenLoai.ToLower()).Contains(CongCu.Loai.XoaUnicode(txtKeyTimKiem.Text.ToLower()))).ToList());
        }

        private void btnLapHoaDon_Click(object sender, EventArgs e)
        {
            if (dgvLinhKienDaChon.Rows.Count > 0)
            {
                List<eChiTietPhieuNhapKho> ls = new List<eChiTietPhieuNhapKho>();
                for (int i = 0; i < dgvLinhKienDaChon.Rows.Count; i++)
                {
                    ls.Add(new eChiTietPhieuNhapKho()
                    {
                        MaLinhKien = htLinhKien.layDanhSachLinhKien().Single(n => n.TenLinhKien == dgvLinhKienDaChon.Rows[i].Cells[0].Value.ToString()).MaLinhKien,
                        GiaMua = double.Parse(dgvLinhKienDaChon.Rows[i].Cells[2].Value.ToString()),
                        SoLuong = int.Parse(dgvLinhKienDaChon.Rows[i].Cells[3].Value.ToString()),
                        ThanhTien = double.Parse(dgvLinhKienDaChon.Rows[i].Cells[2].Value.ToString()) * int.Parse(dgvLinhKienDaChon.Rows[i].Cells[3].Value.ToString()),
                        MaPhieuNhapKho = "PNK-0"
                    });
                }
                tabFather.TabPages[16].Controls.Add(new ucGiaoDienXacNhanPhieuNhapKho(ls, thongTinNhaCungCap, tabFather, htNhanVien.layDanhSachNhanVien().Single(n => n.MaNhanVien == maNhanVien), thongTinThuKho) { Dock = DockStyle.Fill });
                tabFather.SelectedIndex = 16;
            }
            else
                MessageBoxEx.Show(this, "Mời chọn ít nhất 1 linh kiện để thực hiện lập đơn phiếu nhập kho", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
        }

        private void dgvLinhKienDaChon_DoubleClick(object sender, EventArgs e)
        {
            if (dgvLinhKienDaChon.SelectedRows.Count > 0)
                dgvLinhKienDaChon.Rows.Remove(dgvLinhKienDaChon.SelectedRows[0]);
        }
    }
}
