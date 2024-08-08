using Kataandi.Models;
using Kataandi.Models.dto;
using Microsoft.EntityFrameworkCore;

namespace Kataandi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) 
        { 
        }

        public DbSet<Barcode> Barcodes { get; set; }
        public DbSet<Aset> Aset { get; set; }
        public DbSet<MD_Aset> MD_Aset { get; set; }
    }
}