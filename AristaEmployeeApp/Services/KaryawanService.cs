using AristaEmployeeApp.Models;
using AristaEmployeeApp.Repositories;

namespace AristaEmployeeApp.Services
{
    public interface IKaryawanService
    {
        Task<IEnumerable<Karyawan>> GetActiveEmployeesAsync();
        Task<Karyawan?> GetEmployeeByIdAsync(int id);
        Task<bool> IsCodeDuplicateAsync(string code);
        Task<bool> IsCodeDuplicateExcludeIdAsync(string code, int id);
        Task<bool> CreateEmployeeAsync(Karyawan karyawan);
        Task<bool> UpdateEmployeeAsync(Karyawan karyawan);
        Task<bool> SoftDeleteEmployeeAsync(int id);
        Task<IEnumerable<Perusahaan>> GetActiveCompaniesAsync();
        Task<IEnumerable<Cabang>> GetBranchesByCompanyAsync(int perusahaanId);
    }

    public class KaryawanService : IKaryawanService
    {
        private readonly IKaryawanRepository _karyawanRepo;
        private readonly IPerusahaanRepository _perusahaanRepo;
        private readonly ICabangRepository _cabangRepo;

        public KaryawanService(IKaryawanRepository karyawanRepo, IPerusahaanRepository perusahaanRepo, ICabangRepository cabangRepo)
        {
            _karyawanRepo = karyawanRepo;
            _perusahaanRepo = perusahaanRepo;
            _cabangRepo = cabangRepo;
        }

        public async Task<IEnumerable<Karyawan>> GetActiveEmployeesAsync() => await _karyawanRepo.GetAllActiveAsync();
        public async Task<Karyawan?> GetEmployeeByIdAsync(int id) => await _karyawanRepo.GetByIdAsync(id);
        public async Task<bool> IsCodeDuplicateAsync(string code) => await _karyawanRepo.ExistsActiveCodeAsync(code);
        public async Task<bool> IsCodeDuplicateExcludeIdAsync(string code, int id) => await _karyawanRepo.ExistsActiveCodeExcludeIdAsync(code, id);

        public async Task<bool> CreateEmployeeAsync(Karyawan karyawan)
        {
            await _karyawanRepo.AddAsync(karyawan);
            await _karyawanRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateEmployeeAsync(Karyawan karyawan)
        {
            await _karyawanRepo.UpdateAsync(karyawan);
            await _karyawanRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SoftDeleteEmployeeAsync(int id)
        {
            var karyawan = await _karyawanRepo.GetByIdAsync(id);
            if (karyawan == null) return false;
            karyawan.Aktif = false;
            await _karyawanRepo.UpdateAsync(karyawan);
            await _karyawanRepo.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Perusahaan>> GetActiveCompaniesAsync() => await _perusahaanRepo.GetAllActiveAsync();
        public async Task<IEnumerable<Cabang>> GetBranchesByCompanyAsync(int perusahaanId) => await _cabangRepo.GetByPerusahaanIdAsync(perusahaanId);
    }
}
