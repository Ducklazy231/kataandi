using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Kataandi.Models.dto
{
    public class MD_Aset
    {
        [Key]
        public string AsetNo { get; set; }
        public int LineNo { get; set; }
        public int LocationId { get; set; }
        public string NomorAsetMySAP { get; set; }
        public string MySAPLineNo { get; set; }
        public string HarmoniNo { get; set; }
        public string SinasNo { get; set; }
        public string Deskripsi { get; set; }
        public string Merk { get; set; }
        public string KategoriAset { get; set; }
        public int TahunPerolehan { get; set; }    
        public decimal Amount { get; set; }
        public string StatusPenggunaan { get; set; }
        public int Level { get; set; }
        public int KondisiAset { get; set; }
        public string LokasiAset { get; set; }
        public string PathFotoTagging { get; set; }
        public string PathFotoKeseluruhan { get; set; }
    }
}
