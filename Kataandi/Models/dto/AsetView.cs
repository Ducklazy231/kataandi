using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Kataandi.Models.dto
{
    public class AsetView
    {
        public List<Aset> assets { get; set; }
        public Aset newAset { get; set; }
    }
}
