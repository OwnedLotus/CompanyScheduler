using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyScheduler.Models;

public class Address
{
    [Key] public int CustomerId { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    [ForeignKey("CityId")] public int CityId { get; set; }
    public string PostalCode { get; set; }
    public string Phone { get; set; }
    public DateTime CreateDate { get; set; }
    public string CreatedBy { get; set; }
    public string LastUpdate { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
    public string LastUpdateBy { get; set; }
}