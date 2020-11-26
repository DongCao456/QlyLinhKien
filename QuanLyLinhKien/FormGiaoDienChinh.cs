using BLL;
using DevComponents.DotNetBar;
using Entity;
using QuanLyLinhKien.UC;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyLinhKien
{
    public partial class FormGiaoDienChinh : Office2007RibbonForm
    {
        private string maNguoiDung;
        string maPhanQuyen = "";
        private bNhanVien htNhanVien;
        private bLoaiNhanVien htLoaiNhanVien;
        private bKhachHang htKhachHang;
        private Image img_temp;
        private List<RibbonBar> lsBtn;

        public FormGiaoDienChinh(string maNguoiDung)
        {
            InitializeComponent();
            htNhanVien = new bNhanVien();
            htLoaiNhanVien = new bLoaiNhanVien();
            htKhachHang = new bKhachHang();
            this.maNguoiDung = maNguoiDung;
            this.EnableGlass = false;
            hideTabMenu();
            layDanhSachChucNang();

            if (maNguoiDung.Split('-')[0] == "NV")
            {
                maPhanQuyen = htLoaiNhanVien.thongTinLoaiNhanVien((htNhanVien.thongTinNhanVien(maNguoiDung).MaLoaiNhanVien)).MaPhanQuyen;
            }
            else
            {
                maPhanQuyen = "10001000000000000000000000000000";
            }
            phanQuyen(true);

        }

        private void hideTabMenu()
        {
            tabFather.Appearance = TabAppearance.FlatButtons;
            tabFather.ItemSize = new Size(0, 1);
            tabFather.SizeMode = TabSizeMode.Fixed;
        }

        private void FormGiaoDienNhanVien_Load(object sender, EventArgs e)
        {
            if (maNguoiDung.Split('-')[0] == "KH")
            {
                tabBanHang.Controls.Add(new ucQuanLyBanHang(tabFather, "NV-1", maNguoiDung) { Dock = DockStyle.Fill });
                tabThongTinCaNhanKhachHang.Controls.Add(new ucQuanLyThongTinCaNhanKhachHang(maNguoiDung) { Dock = DockStyle.Fill });
            }
            else
            {
                tabBanHang.Controls.Add(new ucQuanLyBanHang(tabFather, maNguoiDung) { Dock = DockStyle.Fill });
                tabThongTinCaNhan.Controls.Add(new ucQuanLyThongTinCaNhan(maNguoiDung) { Dock = DockStyle.Fill });
            }

            tabDonDatHang.Controls.Add(new ucQuanLyDonDatHang(tabFather) { Dock = DockStyle.Fill });
            tabChiTietDonDatHang.Controls.Add(new ucQuanLyChiTietDonDatHang(tabFather) { Dock = DockStyle.Fill });
            tabThanhToanDonDatHang.Controls.Add(new ucQuanLyThanhToanDonDatHang(tabFather) { Dock = DockStyle.Fill });

            tabNhapHang.Controls.Add(new ucQuanLyNhapKho(tabFather, maNguoiDung) { Dock = DockStyle.Fill });
            tabPhieuNhapKho.Controls.Add(new ucQuanLyPhieuNhapKho(tabFather) { Dock = DockStyle.Fill });
            tabChiTietPhieuNhapKho.Controls.Add(new ucQuanLyChiTietPhieuNhapKho(tabFather) { Dock = DockStyle.Fill });
            tabThanhToanPhieuNhapKho.Controls.Add(new ucQuanLyThanhToanPhieuNhapKho(tabFather) { Dock = DockStyle.Fill });

            tabKhachHang.Controls.Add(new ucQuanLyKhachHang(tabFather) { Dock = DockStyle.Fill });
            tabNhanVien.Controls.Add(new ucQuanLyNhanVien(tabFather) { Dock = DockStyle.Fill });
            tabLinhKien.Controls.Add(new ucQuanLyLinhKien(tabFather) { Dock = DockStyle.Fill });
            tabLoaiLinhKien.Controls.Add(new ucQuanLyLoaiLinhKien(tabFather) { Dock = DockStyle.Fill });
            tabLoaiNhanVien.Controls.Add(new ucQuanLyLoaiNhanVien(tabFather) { Dock = DockStyle.Fill });
            tabNhaCungCap.Controls.Add(new ucQuanLyNhaCungCap(tabFather) { Dock = DockStyle.Fill });

            tabReport.Controls.Add(new ucReport() { Dock = DockStyle.Fill });
            tabThongKeLinhKien.Controls.Add(new ucThongKeLinhKien(tabFather) { Dock = DockStyle.Fill });
            tabThongKeNhanVien.Controls.Add(new ucThongKeNhanVien(tabFather) { Dock = DockStyle.Fill });
            tabThongKeKhachHang.Controls.Add(new ucThongKeKhachHang(tabFather) { Dock = DockStyle.Fill });
            tabThongKeDonDatHang.Controls.Add(new ucThongKeDonDatHang(tabFather) { Dock = DockStyle.Fill });
            tabThongKePhieuNhapKho.Controls.Add(new ucThongKePhieuNhapKho(tabFather) { Dock = DockStyle.Fill });
            tabThongKeNhaCungCap.Controls.Add(new ucThongKeNhaCungCap(tabFather) { Dock = DockStyle.Fill });

            tabTruyXuatDonDatHang.Controls.Add(new ucTruyXuatDonDatHang(tabFather) { Dock = DockStyle.Fill });
            tabTruyXuatPhieuNhapKho.Controls.Add(new ucTruyXuatPhieuNhapKho(tabFather) { Dock = DockStyle.Fill });

            
            

        }

        private void btnQuanLyDonDatHang_Click(object sender, EventArgs e)
        {
            ((ucQuanLyDonDatHang)tabDonDatHang.Controls[0]).capNhatDanhSach();
            tabFather.SelectedTab = tabDonDatHang;
            if (((ucQuanLyDonDatHang)tabDonDatHang.Controls[0]).TimKiem != false)
                ((ucQuanLyDonDatHang)tabDonDatHang.Controls[0]).TimKiem = false;
        }

        private void btnChiTietDonDatHang_Click(object sender, EventArgs e)
        {
            ((ucQuanLyChiTietDonDatHang)tabChiTietDonDatHang.Controls[0]).capNhatDanhSach();
            tabFather.SelectedTab = tabChiTietDonDatHang;
            if (((ucQuanLyChiTietDonDatHang)tabChiTietDonDatHang.Controls[0]).TimKiem != false)
                ((ucQuanLyChiTietDonDatHang)tabChiTietDonDatHang.Controls[0]).TimKiem = false;
        }

        private void btnQuanLyNhanVien_Click(object sender, EventArgs e)
        {
            ((ucQuanLyNhanVien)tabNhanVien.Controls[0]).capNhatDanhSach();
            tabFather.SelectedTab = tabNhanVien;
            if (((ucQuanLyNhanVien)tabNhanVien.Controls[0]).TimKiem != false)
                ((ucQuanLyNhanVien)tabNhanVien.Controls[0]).TimKiem = false;
        }

        private void btnQuanLyKhachHang_Click(object sender, EventArgs e)
        {
            ((ucQuanLyKhachHang)tabKhachHang.Controls[0]).capNhatDanhSach();
            tabFather.SelectedTab = tabKhachHang;
            if (((ucQuanLyKhachHang)tabKhachHang.Controls[0]).TimKiem != false)
                ((ucQuanLyKhachHang)tabKhachHang.Controls[0]).TimKiem = false;

        }

        private void btnQuanlyLinhKien_Click(object sender, EventArgs e)
        {
            ((ucQuanLyLinhKien)tabLinhKien.Controls[0]).capNhatDanhSach();
            tabFather.SelectedTab = tabLinhKien;
            if (((ucQuanLyLinhKien)tabLinhKien.Controls[0]).TimKiem != false)
                ((ucQuanLyLinhKien)tabLinhKien.Controls[0]).TimKiem = false;
        }

        private void btnQuanLyLoaiLinhKien_Click(object sender, EventArgs e)
        {
            ((ucQuanLyLoaiLinhKien)tabLoaiLinhKien.Controls[0]).capNhatDanhSach();
            tabFather.SelectedTab = tabLoaiLinhKien;
            if (((ucQuanLyLoaiLinhKien)tabLoaiLinhKien.Controls[0]).TimKiem != false)
                ((ucQuanLyLoaiLinhKien)tabLoaiLinhKien.Controls[0]).TimKiem = false;
        }

        private void btnQuanLyNhaCungCap_Click(object sender, EventArgs e)
        {
            ((ucQuanLyNhaCungCap)tabNhaCungCap.Controls[0]).capNhatDanhSach();
            tabFather.SelectedTab = tabNhaCungCap;
            if (((ucQuanLyNhaCungCap)tabNhaCungCap.Controls[0]).TimKiem != false)
                ((ucQuanLyNhaCungCap)tabNhaCungCap.Controls[0]).TimKiem = false;
        }

        private void btnQuanLyDonNhapHang_Click(object sender, EventArgs e)
        {
            ((ucQuanLyPhieuNhapKho)tabPhieuNhapKho.Controls[0]).capNhatDanhSach();
            tabFather.SelectedTab = tabPhieuNhapKho;
            if (((ucQuanLyPhieuNhapKho)tabPhieuNhapKho.Controls[0]).TimKiem != false)
                ((ucQuanLyPhieuNhapKho)tabPhieuNhapKho.Controls[0]).TimKiem = false;
        }

        private void btnQuanLyChiTietDonNhapHang_Click(object sender, EventArgs e)
        {
            ((ucQuanLyChiTietPhieuNhapKho)tabChiTietPhieuNhapKho.Controls[0]).capNhatDanhSach();
            tabFather.SelectedTab = tabChiTietPhieuNhapKho;
            if (((ucQuanLyChiTietPhieuNhapKho)tabChiTietPhieuNhapKho.Controls[0]).TimKiem != false)
                ((ucQuanLyChiTietPhieuNhapKho)tabChiTietPhieuNhapKho.Controls[0]).TimKiem = false;
        }

        private void btnTimChiTietDonDatHang_Click(object sender, EventArgs e)
        {
            ((ucQuanLyChiTietDonDatHang)tabChiTietDonDatHang.Controls[0]).capNhatDanhSach();
            tabFather.SelectedTab = tabChiTietDonDatHang;
            if (((ucQuanLyChiTietDonDatHang)tabChiTietDonDatHang.Controls[0]).TimKiem != true)
                ((ucQuanLyChiTietDonDatHang)tabChiTietDonDatHang.Controls[0]).TimKiem = true;
        }

        private void btnTimNhanVien_Click(object sender, EventArgs e)
        {
            ((ucQuanLyNhanVien)tabNhanVien.Controls[0]).capNhatDanhSach();
            tabFather.SelectedTab = tabNhanVien;
            if (((ucQuanLyNhanVien)tabNhanVien.Controls[0]).TimKiem != true)
                ((ucQuanLyNhanVien)tabNhanVien.Controls[0]).TimKiem = true;
        }

        private void btnTimKhachHang_Click(object sender, EventArgs e)
        {
            ((ucQuanLyKhachHang)tabKhachHang.Controls[0]).capNhatDanhSach();
            tabFather.SelectedTab = tabKhachHang;
            if (((ucQuanLyKhachHang)tabKhachHang.Controls[0]).TimKiem != true)
                ((ucQuanLyKhachHang)tabKhachHang.Controls[0]).TimKiem = true;
        }

        private void btnTimLinhKien_Click(object sender, EventArgs e)
        {
            ((ucQuanLyLinhKien)tabLinhKien.Controls[0]).capNhatDanhSach();
            tabFather.SelectedTab = tabLinhKien;
            if (((ucQuanLyLinhKien)tabLinhKien.Controls[0]).TimKiem != true)
                ((ucQuanLyLinhKien)tabLinhKien.Controls[0]).TimKiem = true;
        }

        private void btnTimLoaiLinhKien_Click(object sender, EventArgs e)
        {
            ((ucQuanLyLoaiLinhKien)tabLoaiLinhKien.Controls[0]).capNhatDanhSach();
            tabFather.SelectedTab = tabLoaiLinhKien;
            if (((ucQuanLyLoaiLinhKien)tabLoaiLinhKien.Controls[0]).TimKiem != true)
                ((ucQuanLyLoaiLinhKien)tabLoaiLinhKien.Controls[0]).TimKiem = true;
        }

        private void btnTimDonDatHang_Click(object sender, EventArgs e)
        {
            ((ucQuanLyDonDatHang)tabDonDatHang.Controls[0]).capNhatDanhSach();
            tabFather.SelectedTab = tabDonDatHang;
            if (((ucQuanLyDonDatHang)tabDonDatHang.Controls[0]).TimKiem != true)
                ((ucQuanLyDonDatHang)tabDonDatHang.Controls[0]).TimKiem = true;
        }

        private void btnTimNhaCungCap_Click(object sender, EventArgs e)
        {
            ((ucQuanLyNhaCungCap)tabNhaCungCap.Controls[0]).capNhatDanhSach();
            tabFather.SelectedTab = tabNhaCungCap;
            if (((ucQuanLyNhaCungCap)tabNhaCungCap.Controls[0]).TimKiem != true)
                ((ucQuanLyNhaCungCap)tabNhaCungCap.Controls[0]).TimKiem = true;
        }

        private void btnTimLoaiNhanVien_Click(object sender, EventArgs e)
        {
            ((ucQuanLyLoaiNhanVien)tabLoaiNhanVien.Controls[0]).capNhatDanhSach();
            tabFather.SelectedTab = tabLoaiNhanVien;
            if (((ucQuanLyLoaiNhanVien)tabLoaiNhanVien.Controls[0]).TimKiem != true)
                ((ucQuanLyLoaiNhanVien)tabLoaiNhanVien.Controls[0]).TimKiem = true;
        }

        private void btnTimPhieuNhapKho_Click(object sender, EventArgs e)
        {
            ((ucQuanLyPhieuNhapKho)tabPhieuNhapKho.Controls[0]).capNhatDanhSach();
            tabFather.SelectedTab = tabPhieuNhapKho;
            if (((ucQuanLyPhieuNhapKho)tabPhieuNhapKho.Controls[0]).TimKiem != true)
                ((ucQuanLyPhieuNhapKho)tabPhieuNhapKho.Controls[0]).TimKiem = true;
        }

        private void btnTimKiemChiTietPhieuNhapKho_Click(object sender, EventArgs e)
        {
            ((ucQuanLyChiTietPhieuNhapKho)tabChiTietPhieuNhapKho.Controls[0]).capNhatDanhSach();
            tabFather.SelectedTab = tabChiTietPhieuNhapKho;
            if (((ucQuanLyChiTietPhieuNhapKho)tabChiTietPhieuNhapKho.Controls[0]).TimKiem != true)
                ((ucQuanLyChiTietPhieuNhapKho)tabChiTietPhieuNhapKho.Controls[0]).TimKiem = true;
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            ((ucQuanLyThanhToanDonDatHang)tabThanhToanDonDatHang.Controls[0]).capNhatDanhSach();
            tabFather.SelectedTab = tabThanhToanDonDatHang;
        }

        private void btnBanHang_Click(object sender, EventArgs e)
        {
            if (rbbThongTinCaNhan.Enabled && MessageBoxEx.Show(this, "Bạn muốn tiền hành quy trình đặt hàng ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                anChucNangDeDatHang(true);
                if (maNguoiDung.Split('-')[0] == "KH")
                {
                    ((ucQuanLyBanHang)tabFather.TabPages[1].Controls[0]).nvTuVan = htNhanVien.thongTinNhanVien("NV-1");
                    ((ucQuanLyBanHang)tabFather.TabPages[1].Controls[0]).ThongTinKhachHang = htKhachHang.thongTinKhachHang(maNguoiDung);
                    tabFather.SelectedIndex = 1;
                }
                else
                {
                    if (((ucQuanLyKhachHang)tabKhachHang.Controls[0]).TimKiem != true)
                        ((ucQuanLyKhachHang)tabKhachHang.Controls[0]).TimKiem = true;
                    tabFather.SelectedTab = tabKhachHang;
                    MessageBoxEx.Show(this, "Mời chọn khách hàng", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            else if (!rbbThongTinCaNhan.Enabled && MessageBoxEx.Show(this, "Bạn muốn huỷ bỏ quy trình đặt hàng ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                ((ucQuanLyBanHang)tabBanHang.Controls[0]).ThongTinKhachHang = null;
                ((ucQuanLyBanHang)tabBanHang.Controls[0]).nvTuVan = null;
                tabFather.SelectedTab = tabHome;
                anChucNangDeDatHang(false);
            }
            else
            {
                return;
            }
        }

        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            if (rbbThongTinCaNhan.Enabled && MessageBoxEx.Show(this, "Bạn muốn tiền hành quy trình nhập hàng ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                anChucNangDeNhapHang(true);
                if (((ucQuanLyNhaCungCap)tabNhaCungCap.Controls[0]).TimKiem != true)
                    ((ucQuanLyNhaCungCap)tabNhaCungCap.Controls[0]).TimKiem = true;
                tabFather.SelectedTab = tabNhaCungCap;
                MessageBoxEx.Show(this, "Mời chọn nhà cung cấp", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else if (!rbbThongTinCaNhan.Enabled && MessageBoxEx.Show(this, "Bạn muốn huỷ bỏ quy trình nhập hàng ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                ((ucQuanLyNhapKho)tabNhapHang.Controls[0]).thongTinThuKho = null;
                ((ucQuanLyNhapKho)tabNhapHang.Controls[0]).ThongTinNhaCungCap = null;
                tabFather.SelectedTab = tabHome;
                anChucNangDeNhapHang(false);
            }
            else
                return;
        }

        private void btnThanhToanDonNhapHang_Click(object sender, EventArgs e)
        {
            ((ucQuanLyThanhToanPhieuNhapKho)tabThanhToanPhieuNhapKho.Controls[0]).capNhatDanhSach();
            tabFather.SelectedTab = tabThanhToanPhieuNhapKho;
        }

        private void btnLoaiNhanVien_Click(object sender, EventArgs e)
        {
            ((ucQuanLyLoaiNhanVien)tabLoaiNhanVien.Controls[0]).capNhatDanhSach();
            tabFather.SelectedTab = tabLoaiNhanVien;
        }

        public void anChucNangDeDatHang(bool n)
        {
            if (n)
            {
                ((ucQuanLyNhanVien)tabNhanVien.Controls[0]).capNhatDanhSach();
                ((ucQuanLyKhachHang)tabKhachHang.Controls[0]).capNhatDanhSach();
                ((ucQuanLyBanHang)tabBanHang.Controls[0]).capNhatDanhSach();
                img_temp = btnLapDonDatHang.Image;
                btnLapDonDatHang.Image = Image.FromFile(Application.StartupPath + @"\delete.png");
                btnLapDonDatHang.Text = "<div width = '85' align = 'center'>Huỷ tác vụ đặt hàng</div>";
                phanQuyen(false, rbbLapDonDatHang);
            }
            else
            {
                phanQuyen(true);
                btnLapDonDatHang.Image = img_temp;
                btnLapDonDatHang.Text = "<div width = '85' align = 'center'>Lập đơn đặt hàng</div>";
            }
            ((ucQuanLyKhachHang)tabKhachHang.Controls[0]).datHang = ((ucQuanLyNhanVien)tabNhanVien.Controls[0]).datHang = n;

        }

        public void anChucNangDeNhapHang(bool n)
        {

            if (n)
            {
                ((ucQuanLyNhanVien)tabNhanVien.Controls[0]).capNhatDanhSach();
                ((ucQuanLyNhaCungCap)tabNhaCungCap.Controls[0]).capNhatDanhSach();
                ((ucQuanLyNhapKho)tabNhapHang.Controls[0]).capNhatDanhSach();
                img_temp = btnLapPhieuNhapKho.Image;
                btnLapPhieuNhapKho.Image = Image.FromFile(Application.StartupPath + @"\delete.png");
                btnLapPhieuNhapKho.Text = "<div width = '85' align = 'center'>Huỷ tác vụ nhập hàng</div>";
                phanQuyen(false, rbbLapPhieuNhapKho);
            }
            else
            {
                phanQuyen(true);
                btnLapPhieuNhapKho.Image = img_temp;
                btnLapPhieuNhapKho.Text = "<div width = '85' align = 'center'>Lập phiếu nhập kho</div>";

            }
            ((ucQuanLyNhaCungCap)tabNhaCungCap.Controls[0]).nhapHang = ((ucQuanLyNhanVien)tabNhanVien.Controls[0]).nhapHang = n;

        }

        private void btnTruyXuatDonDatHang_Click(object sender, EventArgs e)
        {
            if (maNguoiDung.Split('-')[0] == "KH")
            {
                ((ucTruyXuatDonDatHang)tabFather.TabPages[11].Controls[0]).Kh = htKhachHang.thongTinKhachHang(maNguoiDung);
                ((ucTruyXuatDonDatHang)tabFather.TabPages[11].Controls[0]).lastTabIndex = 2;
                tabFather.SelectedIndex = 11;
            }
            else
                switch (tabFather.SelectedIndex)
                {
                    case 2:
                        ((ucQuanLyDonDatHang)tabDonDatHang.Controls[0]).truyXuatDonDatHang();
                        break;
                    case 4:
                        ((ucQuanLyNhanVien)tabNhanVien.Controls[0]).truyXuatDonDatHang();
                        break;
                    case 5:
                        ((ucQuanLyKhachHang)tabKhachHang.Controls[0]).truyXuatDonDatHang();
                        break;
                    case 20:
                        ((ucThongKeDonDatHang)tabThongKeDonDatHang.Controls[0]).truyXuatDonDatHang();
                        break;
                    case 21:
                        ((ucThongKeNhanVien)tabThongKeNhanVien.Controls[0]).truyXuatDonDatHang();
                        break;
                    case 22:
                        ((ucThongKeKhachHang)tabThongKeKhachHang.Controls[0]).truyXuatDonDatHang();
                        break;
                    case 23:
                        ((ucThongKeLinhKien)tabThongKeLinhKien.Controls[0]).truyXuatDonDatHang();
                        break;
                    default:
                        //MessageBoxEx.Show(this, "Mời chọn nhân viên, khách hàng, hoặc đơn đặt hàng cần truy xuất đơn đặt hàng !!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        MessageBoxEx.Show(this, "Các đối tượng được hỗ trợ truy xuất đơn đặt hàng, gồm các mục:\n\n" +
                            "- Cập nhật dữ liệu\n" +
                            "      + Đơn đặt hàng\n" +
                            "      + Nhân viên\n" +
                            "      + Khách hàng\n\n" +
                            "- Thống kê\n" +
                            "      + Thống kê đơn đặt hàng\n" +
                            "      + Thống kê nhân viên\n" +
                            "      + Thống kê khách hàng\n" +
                            "      + Thống kê linh kiện", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        break;
                }
        }

        private void btnTruyXuatPhieuNhapKho_Click(object sender, EventArgs e)
        {
            switch (tabFather.SelectedIndex)
            {
                case 4:
                    ((ucQuanLyNhanVien)tabNhanVien.Controls[0]).truyXuatPhieuNhapKho();
                    break;
                case 8:
                    ((ucQuanLyNhaCungCap)tabNhaCungCap.Controls[0]).truyXuatPhieuNhapKho();
                    break;
                case 14:
                    ((ucQuanLyPhieuNhapKho)tabPhieuNhapKho.Controls[0]).truyXuatPhieuNhapKho();
                    break;
                case 24:
                    ((ucThongKePhieuNhapKho)tabThongKePhieuNhapKho.Controls[0]).truyXuatPhieuNhapKho();
                    break;
                case 25:
                    ((ucThongKeNhaCungCap)tabThongKeNhaCungCap.Controls[0]).truyXuatPhieuNhapKho();
                    break;
                default:
                    MessageBoxEx.Show(this, "Các đối tượng được hỗ trợ truy xuất phiếu nhập kho, gồm các mục:\n\n" +
                        "- Cập nhật dữ liệu\n" +
                        "      + Nhân viên\n" +
                        "      + Nhà cung cấp\n" +
                        "      + Phiếu nhập kho\n\n" +
                        "- Thống kê\n" +
                        "      + Thống kê phiếu nhập kho\n" +
                        "      + Thống kê nhà cung cấp", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    break;
            }
        }

        private void btnThongKeHoaDon_Click(object sender, EventArgs e)
        {
            ((ucThongKeDonDatHang)tabThongKeDonDatHang.Controls[0]).CapNhatMoi = true;
            tabFather.SelectedTab = tabThongKeDonDatHang;
        }

        private void btnThongKeLinhKien_Click(object sender, EventArgs e)
        {
            ((ucThongKeLinhKien)tabThongKeLinhKien.Controls[0]).CapNhatMoi = true;
            tabFather.SelectedTab = tabThongKeLinhKien;
        }

        private void btnThongKeNhanVien_Click(object sender, EventArgs e)
        {
            ((ucThongKeNhanVien)tabThongKeNhanVien.Controls[0]).CapNhatMoi = true;
            tabFather.SelectedTab = tabThongKeNhanVien;
        }

        private void btnThongKeKhachHang_Click(object sender, EventArgs e)
        {
            ((ucThongKeKhachHang)tabThongKeKhachHang.Controls[0]).CapNhatMoi = true;
            tabFather.SelectedTab = tabThongKeKhachHang;
        }

        private void btnThongKePhieuNhapKho_Click(object sender, EventArgs e)
        {
            ((ucThongKePhieuNhapKho)tabThongKePhieuNhapKho.Controls[0]).CapNhatMoi = true;
            tabFather.SelectedTab = tabThongKePhieuNhapKho;
        }

        private void btnThongKeNhaCungCap_Click(object sender, EventArgs e)
        {
            ((ucThongKeNhaCungCap)tabThongKeNhaCungCap.Controls[0]).CapNhatMoi = true;
            tabFather.SelectedTab = tabThongKeNhaCungCap;
        }



        private void btnThoatHeThong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormGiaoDienChinh_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBoxEx.Show(this, "Bạn có muốn thoát phần mềm ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                e.Cancel = true;
        }

        private void btnThongTinCaNhan_Click(object sender, EventArgs e)
        {
            if (maNguoiDung.Split('-')[0] == "KH")
            {
                ((ucQuanLyThongTinCaNhanKhachHang)tabThongTinCaNhanKhachHang.Controls[0]).capNhatThongTin();
                tabFather.SelectedTab = tabThongTinCaNhanKhachHang;
            }
            else
            {
                ((ucQuanLyThongTinCaNhan)tabThongTinCaNhan.Controls[0]).capNhatThongTin();
                tabFather.SelectedTab = tabThongTinCaNhan;
            }
        }
        private void layDanhSachChucNang()
        {
            lsBtn = new List<RibbonBar>()
            {
                /*0*/rbbLapDonDatHang,
                /*1*/rbbThanhToanDonDatHang,
                /*2*/rbbLapPhieuNhapKho,
                /*3*/rbbThanhToanPhieuNhapKho,
                /*4*/rbbTruyXuatDonDatHang,
                /*5*/rbbTruyXuatPhieuNhapKho,
                /*6*/rbbDonDatHang,
                /*7*/rbbChiTietDonDatHang,
                /*8*/rbbNhanVien,
                /*9*/rbbKhachHang,
                /*10*/rbbLinhKien,
                /*11*/rbbLoaiLinhKien,
                /*12*/rbbNhaCungCap,
                /*13*/rbbLoaiNhanVien,
                /*14*/rbbPhieuNhapKho,
                /*15*/rbbChiTietPhieuNhapKho,
                /*16*/rbbTimDonDatHang,
                /*17*/rbbTimChiTietDonDatHang,
                /*18*/rbbTimNhanVien,
                /*19*/rbbTimKhachHang,
                /*20*/rbbTimLinhKien,
                /*21*/rbbTimLoaiLinhKien,
                /*22*/rbbTimNhaCungCap,
                /*23*/rbbTimLoaiNhanVien,
                /*24*/rbbTimPhieuNhapKho,
                /*25*/rbbTimChiTietPhieuNhapKho,
                /*26*/rbbThongKeDonDatHang,
                /*27*/rbbThongKeNhanVien,
                /*28*/rbbThongKeKhachHang,
                /*29*/rbbThongKeLinhKien,
                /*30*/rbbThongKePhieuNhapKho,
                /*31*/rbbThongKeNhaCungCap
            };
        }

        private void phanQuyen(bool mo, RibbonBar n = null)
        {
            List<char> ls = maPhanQuyen.ToList();

            if (mo)
                for (int i = 0; i < ls.Count; i++)
                {
                    if (ls[i] == '0')
                        lsBtn[i].Visible = false;
                    else
                        lsBtn[i].Visible = lsBtn[i].Enabled = true;
                    rbbThongTinCaNhan.Enabled = true;
                }
            else
            {
                foreach (var item in lsBtn)
                {
                    if (item == n)
                        continue;
                    item.Enabled = false;
                }
                rbbThongTinCaNhan.Enabled = false;
            }
            if (maPhanQuyen.Substring(0, 6) == "000000")
            {
                rtChucNangXuLy.Visible = false;
            }
            if (maPhanQuyen.Substring(17, 10) == "0000000000")
            {
                rtChucNangTimKiem.Visible = false;
            }
            if (maPhanQuyen.Substring(26, 6) == "000000")
            {
                rtChucNangThongKe.Visible = false;
            }
        }
    }
}
