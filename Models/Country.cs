using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyScheduler.Models;

public class Country()
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CountryId { get; set; }
    public string? CountryName { get; set; }
    public DateTimeOffset CreateDate { get; set; }
    public string? CreatedBy { get; set; }
    public string? LastUpdate { get; set; }
    public string? LastUpdateBy { get; set; }
}
