using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Kataandi.Models
{
    public class Barcode
    {

        [Key]
        public string Id_sinas { get; set; }
        [DefaultValue("BETON")]
        public string Nama_Barang { get; set; }
        [DefaultValue("Lorem Ipsum")]

        public string Deskripsi { get; set; }
        public int Tahun_Barang { get; set; }
        [DefaultValue(50)]

        public string Keterangan_aset { get; set; }

    }

}
