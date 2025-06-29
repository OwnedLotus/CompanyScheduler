using System;
using System.Collections.Generic;

namespace CompanyScheduler.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public int AddressId { get; set; }

    public bool Active { get; set; }

    public DateTime CreateDate { get => TimeZoneInfo.ConvertTimeFromUtc(CreateDate, TimeZoneInfo.Local); set => CreateDate = value; }

    public string CreatedBy { get; set; } = null!;

    public DateTime LastUpdate { get => TimeZoneInfo.ConvertTimeFromUtc(LastUpdate, TimeZoneInfo.Local); set => LastUpdate = value; }

    public string LastUpdateBy { get; set; } = null!;

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
