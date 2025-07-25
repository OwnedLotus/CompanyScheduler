using System;
using System.Collections.Generic;

namespace CompanyScheduler.Models;

public partial class Appointment
{
    private DateTime _createDate;
    private DateTime _lastUpdate;
    private DateTime _startDate;
    private DateTime _endDate;


    public int AppointmentId { get; set; }

    public int CustomerId { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string Contact { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Url { get; set; } = null!;

    public DateTime Start { get => _startDate.ToLocalTime(); set => _startDate = value.ToUniversalTime(); }

    public DateTime End { get => _endDate.ToLocalTime(); set => _endDate = value.ToUniversalTime(); }

    public DateTime CreateDate { get => _createDate.ToLocalTime(); set => _createDate = value.ToUniversalTime(); }

    public string CreatedBy { get; set; } = null!;

    public DateTime LastUpdate { get => _lastUpdate.ToLocalTime(); set => _lastUpdate = value.ToUniversalTime(); }

    public string LastUpdateBy { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public static bool CheckTextBoxes(string[] input)
    {
        foreach (string inputItem in input)
        {
            if (string.IsNullOrEmpty(inputItem))
            {
                return false;
            }
        }
        return true;
    }
}
