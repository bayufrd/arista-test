using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AristaEmployeeApp.Models;
using AristaEmployeeApp.Services;

namespace AristaEmployeeApp.Controllers
{
    public class KaryawanController : Controller
    {
        private readonly IKaryawanService _karyawanService;

        public KaryawanController(IKaryawanService karyawanService)
        {
            _karyawanService = karyawanService;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _karyawanService.GetActiveEmployeesAsync();
            var viewModel = employees.Select(e => new KaryawanViewModel
            {
                Id = e.Id,
                KodeKaryawan = e.KodeKaryawan,
                Nama = e.Nama,
                NamaPerusahaan = e.Cabang?.Perusahaan?.Nama,
                NamaCabang = e.Cabang?.Nama
            });

            if (Request.Headers["Accept"].ToString().Contains("application/json"))
            {
                return Json(viewModel);
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new KaryawanViewModel
            {
                PerusahaanList = new SelectList(await _karyawanService.GetActiveCompaniesAsync(), "Id", "Nama")
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KaryawanViewModel model)
        {
            if (await _karyawanService.IsCodeDuplicateAsync(model.KodeKaryawan))
            {
                ModelState.AddModelError("KodeKaryawan", "Kode Karyawan sudah terdaftar dan masih aktif.");
            }

            if (ModelState.IsValid)
            {
                var karyawan = new Karyawan
                {
                    KodeKaryawan = model.KodeKaryawan,
                    Nama = model.Nama,
                    CabangId = model.CabangId,
                    Aktif = true
                };

                if (await _karyawanService.CreateEmployeeAsync(karyawan))
                {
                    TempData["Success"] = "Data Berhasil Disimpan";
                    return RedirectToAction(nameof(Edit), new { id = karyawan.Id });
                }
                TempData["Error"] = "Data Gagal Disimpan";
            }

            model.PerusahaanList = new SelectList(await _karyawanService.GetActiveCompaniesAsync(), "Id", "Nama", model.PerusahaanId);
            model.CabangList = new SelectList(await _karyawanService.GetBranchesByCompanyAsync(model.PerusahaanId), "Id", "Nama", model.CabangId);
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var e = await _karyawanService.GetEmployeeByIdAsync(id.Value);
            if (e == null) return NotFound();

            var viewModel = new KaryawanViewModel
            {
                Id = e.Id,
                KodeKaryawan = e.KodeKaryawan,
                Nama = e.Nama,
                PerusahaanId = e.Cabang?.PerusahaanId ?? 0,
                CabangId = e.CabangId,
                Aktif = e.Aktif,
                PerusahaanList = new SelectList(await _karyawanService.GetActiveCompaniesAsync(), "Id", "Nama", e.Cabang?.PerusahaanId),
                CabangList = new SelectList(await _karyawanService.GetBranchesByCompanyAsync(e.Cabang?.PerusahaanId ?? 0), "Id", "Nama", e.CabangId)
            };

            if (Request.Headers["Accept"].ToString().Contains("application/json"))
            {
                return Json(viewModel);
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var e = await _karyawanService.GetEmployeeByIdAsync(id);
            if (e == null) return NotFound();

            var viewModel = new KaryawanViewModel
            {
                Id = e.Id,
                KodeKaryawan = e.KodeKaryawan,
                Nama = e.Nama,
                NamaPerusahaan = e.Cabang?.Perusahaan?.Nama,
                NamaCabang = e.Cabang?.Nama,
                Aktif = e.Aktif
            };

            return Json(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KaryawanViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (await _karyawanService.IsCodeDuplicateExcludeIdAsync(model.KodeKaryawan, model.Id))
            {
                ModelState.AddModelError("KodeKaryawan", "Kode Karyawan sudah terdaftar dan masih aktif.");
            }

            if (ModelState.IsValid)
            {
                var karyawan = new Karyawan
                {
                    Id = model.Id,
                    KodeKaryawan = model.KodeKaryawan,
                    Nama = model.Nama,
                    CabangId = model.CabangId,
                    Aktif = model.Aktif
                };

                if (await _karyawanService.UpdateEmployeeAsync(karyawan))
                {
                    TempData["Success"] = "Data Berhasil Diupdate";
                }
                else
                {
                    TempData["Error"] = "Data Gagal Diupdate";
                }
            }
            model.PerusahaanList = new SelectList(await _karyawanService.GetActiveCompaniesAsync(), "Id", "Nama", model.PerusahaanId);
            model.CabangList = new SelectList(await _karyawanService.GetBranchesByCompanyAsync(model.PerusahaanId), "Id", "Nama", model.CabangId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _karyawanService.SoftDeleteEmployeeAsync(id);
            return Json(new { success });
        }

        [HttpGet]
        public async Task<JsonResult> GetCabang(int perusahaanId)
        {
            var cabangs = await _karyawanService.GetBranchesByCompanyAsync(perusahaanId);
            var result = cabangs.Select(c => new { id = c.Id, nama = c.Nama });
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetPerusahaan()
        {
            var companies = await _karyawanService.GetActiveCompaniesAsync();
            var result = companies.Select(p => new { id = p.Id, nama = p.Nama });
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetAllCabang()
        {
            // Assuming we need a way to get all active branches
            var companies = await _karyawanService.GetActiveCompaniesAsync();
            var allBranches = new List<object>();
            foreach (var comp in companies)
            {
                var branches = await _karyawanService.GetBranchesByCompanyAsync(comp.Id);
                allBranches.AddRange(branches.Select(c => new { id = c.Id, nama = c.Nama, perusahaanId = c.PerusahaanId, namaPerusahaan = comp.Nama }));
            }
            return Json(allBranches);
        }
    }
}
