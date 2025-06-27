using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CompanyScheduler.Models;

namespace CompanyScheduler.OldModels;

public class Customer()
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CustomerId { get; set; }
    public string? CustomerName { get; set; }
    [ForeignKey("AddressId")]
    public Address? Address { get; set; }
    public byte Active { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public string? CreatedBy { get; set; }
    public string? LastUpdate { get; set; }
    public string? LastUpdateBy { get; set; }
}
