using AngularExampleAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularExampleAPI.Data
{
    public class AngularExampleDbContext :DbContext
    {
        public AngularExampleDbContext(DbContextOptions options) 
            : base(options) { }
        public DbSet<Urun> Urunler { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("Host=localhost; Database=AngularMovieApp; Username=postgres; Password=123; Port=5432");
        }
    }
}
