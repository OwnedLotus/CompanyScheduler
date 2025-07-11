using System;
using System.Collections.Generic;

namespace CompanyScheduler.Models;

public partial class Customer
{
    private DateTime _createDate;
    private DateTime _lastUpdate;

    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public int AddressId { get; set; }

    public bool Active { get; set; }

    public DateTime CreateDate { get => _createDate.ToLocalTime(); set => _createDate = value.ToUniversalTime(); }

    public string CreatedBy { get; set; } = null!;

    public DateTime LastUpdate { get => _lastUpdate.ToLocalTime(); set => _lastUpdate = value.ToUniversalTime(); }

    public string LastUpdateBy { get; set; } = null!;

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
