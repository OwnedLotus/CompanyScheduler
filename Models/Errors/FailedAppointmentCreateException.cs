namespace CompanyScheduler.Models.Errors;

class FailedAppointmentCreateException : Exception
{
    public int ErrorCode { get; }

    public FailedAppointmentCreateException()
        : base("Failed to Create Customer.") { }

    public FailedAppointmentCreateException(string message)
        : base(message) { }

    public FailedAppointmentCreateException(string message, Exception e)
        : base(message, e) { }

    public FailedAppointmentCreateException(string message, int code)
        : base(message)
    {
        ErrorCode = code;
    }

    public override string ToString()
    {
        return $"{base.ToString()}, ErrorCode: {ErrorCode}";
    }
}
