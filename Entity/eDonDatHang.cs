using System;
using System.Text.RegularExpressions;

namespace Entity
{
    public class eDonDatHang
    {
        private string maDonDatHang;
        private string maKhachHang;
        private string maNhanVienThuNgan;
        private string maNhanVienTuVan;
        private DateTime ngayLap;
        private double tongTien;
        private string trangThai;
        private string noiNhanHang;

        public string MaDonDatHang
        {
            get
            {
                return maDonDatHang;
            }

            set
            {
                if (!Regex.IsMatch(value, @"^DDH\-\d+$"))
                    throw new Exception("Sai mã đơn đặt hàng, Ví dụ: DDH-1");
                maDonDatHang = value;
            }
        }

        public string MaKhachHang
        {
            get
            {
                return maKhachHang;
            }

            set
            {
                if (!Regex.IsMatch(value, @"^KH\-\d+$"))
                    throw new Exception("Sai mã khách hàng, Ví dụ: KH-1");
                maKhachHang = value;
            }
        }

        public DateTime NgayLap
        {
            get
            {
                return ngayLap;
            }

            set
            {
                ngayLap = value;
            }
        }

        public string NoiNhanHang
        {
            get
            {
                return noiNhanHang;
            }

            set
            {
                if (value.Trim().Length == 0)
                    throw new Exception("Địa chỉ không được để trống");
                noiNhanHang = value;
            }
        }

        public string TrangThai
        {
            get
            {
                return trangThai;
            }
            set
            {
                trangThai = value;
            }
        }

        public double TongTien
        {
            get
            {
                return tongTien;
            }
            set
            {
                tongTien = value;
            }
        }

        public string MaNhanVienThuNgan
        {
            get
            {
                return maNhanVienThuNgan;
            }
            set
            {
                if (!Regex.IsMatch(value, @"^NV\-\d+$"))
                    throw new Exception("Sai mã nhân viên, Ví dụ: NV-1");
                maNhanVienThuNgan = value;
            }
        }

        public string MaNhanVienTuVan
        {
            get
            {
                return maNhanVienTuVan;
            }
            set
            {
                if (!Regex.IsMatch(value, @"^NV\-\d+$"))
                    throw new Exception("Sai mã nhân viên, Ví dụ: NV-1");
                maNhanVienTuVan = value;
            }
        }
    }
}
