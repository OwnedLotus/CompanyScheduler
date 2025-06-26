using CompanyScheduler.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CompanyScheduler.Data;

public class CompanyContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<User> Users { get; set; }

    public CompanyContext(IConfiguration configuration) =>
        _configuration = configuration;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        var connectionString = _configuration.GetConnectionString("DefaultConnection");

        optionsBuilder.UseMySql(connectionString,
            ServerVersion.AutoDetect(connectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
