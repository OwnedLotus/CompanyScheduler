using System;
using System.Collections.Generic;

namespace CompanyScheduler.Models;

public partial class User
{
    private DateTime _createDate;
    private DateTime _lastUpdated;

    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public sbyte Active { get; set; }

    public DateTime CreateDate { get => _createDate.ToLocalTime(); set => _createDate = value.ToUniversalTime(); }

    public string CreatedBy { get; set; } = null!;

    public DateTime LastUpdate { get => _lastUpdated.ToLocalTime(); set => _lastUpdated = value.ToUniversalTime(); }

    public string LastUpdateBy { get; set; } = null!;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
