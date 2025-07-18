namespace CompanyScheduler.Models.Errors;

class FailedAppointmentUpdateException : Exception
{
    public int ErrorCode { get; }
    public string ErrorMessage { get; }

    public FailedAppointmentUpdateException(string message)
        : base(message) {
        ErrorMessage = message;
    }

    public FailedAppointmentUpdateException(string message, Exception e)
        : base(message, e) {
        ErrorMessage = message;
    }

    public FailedAppointmentUpdateException(string message, int code)
        : base(message)
    {
        ErrorMessage = message;
        ErrorCode = code;
    }

    public override string ToString()
    {
        return $"{ErrorMessage}, ErrorCode: {ErrorCode}";
    }
}

