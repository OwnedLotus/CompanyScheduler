namespace CompanyScheduler.Models.Errors;

class FailedCustomerUpdatedException : Exception
{
    public int ErrorCode { get; }
    private string ErrorMessage { get; }

    public FailedCustomerUpdatedException(string message)
        : base(message) {
        ErrorMessage = message;

    }

    public FailedCustomerUpdatedException(string message, Exception e)
        : base(message, e) {
        ErrorMessage = message;
    }

    public FailedCustomerUpdatedException(string message, int code)
        : base(message)
    {
        ErrorCode = code;
        ErrorMessage = message;

    }

    public override string ToString()
    {
        return $"{ErrorMessage}, ErrorCode: {ErrorCode}";
    }
}
