using System;
using System.Collections.Generic;

namespace CompanyScheduler.Models;

public partial class City
{
    public int CityId { get; set; }

    public string City1 { get; set; } = null!;

    public int CountryId { get; set; }

    public DateTime CreateDate { get => TimeZoneInfo.ConvertTimeFromUtc(CreateDate, TimeZoneInfo.Local); set => CreateDate = value; }

    public string CreatedBy { get; set; } = null!;

    public DateTime LastUpdate { get => TimeZoneInfo.ConvertTimeFromUtc(LastUpdate, TimeZoneInfo.Local); set => LastUpdate = value; }

    public string LastUpdateBy { get; set; } = null!;

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual Country Country { get; set; } = null!;
}
