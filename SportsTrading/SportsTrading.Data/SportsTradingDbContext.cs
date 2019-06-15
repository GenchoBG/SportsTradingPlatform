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

            modelBuilder
                .Entity<League>()
                .HasOne(l => l.Sport)
                .WithMany()
                .HasForeignKey(l => l.SportId);

            modelBuilder
                .Entity<Event>()
                .HasOne(e => e.Sport)
                .WithMany()
                .HasForeignKey(e => e.SportId);

            modelBuilder
                .Entity<Event>()
                .HasOne(e => e.League)
                .WithMany()
                .HasForeignKey(e => e.LeagueId);
        }
    }
}
