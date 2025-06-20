using Microsoft.EntityFrameworkCore;

namespace CompanyScheduler.Models.Data;

public class CompanyContext : DbContext
{
    public CompanyContext(DbContextOptions<CompanyContext> options) : base(options) { };
}
