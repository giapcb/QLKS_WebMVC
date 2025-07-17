using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QLKS_WebMVC.Models;

namespace QLKS_WebMVC.Data
{
    public class QLKS_WebMVCContext : DbContext
    {
        public QLKS_WebMVCContext (DbContextOptions<QLKS_WebMVCContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KhachHang>().HasData(
                new KhachHang { MaKH = 1, HoTen = "Do Hoang Minh", SDT = "123456789", CCCD = "12345", DiaChi = "Nam Dinh" }
                );
            modelBuilder.Entity<DatPhong>().HasData(
                new DatPhong { MaDat = 1, MaKH = 1, MaPhong = 1, NgayDat = new DateTime(2025, 01, 01), NgayNhan = new DateTime(2025, 01, 05), NgayTra = new DateTime(2025, 01, 06) }
                );
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<QLKS_WebMVC.Models.KhachHang> KhachHang { get; set; } = default!;
        public DbSet<QLKS_WebMVC.Models.DatPhong> DatPhong { get; set; } = default!;

        

    }
}
