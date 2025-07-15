namespace CompanyScheduler.Models.Errors;

class FailedAppointmentDeleteException : Exception
{
    public int ErrorCode { get; }

    public FailedAppointmentDeleteException()
        : base("Failed to Create Customer.") { }

    public FailedAppointmentDeleteException(string message)
        : base(message) { }

    public FailedAppointmentDeleteException(string message, Exception e)
        : base(message, e) { }

    public FailedAppointmentDeleteException(string message, int code)
        : base(message)
    {
        ErrorCode = code;
    }

    public override string ToString()
    {
        return $"{base.ToString()}, ErrorCode: {ErrorCode}";
    }
}

