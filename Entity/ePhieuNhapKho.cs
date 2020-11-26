using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ePhieuNhapKho
    {
        private string maPhieuNhapKho;
        private string maNhaCungCap;
        private string maNhanVienThuKho;
        private string maNhanVienKeToanKho;
        private DateTime ngayLap;
        private double tongTien;
        private string trangThai;

        public string MaPhieuNhapKho
        {
            get
            {
                return maPhieuNhapKho;
            }
            set
            {
                maPhieuNhapKho = value;
            }
        }
        public string MaNhaCungCap
        {
            get
            {
                return maNhaCungCap;
            }
            set
            {
                maNhaCungCap = value;
            }
        }
        public string MaNhanVienThuKho
        {
            get
            {
                return maNhanVienThuKho;
            }
            set
            {
                maNhanVienThuKho = value;
            }
        }
        public string MaNhanVienKeToanKho
        {
            get
            {
                return maNhanVienKeToanKho;
            }
            set
            {
                maNhanVienKeToanKho = value;
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
    }
}
