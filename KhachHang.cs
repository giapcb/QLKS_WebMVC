using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace QLKS_WebMVC.Models
{
    public class KhachHang
    {
        [Key]
        public int MaKH { get; set; }

        [Required]
        public string? HoTen { get; set; }

        public string? SDT { get; set; }

        public string? CCCD { get; set; }

        public string? DiaChi { get; set; }

        // Mối quan hệ 1-n với DatPhong
        public ICollection<DatPhong> ?DanhSachDatPhong { get; set; }
    }
}
