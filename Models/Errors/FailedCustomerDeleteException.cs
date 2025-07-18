namespace CompanyScheduler.Models.Errors;

class FailedCustomerDeleteException : Exception
{
    public int ErrorCode { get; }
    private string ErrorMessage { get; }


    public FailedCustomerDeleteException(string message)
        : base(message) {
        ErrorMessage = message;
    }

    public FailedCustomerDeleteException(string message, Exception e)
        : base(message, e) {
        ErrorMessage = message;
    }

    public FailedCustomerDeleteException(string message, int code)
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