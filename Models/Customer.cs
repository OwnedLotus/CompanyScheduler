using System.ComponentModel.DataAnnotations;

namespace CompanyScheduler.Models;

public class Customer
{
    [Key] public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public int[] AddressID { get; set; }
    public byte Active { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public string LastUpdate { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
    public string LastUpdateBy { get; set; }
}