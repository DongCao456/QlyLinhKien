using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class eLoaiNhanVien
    {
        private string maLoaiNhanVien;
        private string tenLoaiNhanVien;
        private string maPhanQuyen;

        public string MaLoaiNhanVien
        {
            get
            {
                return maLoaiNhanVien;
            }
            set
            {
                maLoaiNhanVien = value;
            }
        }
        public string TenLoaiNhanVien
        {
            get
            {
                return tenLoaiNhanVien;
            }
            set
            {
                tenLoaiNhanVien = value;
            }
        }

        public string MaPhanQuyen
        {
            get
            {
                return maPhanQuyen;
            }
            set
            {
                maPhanQuyen = value;
            }
        }
    }
}
