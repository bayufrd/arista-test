using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AristaEmployeeApp.Models
{
    public class KaryawanViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kode Karyawan wajib diisi")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Kode Karyawan wajib 5 digit")]
        public string KodeKaryawan { get; set; }

        [Required(ErrorMessage = "Nama Karyawan wajib diisi")]
        public string Nama { get; set; }

        [Required(ErrorMessage = "Perusahaan wajib dipilih")]
        public int PerusahaanId { get; set; }

        [Required(ErrorMessage = "Cabang wajib dipilih")]
        public int CabangId { get; set; }

        public bool Aktif { get; set; } = true;

        // For display in Index
        public string? NamaPerusahaan { get; set; }
        public string? NamaCabang { get; set; }

        // For dropdowns in Create/Edit
        public SelectList? PerusahaanList { get; set; }
        public SelectList? CabangList { get; set; }
    }
}
