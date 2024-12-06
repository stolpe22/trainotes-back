using Microsoft.EntityFrameworkCore;
using trainote_api.Models;

namespace trainote_api.Context;

public class AppDbContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql
    (
        "Server=localhost;" +
        "Port=5432;" +
        "Database=trainotes;" +
        "User Id=postgres;" +
        "Password=123;"
    );

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
