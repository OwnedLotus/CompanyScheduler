namespace CompanyScheduler.Models.Errors;

class FailedAppointmentCreateException : Exception
{
    public int ErrorCode { get; }
    public string ErrorMessage { get; }

    public FailedAppointmentCreateException(string message)
        : base(message) {
        ErrorMessage = message;
    }

    public FailedAppointmentCreateException(string message, Exception e)
        : base(message, e) {
        ErrorMessage = message;
    }

    public FailedAppointmentCreateException(string message, int code)
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
