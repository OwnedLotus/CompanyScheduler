using System;
using System.Collections.Generic;

namespace CompanyScheduler.Models;

public partial class City
{
    private DateTime _createDate;
    private DateTime _lastUpdate;

    public int CityId { get; set; }

    public string City1 { get; set; } = null!;

    public int CountryId { get; set; }

    public DateTime CreateDate { get => _createDate.ToLocalTime(); set => _createDate = value.ToUniversalTime(); }

    public string CreatedBy { get; set; } = null!;

    public DateTime LastUpdate { get => _lastUpdate.ToLocalTime(); set => _lastUpdate = value.ToUniversalTime(); }

    public string LastUpdateBy { get; set; } = null!;

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual Country Country { get; set; } = null!;
}
