using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace CompanyScheduler.Models;

public partial class Address()
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AddressId { get; set; }
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }

    [ForeignKey("CityId")]
    public City? City { get; set; }
    public string? PostalCode { get; set; }
    public string? Phone { get; set; }
    public DateTimeOffset CreateDate { get; set; }
    public string? CreatedBy { get; set; }
    public string? LastUpdate { get; set; }
    public string? LastUpdateBy { get; set; }

    public static bool OnlyDigitsAndDashes(string input)
    {
        return DigitsAndDashes().Match(input ?? string.Empty).Success;
    }

    [GeneratedRegex(@"^[0-9-]+$")]
    private static partial Regex DigitsAndDashes();
}
