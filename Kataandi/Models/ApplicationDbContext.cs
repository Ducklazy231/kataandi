using Kataandi.Models;
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
    }
}