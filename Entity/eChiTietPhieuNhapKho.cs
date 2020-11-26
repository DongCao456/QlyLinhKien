using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class eChiTietPhieuNhapKho
    {
        private string maPhieuNhapKho;
        private string maLinhKien;
        private int soLuong;
        private double giaMua;
        private double thanhTien;

        public string MaLinhKien
        {
            get
            {
                return maLinhKien;
            }
            set
            {
                maLinhKien = value;
            }
        }
        public int SoLuong
        {
            get
            {
                return soLuong;
            }
            set
            {
                soLuong = value;
            }
        }
        public double GiaMua
        {
            get
            {
                return giaMua;
            }
            set
            {
                giaMua = value;
            }
        }
        public double ThanhTien
        {
            get
            {
                return thanhTien;
            }
            set
            {
                thanhTien = value;
            }
        }

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
    }
}
