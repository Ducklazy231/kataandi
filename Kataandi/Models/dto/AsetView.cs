using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Kataandi.Models.dto
{
    public class AsetView
    {
        public List<MD_Aset> assets { get; set; }
        public MD_Aset newAset { get; set; }
    }
}
