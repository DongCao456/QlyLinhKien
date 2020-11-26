using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class eTaiKhoan
    {
        private string maTaiKhoan;
        private string matKhau;

        public string MaTaiKhoan
        {
            get
            {
                return maTaiKhoan;
            }
            set
            {
                maTaiKhoan = value;
            }
        }
        public string MatKhau
        {
            get
            {
                return matKhau;
            }
            set
            {
                matKhau = value;
            }
        }
    }
}
