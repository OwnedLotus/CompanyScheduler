namespace CompanyScheduler.Models.Errors;

class FailedCustomerCreateException : Exception
{
    public int ErrorCode { get; }
    public string ErrorMessage { get; }

    public FailedCustomerCreateException(string message) : base(message) {
        ErrorMessage = message;
    }

    public FailedCustomerCreateException(string message, Exception e) : base(message, e) {
        ErrorMessage = message;
    }

    public FailedCustomerCreateException(string message, int code) : base(message)
    {
        ErrorMessage = message;
        ErrorCode = code;
    }

    public override string ToString()
    {
        return $"{ErrorMessage}, ErrorCode: {ErrorCode}";
    }
}