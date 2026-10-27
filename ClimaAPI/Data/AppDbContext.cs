using Microsoft.EntityFrameworkCore;
using ClimaAPI.Models;


namespace ClimaAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { 
    }

    public DbSet<Ciudad> Ciudades { get; set;}
    public DbSet<RegistroClima> RegistrosClima { get; set;}
}
