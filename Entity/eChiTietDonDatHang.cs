using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Entity
{
    public class eChiTietDonDatHang
    {
        private string maDonDatHang;
        private string maLinhKien;
        private int soLuong;
        private double giaBan;
        private double mucGiamGia;
        private double thanhTien;

        //Tự phát sinh
        public string MaDonDatHang
        {
            get
            {
                return maDonDatHang;
            }

            set
            {
                //if (!Regex.IsMatch(value, @"^DDH-\d+$"))
                //    throw new Exception("Sai mã đơn đặt hàng, Ví dụ: DDH-1");
                maDonDatHang = value;
            }
        }

        //Tự phát sinh
        public string MaLinhKien
        {
            get
            {
                return maLinhKien;
            }

            set
            {
                //if (!Regex.IsMatch(value, @"^LK-\d+$"))
                //    throw new Exception("Sai mã linh kiện, ví dụ: LK-1");
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
                if (Regex.IsMatch(value + "", @"\D") || value <=0)
                    throw new Exception("Sai số lượng, chỉ chấp nhận số lớn hơn 0, ví dụ: 1");
                soLuong = value;
            }
        }

        //Tự phát sinh
        public double GiaBan
        {
            get
            {
                return giaBan;
            }

            set
            {
                giaBan = value;
            }
        }

        //Tự phát sinh
        public double MucGiamGia
        {
            get
            {
                return mucGiamGia;
            }

            set
            {
                mucGiamGia = value;
            }
        }

        //Tự phát sinh
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
    }
}
