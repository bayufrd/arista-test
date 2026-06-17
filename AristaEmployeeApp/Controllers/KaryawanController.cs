using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AristaEmployeeApp.Data;
using AristaEmployeeApp.Models;

namespace AristaEmployeeApp.Controllers
{
    public class KaryawanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KaryawanController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _context.Karyawans
                .Include(k => k.Cabang)
                    .ThenInclude(c => c.Perusahaan)
                .Where(k => k.Aktif)
                .ToListAsync();
            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.PerusahaanId = new SelectList(await _context.Perusahaans.Where(p => p.Aktif).ToListAsync(), "Id", "Nama");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Karyawan karyawan)
        {
            if (karyawan.KodeKaryawan?.Length != 5)
            {
                ModelState.AddModelError("KodeKaryawan", "Kode Karyawan wajib 5 digit.");
            }

            var existing = await _context.Karyawans
                .AnyAsync(k => k.KodeKaryawan == karyawan.KodeKaryawan && k.Aktif);
            
            if (existing)
            {
                ModelState.AddModelError("KodeKaryawan", "Kode Karyawan sudah terdaftar dan masih aktif.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(karyawan);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Data Berhasil Disimpan";
                    return RedirectToAction(nameof(Edit), new { id = karyawan.Id });
                }
                catch
                {
                    TempData["Error"] = "Data Gagal Disimpan";
                }
            }

            ViewBag.PerusahaanId = new SelectList(await _context.Perusahaans.Where(p => p.Aktif).ToListAsync(), "Id", "Nama", karyawan.PerusahaanId);
            ViewBag.CabangId = new SelectList(await _context.Cabangs.Where(c => c.PerusahaanId == karyawan.PerusahaanId && c.Aktif).ToListAsync(), "Id", "Nama", karyawan.CabangId);
            return View(karyawan);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var karyawan = await _context.Karyawans
                .Include(k => k.Cabang)
                .FirstOrDefaultAsync(k => k.Id == id);
            
            if (karyawan == null) return NotFound();

            karyawan.PerusahaanId = karyawan.Cabang?.PerusahaanId ?? 0;

            ViewBag.PerusahaanId = new SelectList(await _context.Perusahaans.Where(p => p.Aktif).ToListAsync(), "Id", "Nama", karyawan.PerusahaanId);
            ViewBag.CabangId = new SelectList(await _context.Cabangs.Where(c => c.PerusahaanId == karyawan.PerusahaanId && c.Aktif).ToListAsync(), "Id", "Nama", karyawan.CabangId);
            return View(karyawan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Karyawan karyawan)
        {
            if (id != karyawan.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(karyawan);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Data Berhasil Diupdate";
                }
                catch
                {
                    TempData["Error"] = "Data Gagal Diupdate";
                }
            }
            ViewBag.PerusahaanId = new SelectList(await _context.Perusahaans.Where(p => p.Aktif).ToListAsync(), "Id", "Nama", karyawan.PerusahaanId);
            ViewBag.CabangId = new SelectList(await _context.Cabangs.Where(c => c.PerusahaanId == karyawan.PerusahaanId && c.Aktif).ToListAsync(), "Id", "Nama", karyawan.CabangId);
            return View(karyawan);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var karyawan = await _context.Karyawans.FindAsync(id);
            if (karyawan != null)
            {
                karyawan.Aktif = false;
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpGet]
        public async Task<JsonResult> GetCabang(int perusahaanId)
        {
            var cabangs = await _context.Cabangs
                .Where(c => c.PerusahaanId == perusahaanId && c.Aktif)
                .Select(c => new { id = c.Id, nama = c.Nama })
                .ToListAsync();
            return Json(cabangs);
        }
    }
}
