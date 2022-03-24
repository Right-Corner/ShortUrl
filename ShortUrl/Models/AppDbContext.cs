using Microsoft.EntityFrameworkCore;

namespace ShortUrl.Models
{
    public class AppDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-GE7ID1L; Database=ShortURL; User ID='sa'; Password='sa'");
        }
        public DbSet<UrlDetail> UrlDetails { get; set; }
    }
}
