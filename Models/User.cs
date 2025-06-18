using System.ComponentModel.DataAnnotations;

namespace CompanyScheduler.Models;

public class User
{
    [Key]
    public int userID { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public byte Active { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }

    // MYSQL TIMESTAMP DATATYPE
    public string ModifiedDate { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
    public string? LastUpdatedBy { get; set; }
}
