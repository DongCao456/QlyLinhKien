using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyLinhKien
{
    class CongCu
    {
        private static CongCu loai;

        internal static CongCu Loai
        {
            get
            {
                if (loai == null)
                {
                    loai = new CongCu();
                }
                return loai;
            }
            private set
            {
                loai = value;
            }
        }
        public string XoaUnicode(string txt)
        {
            txt = txt.ToLower();
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
            }
            return txt;
        }
    }
}
