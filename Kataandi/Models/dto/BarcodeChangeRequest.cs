using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Kataandi.Models.dto
{
    public class BarcodeChangeRequest
    {
       
        public string Aset_No {  get; set; }
        [DefaultValue("BETON")]
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
