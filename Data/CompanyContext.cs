using CompanyScheduler.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CompanyScheduler.Data;

public class CompanyContext : DbContext
{
    private readonly string? _connectionString;

    public CompanyContext(DbContextOptions<CompanyContext> options, IConfiguration configuration)
        : base(options) 
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public DbSet<Appointment> Appointments => Set<Appointment>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 34));
        optionsBuilder.UseMySql(_connectionString,serverVersion);
    }
}
