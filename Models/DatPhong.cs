using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLKS_WebMVC.Models
{
    public class DatPhong
    {
        [Key]
        public int MaDat { get; set; }

        [ForeignKey("KhachHang")]
        public int MaKH { get; set; } // Khóa ngoại đến KhachHang

        public int MaPhong { get; set; }

        public DateTime NgayDat { get; set; }

        public DateTime NgayNhan { get; set; }

        public DateTime NgayTra { get; set; }

        public string? TrangThai { get; set; }

        // Điều hướng ngược lại tới KhachHang
        public KhachHang ?KhachHang { get; set; }
    }
}
