using Microsoft.EntityFrameworkCore;
using AristaEmployeeApp.Models;

namespace AristaEmployeeApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Karyawan> Karyawans { get; set; }
        public DbSet<Perusahaan> Perusahaans { get; set; }
        public DbSet<Cabang> Cabangs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Karyawan>()
                .HasOne(k => k.Cabang)
                .WithMany()
                .HasForeignKey(k => k.CabangId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Cabang>()
                .HasOne(c => c.Perusahaan)
                .WithMany()
                .HasForeignKey(c => c.PerusahaanId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
