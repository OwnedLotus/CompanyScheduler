using CompanyScheduler.OldModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CompanyScheduler.Data;

public class CompanyContext : DbContext
{
    string connectionString = "server=127.0.0.1:3306;database=client_schedule;user=sqlUser;password=Passw0rd!";

    private readonly IConfiguration _configuration;
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<User> Users { get; set; }

    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseMySql(connectionString,
            ServerVersion.AutoDetect(connectionString));
    }
}
