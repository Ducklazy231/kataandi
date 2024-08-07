using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Kataandi.Models.dto
{
    public class Aset
    {
        [Key]
        public string Aset_No { get; set; }
        public int Line_No { get; set; }
        public string Nomor_Aset_MySAP { get; set; }
        public string MySAP_Line_No { get; set; }
        public string Harmoni_No { get; set; }
        public string Sinas_No { get; set; }
        public string Deskripsi { get; set; }
        public string Merk { get; set; }
        public string Kategori_Asset { get; set; }
        public int Tahun_Perolehan { get; set; }    
        public int Amount { get; set; }
        public string Status_Penggunaan { get; set; }
        public char Level { get; set; }
        public string Kondis_Aset { get; set; }
        public string Lokasi_Aset { get; set; }
        public string Path_Foto_Tagging { get; set; }
        public string Path_Foto_Keseluruhan { get; set; }
    }
}
