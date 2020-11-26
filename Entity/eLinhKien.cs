using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Entity
{
    public class eLinhKien
    {
        private string maLinhKien;
        private string tenLinhKien;
        private string maLoai;
        private double giaMua;
        private double giaBan;
        private int soLuong;
        private double mucGiamGia;
        private string maNhaCungCap;

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

        public string TenLinhKien
        {
            get
            {
                return tenLinhKien;
            }

            set
            {
                if (value.Trim().Length == 0)
                    throw new Exception("Tên linh kiện không được để trống");
                tenLinhKien = value;
            }
        }

        public double GiaBan
        {
            get
            {
                return giaBan;
            }

            set
            {
                if (Regex.IsMatch(value + "", @"\D") || value <= 0)
                    throw new Exception("Giá chỉ chấp nhận số, ví dụ: 20000");
                giaBan = value;
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
                if (Regex.IsMatch(value + "", @"\D") || value <= 0)
                    throw new Exception("Giá chỉ chấp nhận số, ví dụ: 10000");
                giaMua = value;
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
                if (Regex.IsMatch(value + "", @"\D"))
                    throw new Exception("Giá chỉ chấp nhận số, ví dụ: 3 ");
                soLuong = value;
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

        public string MaLoai
        {
            get
            {
                return maLoai;
            }

            set
            {
                maLoai = value;
            }
        }

        public double MucGiamGia
        {
            get
            {
                return mucGiamGia;
            }
            set
            {
                if (value < 0 || value >= 1)
                {
                    throw new Exception("Mức giảm giá phải là số lớn hơn 0 và bé hơn 1");
                }
                mucGiamGia = value;
            }
        }

        private string xoaUnicode(string txt)
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
    }
}
