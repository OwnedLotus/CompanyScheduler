namespace CompanyScheduler.Models.Errors;

class FailedAppointmentUpdateException : Exception
{
    public int ErrorCode { get; }

    public FailedAppointmentUpdateException()
        : base("Failed to Create Customer.") { }

    public FailedAppointmentUpdateException(string message)
        : base(message) { }

    public FailedAppointmentUpdateException(string message, Exception e)
        : base(message, e) { }

    public FailedAppointmentUpdateException(string message, int code)
        : base(message)
    {
        ErrorCode = code;
    }

    public override string ToString()
    {
        return $"{base.ToString()}, ErrorCode: {ErrorCode}";
    }
}

