using System;
using System.Collections.Generic;

namespace CompanyScheduler.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public sbyte Active { get; set; }

    public DateTime CreateDate { get => TimeZoneInfo.ConvertTimeFromUtc(CreateDate, TimeZoneInfo.Local); set => CreateDate = value; }

    public string CreatedBy { get; set; } = null!;

    public DateTime LastUpdate { get => TimeZoneInfo.ConvertTimeFromUtc(LastUpdate, TimeZoneInfo.Local); set => LastUpdate = value; }

    public string LastUpdateBy { get; set; } = null!;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
