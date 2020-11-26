using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Entity
{
    public class eNhaCungCap
    {
        private string maNhaCungCap;
        private string tenNhaCungCap;
        private string soDienThoai;
        private string eMail;
        private string quocGia;
        private string diaChi;

        public string MaNhaCungCap
        {
            get
            {
                return maNhaCungCap;
            }
            set
            {
                if (!Regex.IsMatch(value, @"^NCC\-\d+$"))
                    throw new Exception("Sai mã khách hàng, Ví dụ: NCC-1");
                maNhaCungCap = value;
            }
        }
        public string TenNhaCungCap
        {
            get
            {
                return tenNhaCungCap;
            }
            set
            {
                if (value.Trim().Length == 0)
                    throw new Exception("Tên nhà cùng cấp không được để trống");
                tenNhaCungCap = value;
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
        public string EMail
        {
            get
            {
                return eMail;
            }
            set
            {
                if (!Regex.IsMatch(value, @"^\w+@\w+.\w+$"))
                    throw new Exception("Sai email, ví dụ: quocviet@gmail.com");
                eMail = value;
            }
        }
        public string QuocGia
        {
            get
            {
                return quocGia;
            }
            set
            {
                if (!Regex.IsMatch(xoaUnicode(value), @"^(\w+\s*)+$"))
                    throw new Exception("Quốc gia chỉ chấp nhận chữ và không được để trống");
                quocGia = value;
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
                    throw new Exception("Địa chỉ chỉ chấp nhận chữ và không được để trống");
                diaChi = value;
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
