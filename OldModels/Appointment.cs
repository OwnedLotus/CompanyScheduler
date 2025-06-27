using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyScheduler.OldModels;

public class Appointment()
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AppointmentId { get; set; }

    [ForeignKey("CustomerId")]
    public Customer? Customer { get; set; }

    [ForeignKey("UserID")]
    public User? User { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public string? Contact { get; set; }
    public string? Type { get; set; }
    public Uri? Url { get; set; }
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
    public DateTimeOffset CreateDate { get; set; }
    public string? CreatedBy { get; set; }
    public string? LastUpdate { get; set; }
    public string? LastUpdateBy { get; set; }

    public static bool CheckTextBoxes(string[] input)
    {
        foreach (string inputItem in input)
        {
            if (string.IsNullOrEmpty(inputItem))
            {
                return false;
            }
        }
        return true;
    }

    public static string UpdateFormat()
    {
        return DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
