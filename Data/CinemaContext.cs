using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GestionCinema;

public class CinemaContext : DbContext
{
    public DbSet<Cinema> Cinemas { get; set; } = null!;
    public DbSet<Film> Films { get; set; } = null!;
    public DbSet<Seance> Seances { get; set; } = null!;
    public DbSet<Salle> Salles { get; set; } = null!;

    public string DbPath { get; private set; }

    public CinemaContext()
    {
        // Path to SQLite database file
        DbPath = "Cinemas.db";
    }
    // The following configures EF to create a SQLite database file locally
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // Use SQLite as database
        options.UseSqlite($"Data Source={DbPath}");
        // Optional: log SQL queries to console
        options.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
    }
}