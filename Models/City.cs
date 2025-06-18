using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyScheduler.Models;

public class City
{
    [Key]
    public int CityId { get; set; }
    public string? CityName { get; set; }

    [ForeignKey("CountryId")]
    public Country? Country { get; set; }
    public DateTime CreateDate { get; set; }
    public string? CreatedBy { get; set; }
    public string LastUpdate { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
    public string? LastUpdateBy { get; set; }
}
