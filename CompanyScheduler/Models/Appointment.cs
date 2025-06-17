using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyScheduler.Models;

public class Appointment
{
    [Key] public int AppointmentId { get; set; }
    [ForeignKey("CustomerId")] public Customer Customer { get; set; }
    [ForeignKey("UserID")] public User User { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public string contact { get; set; }
    public string Type { get; set; }
    public Uri Url { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public DateTime CreateDate { get; set; }
    public string CreatedBy { get; set; }
    public string LastUpdate { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
    public string LastUpdateBy { get; set; }
    
}