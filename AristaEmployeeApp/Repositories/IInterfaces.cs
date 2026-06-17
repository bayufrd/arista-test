using AristaEmployeeApp.Models;

namespace AristaEmployeeApp.Repositories
{
    public interface IKaryawanRepository
    {
        Task<IEnumerable<Karyawan>> GetAllActiveAsync();
        Task<Karyawan?> GetByIdAsync(int id);
        Task<bool> ExistsActiveCodeAsync(string code);
        Task<bool> ExistsActiveCodeExcludeIdAsync(string code, int id);
        Task AddAsync(Karyawan karyawan);
        Task UpdateAsync(Karyawan karyawan);
        Task SaveChangesAsync();
    }

    public interface IPerusahaanRepository
    {
        Task<IEnumerable<Perusahaan>> GetAllActiveAsync();
    }

    public interface ICabangRepository
    {
        Task<IEnumerable<Cabang>> GetByPerusahaanIdAsync(int perusahaanId);
    }
}
