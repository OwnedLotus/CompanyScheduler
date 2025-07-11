using System;
using System.Collections.Generic;

namespace CompanyScheduler.Models;

public partial class Country
{
    private DateTime _createDate;
    private DateTime _lastUpdate;

    public int CountryId { get; set; }

    public string Country1 { get; set; } = null!;

    public DateTime CreateDate { get => _createDate.ToLocalTime(); set => _createDate = value.ToUniversalTime(); }

    public string CreatedBy { get; set; } = null!;

    public DateTime LastUpdate { get => _lastUpdate.ToLocalTime(); set => _lastUpdate = value.ToUniversalTime(); }

    public string LastUpdateBy { get; set; } = null!;

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
