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
using System.Collections;
using DevComponents.DotNetBar;
using System.Text.RegularExpressions;

namespace QuanLyLinhKien.UC
{
    public partial class ucQuanLyBanHang : UserControl
    {
        private eKhachHang thongTinKhachHang = null;
        public eNhanVien nvTuVan = null;
        private string maNhanVien;

        private bLinhKien htLinhKien;
        private bLoaiLinhKien htLoaiLinhKien;
        private bNhaCungCap htNhaCungCap;
        private bKhachHang htKhachHang;
        private bNhanVien htNhanVien;

        private List<eLinhKien> ls_Temp;
        private System.Windows.Forms.TabControl tabFather;

        public eKhachHang ThongTinKhachHang
        {
            get
            {
                return thongTinKhachHang;
            }
            set
            {
                if (value != null)
                    llblChuDe.Text = "Giỏ hàng của khách hàng " + value.MaKhachHang + " - " + value.TenKhachHang;
                else
                    dgvLinhKienDaChon.Rows.Clear();
                thongTinKhachHang = value;
            }
        }

        public ucQuanLyBanHang(System.Windows.Forms.TabControl tab, string maNhanVien = null, string maKhachHang = null)
        {
            InitializeComponent();
            htKhachHang = new bKhachHang();
            htNhanVien = new bNhanVien();
            this.maNhanVien = maNhanVien;
            if (maKhachHang != null)
            {
                ThongTinKhachHang = htKhachHang.thongTinKhachHang(maKhachHang);
                nvTuVan = htNhanVien.thongTinNhanVien(maNhanVien);
            }
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

        private void lsLKGoc_Resize(object sender, EventArgs e)
        {
            dgvLinhKienGoc.Columns[0].Width = dgvLinhKienGoc.Width * 24 / 100;
            dgvLinhKienGoc.Columns[1].Width = dgvLinhKienGoc.Width * 15 / 100;
            dgvLinhKienGoc.Columns[2].Width = dgvLinhKienGoc.Width * 15 / 100;
            dgvLinhKienGoc.Columns[3].Width = dgvLinhKienGoc.Width * 10 / 100;
            dgvLinhKienGoc.Columns[4].Width = dgvLinhKienGoc.Width * 22 / 100;
        }


        private void lsLKDaChon_Resize(object sender, EventArgs e)
        {
            dgvLinhKienDaChon.Columns[0].Width = dgvLinhKienDaChon.Width * 24 / 100;
            dgvLinhKienDaChon.Columns[1].Width = dgvLinhKienDaChon.Width * 15 / 100;
            dgvLinhKienDaChon.Columns[2].Width = dgvLinhKienDaChon.Width * 15 / 100;
            dgvLinhKienDaChon.Columns[3].Width = dgvLinhKienDaChon.Width * 10 / 100;
            dgvLinhKienDaChon.Columns[4].Width = dgvLinhKienDaChon.Width * 22 / 100;
        }


        private void ucQuanLyBanHang_Load(object sender, EventArgs e)
        {
            bar1.Font = new Font("Microsoft Sans Serif", 10);
            dgvLinhKienGoc.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9.25f);
        }

