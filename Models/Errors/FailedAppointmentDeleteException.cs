namespace CompanyScheduler.Models.Errors;

class FailedAppointmentDeleteException : Exception
{
    public int ErrorCode { get; }
    public string ErrorMessage { get; }

    public FailedAppointmentDeleteException(string message)
        : base(message) {
        ErrorMessage = message;
    }

    public FailedAppointmentDeleteException(string message, Exception e)
        : base(message, e) {
        ErrorMessage = message;
    }

    public FailedAppointmentDeleteException(string message, int code)
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

