using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Entity
{
    public class eNhanVien
    {
        private string maNhanVien;
        private string tenNhanVien;
        private string cMND;
        private string soDienThoai;
        private string eMail;
        private string diaChi;
        private string trangThai;
        private string maLoaiNhanVien;

        public string MaNhanVien
        {
            get
            {
                return maNhanVien;
            }

            set
            {
                if (!Regex.IsMatch(value, @"^NV\-\d+$"))
                    throw new Exception("Sai mã nhân viên, Ví dụ: NV-1");
                maNhanVien = value;
            }
        }

        public string TenNhanVien
        {
            get
            {
                return tenNhanVien;
            }

            set
            {
                if (!Regex.IsMatch(xoaUnicode(value), @"^([A-Z][a-z]*\s{0,1})+$"))
                    throw new Exception("Sai tên nhân viên, Ví dụ: Công Phạm Quốc Việt");
                tenNhanVien = value;
            }
        }

        public string CMND
        {
            get
            {
                return cMND;
            }

            set
            {
                if (!Regex.IsMatch(value, @"^\d{9,10}$"))
                    throw new Exception("Sai số CMND,Ví dụ: 025142592");
                cMND = value;
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
                if (!Regex.IsMatch(xoaUnicode(value), @"^\w+@\w+.\w+$"))
                    throw new Exception("Sai email, ví dụ: quocviet@gmail.com");
                eMail = value;
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

        public string TrangThai
        {
            get
            {
                return trangThai;
            }
            set
            {
                if (value.Trim().Length == 0)
                    throw new Exception("Trạng thái không được trống");
                trangThai = value;
            }
        }

        public string MaLoaiNhanVien
        {
            get
            {
                return maLoaiNhanVien;
            }
            set
            {
                if (value.Trim().Length == 0)
                    throw new Exception("Loại nhân viên không được trống");
                maLoaiNhanVien = value;
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
