namespace CompanyScheduler.Models.Errors;

class FailedCustomerDeleteException : Exception
{
    public int ErrorCode { get; }

    public FailedCustomerDeleteException()
        : base("Failed to Delete Customer.") { }

    public FailedCustomerDeleteException(string message)
        : base(message) { }

    public FailedCustomerDeleteException(string message, Exception e)
        : base(message, e) { }

    public FailedCustomerDeleteException(string message, int code)
        : base(message)
    {
        ErrorCode = code;
    }

    public override string ToString()
    {
        return $"{base.ToString()}, ErrorCode: {ErrorCode}";
    }
}