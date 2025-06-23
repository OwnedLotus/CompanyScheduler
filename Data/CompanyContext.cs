using CompanyScheduler.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CompanyScheduler.Data;

public class CompanyContext : DbContext
{
    static readonly string connectionString = "Server=localhost; User ID=root; Password=pass; Database=blog";

    public DbSet<Appointment> Appointments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}
