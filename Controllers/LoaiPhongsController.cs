using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLKS_WebMVC.Data;
using QLKS_WebMVC.Models;

namespace QLKS_WebMVC.Controllers
{
    public class LoaiPhongsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoaiPhongsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LoaiPhongs
        public async Task<IActionResult> Index()
        {
            return View(await _context.LoaiPhongs.ToListAsync());
        }

        // GET: LoaiPhongs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiPhong = await _context.LoaiPhongs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loaiPhong == null)
            {
                return NotFound();
            }

            return View(loaiPhong);
        }

        // GET: LoaiPhongs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoaiPhongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoaiPhong loaiPhong, List<IFormFile> images)
        {
            if (ModelState.IsValid)
            {
                var imageNames = new List<string>();

                foreach (var file in images)
                {
                    if (file != null && file.Length > 0)
                    {
                        // Tạo tên file duy nhất
                        var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                        var extension = Path.GetExtension(file.FileName);
                        var uniqueName = $"{fileName}_{Guid.NewGuid()}{extension}";

                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        imageNames.Add(uniqueName);
                    }
                }

                // Gộp tên ảnh thành 1 chuỗi
                loaiPhong.HinhAnh = string.Join(";", imageNames);

                _context.Add(loaiPhong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaiPhong);
        }


        // GET: LoaiPhongs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiPhong = await _context.LoaiPhongs.FindAsync(id);
            if (loaiPhong == null)
            {
                return NotFound();
            }
            return View(loaiPhong);
        }

        // POST: LoaiPhongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenLoai,GiaPhong,MoTa")] LoaiPhong loaiPhong, List<IFormFile> hinhAnhFiles)

        {
            if (id != loaiPhong.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (hinhAnhFiles != null && hinhAnhFiles.Any())
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                    List<string> fileNames = new();

                    foreach (var file in hinhAnhFiles)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        fileNames.Add(fileName);
                    }

                    // 👉 Nếu bạn muốn GỘP với ảnh cũ, dùng đoạn này:
                    var anhCu = await _context.LoaiPhongs.Where(p => p.Id == id)
                                 .Select(p => p.HinhAnh)
                                 .FirstOrDefaultAsync();

                    if (!string.IsNullOrEmpty(anhCu))
                    {
                        fileNames.InsertRange(0, anhCu.Split(';', StringSplitOptions.RemoveEmptyEntries));
                    }

                    // Gán danh sách ảnh mới
                    loaiPhong.HinhAnh = string.Join(";", fileNames);
                }

                try
                {
                    _context.Update(loaiPhong);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiPhongExists(loaiPhong.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(loaiPhong);
        }

        // GET: LoaiPhongs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiPhong = await _context.LoaiPhongs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loaiPhong == null)
            {
                return NotFound();
            }

            return View(loaiPhong);
        }

        // POST: LoaiPhongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loaiPhong = await _context.LoaiPhongs.FindAsync(id);
            if (loaiPhong != null)
            {
                _context.LoaiPhongs.Remove(loaiPhong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiPhongExists(int id)
        {
            return _context.LoaiPhongs.Any(e => e.Id == id);
        }
    }
}
