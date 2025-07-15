namespace CompanyScheduler.Models.Errors;

class FailedCustomerUpdatedException : Exception
{
    public int ErrorCode { get; }

    public FailedCustomerUpdatedException()
        : base("Failed to Update Customer.") { }

    public FailedCustomerUpdatedException(string message)
        : base(message) { }

    public FailedCustomerUpdatedException(string message, Exception e)
        : base(message, e) { }

    public FailedCustomerUpdatedException(string message, int code)
        : base(message)
    {
        ErrorCode = code;
    }

    public override string ToString()
    {
        return $"{base.ToString()}, ErrorCode: {ErrorCode}";
    }
}
