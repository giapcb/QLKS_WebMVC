using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLKS_WebMVC.Models
{
    public class Phong
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tên phòng")]
        public string? TenPhong { get; set; }
        [Required]
        [Display(Name = "Trạng thái")]
        public string? TrangThai { get;set; } //tr

        [Display(Name = "Loại phòng")]
        public int LoaiPhongId {  get; set; }

        [ForeignKey("LoaiPhongId")]
        public LoaiPhong? LoaiPhong { get; set; }    
    }
}
