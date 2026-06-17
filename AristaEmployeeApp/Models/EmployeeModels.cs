using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AristaEmployeeApp.Models
{
    [Table("Perusahaan")]
    public class Perusahaan
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [Column("NamaPerusahaan")]
        public string Nama { get; set; }
        
        public bool Aktif { get; set; } = true;
    }

    [Table("Cabang")]
    public class Cabang
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [Column("NamaCabang")]
        public string Nama { get; set; }
        
        // Based on the error, the column in the database might not be 'PerusahaanId' 
        // but the user's schema info says it IS 'PerusahaanId'.
        // However, the error 'Invalid column name PerusahaanId' twice suggests it's missing in both Karyawan and Cabang or mapped incorrectly.
        // Let's check the Karyawan table mapping too.
        public int PerusahaanId { get; set; }
        
        public bool Aktif { get; set; } = true;

        [ForeignKey("PerusahaanId")]
        public virtual Perusahaan? Perusahaan { get; set; }
    }

    [Table("Karyawan")]
    public class Karyawan
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Kode Karyawan wajib diisi")]
        public string KodeKaryawan { get; set; }

        [Required(ErrorMessage = "Nama Karyawan wajib diisi")]
        [Column("NamaKaryawan")]
        public string Nama { get; set; }

        // The error says 'Invalid column name PerusahaanId' twice.
        // In the SQL query generated:
        // SELECT [k].[Id], ..., [k].[PerusahaanId], ..., [c].[PerusahaanId]
        // This means BOTH Karyawan and Cabang tables are expected to have PerusahaanId.
        // If the database schema provided by the user is correct:
        // Cabang: Id, PerusahaanId, NamaCabang, Aktif
        // Karyawan: Id, CabangId, KodeKaryawan, NamaKaryawan, Aktif
        // WAIT! The user's Karyawan schema DOES NOT have PerusahaanId!
        // Karyawan table: Id, CabangId, KodeKaryawan, NamaKaryawan, Aktif
        // So Karyawan only links to Cabang, and Cabang links to Perusahaan.
        
        public int CabangId { get; set; }

        public bool Aktif { get; set; } = true;

        [ForeignKey("CabangId")]
        public virtual Cabang? Cabang { get; set; }

        // We can still have a Perusahaan property but it must be NotMapped or mapped through Cabang
        [NotMapped]
        public int PerusahaanId { get; set; }

        [NotMapped]
        public virtual Perusahaan? Perusahaan => Cabang?.Perusahaan;
    }
}
