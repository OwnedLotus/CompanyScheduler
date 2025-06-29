using System;
using System.Collections.Generic;

namespace CompanyScheduler.Models;

public partial class Country
{
    public int CountryId { get; set; }

    public string Country1 { get; set; } = null!;

    public DateTime CreateDate { get => TimeZoneInfo.ConvertTimeFromUtc(CreateDate, TimeZoneInfo.Local); set => CreateDate = value; }

    public string CreatedBy { get; set; } = null!;

    public DateTime LastUpdate { get => TimeZoneInfo.ConvertTimeFromUtc(LastUpdate, TimeZoneInfo.Local); set => LastUpdate = value; }

    public string LastUpdateBy { get; set; } = null!;

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
