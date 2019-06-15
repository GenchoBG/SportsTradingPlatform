using Microsoft.EntityFrameworkCore;
using SportsTrading.Data.Models;

namespace SportsTrading.Data
{
    public class SportsTradingDbContext : DbContext
    {
        public SportsTradingDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Sport> Sports { get; set; }

        public DbSet<League> Leagues { get; set; }

        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
