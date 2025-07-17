using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLKS_WebMVC.Data;
using QLKS_WebMVC.Models;

namespace QLKS_WebMVC.Controllers
{
    public class DatPhongsController : Controller
    {
        private readonly QLKS_WebMVCContext _context;

        public DatPhongsController(QLKS_WebMVCContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var datPhongs = _context.DatPhong.Include(d => d.KhachHang);
            return View(await datPhongs.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var datPhong = await _context.DatPhong.Include(d => d.KhachHang)
                .FirstOrDefaultAsync(m => m.MaDat == id);

            if (datPhong == null) return NotFound();

            return View(datPhong);
        }

        public IActionResult Create()
        {
            ViewData["MaKH"] = new SelectList(_context.KhachHang, "MaKH", "HoTen");
            var phongTrong = _context.Phong.Where(p => p.TrangThai == "Trống").ToList();
            ViewData["MaPhong"] = new SelectList(phongTrong, "MaPhong", "MaPhong");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDat,MaKH,MaPhong,NgayDat,NgayNhan,NgayTra,TrangThai")] DatPhong datPhong)
        {
            if (datPhong.NgayNhan >= datPhong.NgayTra)
            {
                ModelState.AddModelError("", "Ngày trả phải sau ngày nhận.");
            }

            if (ModelState.IsValid)
            {
                datPhong.NgayDat = DateTime.Now;
                datPhong.TrangThai = "Đã đặt";

                var phong = await _context.Phong.FindAsync(datPhong.MaPhong);
                if (phong != null)
                {
                    phong.TrangThai = "Đã đặt";
                    _context.Update(phong);
                }

                _context.Add(datPhong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["MaKH"] = new SelectList(_context.KhachHang, "MaKH", "HoTen", datPhong.MaKH);
            var phongTrong = _context.Phong.Where(p => p.TrangThai == "Trống" || p.MaPhong == datPhong.MaPhong).ToList();
            ViewData["MaPhong"] = new SelectList(phongTrong, "MaPhong", "MaPhong", datPhong.MaPhong);
            return View(datPhong);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var datPhong = await _context.DatPhong.FindAsync(id);
            if (datPhong == null) return NotFound();

            ViewData["MaKH"] = new SelectList(_context.KhachHang, "MaKH", "HoTen", datPhong.MaKH);
            ViewData["MaPhong"] = new SelectList(_context.Phong, "MaPhong", "MaPhong", datPhong.MaPhong);
            return View(datPhong);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDat,MaKH,MaPhong,NgayDat,NgayNhan,NgayTra,TrangThai")] DatPhong datPhong)
        {
            if (id != datPhong.MaDat) return NotFound();

            if (datPhong.NgayNhan >= datPhong.NgayTra)
            {
                ModelState.AddModelError("", "Ngày trả phải sau ngày nhận.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(datPhong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DatPhongExists(datPhong.MaDat)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["MaKH"] = new SelectList(_context.KhachHang, "MaKH", "HoTen", datPhong.MaKH);
            ViewData["MaPhong"] = new SelectList(_context.Phong, "MaPhong", "MaPhong", datPhong.MaPhong);
            return View(datPhong);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var datPhong = await _context.DatPhong.Include(d => d.KhachHang)
                .FirstOrDefaultAsync(m => m.MaDat == id);

            if (datPhong == null) return NotFound();

            return View(datPhong);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var datPhong = await _context.DatPhong.FindAsync(id);
            if (datPhong != null)
            {
                var phong = await _context.Phong.FindAsync(datPhong.MaPhong);
                if (phong != null && phong.TrangThai == "Đã đặt")
                {
                    phong.TrangThai = "Trống";
                    _context.Update(phong);
                }

                _context.DatPhong.Remove(datPhong);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> TraPhong(int? id)
        {
            if (id == null) return NotFound();

            var datPhong = await _context.DatPhong.FindAsync(id);
            if (datPhong == null) return NotFound();

            datPhong.TrangThai = "Đã trả";
            _context.Update(datPhong);

            var phong = await _context.Phong.FindAsync(datPhong.MaPhong);
            if (phong != null)
            {
                phong.TrangThai = "Trống";
                _context.Update(phong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DatPhongExists(int id)
        {
            return _context.DatPhong.Any(e => e.MaDat == id);
        }
    }
}
