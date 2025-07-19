using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace QLKS_WebMVC.Models
{
    public class LoaiPhong
    {
        [Key]

        [Display(Name = "Mã loại phòng")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tên loại phòng")]
        public string? TenLoai {  get; set; }

        [Required]
        [Display(Name = "Gía phòng")]
        [Precision(18, 2)]
        public decimal GiaPhong { get;set; }

        [Display(Name ="Mô tả")]
        public string? MoTa {  get; set; }

        [Display(Name = "Hình ảnh")]
        public string? HinhAnh { get; set; }
        public ICollection<Phong>? Phongs { get; set; }

    }
}
