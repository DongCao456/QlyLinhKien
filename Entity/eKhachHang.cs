using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Entity
{
    public class eKhachHang
    {
        private string maKhachHang;
        private string tenKhachHang;
        private string soDienThoai;
        private string diaChi;
        private string eMail;

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

        public string TenKhachHang
        {
            get
            {
                return tenKhachHang;
            }

            set
            {
                if (!Regex.IsMatch(xoaUnicode(value), @"^([A-Z][a-z]*\s{0,1})+$"))
                    throw new Exception("Sai tên khách hàng, Ví dụ: Công Phạm Quốc Việt");
                tenKhachHang = value;
            }
        }

        public string SoDienThoai
        {
            get
            {
                return soDienThoai;
            }

            set
            {
                if (!Regex.IsMatch(value, @"^\d{10,}$"))
                    throw new Exception("Sai số điện thoại,Ví dụ: 01647297306");
                soDienThoai = value;
            }
        }

        public string DiaChi
        {
            get
            {
                return diaChi;
            }

            set
            {
                if (value.Trim().Length == 0)
                    throw new Exception("Địa chỉ không được trống");
                diaChi = value;
            }
        }

        public string EMail
        {
            get
            {
                return eMail;
            }

            set
            {
                if (!Regex.IsMatch(xoaUnicode(value), @"^\w+@\w+.\w+$"))
                    throw new Exception("Sai email, ví dụ: quocviet@gmail.com");
                eMail = value;
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
