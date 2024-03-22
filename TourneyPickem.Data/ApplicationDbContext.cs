using Microsoft.EntityFrameworkCore;
using TourneyPickem.Data.Models;

namespace TourneyPickem.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Player> Players { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Game> Games { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=main.db");
        }
    }
}