        private void lsLKGoc_DoubleClick(object sender, EventArgs e)
        {
            if (int.Parse(dgvLinhKienGoc.SelectedRows[0].Cells[3].Value.ToString()) > 0)
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
                dgvLinhKienDaChon.Rows[stt].Cells[5].Value = dgvLinhKienGoc.SelectedRows[0].Cells[5].Value;
            }
            else
            {
                MessageBoxEx.Show(this, "Linh kiện này đã hết hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }

        }

        private void lsLKDaChon_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            if (Regex.IsMatch(dgvLinhKienDaChon.Rows[e.RowIndex].Cells[3].Value.ToString(), @"\d") && int.Parse(dgvLinhKienDaChon.Rows[e.RowIndex].Cells[3].Value.ToString()) > 0)
            {
                int soLuongTonKho = ls_Temp.Single(n => n.TenLinhKien == dgvLinhKienDaChon.Rows[e.RowIndex].Cells[0].Value.ToString()).SoLuong;
                int soLuongMua = int.Parse(dgvLinhKienDaChon.Rows[e.RowIndex].Cells[3].Value.ToString());
                if (soLuongMua > soLuongTonKho)
                {
                    dgvLinhKienDaChon.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 1;
                    MessageBoxEx.Show(this, "Không đủ linh kiện, kho chỉ còn " + soLuongTonKho.ToString("#,###") + " linh kiện " + dgvLinhKienDaChon.Rows[e.RowIndex].Cells[0].Value.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            else
            {
                dgvLinhKienDaChon.Rows[e.RowIndex].Cells[3].Value = 1;
                MessageBoxEx.Show(this, "Số lượng chỉ nhận số nguyên lớn hơn 1", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        private void btnLapHoaDon_Click(object sender, EventArgs e)
        {
            if (dgvLinhKienDaChon.Rows.Count > 0)
            {
                List<eChiTietDonDatHang> ls = new List<eChiTietDonDatHang>();
                for (int i = 0; i < dgvLinhKienDaChon.Rows.Count; i++)
                {
                    ls.Add(new eChiTietDonDatHang()
                    {
                        MaLinhKien = htLinhKien.layDanhSachLinhKien().Single(n => n.TenLinhKien == dgvLinhKienDaChon.Rows[i].Cells[0].Value.ToString()).MaLinhKien,
                        GiaBan = double.Parse(dgvLinhKienDaChon.Rows[i].Cells[2].Value.ToString()),
                        SoLuong = int.Parse(dgvLinhKienDaChon.Rows[i].Cells[3].Value.ToString()),
                        MucGiamGia = double.Parse(dgvLinhKienDaChon.Rows[i].Cells[5].Value.ToString()),
                        ThanhTien = int.Parse(dgvLinhKienDaChon.Rows[i].Cells[3].Value.ToString()) * double.Parse(dgvLinhKienDaChon.Rows[i].Cells[5].Value.ToString()),
                        MaDonDatHang = "DDH-0"
                    });
                }
                tabFather.TabPages[10].Controls.Clear();
                tabFather.TabPages[10].Controls.Add(new ucGiaoDienXacNhanDonDatHang(ls, thongTinKhachHang, tabFather, htNhanVien.layDanhSachNhanVien().Single(n => n.MaNhanVien == maNhanVien), nvTuVan) { Dock = DockStyle.Fill });
                tabFather.SelectedIndex = 10;
            }
            else
                MessageBoxEx.Show(this, "Mời chọn ít nhất 1 linh kiện để thực hiện lập đơn đặt hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (rdoTenLinhKien.Checked)
                capNhatDanhSach(htLinhKien.layDanhSachLinhKien().Where(n => XoaUnicode(n.TenLinhKien.ToLower()).Contains(XoaUnicode(txtKeyTimKiem.Text.ToLower()))).ToList());
            else if(rdoLoai.Checked)
                capNhatDanhSach(htLinhKien.layDanhSachLinhKien().Where(n => XoaUnicode(htLoaiLinhKien.thongTinLoaiLinhKien(n.MaLoai).TenLoai.ToLower()).Contains(XoaUnicode(txtKeyTimKiem.Text.ToLower()))).ToList());
            else
                capNhatDanhSach(htLinhKien.layDanhSachLinhKien().Where(n => XoaUnicode(htNhaCungCap.thongTinNhaCungCap(n.MaNhaCungCap).TenNhaCungCap.ToLower()).Contains(XoaUnicode(txtKeyTimKiem.Text.ToLower()))).ToList());
        }
        private string XoaUnicode(string txt)
        {
            string[] banRo = new string[] {
                "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
                "đ",
                "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
                "í","ì","ỉ","ĩ","ị",
                "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
                "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
                "ý","ỳ","ỷ","ỹ","ỵ"};
            string[] banThayThe = new string[] {
                "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                "d",
                "e","e","e","e","e","e","e","e","e","e","e",
                "i","i","i","i","i",
                "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
                "u","u","u","u","u","u","u","u","u","u","u",
                "y","y","y","y","y"};
            for (int i = 0; i < banRo.Length; i++)
            {
                txt = txt.Replace(banRo[i], banThayThe[i]);
                txt = txt.Replace(banRo[i].ToUpper(), banThayThe[i].ToUpper());
            }
            return txt;
        }

        private void dgvLinhKienDaChon_DoubleClick(object sender, EventArgs e)
        {
            if (dgvLinhKienDaChon.SelectedRows.Count > 0)
                dgvLinhKienDaChon.Rows.Remove(dgvLinhKienDaChon.SelectedRows[0]);
        }
    }
}
