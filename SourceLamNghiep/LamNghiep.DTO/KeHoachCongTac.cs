using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamNghiep.DTO
{
    public class KeHoachCT
    {
        public string Ngay { get; set; }
        public string HoTen { get; set; }
        public string TenKeHoach { get; set; }
        public int ThuTu { get; set; }
        public string TuNgay { get; set; }
        public string DenNgay { get; set; }
        public string KeHoachCongTac { get; set; }
        public string ThamDu { get; set; }
        public string DiaDiem { get; set; }
        public string ChucVu { get; set; }

        public DateTime ThoiGian { get; set; }
        public TimeSpan Gio { get; set; }

        public int MucDo { get; set; }

        public bool LapLai { get; set; }
        public string UserName { get; set; }

        public string NguoiTao { get; set; }

        public string GioView { set; get; }
        public string MucDoView { get; set; }

    }
   
}
