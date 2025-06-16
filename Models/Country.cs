using System.ComponentModel.DataAnnotations;

namespace CompanyScheduler.Models;

public class Country
{
    [Key] public int CountryId { get; set; }
    public string CountryName { get; set; }
    public DateTime CreateDate { get; set; }
    public string CreatedBy { get; set; }
    public string LastUpdate { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
    public string LastUpdateBy { get; set; }
}