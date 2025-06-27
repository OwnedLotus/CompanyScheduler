using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyScheduler.OldModels;

public class City()
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CityId { get; set; }
    public string? CityName { get; set; }

    [ForeignKey("CountryId")]
    public Country? Country { get; set; }
    public DateTimeOffset CreateDate { get; set; }
    public string? CreatedBy { get; set; }
    public string LastUpdate { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
    public string? LastUpdateBy { get; set; }
}
