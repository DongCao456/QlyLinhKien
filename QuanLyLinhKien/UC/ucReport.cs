using QuanLyLinhKien.Report;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using BLL;
using Entity;

namespace QuanLyLinhKien.UC
{
    public partial class ucReport : UserControl
    {
        private bDonDatHang htDonDatHang;
        private bNhanVien htNhanVien;
        private bKhachHang htKhachHang;
        private bLinhKien htLinhKien;
        private bPhieuNhapKho htPhieuNhapKho;
        private bNhaCungCap htNhaCungCap;

        public ucReport()
        {
            InitializeComponent();
            htDonDatHang = new bDonDatHang();
            htNhanVien = new bNhanVien();
            htKhachHang = new bKhachHang();
            htLinhKien = new bLinhKien();
            htPhieuNhapKho = new bPhieuNhapKho();
            htNhaCungCap = new bNhaCungCap();
        }
        public void thongKeDonDatHang(DateTime ngayBatDau, DateTime ngayKetThuc, decimal loai, string tenLoai, decimal soLuong)
        {
            if (loai == 0)
            {
                crThongKeDonDatHang cr = new crThongKeDonDatHang();
                cr.SetDataSource(htDonDatHang.inThongKe(ngayBatDau, ngayKetThuc, loai, tenLoai, soLuong).Tables["[dbo].[vw_ThongKeDonDatHang]"]);
                crBaoCao.ReportSource = cr;
            }
            else
            {
                crThongKeDonDatHangVer2 cr = new crThongKeDonDatHangVer2();
                cr.SetDataSource(htDonDatHang.inThongKe(ngayBatDau, ngayKetThuc, loai, tenLoai, soLuong).Tables["[dbo].[vw_ThongKeDonDatHang]"]);
                crBaoCao.ReportSource = cr;
            }
            crBaoCao.Refresh();
        }
        public void thongKePhieuNhapKho(DateTime ngayBatDau, DateTime ngayKetThuc, decimal loai, string tenLoai, decimal soLuong)
        {
            if (loai == 0)
            {
                crThongKePhieuNhapKho cr = new crThongKePhieuNhapKho();
                cr.SetDataSource(htPhieuNhapKho.inThongKe(ngayBatDau, ngayKetThuc, loai, tenLoai, soLuong).Tables["[dbo].[vw_ThongKePhieuNhapKho]"]);
                crBaoCao.ReportSource = cr;
            }
            else
            {
                crThongKePhieuNhapKhoVer2 cr = new crThongKePhieuNhapKhoVer2();
                cr.SetDataSource(htPhieuNhapKho.inThongKe(ngayBatDau, ngayKetThuc, loai, tenLoai, soLuong).Tables["[dbo].[vw_ThongKePhieuNhapKho]"]);
                crBaoCao.ReportSource = cr;
            }
            crBaoCao.Refresh();
        }
        public void thongKeNhanVien(DateTime ngayBatDau, DateTime ngayKetThuc, decimal loai, string tenLoai, decimal soLuong)
        {
            crThongKeNhanVien cr = new crThongKeNhanVien();

            cr.SetDataSource(htNhanVien.inThongKe(ngayBatDau, ngayKetThuc, loai, tenLoai, soLuong).Tables["[dbo].[vw_ThongKeNhanVien]"]);
            crBaoCao.ReportSource = cr;
            crBaoCao.Refresh();
        }
        public void thongKeKhachHang(DateTime ngayBatDau, DateTime ngayKetThuc, decimal loai, string tenLoai, decimal soLuong)
        {
            crThongKeKhachHangVer2 cr = new crThongKeKhachHangVer2();
            cr.SetDataSource(htKhachHang.inThongKe(ngayBatDau, ngayKetThuc, loai, tenLoai, soLuong).Tables["[dbo].[vw_ThongKeKhachHang]"]);
            crBaoCao.ReportSource = cr;
            crBaoCao.Refresh();
        }
        public void thongKeNhaCungCap(DateTime ngayBatDau, DateTime ngayKetThuc, decimal loai, string tenLoai, decimal soLuong)
        {
            crThongKeNhaCungCapVer2 cr = new crThongKeNhaCungCapVer2();
            cr.SetDataSource(htNhaCungCap.inThongKe(ngayBatDau, ngayKetThuc, loai, tenLoai, soLuong).Tables["vw_ThongKeNhaCungCap"]);
            crBaoCao.ReportSource = cr;
            crBaoCao.Refresh();
        }
        public void thongKeLinhKien(DateTime ngayBatDau, DateTime ngayKetThuc, decimal loai, string tenLoai, decimal soLuong)
        {
            if (loai == 1)
            {
                crThongKeLinhKienVer2 cr = new crThongKeLinhKienVer2();
                cr.SetDataSource(htLinhKien.inThongKe(ngayBatDau, ngayKetThuc, loai, tenLoai, soLuong).Tables["dbo.vw_ThongKeLinhKien"]);
                crBaoCao.ReportSource = cr;
                
            }
            else if (loai==2 || loai == 3)
            {
                crThongKeLinhKienVer3 cr = new crThongKeLinhKienVer3();
                cr.SetDataSource(htLinhKien.inThongKe(ngayBatDau, ngayKetThuc, loai, tenLoai, soLuong).Tables["dbo.vw_ThongKeLinhKien"]);
                crBaoCao.ReportSource = cr;
            }
            else
            {
                crThongKeLinhKien cr = new crThongKeLinhKien();
                cr.SetDataSource(htLinhKien.inThongKe(ngayBatDau, ngayKetThuc, loai, tenLoai, soLuong).Tables["dbo.vw_ThongKeLinhKien"]);
                crBaoCao.ReportSource = cr;
            }
            crBaoCao.Refresh();
        }
        public void inDonDatHang(string maDonDatHang)
        {
            crInDonDatHang cr = new crInDonDatHang();

            cr.SetDataSource(htDonDatHang.inDonDatHang(maDonDatHang).Tables["[dbo].[vw_InHoaDon]"]);
            crBaoCao.ReportSource = cr;
            crBaoCao.Refresh();
        }
        public void inPhieuNhapKho(string maPhieuNhapKho)
        {
            crInPhieuNhapKho cr = new crInPhieuNhapKho();

            cr.SetDataSource(htPhieuNhapKho.inPhieuNhapKho(maPhieuNhapKho).Tables["vw_inPhieuNhapKho"]);
            crBaoCao.ReportSource = cr;
            crBaoCao.Refresh();
        }
    }
}
