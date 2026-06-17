using Microsoft.EntityFrameworkCore;
using AristaEmployeeApp.Data;
using AristaEmployeeApp.Models;

namespace AristaEmployeeApp.Repositories
{
    public class KaryawanRepository : IKaryawanRepository
    {
        private readonly ApplicationDbContext _context;
        public KaryawanRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Karyawan>> GetAllActiveAsync()
        {
            return await _context.Karyawans
                .Include(k => k.Cabang)
                    .ThenInclude(c => c.Perusahaan)
                .Where(k => k.Aktif)
                .ToListAsync();
        }

        public async Task<Karyawan?> GetByIdAsync(int id)
        {
            return await _context.Karyawans
                .Include(k => k.Cabang)
                .FirstOrDefaultAsync(k => k.Id == id);
        }

        public async Task<bool> ExistsActiveCodeAsync(string code)
        {
            return await _context.Karyawans.AnyAsync(k => k.KodeKaryawan == code && k.Aktif);
        }

        public async Task<bool> ExistsActiveCodeExcludeIdAsync(string code, int id)
        {
            return await _context.Karyawans.AnyAsync(k => k.KodeKaryawan == code && k.Id != id && k.Aktif);
        }

        public async Task AddAsync(Karyawan karyawan) => await _context.Karyawans.AddAsync(karyawan);
        public async Task UpdateAsync(Karyawan karyawan) => _context.Karyawans.Update(karyawan);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    }

    public class PerusahaanRepository : IPerusahaanRepository
    {
        private readonly ApplicationDbContext _context;
        public PerusahaanRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Perusahaan>> GetAllActiveAsync()
        {
            return await _context.Perusahaans.Where(p => p.Aktif).ToListAsync();
        }
    }

    public class CabangRepository : ICabangRepository
    {
        private readonly ApplicationDbContext _context;
        public CabangRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Cabang>> GetByPerusahaanIdAsync(int perusahaanId)
        {
            return await _context.Cabangs.Where(c => c.PerusahaanId == perusahaanId && c.Aktif).ToListAsync();
        }
    }
}
