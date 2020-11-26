using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Entity
{
    public class eLoaiLinhKien
    {
        private string maLoai;
        private string tenLoai;

        public string MaLoai
        {
            get
            {
                return maLoai;
            }

            set
            {
                if (!Regex.IsMatch(value, @"^LLK\-(\d+)$"))
                    throw new Exception("Sai mã loại linh kiện, ví dụ: LLK-1");
                maLoai = value;
            }
        }

        public string TenLoai
        {
            get
            {

                return tenLoai;
            }

            set
            {
                if (value.Trim().Length == 0)
                    throw new Exception("Tên loại linh kiện không được để trống");
                tenLoai = value;
            }
        }
    }
}
