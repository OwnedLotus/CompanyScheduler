﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CompanyScheduler.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public string Address1 { get; set; } = null!;

    public string Address2 { get; set; } = null!;

    public int CityId { get; set; }

    public string PostalCode { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime LastUpdate { get => TimeZoneInfo.ConvertTimeFromUtc(LastUpdate, TimeZoneInfo.Local); set => LastUpdate = value; }

    public string LastUpdateBy { get; set; } = null!;

    public virtual City City { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public static bool OnlyDigitsAndDashes(string input)
    {
        return DigitsAndDashes().Match(input ?? string.Empty).Success;
    }

    [GeneratedRegex(@"^[0-9-]+$")]
    private static partial Regex DigitsAndDashes();
}
