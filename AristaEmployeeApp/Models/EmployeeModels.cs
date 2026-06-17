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
        
        public int CabangId { get; set; }

        public bool Aktif { get; set; } = true;

        [ForeignKey("CabangId")]
        public virtual Cabang? Cabang { get; set; }

        [NotMapped]
        public int PerusahaanId { get; set; }

        [NotMapped]
        public virtual Perusahaan? Perusahaan => Cabang?.Perusahaan;
    }
}
